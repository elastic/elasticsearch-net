using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Mlt
{
	public partial class MltTests
	{	


		public class BasicMltTests : YamlTestsBase
		{
			[Test]
			public void BasicMltTest()
			{	

				//do index 
				_body = new {
					foo= "bar",
					title= "howdy"
				};
				this.Do(()=> this._client.IndexPost("test_1", "test", "1", _body));

				//do indices.refresh 
				this.Do(()=> this._client.IndicesRefreshGet());

				//do cluster.health 
				this.Do(()=> this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status","green")
					.Add("timeout","1s")
				));

				//do mlt 
				this.Do(()=> this._client.MltGet("test_1", "test", "1", nv=>nv
					.Add("mlt_fields","title")
				));

				//match _response.hits.total: 
				this.IsMatch(_response.hits.total, 0);

			}
		}
	}
}

