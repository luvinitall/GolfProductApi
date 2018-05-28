using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OData.UriParser;

namespace GolfProductApi.Helpers
{
    public static class ODataHelpers
    {
        public static bool HasProperty(this object instance, string propertyName)
        {
            var propertyInfo = instance.GetType().GetProperty(propertyName);
            return (propertyInfo != null);
        }

        public static object GetValue(this object instance, string propertyName)
        {
            var propertyInfo = instance.GetType().GetProperty(propertyName);
            if (propertyInfo == null)
            {
                throw new HttpRequestException("Can't find property with name " + propertyName);
            }
            var propertyValue = propertyInfo.GetValue(instance, new object[] { });

            return propertyValue;
        }

        public static IActionResult CreateOKHttpActionResult(this ODataController controller, object propertyValue)
        {
            var okMethod = default(MethodInfo);

            // find the ok method on the current controller
            var methods = controller.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (var method in methods)
            {
                if (method.Name == "Ok" && method.GetParameters().Length == 1)
                {
                    okMethod = method;
                    break;
                }
            }

            // invoke the method, passing in the propertyValue
            okMethod = okMethod.MakeGenericMethod(propertyValue.GetType());
            var returnValue = okMethod.Invoke(controller, new object[] { propertyValue });
            return (IActionResult)returnValue;
        }


        ///// <summary>
        ///// Helper method to get the odata path for an arbitrary odata uri.
        ///// </summary>
        ///// <param name="request">The request instance in current context</param>
        ///// <param name="uri">OData uri</param>
        ///// <returns>The parsed odata path</returns>
        //public static ODataPath CreateODataPath(this HttpRequestMessage request, Uri uri)
        //{
        //    if (uri == null)
        //    {
        //        throw new ArgumentNullException(nameof(uri));
        //    }
           
        //    var newRequest = new HttpRequestMessage(HttpMethod.Get, uri);
        //    var route = request.GetRouteData().Route;

        //    var newRoute = new HttpRoute(
        //        route.RouteTemplate,
        //        new HttpRouteValueDictionary(route.Defaults),
        //        new HttpRouteValueDictionary(route.Constraints),
        //        new HttpRouteValueDictionary(route.DataTokens),
        //        route.Handler);

        //    newRequest.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, request.Properties[HttpPropertyKeys.HttpConfigurationKey]);

        //    var routeData = newRoute.GetRouteData(request.GetConfiguration().VirtualPathRoot, newRequest);
        //    if (routeData == null)
        //    {
        //        throw new InvalidOperationException("This link is not a valid OData link.");
        //    }

        //    return newRequest.ODataProperties().Path;
        //}

        public static TKey GetKeyValue<TKey>(this HttpRequest request, Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            //get the odata path Ex: ~/entityset/key/$links/navigation
            var odataPath = request.ToUri();
            var keySegment = odataPath.Segments.OfType<KeySegment>().LastOrDefault();
            if (keySegment == null)
            {
                throw new InvalidOperationException("This link does not contain a key.");
            }


            return (TKey)keySegment.Keys.Last().Value;
        }

        public static Uri ToUri(this HttpRequest request)
        {
            var hostComponents = request.Host.ToUriComponent().Split(':');

            var builder = new UriBuilder
            {
                Scheme = request.Scheme,
                Host = hostComponents[0],
                Path = request.Path,
                Query = request.QueryString.ToUriComponent()
            };

            if (hostComponents.Length == 2)
            {
                builder.Port = Convert.ToInt32(hostComponents[1]);
            }

            return builder.Uri;
        }
    }
}
