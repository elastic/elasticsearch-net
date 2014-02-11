using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesPutAlias
{
	public partial class IndicesPutAliasTests
	{	


		public class BasicTestForPutAliasTests : YamlTestsBase
		{
			[Test]
			public void BasicTestForPutAliasTest()
			{	

				//do indices.create 
				_status = this._client.IndicesCreatePost("test_index", null);
				_response = _status.Deserialize<dynamic>();

				//do indices.exists_alias 
				_status = this._client.IndicesExistsAliasHead("test_alias");
				_response = _status.Deserialize<dynamic>();

				//is_false ; 
				this.IsFalse(_response);

				//do indices.put_alias 
				_status = this._client.IndicesPutAlias("test_alias", null, nv=>nv
					.Add("index","test_index")
				);
				_response = _status.Deserialize<dynamic>();

				//is_true .ok; 
				this.IsTrue(_response.ok);

				//do indices.exists_alias 
				_status = this._client.IndicesExistsAliasHead("test_alias");
				_response = _status.Deserialize<dynamic>();

				//is_true ; 
				this.IsTrue(_response);

				//do indices.get_alias 
				_status = this._client.IndicesGetAlias("test_alias");
				_response = _status.Deserialize<dynamic>();

			}
		}
	}
}

