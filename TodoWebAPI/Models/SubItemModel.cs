﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebAPI.Models
{
    public class SubItemModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ListItemId { get; set; }
        public bool Completed { get; set; }
    }
}
