using CommandLine;

namespace Tests.LongRunning.CommandLineArgs
{
	[Verb("tags", HelpText = "Updates Stack Overflow answers with the tags of the question in Elasticsearch")]
	public class UpdateAnswersWithTagsOptions : IndexOptions
	{
		[Option('f', "posts-path", Required = true, HelpText = "The path to the Stack Overflow Posts.xml file")]
		public string PostsPath { get; set; }

		[Option('s', "size", Required = false, HelpText = "The number of updates to perform per batch", Default = 200)]
		public int Size { get; set; }
	}
}
