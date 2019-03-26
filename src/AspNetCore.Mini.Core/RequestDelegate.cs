using System.Threading.Tasks;

namespace AspNetCore.Mini.Core
{
    /// <summary>
    /// 请求处理委托
    /// </summary>
    /// <param name="context">Http上下文</param>
    /// <returns>Task</returns>
    public delegate Task RequestDelegate(HttpContext context);
}
