using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MinecraftServerAdmin.Helpers;

namespace MinecraftServerAdmin {
	public class Startup {
		public IConfiguration Configuration { get; }
		public IWebHostEnvironment Environment { get; }

		public Startup(IConfiguration configuration, IWebHostEnvironment environment) {
			Configuration = configuration;
			Environment = environment;
		}
		
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services) {
			services.AddControllersWithViews();
			
			services.AddMinecraft()
				.AddRcon(Configuration)
				.AddRconSharp(Configuration);

			var authenticationConfig = Configuration.GetSection(nameof(AuthenticationConfig)).Get<AuthenticationConfig>();
			
			services
				.AddAuthentication(options => {
					options.DefaultScheme = "Cookies";
					options.DefaultChallengeScheme = "oidc";
				})
				.AddCookie("Cookies")
				.AddOpenIdConnect("oidc", options => {
					options.ResponseType = "code";
					options.SaveTokens = true;
					options.GetClaimsFromUserInfoEndpoint = true;
					options.RequireHttpsMetadata = false;

					options.Authority = authenticationConfig.Authority;
					options.ClientId = authenticationConfig.ClientId;
					options.ClientSecret = authenticationConfig.ClientSecret;
					options.Scope.Add(authenticationConfig.Scope);
				});

			services.AddAuthorization(options => {
				options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole(authenticationConfig.RequiredRole));
				options.DefaultPolicy = options.GetPolicy("RequireAdministratorRole")!;
			});

			services.PostConfigure<KestrelServerOptions>(kso => {
				kso.ListenAnyIP(Configuration.GetValue<int>("Port", 5100));
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			} else {
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseMiddleware<PublicFacingUrlMiddleware>(
				Configuration.GetValue<string>("PublicFacingUrl", "https://localhost:5001"),
				Configuration.GetValue<string>("PathBase", "/")
			);

			app.UseStaticFiles();
			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints => {
				endpoints.MapDefaultControllerRoute()
					.RequireAuthorization();
			});
		}
	}
}
