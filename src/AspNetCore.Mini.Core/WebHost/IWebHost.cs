using System.Threading.Tasks;

namespace AspNetCore.Mini.Core
{
    public interface IWebHost
    {
        Task Run();
    }
}
