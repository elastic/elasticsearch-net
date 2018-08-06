using Elastic.Xunit.XunitPlumbing;

namespace Tests.Framework
{
	public class IntegrationOnlyAttribute : SkipTestAttributeBase
	{
		public override bool Skip => TestConfiguration.Instance.RunUnitTests;
		public override string Reason { get; } = "Inherited unit tests are ignored on this integration test class";
	}
}