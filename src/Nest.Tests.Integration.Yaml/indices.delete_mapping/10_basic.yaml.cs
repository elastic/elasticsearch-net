using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesDeleteMapping
{
	public partial class IndicesDeleteMappingTests
	{	


		public class DeleteMappingTestsTests : YamlTestsBase
		{
			[Test]
			public void DeleteMappingTestsTest()
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
				_status = this._client.IndicesCreatePost("test_index", _body);
				_response = _status.Deserialize<dynamic>();

				//do indices.exists_type 
				_status = this._client.IndicesExistsTypeHead("test_index", "test_type");
				_response = _status.Deserialize<dynamic>();

				//is_true ; 
				this.IsTrue(_response);

				//do indices.delete_mapping 
				_status = this._client.IndicesDeleteMapping("test_index", "test_type");
				_response = _status.Deserialize<dynamic>();

				//do indices.exists_type 
				_status = this._client.IndicesExistsTypeHead("test_index", "test_type");
				_response = _status.Deserialize<dynamic>();

				//is_false ; 
				this.IsFalse(_response);

			}
		}
	}
}

