using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Thinktecture.AuthorizationServer.Interfaces;
using Thinktecture.AuthorizationServer.Models;
using Thinktecture.AuthorizationServer.WebHost.Areas.Admin.Models;
using Thinktecture.IdentityModel.WebApi;

namespace Thinktecture.AuthorizationServer.WebHost.Areas.Admin.Api
{
    [ResourceActionAuthorize(Constants.Actions.Configure, Constants.Resources.Server)]
    [ValidateHttpAntiForgeryToken]
    public class ApplicationRememberOptionsController : ApiController
    {
        IAuthorizationServerAdministration config;

        public ApplicationRememberOptionsController(IAuthorizationServerAdministration config)
        {
            this.config = config;
        }

        public HttpResponseMessage Get(int id)
        {
            var app = this.config.Applications.All.SingleOrDefault(x => x.ID == id);
            if (app == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var data =
                 from s in app.RememberOptions
                 select new
                 {
                     s.ID,
                     s.OptionLabel,
                     s.Value
                 };
            return Request.CreateResponse(HttpStatusCode.OK, data.ToArray());
        }

        public HttpResponseMessage Post(int id, RememberOptionModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState.GetErrors());
            }

            var app = config.Applications.All.SingleOrDefault(x => x.ID == id);
            if (app == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var rememberOption = RememberOptionParser.Parse(model);
            

            if (app.RememberOptions.Any(x => x.Value == rememberOption.Value))
            {
                ModelState.AddModelError("", "That Remember Option already exists");
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState.GetErrors());
            }
            app.RememberOptions.Add(rememberOption);
            config.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                rememberOption.ID,
                rememberOption.OptionLabel,
                rememberOption.Value
            });
        }
	}
}