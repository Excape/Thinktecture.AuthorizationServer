﻿/*
 * Copyright (c) Dominick Baier, Brock Allen.  All rights reserved.
 * see license.txt
 */

using System.Web.Mvc;
using Thinktecture.IdentityModel.SystemWeb.Mvc;

namespace Thinktecture.AuthorizationServer.WebHost.Areas.Admin.Controllers
{
    [ResourceActionAuthorize(Constants.Actions.Configure, Constants.Resources.Server)]
    public class ApplicationController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Manage()
        {
            return View();
        }

        public ActionResult Scopes()
        {
            return View();
        }
        
        public ActionResult Scope()
        {
            return View();
        }

        public ActionResult RememberOptions()
        {
            return View();
        }

        public ActionResult RememberOption()
        {
            return View();
        }
    }
}
