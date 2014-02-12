namespace CodeGeneration.YamlTestsRunner.Domain
{
	public class IsTrueStep : ITestStep
	{
		public string Type { get { return "is_true"; } }
		public string ResponseValue { get; set; }
	}
}