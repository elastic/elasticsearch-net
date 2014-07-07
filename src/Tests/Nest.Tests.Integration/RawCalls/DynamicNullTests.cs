using Elasticsearch.Net.Connection;
using FluentAssertions;
using NUnit.Framework;
using Elasticsearch.Net;

namespace Nest.Tests.Integration.RawCalls
{
	[TestFixture]
	public class DynamicNullTests : IntegrationTests
	{
		[Test]
		public void Calling_A_Null_Field_Should_Not_Throw_With_Nests_Serializer()
		{
			var result = this._client.Raw.Search("{ size: 10}");
			var hit = result.Response["hits"]["hits"][0];
			Assert.DoesNotThrow(() => { var x = hit["testfield"]; });
			var exists = false;
			Assert.DoesNotThrow(() => { exists = hit["testfield"] != null; });
			exists.Should().BeFalse();
			var field = hit["testfield"];
		}
		
		[Test]
		public void Calling_A_Null_Field_Should_Not_Throw_With_ElasticsearchNet_Serializer()
		{
			var client = new ElasticsearchClient(new ConnectionConfiguration(ElasticsearchConfiguration.CreateBaseUri()));
			var result = client.Search("_all","elasticsearchprojects","{ size: 10}");
			var hit = result.Response["hits"]["hits"][0];
				
			Assert.DoesNotThrow(() => { var x = hit["testfield"]; });
			var exists = false;
			Assert.DoesNotThrow(() => { exists = hit["testfield"] != null; });
			exists.Should().BeFalse();
			Assert.DoesNotThrow(() => { exists = hit["_index"] != null; });
			exists.Should().BeTrue();

			var source = hit["_source"];
			((object) source).Should().NotBeNull();
			Assert.DoesNotThrow(() => { var x = source["country"]; });
			string field = source["country"];

			field.Should().NotBeNullOrWhiteSpace();
		}
		
	}
}
