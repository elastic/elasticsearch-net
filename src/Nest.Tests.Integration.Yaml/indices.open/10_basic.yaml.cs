using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesOpen
{
	public partial class IndicesOpenTests
	{	


		public class BasicTestForIndexOpenCloseTests : YamlTestsBase
		{
			[Test]
			public void BasicTestForIndexOpenCloseTest()
			{	

				//do indices.create 
				_status = this._client.IndicesCreatePost("test_index", null);
				_response = _status.Deserialize<dynamic>();

				//do cluster.health 
				_status = this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status","yellow")
				);
				_response = _status.Deserialize<dynamic>();

				//do indices.close 
				_status = this._client.IndicesClosePost("test_index");
				_response = _status.Deserialize<dynamic>();

				//do search 
				_status = this._client.SearchGet("test_index");
				_response = _status.Deserialize<dynamic>();

				//do indices.open 
				_status = this._client.IndicesOpenPost("test_index");
				_response = _status.Deserialize<dynamic>();

				//do cluster.health 
				_status = this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status","yellow")
				);
				_response = _status.Deserialize<dynamic>();

				//do search 
				_status = this._client.SearchGet("test_index");
				_response = _status.Deserialize<dynamic>();

			}
		}
	}
}

