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
	
		public class IndicesGetMapping10BasicYamlBase : YamlTestsBase
		{
			public IndicesGetMapping10BasicYamlBase() : base()
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

			}
		}


		public class GetIndexMappingTests : IndicesGetMapping10BasicYamlBase
		{
			[Test]
			public void GetIndexMappingTest()
			{	

				//do indices.get_mapping 
				_status = this._client.IndicesGetMapping("test_index");
				_response = _status.Deserialize<dynamic>();

			}
		}

		public class GetTypeMappingTests : IndicesGetMapping10BasicYamlBase
		{
			[Test]
			public void GetTypeMappingTest()
			{	

				//do indices.get_mapping 
				_status = this._client.IndicesGetMapping("test_index", "test_type");
				_response = _status.Deserialize<dynamic>();

			}
		}
	}
}

