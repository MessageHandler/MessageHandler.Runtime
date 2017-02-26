using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MessageHandler.Runtime
{
    public interface ICreateLeases<T> where T : Lease
    {
        T Create(string leaseId);
    }
}