using CommandLine;

namespace ExamplesGenerator
{
	internal class Program
	{
		private static int Main(string[] args) =>
			Parser.Default.ParseArguments<AsciiDocOptions, CSharpOptions>(args)
				.MapResult(
					(AsciiDocOptions opts) => RunAsciiDocAndReturnExitCode(opts),
					(CSharpOptions opts) => RunCSharpAndReturnExitCode(opts),
					errs => 1);

		private static int RunAsciiDocAndReturnExitCode(AsciiDocOptions opts)
		{
			var examples = CSharpParser.ParseImplementedExamples();
			AsciiDocGenerator.GenerateExampleAsciiDoc(examples);
			return 0;
		}

		private static int RunCSharpAndReturnExitCode(CSharpOptions opts)
		{
			var pages = AsciiDocParser.ParsePages(opts.Path);
			CSharpGenerator.GenerateExampleClasses(pages);
			return 0;
		}
	}

	[Verb("asciidoc", HelpText = "Generate asciidoc client example files from the C# Examples classes")]
	public class AsciiDocOptions { }

	[Verb("csharp", HelpText = "Generate C# Examples classes from the asciidoc master reference")]
	public class CSharpOptions
	{
		[Option('p', "path", Required = true, HelpText = "Path to the master reference file")]
		public string Path { get; set; }
	}
}
