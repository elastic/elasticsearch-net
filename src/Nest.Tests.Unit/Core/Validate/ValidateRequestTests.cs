using System;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.Core.Validate
{
	[TestFixture]
	public class ValidateRequestTests : BaseJsonTests
	{

		[Test]
		public void Inferred()
		{
			var result = this._client.Validate<ElasticSearchProject>(vq=>vq
				.Query(q=>q
					.MatchAll()	
				)
			);
			Assert.NotNull(result, "PutWarmer result should not be null");
			var status = result.ConnectionStatus;
			var uri = new Uri(status.RequestUrl);
			Assert.AreEqual("/nest_test_data/elasticsearchprojects/_validate/query", uri.AbsolutePath);
			StringAssert.AreEqualIgnoringCase("POST", status.RequestMethod);
		}

		[Test]
		public void AllIndices()
		{
			var result = this._client.Validate<ElasticSearchProject>(vq=>vq
				.AllIndices()
				.Query(q=>q
					.MatchAll()
				)
			);
			Assert.NotNull(result, "PutWarmer result should not be null");
			var status = result.ConnectionStatus;
			var uri = new Uri(status.RequestUrl);
			Assert.AreEqual("/_validate/query", uri.AbsolutePath);
			StringAssert.AreEqualIgnoringCase("POST", status.RequestMethod);
		}

		[Test]
		public void CustomIndex()
		{
			var result = this._client.Validate<ElasticSearchProject>(vq=>vq
				.Index("myindex")
				.AllTypes()
				.Query(q=>q
					.MatchAll()
				)
			);
			Assert.NotNull(result, "PutWarmer result should not be null");
			var status = result.ConnectionStatus;
			var uri = new Uri(status.RequestUrl);
			Assert.AreEqual("/myindex/_validate/query", uri.AbsolutePath);
			StringAssert.AreEqualIgnoringCase("POST", status.RequestMethod);
		}
		[Test]
		public void CustomIndexAndType()
		{
			var result = this._client.Validate<ElasticSearchProject>(vq=>vq
				.Index("myindex")
				.Type("mytype")
				.Query(q=>q
					.MatchAll()
				)
			);
			Assert.NotNull(result, "PutWarmer result should not be null");
			var status = result.ConnectionStatus;
			var uri = new Uri(status.RequestUrl);
			Assert.AreEqual("/myindex/mytype/_validate/query", uri.AbsolutePath);
			StringAssert.AreEqualIgnoringCase("POST", status.RequestMethod);
		}

		[Test]
		public void InferredForceQueryString()
		{
			var result = this._client.Validate<ElasticSearchProject>(vq=>vq
				.Source("{ match_all : {} }")
			);
			Assert.NotNull(result, "PutWarmer result should not be null");
			var status = result.ConnectionStatus;
			var uri = new Uri(status.RequestUrl);
			Assert.AreEqual("/nest_test_data/elasticsearchprojects/_validate/query", uri.AbsolutePath);
			Assert.AreEqual("?source=%7B%20match_all%20:%20%7B%7D%20%7D", uri.Query);
			StringAssert.AreEqualIgnoringCase("GET", status.RequestMethod);
		}
	}
}
