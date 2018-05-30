using System.IO;
using System.Text;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Framework;

namespace Tests.Search.Request
{
	public class IndicesBoostSerializationTests
	{
		[U] public void CanDeserializeArrayFormat()
		{
			var json = "{\"indices_boost\": [{\"project\":1.4},{\"devs\":1.3}]}";

			using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
			{
				var searchRequest = TestClient.Default.RequestResponseSerializer.Deserialize<SearchRequest>(stream);

				searchRequest.Should().NotBeNull();
				searchRequest.IndicesBoost.Should().NotBeNull().And.ContainKeys((IndexName) "project", (IndexName) "devs");
			}
		}

		[U] public void CanDeserializeObjectFormat()
		{
			var json = "{\"indices_boost\": {\"project\":1.4,\"devs\":1.3}}";

			using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
			{
				var searchRequest = TestClient.Default.RequestResponseSerializer.Deserialize<SearchRequest>(stream);

				searchRequest.Should().NotBeNull();
				searchRequest.IndicesBoost.Should().NotBeNull().And.ContainKeys((IndexName) "project", (IndexName) "devs");
			}
		}
	}
}
