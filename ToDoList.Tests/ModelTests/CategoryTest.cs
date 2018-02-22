using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using ToDoList.Models;

namespace ToDoList.Tests
{
    [TestClass]
    public class CategoryTests : IDisposable
    {
        // public CategoryTests()
        // {
        //     DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=todo_test;";
        // }

        [TestMethod]
        public void GetAll_CategoriesEmptyAtFirst_0()
        {
            //Arrange, Act
            int result = Category.GetAll().Count;

            //Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Equals_ReturnsTrueForSameName_Category()
        {
            //Arrange, Act
            Category firstCategory = new Category("Household chores");
            Category secondCategory = new Category("Household chores");

            //Assert
            Assert.AreEqual(firstCategory, secondCategory);
        }

        [TestMethod]
        public void Save_SavesCategoryToDatabase_CategoryList()
        {
        //Arrange
            Category testCategory = new Category("Household chores");
            testCategory.Save();

        //Act
            List<Category> result = Category.GetAll();
            List<Category> testList = new List<Category>{testCategory};

        //Assert
            CollectionAssert.AreEqual(testList, result);
        }


        [TestMethod]
        public void Save_DatabaseAssignsIdToCategory_Id()
        {
            //Arrange
            Category testCategory = new Category("Household chores");
            testCategory.Save();

            //Act
            Category savedCategory = Category.GetAll()[0];

            int result = savedCategory.GetId();
            int testId = testCategory.GetId();

            //Assert
            Assert.AreEqual(testId, result);
        }


        [TestMethod]
        public void Find_FindsCategoryInDatabase_Category()
        {
          //Arrange
            Category testCategory = new Category("Household chores");
            testCategory.Save();

          //Act
            Category foundCategory = Category.Find(testCategory.GetId());

          //Assert
            Assert.AreEqual(testCategory, foundCategory);
        }

        [TestMethod]
        public void GetItems_RetrievesAllItemsWithCategory_ItemList()
        {
            Category testCategory = new Category("Household chores");
            testCategory.Save();

            Item firstItem = new Item("Mow the lawn", testCategory.GetId());
            firstItem.Save();
            Item secondItem = new Item("Do the dishes", testCategory.GetId());
            secondItem.Save();


            List<Item> testItemList = new List<Item> {firstItem, secondItem};
            List<Item> resultItemList = testCategory.GetItems();

            CollectionAssert.AreEqual(testItemList, resultItemList);
        }

        public void Dispose()
        {
            Item.DeleteAll();
            Category.DeleteAll();
        }
    }
}
