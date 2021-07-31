using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RconSharp;

namespace MinecraftServerAdmin.Services.Rcon {
	public class RconSharpRconService : IRconService {
		private readonly SemaphoreSlim m_Lock;
		private readonly RconClient m_Client;
		private readonly RconConfig m_Config;
		private readonly ILogger<RconSharpRconService> m_Logger;

		public RconSharpRconService(IOptions<RconConfig> configAccessor, ILogger<RconSharpRconService> logger) {
			m_Config = configAccessor.Value ?? throw new ArgumentException(nameof(RconConfig) + " has not been configured.", nameof(configAccessor));
			m_Client = RconClient.Create(m_Config.Host, m_Config.Port);
			m_Logger = logger;
			m_Lock = new SemaphoreSlim(1);
			
			ConnectAsync().GetAwaiter().GetResult();
		}

		private async Task ConnectAsync() {
			await m_Client.ConnectAsync();
			await m_Client.AuthenticateAsync(m_Config.Password);
		}

		public async Task<string> ExecuteCommandAsync(string command) {
			await m_Lock.WaitAsync();
			try {
				return await m_Client.ExecuteCommandAsync(command);
			} catch (SocketException ex) {
				m_Logger.LogError(ex, "Socket exception, attempting to reconnect");
				await ConnectAsync();
				// TODO Check if this actually works
				// I wrote this when I was still using ReaderWriterLock, which supports recursion.
				// However it quickly became apparent that RWL does not work when you `await` in between entering and exiting,
				//  because the thread that enters is not the same as the one that exits.
				// SemaphoreSlim is "await-aware" in that you can `await` in between entering and exiting and it totally works.
				// However, the documentation seems to make no mention of this pattern being supported.
				// If it is not supported we may have to go back to using a goto
				return await ExecuteCommandAsync(command);
			} finally {
				m_Lock.Release();
			}
		}
	}
}
