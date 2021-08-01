using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MinecraftServerAdmin {
	public class Program {
		public static void Main(string[] args) {
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder => {
					webBuilder.UseStartup<Startup>();
					// webBuilder.UseKestrel(serverOptions => {
					// 	serverOptions.Listen(IPAddress.Any, 443, listenOptions => {
					// 		listenOptions.UseHttps("/etc/ssl/private/corsac.nl.pfx");
					// 	});
					// 	serverOptions.AddServerHeader = false;
					// });
				});
	}
}
