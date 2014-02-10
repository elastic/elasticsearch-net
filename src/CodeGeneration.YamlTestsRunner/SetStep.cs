namespace CodeGeneration.YamlTestsRunner
{
	public class SetStep : ITestStep
	{
		public string Type { get { return "set"; }}

		public string VariableName { get; set; }
		public string ResponseValue { get; set; }
	}

	public class IsTrueStep : ITestStep
	{
		public string Type { get { return "is_true"; } }
		public string ResponseValue { get; set; }
	}
	
	public class IsFalseStep : ITestStep
	{
		public string Type { get { return "is_false"; } }
		public string ResponseValue { get; set; }
	}
}