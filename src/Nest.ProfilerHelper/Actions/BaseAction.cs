using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Nest.ProfilerHelper.Actions
{
	public static class BaseAction
	{
		public static readonly string DefaultIndex = "nest-profiling-" + Process.GetCurrentProcess().Id.ToString();

		public static IConnectionSettings Settings(int? port = null)
		{
			var host = "localhost";
			if (port == null && Process.GetProcessesByName("fiddler").Count() > 0)
				host = "ipv4.fiddler";

			var uri = new UriBuilder("http", host, port.GetValueOrDefault(9200)).Uri;

			return new ConnectionSettings(uri)
				.SetDefaultIndex(BaseAction.DefaultIndex)
				.SetMaximumAsyncConnections(20)
				.UsePrettyResponses();
		}

		public static readonly ElasticClient Client = new ElasticClient(Settings());

		public static string NewUniqueIndexName()
		{
			return "nest-profiling-" + Guid.NewGuid().ToString();
		}

		public static void Setup()
		{
			var client = BaseAction.Client;

			if (client.IndexExists(BaseAction.DefaultIndex).Exists)
				return;

			var projects = NestTestData.Data;
			var people = NestTestData.Session.List<Person>(1000).Get();

			client.CreateIndex(BaseAction.DefaultIndex, c => c
				.NumberOfReplicas(0)
				.NumberOfShards(1)
				.AddMapping<ElasticSearchProject>(m => m.MapFromAttributes())
				.AddMapping<Person>(m => m.MapFromAttributes())
			);
			

			var bulkParameters = new SimpleBulkParameters() { Refresh = true };
			client.IndexMany(projects, bulkParameters);
			client.IndexMany(people, bulkParameters);
			client.Refresh(new[] { BaseAction.DefaultIndex });

		}

		public static void TearDown()
		{
			var client = BaseAction.Client;
			client.DeleteIndex("nest-*");
		}
	}
}
