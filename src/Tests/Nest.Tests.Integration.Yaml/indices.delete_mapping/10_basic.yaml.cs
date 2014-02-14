using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesDeleteMapping1
{
	public partial class IndicesDeleteMapping1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class DeleteMappingTests1Tests : YamlTestsBase
		{
			[Test]
			public void DeleteMappingTests1Test()
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
				this.Do(()=> this._client.IndicesCreatePost("test_index", _body));

				//do indices.exists_type 
				this.Do(()=> this._client.IndicesExistsTypeHead("test_index", "test_type"));

				//is_true this._status; 
				this.IsTrue(this._status);

				//do indices.delete_mapping 
				this.Do(()=> this._client.IndicesDeleteMapping("test_index", "test_type"));

				//do indices.exists_type 
				this.Do(()=> this._client.IndicesExistsTypeHead("test_index", "test_type"));

				//is_false this._status; 
				this.IsFalse(this._status);

			}
		}
	}
}

