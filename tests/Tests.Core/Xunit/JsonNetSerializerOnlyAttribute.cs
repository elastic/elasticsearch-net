using Elastic.Xunit.XunitPlumbing;
using Tests.Core.Client;

namespace Tests.Core.Xunit
{
	public class JsonNetSerializerOnlyAttribute : SkipTestAttributeBase
	{
		public override string Reason { get; } = "Skipping this test because we are not running with JsonNetSerializer";
		public override bool Skip => !TestClient.Configuration.Random.SourceSerializer;
	}
}
