using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA_Main.Models
{
    public class OutputResult
    {
        public string? Message { get; set; }
        public System.Windows.Media.Brush? Colour { get; set; }

        public OutputResult(string msg, System.Windows.Media.Brush colour)
        {
            Message = msg;
            Colour = colour;
        }
    }
}
