using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Tests.Configuration;

namespace Tests.Core.Xunit
{
	public class IntegrationOnlyAttribute : SkipTestAttributeBase
	{
		public override string Reason { get; } = "Inherited unit tests are ignored on this integration test class";
		public override bool Skip => TestConfiguration.Instance.RunUnitTests;
	}
}
