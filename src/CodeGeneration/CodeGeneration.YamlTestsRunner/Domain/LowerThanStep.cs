namespace CodeGeneration.YamlTestsRunner.Domain
{
	public class LowerThanStep : ITestStep
	{
		public string Type { get { return "lt"; }}

		public int Value { get; set; }
		public string ResponseValue { get; set; }
	}
}