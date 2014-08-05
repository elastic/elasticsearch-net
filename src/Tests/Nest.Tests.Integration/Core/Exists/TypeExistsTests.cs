using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Indices
{
	[TestFixture]
	public class TypeExistsRequests : IntegrationTests
	{
		[Test]
		public void ShouldNotExist()
		{
			var r = this.Client.TypeExists(f=>f.Index("yadadadadadaadada").Type("blah"));
			Assert.False(r.Exists);
			//404 is a valid response in this case
			Assert.True(r.IsValid);
		}

		[Test]
		public void ShouldNotExist_WhenIndexDoesExist()
		{
			var r = this.Client.TypeExists(f=>f.Index<ElasticsearchProject>().Type("blah"));
			Assert.False(r.Exists);
			//404 is a valid response in this case
			Assert.True(r.IsValid);
		}

		[Test]
		public void ShouldExist()
		{
			var r = this.Client.TypeExists(new TypeExistsRequest(ElasticsearchConfiguration.DefaultIndex, "elasticsearchprojects"));
			Assert.True(r.Exists);
			//404 is a valid response in this case
			Assert.True(r.IsValid);
		}
	}
}