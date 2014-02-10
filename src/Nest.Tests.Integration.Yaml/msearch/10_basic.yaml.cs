using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.Msearch
{
	public partial class Msearch10BasicYaml10Tests
	{
		
		public class BasicMultiSearch10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public BasicMultiSearch10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void BasicMultiSearchTests()
			{

				//do index 
				_body = new {
					foo= "bar"
				};
				this._client.IndexPost("test_1", "test", "1", _body, nv=>nv);

				//do index 
				_body = new {
					foo= "baz"
				};
				this._client.IndexPost("test_1", "test", "2", _body, nv=>nv);

				//do index 
				_body = new {
					foo= "foo"
				};
				this._client.IndexPost("test_1", "test", "3", _body, nv=>nv);

				//do indices.refresh 
				
				this._client.IndicesRefreshGet(nv=>nv);

				//do msearch 
				_body = @"{""index"":""test_1""}
{""query"":{""match_all"":{}}}
{""index"":""test_2""}
{""query"":{""match_all"":{}}}
{""search_type"":""count"",""index"":""test_1""}
{""query"":{""match"":{""foo"":""bar""}}}";
				this._client.MsearchPost(_body, nv=>nv);
			}
		}
	}
}
