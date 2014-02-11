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
	public partial class IndicesAnalyzeTests
	{	
	
		public class IndicesAnalyze10AnalyzeYamlBase : YamlTestsBase
		{
			public IndicesAnalyze10AnalyzeYamlBase() : base()
			{	

				//do ping 
				this.Do(()=> this._client.PingHead());

			}
		}


		public class BasicTestTests : IndicesAnalyze10AnalyzeYamlBase
		{
			[Test]
			public void BasicTestTest()
			{	

				//do indices.analyze 
				this.Do(()=> this._client.IndicesAnalyzeGet(nv=>nv
					.Add("text","Foo Bar")
				));

				//length _response.tokens: 2; 
				this.IsLength(_response.tokens, 2);

				//match _response.tokens[0].token: 
				this.IsMatch(_response.tokens[0].token, @"foo");

				//match _response.tokens[1].token: 
				this.IsMatch(_response.tokens[1].token, @"bar");

			}
		}

		public class TokenizerAndFilterTests : IndicesAnalyze10AnalyzeYamlBase
		{
			[Test]
			public void TokenizerAndFilterTest()
			{	

				//do indices.analyze 
				this.Do(()=> this._client.IndicesAnalyzeGet(nv=>nv
					.Add("filters","lowercase")
					.Add("text","Foo Bar")
					.Add("tokenizer","keyword")
				));

				//length _response.tokens: 1; 
				this.IsLength(_response.tokens, 1);

				//match _response.tokens[0].token: 
				this.IsMatch(_response.tokens[0].token, @"foo bar");

			}
		}

		public class IndexAndFieldTests : IndicesAnalyze10AnalyzeYamlBase
		{
			[Test]
			public void IndexAndFieldTest()
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
				this.Do(()=> this._client.IndicesCreatePost("test", _body));

				//do cluster.health 
				this.Do(()=> this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status","yellow")
				));

				//do indices.analyze 
				this.Do(()=> this._client.IndicesAnalyzeGet("test", nv=>nv
					.Add("field","text")
					.Add("text","Foo Bar!")
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

