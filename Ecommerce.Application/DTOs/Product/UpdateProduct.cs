﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.DTOs.Product
{
    public class UpdateProduct:ProductBase
    {
        public Guid Id { get; set; }
    }
}
