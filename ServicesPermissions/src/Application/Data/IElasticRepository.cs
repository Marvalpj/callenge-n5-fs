using Domain.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data
{
    public interface IElasticRepository<T>
    {
        Task<IEnumerable<string>> Index(IEnumerable<T> documents);
        Task<T> GetById(string id);
        Task Update(T document, string id);
        Task Delete(string id);
    }
}
