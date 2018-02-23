using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList.Tests
{
    [TestClass]
    public class ItemTest : IDisposable
    {
        public ItemTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=todo_test;";
        }
        public void Dispose()
        {
            Item.DeleteAll();
            Category.DeleteAll();
        }

        [TestMethod]
        public void GetAll_DatabaseEmptyAtFirst_0()
        {
            //Arrange, Act
            int result = Item.GetAll().Count;

            //Assert
            Assert.AreEqual(0, result);
        }
        [TestMethod]
        public void Item_DoesntSaveToDatabase_ItemCount()
        {
            //Arrange
            Item testItem = new Item("Mow the lawn", 1);

            //Act
            int result = Item.GetAll().Count;

            //Assert
            Assert.AreEqual(0, result);
        }
        [TestMethod]
        public void Equals_ReturnsTrueIfDescriptionsAreTheSame_Item()
        {
            // Arrange, Act
            Item firstItem = new Item("Mow the lawn", 1);
            Item secondItem = new Item("Mow the lawn", 1);

            // Assert
            Assert.AreEqual(firstItem, secondItem);
        }
        [TestMethod]
        public void Save_SavesToDatabase_ItemList()
        {
            //Arrange
            Item testItem = new Item("Mow the lawn", 1);

            //Act
            testItem.Save();
            List<Item> result = Item.GetAll();
            List<Item> testList = new List<Item>{testItem};
            //Assert
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Find_FindsItemInDatabase_Item()
        {
            //Arrange
            Item testItem = new Item("Mow the lawn", 1);
            testItem.Save();

            //Act
            Item foundItem = Item.Find(testItem.GetId());

            //Assert
            Assert.AreEqual(testItem, foundItem);
        }

        [TestMethod]
        public void Edit_UpdatesItemInDatabase_String()
        {
            //Arrange
            string firstDescription = "Walk the Dog";
            Item testItem = new Item(firstDescription, 1);
            testItem.Save();
            string secondDescription = "Mow the lawn";

            //Act
            testItem.Edit(secondDescription);

            string result = Item.Find(testItem.GetId()).GetDescription();

            //Assert
            Assert.AreEqual(secondDescription , result);
        }


    }

}
