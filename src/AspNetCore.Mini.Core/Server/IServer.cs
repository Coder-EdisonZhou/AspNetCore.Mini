using System.Threading.Tasks;

namespace AspNetCore.Mini.Core
{
    public interface IServer
    {
        Task RunAsync(RequestDelegate handler);
    }
}
