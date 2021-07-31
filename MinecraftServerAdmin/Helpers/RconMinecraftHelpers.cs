using Microsoft.Extensions.Configuration;
using MinecraftServerAdmin.Services;
using RconSharp;

namespace Microsoft.Extensions.DependencyInjection {
	public static class RconMinecraftHelpers {
		public static IServiceCollection AddMinecraftRcon(this IServiceCollection isc, IConfiguration configuration) {
			var config = configuration.GetValue<MinecraftRconConfig>(nameof(MinecraftRconConfig));
			isc.AddSingleton(_ => {
				var ret = RconClient.Create(config.Host, config.Port);
				ret.AuthenticateAsync(config.Password).GetAwaiter().GetResult();
				return ret;
			});
			
			isc.AddTransient<RconMinecraftService>();

			return isc;
		}
	}
}
