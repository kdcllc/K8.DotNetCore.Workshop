using System.Threading;
using System.Threading.Tasks;

namespace K8.LongProcess.Services
{
    public interface IProcessService
    {
        Task<int> ExecuteAsync(CancellationToken cancellationToken);
    }
}
