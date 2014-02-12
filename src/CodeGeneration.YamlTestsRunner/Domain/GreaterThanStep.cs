namespace CodeGeneration.YamlTestsRunner.Domain
{
	public class GreaterThanStep : ITestStep
	{
		public string Type { get { return "gt"; }}

		public int Value { get; set; }
		public string ResponseValue { get; set; }
	}
}