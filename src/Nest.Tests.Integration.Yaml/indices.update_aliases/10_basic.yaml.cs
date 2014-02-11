using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesUpdateAliases
{
	public partial class IndicesUpdateAliasesTests
	{	


		public class BasicTestForAliasesTests : YamlTestsBase
		{
			[Test]
			public void BasicTestForAliasesTest()
			{	

				//do indices.create 
				_status = this._client.IndicesCreatePost("test_index", null);
				_response = _status.Deserialize<dynamic>();

				//do indices.exists_alias 
				_status = this._client.IndicesExistsAliasHead("test_alias");
				_response = _status.Deserialize<dynamic>();

				//is_false ; 
				this.IsFalse(_response);

				//do indices.update_aliases 
				_body = new {
					actions= new [] {
						new {
							add= new {
								index= "test_index",
								alias= "test_alias",
								routing= "routing_value"
							}
						}
					}
				};
				_status = this._client.IndicesUpdateAliasesPost(_body);
				_response = _status.Deserialize<dynamic>();

				//is_true .ok; 
				this.IsTrue(_response.ok);

				//do indices.exists_alias 
				_status = this._client.IndicesExistsAliasHead("test_alias");
				_response = _status.Deserialize<dynamic>();

				//is_true ; 
				this.IsTrue(_response);

				//do indices.get_aliases 
				_status = this._client.IndicesGetAliases("test_index");
				_response = _status.Deserialize<dynamic>();

			}
		}
	}
}

