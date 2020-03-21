using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BackApis
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            /**
             eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJhdWQiOm51bGwsImlzcyI6Ik1iaEl0LVo4VDVpb3NwSl9GYU1OUnciLCJleHAiOjE1ODUyNDU4MTUsImlhdCI6MTU4NDY0MTAxNH0.wQVUZ0kjB7DrHlNVa8gjrs3S4jdzVclVz7zyeo1j7NU
             */
            // add web api
            Task.Run(() => MeetingsControl.AddMeetings());
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }
    }
}