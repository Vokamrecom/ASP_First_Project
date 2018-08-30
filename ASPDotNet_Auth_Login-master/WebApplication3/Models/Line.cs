using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Line
    {
        public Point InitialPosition { get; set; }
        public Point EndPosition { get; set; }
        public int Width { get; set; }
        public string Color { get; set; }
    }
}