using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SuperShopProject.Models;
using SuperShopProject.ViewModels;
using WebGrease;

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

        public async Task Add(NewItemViewModel model, ModelStateDictionary modelState)
        {
            if (await _db.Items.AnyAsync(it => it.Name == model.Name))
            {
                modelState.AddModelError("","Already exists");
                return;
            }
            var item = new Item {Id = Guid.NewGuid()};
            item = model.SetValues(item);
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