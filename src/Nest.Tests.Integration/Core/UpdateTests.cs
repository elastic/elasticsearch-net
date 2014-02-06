using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Core
{
	[TestFixture]
	public class UpdateIntegrationTests : IntegrationTests
	{
		[Test]
		public void TestUpdate()
		{
			var project = this._client.Source<ElasticSearchProject>(s=>s.Id(1));
			Assert.NotNull(project);
			Assert.Greater(project.LOC, 0);
			var loc = project.LOC;
			this._client.Update<ElasticSearchProject>(u => u
			  .Object(project)
			  .Script("ctx._source.loc += 10")
			  .RetryOnConflict(5)
			  .Refresh()
			);
			project = this._client.Source<ElasticSearchProject>(s=>s.Id(1));
			Assert.AreEqual(project.LOC, loc + 10);
			Assert.AreNotEqual(project.Version, "1");
		}
	}
}
