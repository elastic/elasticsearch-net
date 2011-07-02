using System;

namespace ElasticSearch.Client
{
	internal interface IConnection
	{
		void Get(string path, Action<ConnectionStatus> callback);
		ConnectionStatus GetSync(string path);

		void Post(string path, string data, Action<ConnectionStatus> callback);
		ConnectionStatus PostSync(string path, string data);

	    void Delete(string path, Action<ConnectionStatus> callback);
	    ConnectionStatus DeleteSync(string path);
	}
}
