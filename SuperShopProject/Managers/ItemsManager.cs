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

        public async Task<EditItemViewModel> GetById(Guid id)
        {
            var item = await _db.Items.FindAsync(id);

            return new EditItemViewModel
            {
                Id = item.Id,
                Count = item.Count,
                Description = item.Description,
                Name = item.Name,
                Price = item.Price,
                Type = item.Type
            };
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

        public async Task Save(EditItemViewModel item,ModelStateDictionary modelState)
        {
            var tochange = await _db.Items.FindAsync(item.Id);
            if (tochange != null)
            {
                if (item.Name!=tochange.Name && await _db.Items.AnyAsync(it => it.Name == item.Name))
                {
                    modelState.AddModelError("Name", "This name is occupied.");
                    return;
                }
                tochange.Count = item.Count;
                tochange.Description = item.Description;
                tochange.Name = item.Name;
                tochange.Price = item.Price;
                tochange.Type = item.Type;
            }
            else
            {
                modelState.AddModelError("","Item wasn't found");
                return;
            }
            await _db.SaveChangesAsync();
        }

        public async Task Remove(Guid[] items)
        {
            foreach (var id in items)
            {
                var toRemove = await _db.Items.FindAsync(id);
                if (toRemove != null)
                {
                    _db.Items.Remove(toRemove);
                }
            }
            await _db.SaveChangesAsync();
        } 
    }
}