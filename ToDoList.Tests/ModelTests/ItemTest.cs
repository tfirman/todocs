using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList;

namespace ToDoList.Tests
{
  [TestClass]
  public class ItemTest
  {
    [TestMethod]
    public void GetDescription_ReturnsDescription_String()
    {
      //Arrange
      string description = "Walk the dog.";
      Item newItem = new Item(description);

      //Act
      string result = newItem.GetDescription();

      //Assert
      Assert.AreEqual(description, result);
    }
  }
}
