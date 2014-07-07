using System.Text;

namespace Nest.Tests.Unit
{
	public static class TestElasticClient
	{
		public static ElasticClient Client;
		public static ConnectionSettings Settings;
		static TestElasticClient()
		{
			Settings = new ConnectionSettings(UnitTestDefaults.Uri, UnitTestDefaults.DefaultIndex);

			Client = new ElasticClient(Settings);
		}
		public static string Serialize<T>(T obj) where T : class
		{
			return Encoding.UTF8.GetString(Client.Serializer.Serialize(obj));
		}
	}
}
