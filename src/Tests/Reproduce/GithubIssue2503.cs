using System.IO;
using System.Text;
using FluentAssertions;
using Nest;
using Tests.Framework;

namespace Tests.Reproduce
{
	public class GithubIssue2503
	{
		[U]
		public void DeserializeTermsIncludeExcludeValues()
		{
			var json = @"{
						  ""aggs"": {
							""sizes"": {
							  ""terms"": {
								""field"": ""size"",
								""size"": 20,
								""include"": [
								  ""35"",
								  ""50"",
								  ""70"",
								  ""75"",
								  ""100""
								]
							  }
							}
						  }
						}";

			var client = TestClient.GetInMemoryClient();
			SearchRequest request;

			using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
			{
				request = client.Serializer.Deserialize<SearchRequest>(stream);
			}

			request.Should().NotBeNull();
			request.Aggregations.Should().NotBeNull().And.HaveCount(1);
			var termsAggregation = request.Aggregations["sizes"].Terms;
			termsAggregation.Should().NotBeNull();
			termsAggregation.Include.Should().NotBeNull();
			termsAggregation.Include.Values.Should().NotBeNull().And.HaveCount(5);
		}

		[U]
		public void DeserializeTermsIncludeExcludePattern()
		{
			var json = @"{
						  ""aggs"": {
							""sizes"": {
							  ""terms"": {
								""field"": ""size"",
								""size"": 20,
								""include"": {
								  ""pattern"" : ""\\d+""
								}
							  }
							}
						  }
						}";

			var client = TestClient.GetInMemoryClient();
			SearchRequest request;

			using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
			{
				request = client.Serializer.Deserialize<SearchRequest>(stream);
			}

			request.Should().NotBeNull();
			request.Aggregations.Should().NotBeNull().And.HaveCount(1);
			var termsAggregation = request.Aggregations["sizes"].Terms;
			termsAggregation.Should().NotBeNull();
			termsAggregation.Include.Should().NotBeNull();
			termsAggregation.Include.Pattern.Should().NotBeNull();
		}
	}
}
