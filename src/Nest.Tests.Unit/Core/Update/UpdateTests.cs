using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Core.Update
{
	[TestFixture]
	public class UpdateTests
	{
		[Test]
		public void TestUpdate()
		{
			var s = new UpdateDescriptor<ElasticSearchProject>()
			  .Script("ctx._source.counter += count")
			  .Params(p => p
				  .Add("count", 4)
			  );
			var json = TestElasticClient.Serialize(s);
			var expected = @"  {
	      script: ""ctx._source.counter += count"",
	      params: {
	        count: 4
	      }
	    }";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
