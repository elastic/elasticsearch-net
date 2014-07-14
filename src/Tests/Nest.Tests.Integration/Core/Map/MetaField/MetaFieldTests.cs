using Elasticsearch.Net;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Core.Map.MetaField
{
	[TestFixture]
	public class MetaFieldTests : BaseMappingTests
	{
		[Test]
		public void MetaFieldSerializes()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
				.Meta(d => d
					.Add("attr1", "value1")
					.Add("attr2", new { attr3 = "value3" })
					.Add("Attr4", 10)
				)
			);
			//make sure Attr4 is not serialized as attr4
			var request = result.ConnectionStatus.Request.Utf8String();
			StringAssert.DoesNotContain("attr4", request, request);
			this.DefaultResponseAssertations(result);
		}
	}
}
