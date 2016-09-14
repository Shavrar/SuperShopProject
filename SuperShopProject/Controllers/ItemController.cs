using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SuperShopProject.Managers;
using SuperShopProject.Models;
using SuperShopProject.ViewModels;

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
        public async Task<ActionResult> Item(NewItemViewModel item)
        {
            if (ModelState.IsValid)
            {
                await _itemsManager.Add(item, ModelState);
                if (ModelState.IsValid)
                {
                    Response.StatusCode = (int) HttpStatusCode.Created;
                    return Json(new {Id = -1});
                }
            }
            var errorList = ModelState.ToDictionary(kvp => kvp.Key,kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray());
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(errorList);
        }
    }
}