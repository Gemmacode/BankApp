﻿using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Session
{
    public class UserSession
    {
        public static User LoggedInUser { get; set; }
    }
}
