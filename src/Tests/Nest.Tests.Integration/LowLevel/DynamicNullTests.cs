using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using Elasticsearch.Net;

namespace Nest.Tests.Integration.LowLevel
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
			var client = new ElasticsearchClient();
			var result = client.Search("{ size: 10}");
			var hit = result.Response["hits"]["hits"][0];
				
			Assert.DoesNotThrow(() => { var x = hit["testfield"]; });
			var exists = false;
			Assert.DoesNotThrow(() => { exists = hit["testfield"] != null; });
			exists.Should().BeFalse();
			Assert.DoesNotThrow(() => { exists = hit["_index"] != null; });
			exists.Should().BeTrue();

			var source = hit["_source"];
			((object) source).Should().NotBeNull();
			Assert.DoesNotThrow(() => { var x = source["name"]; });
			string field = source["name"];

			field.Should().NotBeNullOrWhiteSpace();
		}
		
	}
}
