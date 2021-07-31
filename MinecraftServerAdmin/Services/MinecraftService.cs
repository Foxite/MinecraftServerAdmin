using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinecraftServerAdmin.Services {
	public abstract class MinecraftService {
		public abstract Task<IReadOnlyCollection<string>> GetOnlinePlayers();
		public abstract Task<IReadOnlyCollection<string>> GetWhitelist();
		public abstract Task<IReadOnlyCollection<(string Name, string Issuer, string Reason)>> GetBanlist();

		public abstract Task AddToWhitelist(string name);
		public abstract Task RemoveFromWhitelist(string name);
		public abstract Task BanPlayer(string name, string reason);
		public abstract Task UnbanPlayer(string name);
		public abstract Task BanIp(string ip, string reason);
		public abstract Task UnbanIp(string ip);

		public abstract Task<string> ExecuteCommand(string command);
	}
}
