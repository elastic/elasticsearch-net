using Elastic.Xunit.XunitPlumbing;

namespace Examples {
	public class SkipExampleAttribute : SkipTestAttributeBase
	{
		public override bool Skip => true;

		public override string Reason { get; } = "Example not implemented";
	}
}