using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Delete4
{
	public partial class Delete4YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Routing1Tests : YamlTestsBase
		{
			[Test]
			public void Routing1Test()
			{	

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body, nv=>nv
					.Add("routing", 5)
				));

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.Add("wait_for_status", @"yellow")
				));

				//do delete 
				this.Do(()=> _client.Delete("test_1", "test", "1", nv=>nv
					.Add("routing", 4)
				), shouldCatch: @"missing");

				//do delete 
				this.Do(()=> _client.Delete("test_1", "test", "1", nv=>nv
					.Add("routing", 5)
				));

			}
		}
	}
}

