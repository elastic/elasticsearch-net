using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class RegexpQueryJson
	{
		[Test]
		public void RegexpQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.Regexp(r => r.OnField(p => p.Name).Value("ab?"))
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ regexp: { name : { value : ""ab?"" } }}}";
			Assert.True(json.JsonEquals(expected), json);
		}
		[Test]
		public void RegexpQueryStatic()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(Query<ElasticSearchProject>.Regexp(r=>r.OnField(p=>p.Name).Value("ab?")));
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ regexp: { name : { value : ""ab?"" } }}}";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void RegexpWithBoost()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.Regexp(r => r.OnField(p => p.Name).Value("ab?").Boost(1.2))
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ regexp: { name : { value : ""ab?"", boost: 1.2 } }}}";
			Assert.True(json.JsonEquals(expected), json);
		}

	}
}
