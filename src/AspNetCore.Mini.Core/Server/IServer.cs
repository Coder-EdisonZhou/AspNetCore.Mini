using System.Threading.Tasks;

namespace AspNetCore.Mini.Core
{
    public interface IServer
    {
        Task StartAsync(RequestDelegate handler);
    }
}
