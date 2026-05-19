using Microsoft.AspNetCore.Identity.Data;

namespace sk.Core.Interfaces
{
    public interface IDataBase<T> where T : IModel
    {
        Task<bool> IsExist(T model);
        Task Save(T model);
        Task Delete(T model);
        Task<List<T>> GetList(int quant = default);
    }
}
