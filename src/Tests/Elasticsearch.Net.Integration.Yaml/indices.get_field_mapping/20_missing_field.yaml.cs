using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesGetFieldMapping2
{
	public partial class IndicesGetFieldMapping2YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class ReturnEmptyObjectIfFieldDoesntExistButTypeAndIndexDo1Tests : YamlTestsBase
		{
			[Test]
			public void ReturnEmptyObjectIfFieldDoesntExistButTypeAndIndexDo1Test()
			{	

				//do indices.create 
				_body = new {
					mappings= new {
						test_type= new {
							properties= new {
								text= new {
									type= "string",
									analyzer= "whitespace"
								}
							}
						}
					}
				};
				this.Do(()=> _client.IndicesCreate("test_index", _body));

				//do indices.get_field_mapping 
				this.Do(()=> _client.IndicesGetFieldMapping("test_index", "test_type", "not_existent"));

				//match this._status: 
				this.IsMatch(this._status, new {});

			}
		}
	}
}

