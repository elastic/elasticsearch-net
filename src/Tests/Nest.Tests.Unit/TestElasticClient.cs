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
			Settings = new ConnectionSettings(Test.Default.Uri,"mydefaultindex");
			Client = new ElasticClient(Settings);
		}
		public static string Serialize<T>(T obj)
		{
			return Client.Serializer.Serialize(obj);
		}
	}
}
