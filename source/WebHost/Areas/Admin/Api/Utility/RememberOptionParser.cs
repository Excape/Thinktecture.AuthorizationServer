using System;
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
                    timeUnit = model.UserValue == 1 ? "hour" : "hours";
                    break;
                case 24:
                    timeUnit = model.UserValue == 1 ? "day" : "days";
                    break;
                case 168:
                    timeUnit = model.UserValue == 1 ? "week" : "weeks";
                    break;
                case 720:
                    timeUnit = model.UserValue == 1 ? "month" : "months";
                    break;
                case 8640:
                    timeUnit = model.UserValue == 1 ? "year" : "years";
                    break;
                case -1:
                    expirationValue = model.OptionSelect;
                    break;
                default:
                    throw new Exception("Wrong expiration time unit");
            }

            string rememberOptionLabel;
            if (model.OptionSelect != -1 && model.UserValue != null)
            {

                expirationValue = model.OptionSelect * (int) model.UserValue;
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