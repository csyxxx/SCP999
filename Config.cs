using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCP999
{
    public class Config
    {
        [Description("是否启用")]
        public bool isEnable { get; set; } = true;
        [Description("999一次加多少血")]
        public int Health { get; set; } = 5;
    }
}
