using System;
using System.ComponentModel.DataAnnotations;
using SuperShopProject.Constants;

namespace SuperShopProject.ViewModels
{
    public class EditItemViewModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public ItemType Type { get; set; }
    }
}