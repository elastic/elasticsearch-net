using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.IndicesPutAlias
{
	public partial class IndicesPutAlias10BasicYaml10Tests
	{
		
		public class BasicTestForPutAlias10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public BasicTestForPutAlias10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void BasicTestForPutAliasTests()
			{

				//do indices.create 
				
				_status = this._client.IndicesCreatePost("test_index", null);
				_response = _status.Deserialize<dynamic>();

				//do indices.exists_alias 
				
				_status = this._client.IndicesExistsAliasHead("test_alias");
				_response = _status.Deserialize<dynamic>();

				//do indices.put_alias 
				
				_status = this._client.IndicesPutAlias("test_alias", null, nv=>nv
					.Add("index","test_index")
				);
				_response = _status.Deserialize<dynamic>();

				//do indices.exists_alias 
				
				_status = this._client.IndicesExistsAliasHead("test_alias");
				_response = _status.Deserialize<dynamic>();

				//do indices.get_alias 
				
				_status = this._client.IndicesGetAlias("test_alias");
				_response = _status.Deserialize<dynamic>();
			}
		}
	}
}
