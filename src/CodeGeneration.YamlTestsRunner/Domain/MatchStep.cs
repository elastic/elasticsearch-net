namespace CodeGeneration.YamlTestsRunner.Domain
{
	public class MatchStep : ITestStep
	{
		public string Type { get { return "match"; }}

		public string RawValue { get; set; }
		public string ResponseValue { get; set; }
	}
}