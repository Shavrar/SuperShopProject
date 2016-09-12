using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SuperShopProject.Managers;
using SuperShopProject.Models;

namespace SuperShopProject.Controllers
{
    [RoutePrefix("item")]
    public class ItemController: BaseController
    {
        private readonly ItemsManager _itemsManager;

        public ItemController(ItemsManager itemsManager)
        {
            _itemsManager = itemsManager;
        }
        [Route("add")]
        public ActionResult Add()
        {
            if (User.IsInRole(Constants.Consts.AdministratorRole))
            {
                return View("AddItem");
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpPut]
        [Route("")]      
        public async Task<ActionResult> Item(Item2 item)
            {
            await _itemsManager.Add(item);
            return new HttpStatusCodeResult(200);
        }
    }
}