using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MinecraftServerAdmin.Services;

namespace MinecraftServerAdmin.Controllers {
	public class MainController : Controller {
		private readonly MinecraftService m_Service;
		
		public MainController(MinecraftService service) {
			m_Service = service;
		}

		[HttpGet]
		public IActionResult Index() {
			return View();
		}

		[HttpGet("Players")]
		public async Task<IActionResult> GetPlayers() {
			return Json(await m_Service.GetOnlinePlayers());
		}

		[HttpGet("Ban")]
		public async Task<IActionResult> GetBanlist() {
			return Json(await m_Service.GetBanlist());
		}

		[HttpPut("Ban/Player/{player}")]
		public async Task<IActionResult> BanPlayer(string player, [FromBody] string reason) {
			await m_Service.BanPlayer(player, reason);
			return Ok();
		}

		[HttpDelete("Ban/Player/{player}")]
		public async Task<IActionResult> UnbanPlayer(string player) {
			await m_Service.UnbanPlayer(player);
			return Ok();
		}

		[HttpPut("Ban/Ip/{ip}")]
		public async Task<IActionResult> BanIp(string ip, [FromBody] string reason) {
			await m_Service.BanIp(ip, reason);
			return Ok();
		}

		[HttpDelete("Ban/Ip/{ip}")]
		public async Task<IActionResult> UnbanIp(string ip) {
			await m_Service.UnbanIp(ip);
			return Ok();
		}

		[HttpGet("Whitelist")]
		public async Task<IActionResult> GetWhitelist() {
			return Json(await m_Service.GetWhitelist());
		}

		[HttpPut("Whitelist/{player}")]
		public async Task<IActionResult> AddWhitelist(string player) {
			await m_Service.AddToWhitelist(player);
			return Ok();
		}

		[HttpDelete("Whitelist/{player}")]
		public async Task<IActionResult> RemoveWhitelist(string player) {
			await m_Service.RemoveFromWhitelist(player);
			return Ok();
		}

		[HttpPost("Command")]
		public async Task<IActionResult> ExecuteCommand([FromBody] string command) {
			return Forbid(); // TODO different levels of authorization, based on oauth2 claim
			return Content(await m_Service.ExecuteCommand(command));
		}
	}
}
