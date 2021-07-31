using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RconSharp;

namespace MinecraftServerAdmin.Services {
	public class RconMinecraftService : MinecraftService {
		private readonly RconClient m_Client;

		public RconMinecraftService(RconClient client) {
			m_Client = client;
		}

		public override Task<string> ExecuteCommand(string command) => m_Client.ExecuteCommandAsync(command);

		public async override Task<IReadOnlyCollection<string>> GetOnlinePlayers() {
			return (await ExecuteCommand("list")).Split(": ")[1].Split(", ");
		}
		
		public async override Task<IReadOnlyCollection<string>> GetWhitelist() {
			return (await ExecuteCommand("whitelist list")).Split(": ")[1].Split(", ");
		}
		
		public async override Task<IReadOnlyCollection<(string Name, string Issuer, string Reason)>> GetBanlist() {
			throw new NotImplementedException("Not implemented due to a minecraft bug. Waiting for a mod to update and fix it.");
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
			return m_Client.ExecuteCommandAsync($"whitelist add {name}");
		}
		
		public override Task RemoveFromWhitelist(string name) {
			return m_Client.ExecuteCommandAsync($"whitelist add {name}");
		}
		
		public override Task BanPlayer(string name, string reason) {
			return m_Client.ExecuteCommandAsync($"ban {name} {reason}");
		}
		
		public override Task UnbanPlayer(string name) {
			return m_Client.ExecuteCommandAsync($"pardon {name}");
		}
		
		public override Task BanIp(string ip, string reason) {
			return m_Client.ExecuteCommandAsync($"ban-ip {ip} {reason}");
		}
		
		public override Task UnbanIp(string ip) {
			return m_Client.ExecuteCommandAsync($"pardon-ip {ip}");
		}
	}
}
