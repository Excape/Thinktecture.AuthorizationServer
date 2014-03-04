using System.Collections.Generic;
using Thinktecture.AuthorizationServer.Interfaces;
using Thinktecture.AuthorizationServer.Models;

namespace Thinktecture.AuthorizationServer.WebHost
{
    public class RememberOptionsDefaultData
    {
        public static void PopulateDefaultData(Application app, IAuthorizationServerAdministration config)
        {
            var currentRememberOptions = app.RememberOptions;

            if (currentRememberOptions.Count > 0)
            {
                foreach (var remb in currentRememberOptions.ToArray())
                {
                    config.RememberOptions.Remove(remb);
                }
                config.SaveChanges();
            }

            var rememberOptions = CreateRememberOptions();

            foreach (var r in rememberOptions)
            {
                app.RememberOptions.Add(r);
            }

            config.SaveChanges();
        }

        public static List<RememberOption> CreateRememberOptions()
        {
            return new List<RememberOption>
            {
                new RememberOption()
                {
                    OptionLabel = "1 hour",
                    Value = 1
                },
                new RememberOption()
                {
                    OptionLabel = "3 hours",
                    Value = 3
                },
                new RememberOption()
                {
                    OptionLabel = "12 hours",
                    Value = 12
                },
                new RememberOption()
                {
                    OptionLabel = "1 day",
                    Value = 24
                },
                new RememberOption()
                {
                    OptionLabel = "3 days",
                    Value = 72
                },
                new RememberOption()
                {
                    OptionLabel = "1 week",
                    Value = 168
                },
                new RememberOption()
                {
                    OptionLabel = "2 weeks",
                    Value = 336
                },
                new RememberOption()
                {
                    OptionLabel = "1 month",
                    Value = 720
                },
                new RememberOption()
                {
                    OptionLabel = "3 months",
                    Value = 2190
                },
                new RememberOption()
                {
                    OptionLabel = "6 months",
                    Value = 4380
                },
                new RememberOption()
                {
                    OptionLabel = "1 year",
                    Value = 8760
                },
                new RememberOption()
                {
                    OptionLabel = "Forever",
                    Value = -1
                }
            };
        }
    }
}