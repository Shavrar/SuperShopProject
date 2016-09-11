using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SuperShopProject.Models;

namespace SuperShopProject.Managers
{
    public class ItemsManager: BaseManager
    {
        private readonly ApplicationDbContext _db;

        public ItemsManager(ApplicationDbContext applicationDbContext)
        {
            _db = applicationDbContext;
        }

        public async Task<ICollection<Item>> GetAll()
        {
            return await _db.Items.ToListAsync();
        }

        public async Task<Item> GetById(Guid id)
        {
            return await _db.Items.FindAsync(id);
        }

        public async Task Add(Item item)
        {
            item.Id = Guid.NewGuid();
            _db.Items.Add(item);
            await _db.SaveChangesAsync();
        }

        public async Task Save(Item item)
        {
            var tochange = await _db.Items.FindAsync(item.Id);
            if (tochange != null)
            {
                tochange.Count = item.Count;
                tochange.Description = item.Description;
                tochange.Name = item.Name;
                tochange.Price = item.Price;
                tochange.Type = item.Type;
            }
            await _db.SaveChangesAsync();
        }
    }
}