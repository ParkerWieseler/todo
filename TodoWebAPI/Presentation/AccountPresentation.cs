﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebAPI.Presentation
{
    public class AccountPresentation
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Picture { get; set; }
        public string Email { get; set; }
    }
}
