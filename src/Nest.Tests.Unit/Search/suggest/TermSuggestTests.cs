using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Bool
{
	[TestFixture]
	public class TermSuggestTests : BaseJsonTests
	{
		[Test]
        public void TermSuggestDescriptorTest()
		{
		    var termSuggestDescriptor = new TermSuggestDescriptor<ElasticSearchProject>().MaxEdits(3).MaxInspections(17).OnField("field1");
            var json = TestElasticClient.Serialize(termSuggestDescriptor);

            var expected = @"{
                              ""max_edits"": 3,
                              ""max_inspections"": 17,
                              ""field"": ""field1""
                            }";

            Assert.True(json.JsonEquals(expected), json);
        }


	} 
}
