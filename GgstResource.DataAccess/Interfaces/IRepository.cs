using System.Collections.Generic;
using System.Threading.Tasks;
using GgstResource.Models;

namespace GgstResource.DataAccess.Interfaces
{
    public interface IRepository<T> where T : BaseResource
    {
        public Task<List<T>> GetAll();
        public Task<T> GetByReference(string reference);
        public Task<T> Create(T data);
        public Task<T> Update(string reference, long version, T data);
        public void SupplyTableNameToCommandHelper(string tableName);
    }
}