namespace CodeGeneration.YamlTestsRunner.Domain
{
	public class SetStep : ITestStep
	{
		public string Type { get { return "set"; }}

		public string VariableName { get; set; }
		public string ResponseValue { get; set; }
	}
}