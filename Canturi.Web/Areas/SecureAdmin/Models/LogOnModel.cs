﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Canturi.Web.Areas.SecureAdmin.Models
{
    public class LogOnModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public bool IsRemember { get; set; }
    }
}