﻿using System;
using NHIdentityRole = NHibernate.AspNetCore.Identity.IdentityRole;

namespace WebTest.Entities {

    public class ApplicationRole : NHIdentityRole {

        public virtual string Description { get; set; }

    }

}