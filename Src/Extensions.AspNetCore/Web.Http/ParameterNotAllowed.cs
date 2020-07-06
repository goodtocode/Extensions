using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace GoodToCode.Extensions.Web.Http
{
    /// <summary>
    /// Restricts http verbs to route that has the specified parameter (like id) in the route
    /// Usage: public static void RegisterRoutes(RouteCollection routes)
    ///         {routes.MapHttpRoute("DefaultApi","api/{controller}/{id}",
    ///             new { id = RouteParameter.Optional},
    ///             new { id = new ParameterNotAllowed() } );}
    /// </summary>
    public class ParameterNotAllowed : IRouteConstraint
    {
        string httpMethod = string.Empty;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="method"></param>
        public ParameterNotAllowed(string method)
        {
            httpMethod = method;
        }

        /// <summary>
        /// Match criteria
        /// </summary>
        /// <param name="httpContext">Http Context</param>
        /// <param name="routeName">Route name</param>
        /// <param name="parameterName">ParameterName, typically Id</param>
        /// <param name="valueList">List of dictionary values</param>
        /// <param name="routeDirection">Direction of the route</param>
        /// <returns></returns>
        public bool Match(HttpContext httpContext, Route routeName, string parameterName, RouteValueDictionary valueList, RouteDirection routeDirection)
        {
            if (routeDirection == RouteDirection.IncomingRequest && httpContext.Request.Method == httpMethod && valueList[parameterName] != null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Match criteria
        /// </summary>
        /// <param name="httpContext">Http Context</param>
        /// <param name="route">Route name</param>
        /// <param name="routeKey">ParameterName, typically Id</param>
        /// <param name="values">List of dictionary values</param>
        /// <param name="routeDirection">Direction of the route</param>
        /// <returns></returns>
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return Match(httpContext, route, route.ToString(), values, routeDirection);
        }
    }
}
