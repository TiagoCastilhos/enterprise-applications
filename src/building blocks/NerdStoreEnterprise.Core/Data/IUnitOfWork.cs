using System.Threading.Tasks;

namespace NerdStoreEnterprise.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}