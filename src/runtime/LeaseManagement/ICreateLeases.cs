using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MessageHandler.Runtime
{
    public interface ICreateLeases<T> where T : ILease
    {
        T Create(string leaseId);
    }
}