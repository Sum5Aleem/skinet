using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Dtos;
using API.DTOs;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly IMapper _mapper;

        public AccountController(IAccountRepository accountRepository, ITokenRepository tokenRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _tokenRepository = tokenRepository;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Authenticate(LoginDto loginDto)
        {
            var user = await _accountRepository.Login(loginDto.Email, loginDto.Password);

            if (user == null)
                return Unauthorized(new ApiResponse(401));

            return new UserDto { Email = user.Email, Token = _tokenRepository.GenerateJwtToken(user), DisplayName = user.DisplayName };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = new User { DisplayName = registerDto.DisplayName, Email = registerDto.Email, Password = registerDto.Password };
            await _accountRepository.Register(user);

            if (user == null)
                return BadRequest(new ApiResponse(400));

            return new UserDto { Email = user.Email, Token = _tokenRepository.GenerateJwtToken(user), DisplayName = user.DisplayName };
        }

        [HttpGet("getCurrentUser")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

            var user = await _accountRepository.GetCurrentUser(email);

            return new UserDto { Email = user.Email, Token = _tokenRepository.GenerateJwtToken(user), DisplayName = user.DisplayName };
        }

        [HttpGet("emailExists")]
        [Authorize]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            var user = await _accountRepository.CheckEmailExists(email);

            return user != null;
        }

        [HttpGet("address")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            var user = await _accountRepository.GetCurrentUser(email);

            return _mapper.Map<Address, AddressDto>(user.Address);
        }

        [HttpPut("address")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
        {
            var email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            var user = await _accountRepository.GetCurrentUser(email);

            user.Address= _mapper.Map<AddressDto, Address>(address);
            user = await _accountRepository.UpdateUserAddress(user);

            return Ok(_mapper.Map<Address, AddressDto>(user.Address));
        }
    }
}
