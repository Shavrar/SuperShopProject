using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SuperShopProject.Constants;

namespace SuperShopProject.Models
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
        public ItemType Type { get; set; }
        public int Count { get; set; }
        public ICollection<UserItem> ItemUsers { get; set; }
    }

    public class UserItem
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public Guid ItemId { get; set; }
        public virtual Item Item { get; set; }
        public int Amount { get; set; }
    }
}