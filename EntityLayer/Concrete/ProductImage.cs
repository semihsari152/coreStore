﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class ProductImage
    {
        [Key]
        public int ImageID { get; set; }
        public string ImagePath { get; set; }

        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}
