using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Thinktecture.AuthorizationServer.Models;
using Thinktecture.AuthorizationServer.WebHost.Areas.Admin.Models;

namespace Thinktecture.AuthorizationServer.WebHost.Areas.Admin.Api
{
    static class RememberOptionParser
    {
        public static RememberOption Parse(RememberOptionModel model)
        {
            int expirationValue = 0;
            string timeUnit = null;

            switch (model.OptionSelect)
            {
                case 1:
                    timeUnit = "hours";
                    break;
                case 24:
                    timeUnit = "days";
                    break;
                case 168:
                    timeUnit = "weeks";
                    break;
                case 720:
                    timeUnit = "months";
                    break;
                case 8640:
                    timeUnit = "years";
                    break;
                case -1:
                    expirationValue = model.OptionSelect;
                    break;
                default:
                    throw new Exception("Wrong expiration time unit");
            }

            string rememberOptionLabel;
            if (model.OptionSelect != -1)
            {

                expirationValue = model.OptionSelect * model.UserValue;
                rememberOptionLabel = model.UserValue + " " + timeUnit;
            }
            else
            {
                rememberOptionLabel = "Forever";
            }

            return new RememberOption
            {
                OptionLabel = rememberOptionLabel,
                Value = expirationValue
            };
        }
    }
}