using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MinecraftServerAdmin.Helpers {
	public class PathBaseMiddleware {
		private readonly RequestDelegate m_Next;
		private readonly string m_PathBase;

		public PathBaseMiddleware(RequestDelegate next, string pathBase = null) {
			m_PathBase = pathBase;
			m_Next = next;
		}

		public async Task Invoke(HttpContext context) {
			if (m_PathBase != null) {
				context.Request.PathBase = m_PathBase;
			}

			await m_Next(context);
		}
	}
}
