namespace CodeGeneration.YamlTestsRunner
{
	public class MatchStep : ITestStep
	{
		public string Type { get { return "match"; }}

		public string RawValue { get; set; }
		public string ResponseValue { get; set; }
	}
	public class LowerThanStep : ITestStep
	{
		public string Type { get { return "lt"; }}

		public int Value { get; set; }
		public string ResponseValue { get; set; }
	}
	public class GreaterThanStep : ITestStep
	{
		public string Type { get { return "gt"; }}

		public int Value { get; set; }
		public string ResponseValue { get; set; }
	}
	public class LengthStep : ITestStep
	{
		public string Type { get { return "length"; }}

		public int Value { get; set; }
		public string ResponseValue { get; set; }
	}
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