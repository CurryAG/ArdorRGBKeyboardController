using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ArdorRGBKeyboardController
{
    public class DynamicLightMode
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Thread ModeThread { get; set; }
        public DynamicLightMode(string name, string description, Thread thread)
        {
            Name = name;
            Description = description;
            ModeThread = thread;
        }
        public DynamicLightMode() { }
    }
}
