using Core.Entities;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<IReadOnlyList<T>> GetAll();
        Task<T> GetById(BaseSpecification<T> specification);
        Task<IReadOnlyList<T>> GetAll(BaseSpecification<T> specification);

        Task<int> GetCount(BaseSpecification<T> specification);
    }
}
