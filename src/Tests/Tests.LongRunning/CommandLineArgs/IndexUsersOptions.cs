using CommandLine;

namespace Tests.LongRunning.CommandLineArgs
{
	[Verb("users", HelpText = "Index Stack Overflow users and their badges into Elasticsearch")]
	public class IndexUsersOptions : IndexOptions
	{
		[Option('b', "badges-path", Required = true, HelpText = "The path to the Stack Overflow Badges.xml file")]
		public string BadgesPath { get; set; }

		[Option('f', "users-path", Required = true, HelpText = "The path to the Stack Overflow Users.xml file")]
		public string UsersPath { get; set; }
	}
}
