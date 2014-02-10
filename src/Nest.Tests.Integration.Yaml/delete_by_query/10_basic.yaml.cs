using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.DeleteByQuery
{
	public partial class DeleteByQuery10BasicYaml10Tests
	{
		
		public class BasicDeleteByQuery10Tests
		{
			private readonly RawElasticClient _client;
		
			public BasicDeleteByQuery10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void BasicDeleteByQueryTests()
			{

				//do index 
				this._client.IndexPost("test_1", "test", "1", "SERIALIZED BODY HERE", nv=>nv);

				//do index 
				this._client.IndexPost("test_1", "test", "2", "SERIALIZED BODY HERE", nv=>nv);

				//do index 
				this._client.IndexPost("test_1", "test", "3", "SERIALIZED BODY HERE", nv=>nv);

				//do indices.refresh 
				this._client.IndicesRefreshPost(nv=>nv);

				//do delete_by_query 
				this._client.DeleteByQuery("test_1", "SERIALIZED BODY HERE", nv=>nv);

				//do indices.refresh 
				this._client.IndicesRefreshPost(nv=>nv);

				//do count 
				this._client.CountPost("test_1", null, nv=>nv);
			}
		}
	}
}