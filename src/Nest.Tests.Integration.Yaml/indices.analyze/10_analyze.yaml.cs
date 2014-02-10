using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesAnalyze
{
	public partial class IndicesAnalyze10AnalyzeYaml10Tests
	{
		
		public class Setup10Tests : YamlTestsBase
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public Setup10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void SetupTests()
			{

				//do ping 
				
				_status = this._client.PingHead();
				_response = _status.Deserialize<dynamic>();
			}
		}
		
		public class BasicTest10Tests : YamlTestsBase
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public BasicTest10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void BasicTestTests()
			{

				//do indices.analyze 
				
				_status = this._client.IndicesAnalyzeGet(nv=>nv
					.Add("text","Foo Bar")
				);
				_response = _status.Deserialize<dynamic>();
			}
		}
		
		public class TokenizerAndFilter10Tests : YamlTestsBase
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public TokenizerAndFilter10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void TokenizerAndFilterTests()
			{

				//do indices.analyze 
				
				_status = this._client.IndicesAnalyzeGet(nv=>nv
					.Add("filters","lowercase")
					.Add("text","Foo Bar")
					.Add("tokenizer","keyword")
				);
				_response = _status.Deserialize<dynamic>();
			}
		}
		
		public class IndexAndField10Tests : YamlTestsBase
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public IndexAndField10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void IndexAndFieldTests()
			{

				//do indices.create 
				_body = new {
					mappings= new {
						test= new {
							properties= new {
								text= new {
									type= "string",
									analyzer= "whitespace"
								}
							}
						}
					}
				};
				_status = this._client.IndicesCreatePost("test", _body);
				_response = _status.Deserialize<dynamic>();

				//do cluster.health 
				
				_status = this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status","yellow")
				);
				_response = _status.Deserialize<dynamic>();

				//do indices.analyze 
				
				_status = this._client.IndicesAnalyzeGet("test", nv=>nv
					.Add("field","text")
					.Add("text","Foo Bar!")
				);
				_response = _status.Deserialize<dynamic>();
			}
		}
	}
}
