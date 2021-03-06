﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Canvas
    {
        public string Id { get; set; }
        public ICollection<Line> Lines { get; set; }
        public HashSet<string> ConnectionsId { get; set; }

        public Canvas()
        {
            Lines = new List<Line>();
            ConnectionsId = new HashSet<string>();
        }
    }
}