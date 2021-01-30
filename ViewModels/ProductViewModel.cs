using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pizza.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Категория")]
        public string Category { get; set; }
        [Required]
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Цена (max 65034)")]
        public ushort Price { get; set; }
        public IFormFile Image { get; set; }
    }
}
