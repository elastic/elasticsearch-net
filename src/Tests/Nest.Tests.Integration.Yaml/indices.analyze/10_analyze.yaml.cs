using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesAnalyze1
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
					.Add("text", @"Foo Bar")
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
					.Add("filters", @"lowercase")
					.Add("text", @"Foo Bar")
					.Add("tokenizer", @"keyword")
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
					.Add("wait_for_status", @"yellow")
				));

				//do indices.analyze 
				this.Do(()=> _client.IndicesAnalyzeGet("test", nv=>nv
					.Add("field", @"text")
					.Add("text", @"Foo Bar!")
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

