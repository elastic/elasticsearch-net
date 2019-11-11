using System;
using CommandLine;

namespace Tests.LongRunning.CommandLineArgs
{

	public abstract class LongRunningAppArgumentsBase
	{
		[Option("profile", Required = false,
			HelpText =
				"Prints the current process id and waits on stdin for confirmation to start allowing profiles/listeners to be attached")]
		public bool ProfileApplication { get; set; }
	}

	public class IndexOptions : LongRunningAppArgumentsBase
	{
		[Option('k', "insecure", Required = false,
			HelpText =
				"Allow unsecured connections to Elasticsearch. Useful when Elasticsearch is running over HTTPS with a self-signed cert. Not recommended for production, fine for demo purposes")]
		public bool AllowInsecure { get; set; }

		[Option('e', "elasticsearch", Required = true, HelpText = "The url to use to connect to Elasticsearch")]
		public Uri ElasticsearchUrl { get; set; }

		[Option('p', "password", Required = false,
			HelpText = "The password to use to connect to Elasticsearch when the cluster has security enabled")]
		public string Password { get; set; }

		[Option('u', "username", Required = false,
			HelpText = "The username to use to connect to Elasticsearch when the cluster has security enabled")]
		public string UserName { get; set; }
	}
}
