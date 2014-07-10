using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesGetTemplate1
{
	public partial class IndicesGetTemplate1YamlTests
	{	
	
		public class IndicesGetTemplate110BasicYamlBase : YamlTestsBase
		{
			public IndicesGetTemplate110BasicYamlBase() : base()
			{	

				//do indices.put_template 
				_body = new {
					template= "test-*",
					settings= new {
						number_of_shards= "1",
						number_of_replicas= "0"
					}
				};
				this.Do(()=> _client.IndicesPutTemplateForAll("test", _body));

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetTemplate2Tests : IndicesGetTemplate110BasicYamlBase
		{
			[Test]
			public void GetTemplate2Test()
			{	

				//do indices.get_template 
				this.Do(()=> _client.IndicesGetTemplateForAll("test"));

				//match _response.test.template: 
				this.IsMatch(_response.test.template, @"test-*");

				//match _response.test.settings: 
				this.IsMatch(_response.test.settings, new Dictionary<string, object> {
					{ @"index.number_of_shards", @"1" },
					{ @"index.number_of_replicas", @"0" }
				});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetAllTemplates3Tests : IndicesGetTemplate110BasicYamlBase
		{
			[Test]
			public void GetAllTemplates3Test()
			{	

				//do indices.put_template 
				_body = new {
					template= "test2-*",
					settings= new {
						number_of_shards= "1"
					}
				};
				this.Do(()=> _client.IndicesPutTemplateForAll("test2", _body));

				//do indices.get_template 
				this.Do(()=> _client.IndicesGetTemplateForAll());

				//match _response.test.template: 
				this.IsMatch(_response.test.template, @"test-*");

				//match _response.test2.template: 
				this.IsMatch(_response.test2.template, @"test2-*");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetTemplateWithLocalFlag4Tests : IndicesGetTemplate110BasicYamlBase
		{
			[Test]
			public void GetTemplateWithLocalFlag4Test()
			{	

				//do indices.get_template 
				this.Do(()=> _client.IndicesGetTemplateForAll("test", nv=>nv
					.AddQueryString("local", @"true")
				));

				//is_true _response.test; 
				this.IsTrue(_response.test);

			}
		}
	}
}

