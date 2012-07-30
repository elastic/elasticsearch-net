using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Integration
{
	[TestFixture]
	public class UpdateIntegrationTests : BaseElasticSearchTests
	{
		[Test]
		public void TestUpdate()
		{
			this.ResetIndexes();
			var project = this.ConnectedClient.Get<ElasticSearchProject>(1);
			Assert.NotNull(project);
			Assert.Greater(project.LOC, 0);
			var loc = project.LOC;
			this.ConnectedClient.Update<ElasticSearchProject>(u => u
			  .Object(project)
			  .Script("ctx._source.loc += 10")
			  .RetriesOnConflict(5)
			  .Refresh()
			);
			project = this.ConnectedClient.Get<ElasticSearchProject>(1);
			Assert.AreEqual(project.LOC, loc + 10);
			Assert.AreNotEqual(project.Version, "1");
		}
	}
}
