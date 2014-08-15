using System;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.Core.Explain
{
	[TestFixture]
	public class ExplainRequestTests : BaseJsonTests
	{

		[Test]
		public void Inferred()
		{
			var result = this._client.Explain<ElasticsearchProject>(vq=>vq
				.Id(1)
				.Query(q=>q
					.MatchAll()	
				)
			);
			Assert.NotNull(result, "PutWarmer result should not be null");
			var status = result.ConnectionStatus;
			var uri = new Uri(status.RequestUrl);
			Assert.AreEqual("/nest_test_data/elasticsearchprojects/1/_explain", uri.AbsolutePath);
			StringAssert.AreEqualIgnoringCase("POST", status.RequestMethod);
		}

		[Test]
		public void InferredForceQueryString()
		{
			var result = this._client.Explain<ElasticsearchProject>(vq=>vq
				.Id(1)
				.Q("searchme")
			);
			var status = result.ConnectionStatus;
			var uri = new Uri(status.RequestUrl);
			Assert.AreEqual("/nest_test_data/elasticsearchprojects/1/_explain", uri.AbsolutePath);

			//normalize difference between .NET 4.5 and prior
			var query = uri.Query.Replace("%3A", ":");
			Assert.AreEqual("?q=searchme", query);
			StringAssert.AreEqualIgnoringCase("GET", status.RequestMethod);
		}
	}
}
