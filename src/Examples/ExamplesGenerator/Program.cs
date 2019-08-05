using System;

namespace ExamplesGenerator
{
	internal class Program
	{
		private static int Main(string[] args)
		{
			if (args.Length != 1)
			{
				Console.Error.Write("must provide a path to examples file");
				return 1;
			}

			var pages = AsciiDocParser.ParsePages(args[0]);
			CSharpGenerator.GenerateExamples(pages);

			return 0;
		}
	}
}
