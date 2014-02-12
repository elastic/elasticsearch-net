namespace CodeGeneration.YamlTestsRunner.Domain
{
	public class IsFalseStep : ITestStep
	{
		public string Type { get { return "is_false"; } }
		public string ResponseValue { get; set; }
	}
}