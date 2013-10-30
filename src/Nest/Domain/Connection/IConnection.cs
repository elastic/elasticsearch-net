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

		Task<ConnectionStatus> Post(string path, string data);
		ConnectionStatus PostSync(string path, string data);

		Task<ConnectionStatus> Put(string path, string data);
		ConnectionStatus PutSync(string path, string data);

		Task<ConnectionStatus> Delete(string path);
		ConnectionStatus DeleteSync(string path);

		Task<ConnectionStatus> Delete(string path, string data);
		ConnectionStatus DeleteSync(string path, string data);
	}
}
