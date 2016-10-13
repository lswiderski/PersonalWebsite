using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Controllers;
using PersonalWebsite.Services.Models;

namespace PersonalWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class AdventureController : PersonalController
    {
        private readonly IAdventureModel _adventureModel;

        public AdventureController(IAdventureModel adventureModel)
        {
            _adventureModel = adventureModel;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = _adventureModel.GetListForAdmin();
            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var model = _adventureModel.GetAdd();
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(AddAdventureViewModel model)
        {
            if (ModelState.IsValid)
            {
                _adventureModel.Add(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _adventureModel.GetEdit(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditAdventureViewModel model)
        {
            if (ModelState.IsValid)
            {
                _adventureModel.Edit(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Remove(int id)
        {
            _adventureModel.Remove(id);
            return RedirectToAction("Index");
        }


    }
}