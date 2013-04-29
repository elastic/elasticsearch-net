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
				host = "localhost.fiddler";

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
	}
}
