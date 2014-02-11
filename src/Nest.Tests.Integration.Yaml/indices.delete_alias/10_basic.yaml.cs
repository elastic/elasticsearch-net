using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesDeleteAlias
{
	public partial class IndicesDeleteAliasTests
	{	


		public class BasicTestForDeleteAliasTests : YamlTestsBase
		{
			[Test]
			public void BasicTestForDeleteAliasTest()
			{	

				//do indices.create 
				_status = this._client.IndicesCreatePost("testind", null);
				_response = _status.Deserialize<dynamic>();

				//do indices.put_alias 
				_body = new {
					routing= "routing value"
				};
				_status = this._client.IndicesPutAlias("testali", _body, nv=>nv
					.Add("index","testind")
				);
				_response = _status.Deserialize<dynamic>();

				//do indices.get_alias 
				_status = this._client.IndicesGetAlias("testali");
				_response = _status.Deserialize<dynamic>();

				//do indices.delete_alias 
				_status = this._client.IndicesDeleteAlias("testind", "testali");
				_response = _status.Deserialize<dynamic>();

				//do indices.get_alias 
				_status = this._client.IndicesGetAlias("testind", "testali");
				_response = _status.Deserialize<dynamic>();

			}
		}
	}
}

