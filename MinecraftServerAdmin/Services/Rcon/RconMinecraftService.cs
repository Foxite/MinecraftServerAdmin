using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MinecraftServerAdmin.Services.Rcon {
	public class RconMinecraftService : MinecraftService {
		private readonly IRconService m_Rcon;

		public RconMinecraftService(IRconService rcon) {
			m_Rcon = rcon;
		}

		public override Task<string> ExecuteCommand(string command) => m_Rcon.ExecuteCommandAsync(command);

		public async override Task<IReadOnlyCollection<string>> GetOnlinePlayers() {
			return (await ExecuteCommand("list")).Split(": ")[1].Split(", ");
		}
		
		public async override Task<IReadOnlyCollection<string>> GetWhitelist() {
			return (await ExecuteCommand("whitelist list")).Split(": ")[1].Split(", ");
		}
		
		public async override Task<IReadOnlyCollection<(string Name, string Issuer, string Reason)>> GetBanlist() {
			throw new NotImplementedException("Not implemented due to a minecraft bug.");
			string result = await ExecuteCommand("banlist");
			Regex banRegex = new Regex(@"(?<BannedPlayer>[A-z0-9_]+) was banned by (?<Issuer>[A-z0-9_]+): (?<Reason>.*)");
			return result.Split('\n').ListSelect(line => {
				Match match = banRegex.Match(line);
				return (
					Name: match.Groups["BannedPlayer"].Value,
					Issuer: match.Groups["Issuer"].Value,
					Reason: match.Groups["Issuer"].Value
				);
			});
		}
		
		public override Task AddToWhitelist(string name) {
			return m_Rcon.ExecuteCommandAsync($"whitelist add {name}");
		}
		
		public override Task RemoveFromWhitelist(string name) {
			return m_Rcon.ExecuteCommandAsync($"whitelist remove {name}");
		}
		
		public override Task BanPlayer(string name, string reason) {
			return m_Rcon.ExecuteCommandAsync($"ban {name} {reason}");
		}
		
		public override Task UnbanPlayer(string name) {
			return m_Rcon.ExecuteCommandAsync($"pardon {name}");
		}
		
		public override Task BanIp(string ip, string reason) {
			return m_Rcon.ExecuteCommandAsync($"ban-ip {ip} {reason}");
		}
		
		public override Task UnbanIp(string ip) {
			return m_Rcon.ExecuteCommandAsync($"pardon-ip {ip}");
		}
	}
}
