using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("errors/{code}")]
    public class Error : BaseApi
    {        
        public IActionResult Errors(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}
