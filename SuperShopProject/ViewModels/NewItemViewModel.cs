using System.ComponentModel.DataAnnotations;
using SuperShopProject.Constants;
using SuperShopProject.Models;

namespace SuperShopProject.ViewModels
{
    public class NewItemViewModel
    {
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

        public Item SetValues(Item item)
        {
            item.Name = this.Name;
            item.Description = Description;
            item.Price = Price;
            item.Count = Count;
            item.Type = Type;
            return item;
        }
    }
}