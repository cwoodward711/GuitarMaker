using FakeItEasy;
using GuitarMaker;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GuitarMakerTests
{
    [TestFixture]
    public class GuitarSaverTests
    {
        private Guitar fakeGuitar;
        private Component fakeComponent1;
        private Component fakeComponent2;

        [SetUp]
        public void SetUp()
        {
            this.fakeGuitar = A.Fake<Guitar>();
            this.fakeComponent1 = A.Fake<Component>();
            this.fakeComponent2 = A.Fake<Component>();
            this.fakeComponent1.Name = "Component1";
            this.fakeComponent2.Name = "Component2";
        }

        [Test]
        public async Task Save_SaveSuccess_True()
        {
            // Arrange
            var dir = Environment.CurrentDirectory;
            string fileName = dir + @"\Resources\TestSave.json"; 

            // Act
            fakeGuitar.Body = fakeComponent1;
            fakeGuitar.Headstock = fakeComponent2;
            GuitarSaver.Save(fileName, fakeGuitar);
            fakeGuitar.Body = null;
            fakeGuitar.Headstock = null;
            

            // Assert
            Assert.IsTrue(File.Exists(fileName));
        }

        //Must run Save_SaveSuccess_True() first to ensure a file exists, happens by default if all tests are run
        [Test]
        public async Task Load_LoadSuccess_True()
        {
            // Arrange
            await(Save_SaveSuccess_True());
            var dir = Environment.CurrentDirectory;
            string fileName = dir + @"\Resources\TestSave.json";

            // Act
            Guitar fakeGuitar = GuitarSaver.Load(fileName);
        
            // Assert
            Assert.AreEqual(fakeGuitar.Body.Name, fakeComponent1.Name);
            Assert.AreEqual(fakeGuitar.Headstock.Name, fakeComponent2.Name);
            File.Delete(fileName);
        }
        
        [Test]
        public void Export_ExportSuccess_True()
        {
            // Arrange
            var dir = Environment.CurrentDirectory;
            string partsList = dir + @"\Resources\PartsList.json";
            IComponentCollection componentCollection = new ConcreteComponentCollection(partsList);
            this.fakeGuitar = new Guitar(componentCollection);
            string fileName = dir + @"\Resources\TestExport.txt";
        
            // Act
            GuitarSaver.Export(fileName, fakeGuitar);

            // Assert
            Assert.IsTrue(File.Exists(fileName));
            File.Delete(fileName);
        }
    }
}
