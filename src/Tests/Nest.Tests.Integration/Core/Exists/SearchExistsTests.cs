using System.Linq;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Core.Exists
{
	[TestFixture]
	public class SearchExistsTests : IntegrationTests
	{
		[Test]
		[SkipVersion("0 - 1.3.9", "Search exists api added in ES 1.4")]
		public void ShouldNotExist_WrongIndex()
		{
			var r = this.Client.SearchExists<ElasticsearchProject>(f=>f.Index("yadadadadadaadada"));
			Assert.False(r.Exists);
			//404 is a valid response in this case
			Assert.True(r.IsValid);
		}

		[Test]
		[SkipVersion("0 - 1.3.9", "Search exists api added in ES 1.4")]
		public void ShouldNotExist_WrongData()
		{
			var lookfor = NestTestData.Data.First().Country + "blah";
			var r = this.Client.SearchExists<ElasticsearchProject>(f=>f.Query(q=>q.Term(p=>p.Country, lookfor)));
			Assert.False(r.Exists);
			//404 is a valid response in this case
			Assert.True(r.IsValid);
		}

		[Test]
		[SkipVersion("0 - 1.3.9", "Search exists api added in ES 1.4")]
		public void ShouldExist()
		{
			var lookfor = NestTestData.Data.First().Country;
			var r = this.Client.SearchExists<ElasticsearchProject>(f=>f.Query(q=>q.Term(p=>p.Country, lookfor)));
			Assert.True(r.Exists);
			//404 is a valid response in this case
			Assert.True(r.IsValid);
		}
	}
}