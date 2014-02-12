namespace CodeGeneration.YamlTestsRunner.Domain
{
	public class LengthStep : ITestStep
	{
		public string Type { get { return "length"; }}

		public int Value { get; set; }
		public string ResponseValue { get; set; }
	}
}