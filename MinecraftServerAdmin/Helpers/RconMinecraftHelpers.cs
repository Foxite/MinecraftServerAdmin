using Microsoft.Extensions.Configuration;
using MinecraftServerAdmin.Services;
using MinecraftServerAdmin.Services.Rcon;

namespace Microsoft.Extensions.DependencyInjection {
	public static class RconMinecraftHelpers {
		public static IMinecraftServiceBuilder AddMinecraft(this IServiceCollection isc) {
			return new MinecraftServiceBuilder(isc);
		}

		public static IRconMinecraftBuilder AddRcon(this IMinecraftServiceBuilder builder, IConfiguration configuration) {
			builder.Services.AddTransient<MinecraftService, RconMinecraftService>();

			builder.Services.Configure<RconConfig>(configuration.GetSection(nameof(RconConfig)));
			
			return new RconMinecraftBuilder(builder.Services);
		}

		public static IRconMinecraftBuilder AddRconSharp(this IRconMinecraftBuilder builder, IConfiguration configuration) {
			// It must be a singleton because it implements the thread synchronization. If it were scoped or transient, there would be multiple locks on the same tcp port.
			builder.Services.AddSingleton<IRconService, RconSharpRconService>();

			return builder;
		}
	}

	public interface IMinecraftServiceBuilder {
		public IServiceCollection Services { get; }
	}

	internal class MinecraftServiceBuilder : IMinecraftServiceBuilder {
		public IServiceCollection Services { get; }
		
		public MinecraftServiceBuilder(IServiceCollection services) {
			Services = services;
		}
	}

	public interface IRconMinecraftBuilder {
		public IServiceCollection Services { get; }
	}

	internal class RconMinecraftBuilder : IRconMinecraftBuilder {
		public IServiceCollection Services { get; }
		
		public RconMinecraftBuilder(IServiceCollection services) {
			Services = services;
		}
	}
}
