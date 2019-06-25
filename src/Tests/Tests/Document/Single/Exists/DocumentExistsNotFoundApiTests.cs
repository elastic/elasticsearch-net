using System.Globalization;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Document.Single.Exists 
{
	public class DocumentExistsNotFoundApiTests : DocumentExistsApiTests
	{
		public DocumentExistsNotFoundApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override string CallIsolatedValue => int.MaxValue.ToString(CultureInfo.InvariantCulture);

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 404;

		protected override void ExpectResponse(ExistsResponse response) { }
	}
}