﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    public class Token
    {
        public string TokenBody { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
