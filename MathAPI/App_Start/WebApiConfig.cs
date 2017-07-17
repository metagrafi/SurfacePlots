﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MathAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.EnableCors();
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "MathApi",
                routeTemplate: "api/{controller}/{action}"//,
                                                               //defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
