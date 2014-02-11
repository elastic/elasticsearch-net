using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesGetMapping
{
	public partial class IndicesGetMappingTests
	{	


		public class Raise404WhenTypeDoesntExistTests : YamlTestsBase
		{
			[Test]
			public void Raise404WhenTypeDoesntExistTest()
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

				//do indices.get_mapping 
				_status = this._client.IndicesGetMapping("test_index", "not_test_type");
				_response = _status.Deserialize<dynamic>();

			}
		}
	}
}

