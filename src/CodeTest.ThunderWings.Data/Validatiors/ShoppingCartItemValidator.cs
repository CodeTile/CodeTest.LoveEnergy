using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CodeTest.ThunderWings.Data.Models;

using FluentValidation;

namespace CodeTest.ThunderWings.Data.Validatiors
{
	public class ShoppingCartItemValidator : AbstractValidator<ShoppingCartItem>
	{
		public ShoppingCartItemValidator()
		{
			RuleFor(m => m.ShopperId).NotEmpty();
			RuleFor(m => m.Name).NotEmpty();
			RuleFor(m => m.Quantity)
				.GreaterThan(0)
				.LessThanOrEqualTo(Int32.MaxValue);
		}
	}
}