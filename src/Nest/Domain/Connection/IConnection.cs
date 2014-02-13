using System;
using System.Threading.Tasks;

namespace Nest
{
	public interface IConnection
	{
		Task<ConnectionStatus> Get(string path);
		ConnectionStatus GetSync(string path);

		Task<ConnectionStatus> Head(string path);
		ConnectionStatus HeadSync(string path);

		Task<ConnectionStatus> Post(string path, byte[] data);
		ConnectionStatus PostSync(string path, byte[] data);

		Task<ConnectionStatus> Put(string path, byte[] data);
		ConnectionStatus PutSync(string path, byte[] data);

		Task<ConnectionStatus> Delete(string path);
		ConnectionStatus DeleteSync(string path);

		Task<ConnectionStatus> Delete(string path, byte[] data);
		ConnectionStatus DeleteSync(string path, byte[] data);
	}
}
