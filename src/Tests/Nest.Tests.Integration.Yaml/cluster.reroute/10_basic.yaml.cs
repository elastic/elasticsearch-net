using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.ClusterReroute1
{
	public partial class ClusterReroute1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class BasicSanityCheck1Tests : YamlTestsBase
		{
			[Test]
			public void BasicSanityCheck1Test()
			{	

				//do cluster.reroute 
				this.Do(()=> _client.ClusterReroute(null));

			}
		}
	}
}

