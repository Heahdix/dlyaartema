using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;
using System.Linq;
using TheMostImportantProject;
using System.Windows.Media.Imaging;


namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ShowMethodTest()
        {
            MenuScreen menuScreen = new MenuScreen();
            int actual = menuScreen.mainPLace.Children.Count;
            int expected = 2;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeleteMethodTest()
        {
            AdminMenu adminMenu = new AdminMenu();
            adminMenu.DeletePostition(27);
            TheMostImportantProject.Position actual;
            using (Pizza_MozzarellaEntities db = new Pizza_MozzarellaEntities())
            {
                actual = db.Position.Where(x => x.PositionID == 27).FirstOrDefault();
            }
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void AddNewTest()
        {
            PositionAdd positionAdd = new PositionAdd();
            string expectedName = "UnitTest";
            decimal expectedPrice = 30;
            bool expectedHide = false;
            positionAdd.fileName = @"C:\Users\Almaz\OneDrive\Рабочий стол\Картиночки\06c75b36952747a694a169662cb3267b_292x292.png";
            positionAdd.positionImageToConvert = System.Drawing.Image.FromFile(positionAdd.fileName);
            positionAdd.AddNew(expectedName, expectedPrice, expectedHide);
            using (Pizza_MozzarellaEntities db = new Pizza_MozzarellaEntities())
            {
                Position actual = db.Position.OrderByDescending(x=>x.PositionID).First();
                Assert.AreEqual(expectedName, actual.Name);
                Assert.AreEqual(expectedPrice, actual.Price);
                Assert.AreEqual(expectedHide, actual.IsHidden);
            }
        }

        [TestMethod]
        public void ChangeTest()
        {
            string sourceName;
            decimal sourcePrice;
            bool sourceHide;
            using (Pizza_MozzarellaEntities db = new Pizza_MozzarellaEntities())
            {
                Position sourcePosition = new Position();
                sourcePosition = db.Position.Where(x => x.PositionID == 30).First();
                sourceName= sourcePosition.Name;
                sourcePrice = sourcePosition.Price;
                sourceHide = sourcePosition.IsHidden;

                PositionAdd positionAdd = new PositionAdd(sourcePosition);
                positionAdd.Change("UnitTestGhange21", 45, true);
            }
            
            using (Pizza_MozzarellaEntities db = new Pizza_MozzarellaEntities())
            {
                Position actualPosition = new Position();
                actualPosition = db.Position.Where(x => x.PositionID == 30).First();

                string actualName = actualPosition.Name;
                decimal actualPrice = actualPosition.Price;
                bool actualHide = actualPosition.IsHidden;

                Assert.AreNotEqual(sourceName, actualName);
                Assert.AreNotEqual(sourcePrice, actualPrice);
                Assert.AreNotEqual(sourceHide, actualHide);
            }
        }
    }
}
