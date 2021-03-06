using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class ItemsController : Controller
    {
        [HttpGet("/items")]
        public ActionResult Index()
        {
            List<Item> allItems = Item.GetAll();
            return View(allItems);
        }

        [HttpGet("/items/new")]
        public ActionResult CreateForm()
        {
            return View();
        }
        [HttpPost("/items")]
        public ActionResult Create()
        {
            Item newItem = new Item (Request.Form["new-item"]);
            newItem.Save();
            List<Item> allItems = Item.GetAll();
            return RedirectToAction("Index");
        }

        [HttpPost("/items/delete")]
        public ActionResult DeleteAll()
        {
            Item.DeleteAll();
            return View();
        }

        [HttpGet("/items/{id}")]
        public ActionResult Details(int id)
        {
            Item item = Item.Find(id);
            return View(item);
        }
        [HttpGet("/items/{id}/update")]
        public ActionResult Update(int id)
        {
            Item thisItem = Item.Find(id);
            return View(thisItem);
        }

        [HttpPost("/items/{id}/update")]
        public ActionResult UpdatePost(int id)
        {
            Item thisItem = Item.Find(id);
            thisItem.Edit(Request.Form["newname"]);
            return RedirectToAction("Index");
        }
        [HttpPost("/items/{id}/addcat")]
        public ActionResult AddCatToItem(int id)
        {
            Category thisCategory = Category.Find(Int32.Parse(Request.Form["newcat"]));
            Item thisItem = Item.Find(id);
            thisItem.AddCategory(thisCategory);
            return RedirectToAction("Update", id);
        }
        [HttpPost("/items/{id}/delete")]
        public ActionResult DeletePost(int id)
        {
            Item thisItem = Item.Find(id);
            thisItem.Delete();
            return RedirectToAction("Index");
        }
    }
}
