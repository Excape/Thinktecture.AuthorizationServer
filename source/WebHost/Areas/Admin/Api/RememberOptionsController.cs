using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using Thinktecture.AuthorizationServer.Interfaces;
using Thinktecture.AuthorizationServer.WebHost.Areas.Admin.Models;
using Thinktecture.IdentityModel.WebApi;

namespace Thinktecture.AuthorizationServer.WebHost.Areas.Admin.Api
{
    [ResourceActionAuthorize(Constants.Actions.Configure, Constants.Resources.Server)]
    [ValidateHttpAntiForgeryToken]
    public class RememberOptionsController : ApiController
    {
        IAuthorizationServerAdministration config;

        public RememberOptionsController(IAuthorizationServerAdministration config)
        {
            this.config = config;
        }

        public HttpResponseMessage Get(int id)
        {
            var rememberOption = this.config.RememberOptions.All.SingleOrDefault(x => x.ID == id);
            if (rememberOption == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var m = Regex.Match(rememberOption.OptionLabel, @"^([0-9]+) (.+)$");

            var timeUnit = 0;
            switch (m.Groups[2].Value)
            {
                case "hours":
                case "hour":
                    timeUnit = 1;
                    break;
                case "days":
                case "day":
                    timeUnit = 24;
                    break;
                case "weeks":
                case "week":
                    timeUnit = 168;
                    break;
                case "months":
                case "month":
                    timeUnit = 720;
                    break;
                case "years":
                case "year":
                    timeUnit = 8640;
                    break;
            }

            var expirationValue = 0;
            if (rememberOption.OptionLabel == "Forever")
            {
                timeUnit = -1;
                expirationValue = -1;
            }
            else
            {
                expirationValue = rememberOption.Value/timeUnit;
            }


            var data =
                new
                {
                    id = rememberOption.ID,
                    optionSelect = timeUnit,
                    userValue = Convert.ToString(expirationValue)
                };
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        public HttpResponseMessage Put(int id, RememberOptionModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState.GetErrors());
            }

            var rememberOption = config.RememberOptions.All.SingleOrDefault(x => x.ID == id);
            if (rememberOption == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var query =
                (from a in this.config.Applications.All
                 from s in a.RememberOptions
                 where s.ID == id
                 select a);
            var app = query.Single();

            var updatedRememberOption = RememberOptionParser.Parse(model);

            if (app.RememberOptions.Any(x => x.Value == updatedRememberOption.Value && x.ID != id))
            {
                ModelState.AddModelError("", "A remember option with that value already exists");
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState.GetErrors());
            }

            rememberOption.OptionLabel = updatedRememberOption.OptionLabel;
            rememberOption.Value = updatedRememberOption.Value;

            config.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        public HttpResponseMessage Delete(int id)
        {
            var rememberOption = this.config.RememberOptions.All.SingleOrDefault(x => x.ID == id);
            if (rememberOption != null)
            {
                this.config.RememberOptions.Remove(rememberOption);
                this.config.SaveChanges();
            }

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}