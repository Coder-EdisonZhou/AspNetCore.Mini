using System.Threading.Tasks;

namespace AspNetCore.Mini.Core
{
    public delegate Task RequestDelegate(HttpContext context);
}
