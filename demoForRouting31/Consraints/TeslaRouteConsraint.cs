using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace demoForRouting31.Consraints
{
    /// <summary>
    /// 自定义路由约束
    /// </summary>
    public class TeslaRouteConsraint : IRouteConstraint
    {
        /// <summary>
        /// 是否匹配
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="route"></param>
        /// <param name="routeKey"></param>
        /// <param name="values"></param>
        /// <param name="routeDirection"></param>
        /// <returns></returns>
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if(RouteDirection.IncomingRequest == routeDirection)
            {
                var value = values[routeKey];
                if(long.TryParse(value.ToString(), out var longValue))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
