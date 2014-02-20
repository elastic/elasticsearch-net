using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Mlt1
{
	public partial class Mlt1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class BasicMlt1Tests : YamlTestsBase
		{
			[Test]
			public void BasicMlt1Test()
			{	

				//do index 
				_body = new {
					foo= "bar",
					title= "howdy"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body));

				//do indices.refresh 
				this.Do(()=> _client.IndicesRefreshForAll());

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.Add("wait_for_status", @"green")
					.Add("timeout", @"1s")
				));

				//do mlt 
				this.Do(()=> _client.MltGet("test_1", "test", "1", nv=>nv
					.Add("mlt_fields", @"title")
				));

				//match _response.hits.total: 
				this.IsMatch(_response.hits.total, 0);

			}
		}
	}
}

