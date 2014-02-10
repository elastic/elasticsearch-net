using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.IndicesDeleteAlias
{
	public partial class IndicesDeleteAlias10BasicYaml10Tests
	{
		
		public class BasicTestForDeleteAlias10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public BasicTestForDeleteAlias10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void BasicTestForDeleteAliasTests()
			{

				//do indices.create 
				
				this._client.IndicesCreatePost("testind", null, nv=>nv);

				//do indices.put_alias 
				_body = new {
					routing= "routing value"
				};
				this._client.IndicesPutAlias("testali", _body, nv=>nv);

				//do indices.get_alias 
				
				this._client.IndicesGetAlias("testali", nv=>nv);

				//do indices.delete_alias 
				
				this._client.IndicesDeleteAlias("testind", "testali", nv=>nv);

				//do indices.get_alias 
				
				this._client.IndicesGetAlias("testind", "testali", nv=>nv);
			}
		}
	}
}
