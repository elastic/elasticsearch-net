using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesPutTemplate
{
	public partial class IndicesPutTemplateTests
	{	


		public class PutTemplateTests : YamlTestsBase
		{
			[Test]
			public void PutTemplateTest()
			{	

				//do indices.put_template 
				_body = new {
					template= "test-*",
					settings= new {
						number_of_shards= "1",
						number_of_replicas= "0"
					}
				};
				this.Do(()=> this._client.IndicesPutTemplatePost("test", _body));

				//is_true _response.ok; 
				this.IsTrue(_response.ok);

				//do indices.get_template 
				this.Do(()=> this._client.IndicesGetTemplate("test"));

				//match _response.test.template: 
				this.IsMatch(_response.test.template, @"test-*");

				//match _response.test.settings: 
				this.IsMatch(_response.test.settings, new Dictionary<string, object> {
					 { "index.number_of_shards",  "1" },
					 { "index.number_of_replicas",  "0" }
				});

			}
		}
	}
}

