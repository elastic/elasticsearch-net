using Nest.Litterateur.Documentation;

namespace Nest.Litterateur
{
	public static class Program
	{

		private static readonly string DefaultTestFolder = @"..\..\..\..\..\src\Tests";
		public static string InputFolder => DefaultTestFolder;

		private static readonly string DefaultDocFolder = @"..\..\..\..\..\docs\asciidoc";
		public static string OutputFolder => DefaultDocFolder;

		static void Main(string[] args) => LitUp.Go(args);

	}
}