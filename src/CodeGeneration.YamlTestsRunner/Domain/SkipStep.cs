namespace CodeGeneration.YamlTestsRunner.Domain
{
	public class SkipStep : ITestStep
	{
		public string Type { get { return "skip"; }}

		public string Version { get; set; }
		public string Reason { get; set; }
	}
}