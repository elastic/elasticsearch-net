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
				_status = this._client.PingHead();
				_response = _status.Deserialize<dynamic>();

			}
		}


		public class BasicTestTests : IndicesAnalyze10AnalyzeYamlBase
		{
			[Test]
			public void BasicTestTest()
			{	

				//do indices.analyze 
				_status = this._client.IndicesAnalyzeGet(nv=>nv
					.Add("text","Foo Bar")
				);
				_response = _status.Deserialize<dynamic>();

				//length tokens: 0; 
				this.IsLength(_response.tokens, 0);

			}

		}

		public class TokenizerAndFilterTests : IndicesAnalyze10AnalyzeYamlBase
		{
			[Test]
			public void TokenizerAndFilterTest()
			{	

				//do indices.analyze 
				_status = this._client.IndicesAnalyzeGet(nv=>nv
					.Add("filters","lowercase")
					.Add("text","Foo Bar")
					.Add("tokenizer","keyword")
				);
				_response = _status.Deserialize<dynamic>();

				//length tokens: 0; 
				this.IsLength(_response.tokens, 0);

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

				//length tokens: 0; 
				this.IsLength(_response.tokens, 0);

			}
		}
	}
}

