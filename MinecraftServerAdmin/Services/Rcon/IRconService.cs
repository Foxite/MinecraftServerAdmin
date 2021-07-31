using System.Threading.Tasks;

namespace MinecraftServerAdmin.Services.Rcon {
	public interface IRconService {
		public Task<string> ExecuteCommandAsync(string command);
	}
}
