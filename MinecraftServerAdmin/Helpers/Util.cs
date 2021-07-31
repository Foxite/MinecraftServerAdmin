using System;
using System.Threading;

namespace MinecraftServerAdmin.Helpers {
	/*
	public static class Util {
		public static IDisposable EnterRead2(this ReaderWriterLock @lock) {
			return new Lock(@lock, false);
		}
		
		public static IDisposable EnterWrite2(this ReaderWriterLock @lock) {
			return new Lock(@lock, true);
		}
		
		private sealed class Lock : IDisposable {
			private readonly ReaderWriterLock m_Lock;
			private readonly bool m_IsWriteLock;
			
			public Lock(ReaderWriterLock @lock, bool isWriteLock) {
				m_Lock = @lock;
				m_IsWriteLock = isWriteLock;
				
				if (isWriteLock) {
					Console.WriteLine("Enter write");
					m_Lock.AcquireWriterLock(TimeSpan.Zero);
				} else {
					Console.WriteLine("Enter read");
					m_Lock.AcquireReaderLock(TimeSpan.Zero);
				}
			}

			public void Dispose() {
				if (m_IsWriteLock) {
					Console.WriteLine("Exit write");
					m_Lock.ReleaseWriterLock();
				} else {
					Console.WriteLine("Exit read");
					m_Lock.ReleaseReaderLock();
				}
			}
		}
	}//*/
}
