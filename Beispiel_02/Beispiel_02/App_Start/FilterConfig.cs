﻿using System.Web;
using System.Web.Mvc;

namespace Beispiel_02
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
