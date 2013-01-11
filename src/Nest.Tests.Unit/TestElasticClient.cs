using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest.Tests.Unit
{
	public static class TestElasticClient
	{
		private static ElasticClient client;
		static TestElasticClient()
		{
			client = new ElasticClient(new ConnectionSettings(Test.Default.Uri));
		}
		public static string Serialize<T>(T obj)
		{
			return client.Serialize(obj);
		}
		public static string SerializeCamelCase<T>(T obj)
		{
			return client.SerializeCamelCase(obj);
		}
	}
}
