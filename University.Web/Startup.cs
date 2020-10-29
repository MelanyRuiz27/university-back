using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using University.BL.Data;

namespace University.Web
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            //Configura la db context para instanciar el request
            app.CreatePerOwinContext(UniversityContext.Create);
        }
    }
}
