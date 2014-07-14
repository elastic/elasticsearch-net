using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesAnalyze1
{
	public partial class IndicesAnalyze1YamlTests
	{	
	
		public class IndicesAnalyze110AnalyzeYamlBase : YamlTestsBase
		{
			public IndicesAnalyze110AnalyzeYamlBase() : base()
			{	

				//do ping 
				this.Do(()=> _client.Ping());

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class BasicTest2Tests : IndicesAnalyze110AnalyzeYamlBase
		{
			[Test]
			public void BasicTest2Test()
			{	

				//do indices.analyze 
				this.Do(()=> _client.IndicesAnalyzeGetForAll(nv=>nv
					.AddQueryString("text", @"Foo Bar")
				));

				//length _response.tokens: 2; 
				this.IsLength(_response.tokens, 2);

				//match _response.tokens[0].token: 
				this.IsMatch(_response.tokens[0].token, @"foo");

				//match _response.tokens[1].token: 
				this.IsMatch(_response.tokens[1].token, @"bar");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TokenizerAndFilter3Tests : IndicesAnalyze110AnalyzeYamlBase
		{
			[Test]
			public void TokenizerAndFilter3Test()
			{	

				//do indices.analyze 
				this.Do(()=> _client.IndicesAnalyzeGetForAll(nv=>nv
					.AddQueryString("filters", @"lowercase")
					.AddQueryString("text", @"Foo Bar")
					.AddQueryString("tokenizer", @"keyword")
				));

				//length _response.tokens: 1; 
				this.IsLength(_response.tokens, 1);

				//match _response.tokens[0].token: 
				this.IsMatch(_response.tokens[0].token, @"foo bar");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class IndexAndField4Tests : IndicesAnalyze110AnalyzeYamlBase
		{
			[Test]
			public void IndexAndField4Test()
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
				this.Do(()=> _client.IndicesCreate("test", _body));

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.AddQueryString("wait_for_status", @"yellow")
				));

				//do indices.analyze 
				this.Do(()=> _client.IndicesAnalyzeGet("test", nv=>nv
					.AddQueryString("field", @"text")
					.AddQueryString("text", @"Foo Bar!")
				));

				//length _response.tokens: 2; 
				this.IsLength(_response.tokens, 2);

				//match _response.tokens[0].token: 
				this.IsMatch(_response.tokens[0].token, @"Foo");

				//match _response.tokens[1].token: 
				this.IsMatch(_response.tokens[1].token, @"Bar!");

			}
		}
	}
}

