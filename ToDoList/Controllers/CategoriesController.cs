using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class CategoriesController : Controller
    {
        [HttpGet("/categories")]
        public ActionResult IndexCat()
        {
            List<Category> allCategories = Category.GetAll();
            return View(allCategories);
        }
        [HttpGet("/categories/new")]
        public ActionResult CreateCatForm()
        {
            return View();
        }
        [HttpPost("/categories")]
        public ActionResult CreateCat()
        {
            Category newCategory = new Category (Request.Form["new-category"]);
            newCategory.Save();
            List<Category> allItems = Category.GetAll();
            return RedirectToAction("IndexCat");
        }
        [HttpPost("/categories/delete")]
        public ActionResult DeleteAllCat()
        {
            Category.DeleteAll();
            return View();
        }
        [HttpGet("/categories/{id}")]
        public ActionResult DetailsCat(int id)
        {
            Category thisCategory = Category.Find(id);
            return View(thisCategory);
        }
        [HttpGet("/categories/{id}/update")]
        public ActionResult UpdateCat(int id)
        {
            Category thisCategory = Category.Find(id);
            return View(thisCategory);
        }

        [HttpPost("/categories/{id}/additem")]
        public ActionResult AddItemtoCat(int id)
        {
            Item thisItem = Item.Find(Int32.Parse(Request.Form["newitem"]));
            Category thisCategory = Category.Find(id);
            thisCategory.AddItem(thisItem);
            return RedirectToAction("UpdateCat", id);
        }
        [HttpPost("/categories/{id}/delete")]
        public ActionResult DeletePostCat(int id)
        {
            Category thisCategory = Category.Find(id);
            thisCategory.Delete();
            return RedirectToAction("IndexCat");
        }
    }
}
