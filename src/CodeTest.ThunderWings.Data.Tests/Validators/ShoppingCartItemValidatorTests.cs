using CodeTest.ThunderWings.Data.Models;
using CodeTest.ThunderWings.Data.Validatiors;

using FluentValidation.TestHelper;

namespace CodeTest.ThunderWings.Data.Tests.Validators
{
	[TestClass]
	public class ShoppingCartItemValidatorTests

	{
		[TestMethod]
		public void NameEmpty()
		{
			//arrange
			var uot = new ShoppingCartItemValidator();
			var model = new ShoppingCartItem() { Name = "", Quantity = 10, ShopperId = "AAA" };
			// Act
			var actual = uot.TestValidate(model);
			// Assert
			actual.ShouldHaveValidationErrorFor(m => m.Name);
			actual.ShouldNotHaveValidationErrorFor(m => m.Quantity);
			actual.ShouldNotHaveValidationErrorFor(m => m.ShopperId);
		}

		[TestMethod]
		public void QuantityNegative()
		{
			//arrange
			var uot = new ShoppingCartItemValidator();
			var model = new ShoppingCartItem() { Name = "Test", Quantity = -10, ShopperId = "AAA" };
			// Act
			var actual = uot.TestValidate(model);
			// Assert
			actual.ShouldNotHaveValidationErrorFor(m => m.Name);
			actual.ShouldHaveValidationErrorFor(m => m.Quantity);
			actual.ShouldNotHaveValidationErrorFor(m => m.ShopperId);
		}

		[TestMethod]
		public void QuantityPositive()
		{
			//arrange
			var uot = new ShoppingCartItemValidator();
			var model = new ShoppingCartItem() { Name = "Test", Quantity = 1234, ShopperId = "AAA" };
			// Act
			var actual = uot.TestValidate(model);
			// Assert
			actual.ShouldNotHaveValidationErrorFor(m => m.Name);
			actual.ShouldNotHaveValidationErrorFor(m => m.Quantity);
			actual.ShouldNotHaveValidationErrorFor(m => m.ShopperId);
		}

		[TestMethod]
		public void QuantityZero()
		{
			//arrange
			var uot = new ShoppingCartItemValidator();
			var model = new ShoppingCartItem() { Name = "Test", Quantity = 0, ShopperId = "AAA" };
			// Act
			var actual = uot.TestValidate(model);
			// Assert
			actual.ShouldNotHaveValidationErrorFor(m => m.Name);
			actual.ShouldHaveValidationErrorFor(m => m.Quantity);
			actual.ShouldNotHaveValidationErrorFor(m => m.ShopperId);
		}

		[TestMethod]
		public void ShopperIdEmpty()
		{
			//arrange
			var uot = new ShoppingCartItemValidator();
			var model = new ShoppingCartItem() { Name = "ABC", Quantity = 10, ShopperId = "" };
			// Act
			var actual = uot.TestValidate(model);
			// Assert
			actual.ShouldNotHaveValidationErrorFor(m => m.Name);
			actual.ShouldNotHaveValidationErrorFor(m => m.Quantity);
			actual.ShouldHaveValidationErrorFor(m => m.ShopperId);
		}
	}
}