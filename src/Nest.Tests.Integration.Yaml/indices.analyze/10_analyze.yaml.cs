using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.IndicesAnalyze
{
	public partial class IndicesAnalyze10AnalyzeYaml10Tests
	{
		
		public class Setup10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
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
				
				this._client.PingHead(nv=>nv);
			}
		}
		
		public class BasicTest10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
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
				
				this._client.IndicesAnalyzeGet(nv=>nv);
			}
		}
		
		public class TokenizerAndFilter10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
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
				
				this._client.IndicesAnalyzeGet(nv=>nv);
			}
		}
		
		public class IndexAndField10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
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
				this._client.IndicesCreatePost("test", _body, nv=>nv);

				//do cluster.health 
				
				this._client.ClusterHealthGet(nv=>nv);

				//do indices.analyze 
				
				this._client.IndicesAnalyzeGet("test", nv=>nv);
			}
		}
	}
}
