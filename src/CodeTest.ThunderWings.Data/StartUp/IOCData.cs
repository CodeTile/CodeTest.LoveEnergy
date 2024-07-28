using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

using Microsoft.Extensions.DependencyInjection;
using System.Net;
using Microsoft.Extensions.Configuration;
using CodeTest.ThunderWings.Data.Services;

namespace CodeTest.ThunderWings.Data.StartUp
{
	[ExcludeFromCodeCoverage]
	public class IocData()
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddAutoMapper(typeof(MappingProfile));
			//Add Transient services to the container.
			services.AddTransient<IThunderWingService, ThunderWingService>();
			//Add Repositories
			//
			//
			// Add Singleton's
			services.AddSingleton(TimeProvider.System);
		}
	}
}