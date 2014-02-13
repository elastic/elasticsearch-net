using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest.Tests.Unit
{
	public static class TestElasticClient
	{
		public static ElasticClient Client;
		public static ConnectionSettings Settings;
		static TestElasticClient()
		{
			Settings = new ConnectionSettings(Test.Default.Uri,"nest_test_data");
			Client = new ElasticClient(Settings);
		}
		public static string Serialize<T>(T obj) where T : class
		{
			return Encoding.UTF8.GetString(Client.Serializer.Serialize(obj));
		}
	}
}
