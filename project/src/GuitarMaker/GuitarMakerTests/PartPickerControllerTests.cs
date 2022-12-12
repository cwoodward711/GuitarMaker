using FakeItEasy;
using GuitarMaker;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace GuitarMakerTests
{
    [TestFixture]
    public class PartPickerControllerTests
    {
        private MainWindow fakeMainWindow;

        [SetUp]
        public void SetUp()
        {
            this.fakeMainWindow = A.Fake<MainWindow>();
        }

        private PartPickerController CreatePartPickerController()
        {
            return new PartPickerController(this.fakeMainWindow);
        }

        //[Test]
        //public void UpdatePriceComponents_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var partPickerController = this.CreatePartPickerController();
        //    Guitar testGuitar = new Guitar();
        //
        //    // Act
        //    Dictionary<string, double> prices = new Dictionary<string, double>();
        //    prices[testGuitar.Body.Name] = testGuitar.Body.Price;
        //    prices[testGuitar.Bridge.Name] = testGuitar.Bridge.Price;
        //    prices[testGuitar.FretBoard.Name] = testGuitar.FretBoard.Price;
        //    prices[testGuitar.Headstock.Name] = testGuitar.Headstock.Price;
        //    prices[testGuitar.Knob.Name] = testGuitar.Knob.Price;
        //    prices[testGuitar.Nut.Name] = testGuitar.Nut.Price;
        //    prices[testGuitar.PickGuard.Name] = testGuitar.PickGuard.Price;
        //    prices[testGuitar.Pickup_B.Name] = testGuitar.Pickup_B.Price;
        //    prices[testGuitar.Pickup_M.Name] = testGuitar.Pickup_M.Price;
        //    prices[testGuitar.Pickup_N.Name] = testGuitar.Pickup_N.Price;
        //    prices[testGuitar.StrapPeg.Name] = testGuitar.StrapPeg.Price;
        //    prices[testGuitar.Switch.Name] = testGuitar.Switch.Price;
        //    prices[testGuitar.Tuner.Name] = testGuitar.Tuner.Price;
        //    prices[testGuitar.Jack.Name] = testGuitar.Jack.Price;
        //
        //    // Assert
        //    Assert.Fail();
        //}

        //[Test]
        //public void Undo_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var partPickerController = this.CreatePartPickerController();
        //    object sender = null;
        //    //RoutedEventArgs e = null;
        //
        //    // Act
        //    //partPickerController.Undo(sender, e);
        //
        //    // Assert
        //    Assert.Fail();
        //}

        //[Test]
        //public void Redo_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var partPickerController = this.CreatePartPickerController();
        //    object sender = null;
        //    //RoutedEventArgs e = null;
        //
        //    // Act
        //    partPickerController.Redo(sender, e);
        //
        //    // Assert
        //    Assert.Fail();
        //}

        //[Test]
        //public void RefreshImages_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var partPickerController = this.CreatePartPickerController();
        //
        //    // Act
        //    partPickerController.RefreshImages();
        //
        //    // Assert
        //    Assert.Fail();
        //}

        //[Test, Apartment(System.Threading.ApartmentState.STA)]
        //public void LoadGuitar_LoadSuccesful_True()
        //{
        //    // Arrange
        //    var partPickerController = this.CreatePartPickerController();
        //    string fileName = null;
        //
        //    // Act
        //    partPickerController.LoadGuitar(fileName);
        //
        //    // Assert
        //    A.CallTo(() => partPickerController.LoadGuitar(fileName)).MustHaveHappened();
        //    //Assert.That(partPickerController.LoadGuitar(fileName), )
        //
        //}
        //[Test]
        //public void SaveGuitar_SaveSuccessful_True()
        //{
        //    // Arrange
        //    var partPickerController = this.CreatePartPickerController();
        //    string fileName = null;
        //
        //    // Act
        //    partPickerController.SaveGuitar(fileName);
        //
        //    // Assert
        //    Assert.Fail();
        //}

        //[Test]
        //public void ExportGuitar_ExportSuccessful_True()
        //{
        //    // Arrange
        //    var partPickerController = this.CreatePartPickerController();
        //    string fileName = "testExport.txt";
        //
        //    // Act
        //    partPickerController.ExportGuitar(fileName);
        //
        //    // Assert
        //    Assert.Fail();
        //}
    }
}
