﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Category Name")]
        [Required]
        public string Name { get; set; }

        //[ForeignKey("Brand")]
        //public int BrandId { get; set; }
        //public virtual Brand Brand { get; set; }

        public virtual List<Product> Products { get; set; }

    }
}
