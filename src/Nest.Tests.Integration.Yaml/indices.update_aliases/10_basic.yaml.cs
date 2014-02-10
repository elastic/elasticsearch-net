using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.IndicesUpdateAliases
{
	public partial class IndicesUpdateAliases10BasicYaml10Tests
	{
		
		public class BasicTestForAliases10Tests
		{
			private readonly RawElasticClient _client;
		
			public BasicTestForAliases10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void BasicTestForAliasesTests()
			{

				//do indices.create 
				this._client.IndicesCreatePost("test_index", null, nv=>nv);

				//do indices.exists_alias 
				this._client.IndicesExistsAliasHead("test_alias", nv=>nv);

				//do indices.update_aliases 
				this._client.IndicesUpdateAliasesPost("SERIALIZED BODY HERE", nv=>nv);

				//do indices.exists_alias 
				this._client.IndicesExistsAliasHead("test_alias", nv=>nv);

				//do indices.get_aliases 
				this._client.IndicesGetAliases("test_index", nv=>nv);
			}
		}
	}
}