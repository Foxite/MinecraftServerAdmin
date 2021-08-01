using System;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Http;

namespace MinecraftServerAdmin.Helpers {
	/// <summary>
	/// Configures the HttpContext by assigning IdentityServerOrigin.
	/// </summary>
	/// <remarks>
	/// http://amilspage.com/set-identityserver4-url-behind-loadbalancer/
	/// </remarks>
	public class PublicFacingUrlMiddleware {
		private readonly RequestDelegate m_Next;
		private readonly string m_PublicFacingUri;
		private readonly string m_PathBase;

		public PublicFacingUrlMiddleware(RequestDelegate next, string publicFacingUri, string pathBase = null) {
			m_PublicFacingUri = publicFacingUri;
			m_PathBase = pathBase;
			m_Next = next;
		}

		public async Task Invoke(HttpContext context) {
			var request = context.Request;
			
			//context.SetIdentityServerOrigin(new Uri(_publicFacingUri).GetLeftPart(UriPartial.Authority));
			//context.SetIdentityServerBasePath(new Uri(_publicFacingUri).AbsolutePath);
			context.SetIdentityServerOrigin(m_PublicFacingUri);

			if (m_PathBase != null) {
				context.Request.PathBase = m_PathBase;
			}

			context.SetIdentityServerBasePath(request.PathBase.Value!.TrimEnd('/'));

			await m_Next(context);
		}
	}
}
