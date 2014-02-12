using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Index
{
	[TestFixture]
	public class IndexUsingUrlIdTests : IntegrationTests
	{
		[Test]
		public void IndexUsingAnUrlAsId()
		{
			var id = "http://www.skusuplier.com/products/123?affiliateId=23131#oopsIcopiedAnAnchor";
			var newProduct = new Product
			{
				Id = id,
				Name = "Top Product"
			};
			var response = this._client.Index(newProduct);

			var productInElasticsearch = this._client.Source<Product>(i=>i.Id(id));
			Assert.NotNull(productInElasticsearch);
			Assert.AreEqual(productInElasticsearch.Id, id);
			Assert.True(response.IsValid);
		}


		[Test]
		public void IndexUsingAnUrlAsIdUsingCustomUrlParameterInSettings()
		{
			var settings = ElasticsearchConfiguration.Settings().SetGlobalQueryStringParameters(new NameValueCollection
			{
				{"apiKey", "my-api-key"}
			});
			var client = new ElasticClient(settings);

			var id = "http://www.skusuplier.com/products/123?affiliateId=23131#oopsIcopiedAnAnchor";
			var newProduct = new Product
			{
				Id = id,
				Name = "Top Product"
			};
			var response = client.Index(newProduct);

			var productInElasticsearch = client.Source<Product>(id);
			Assert.NotNull(productInElasticsearch);
			Assert.AreEqual(productInElasticsearch.Id, id);
			Assert.True(response.IsValid);
		}
	}
}
