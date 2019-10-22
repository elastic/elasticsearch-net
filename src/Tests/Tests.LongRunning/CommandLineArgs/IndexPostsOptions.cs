using CommandLine;

namespace Tests.LongRunning.CommandLineArgs
{
	[Verb("posts", HelpText = "Index Stack Overflow posts into Elasticsearch")]
	public class IndexPostsOptions : IndexOptions
	{
		[Option('f', "posts-path", Required = true, HelpText = "The path to the Stack Overflow Posts.xml file")]
		public string PostsPath { get; set; }
	}
}
