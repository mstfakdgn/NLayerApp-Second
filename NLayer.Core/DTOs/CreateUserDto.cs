﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class CreateUserDto : BaseEntityDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
