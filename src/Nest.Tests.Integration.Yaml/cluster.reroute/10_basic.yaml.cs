using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.ClusterReroute
{
	public partial class ClusterRerouteTests
	{	


		public class BasicSanityCheckTests : YamlTestsBase
		{
			[Test]
			public void BasicSanityCheckTest()
			{	

				//do cluster.reroute 
				this.Do(()=> this._client.ClusterReroutePost(null));

				//is_true _response.ok; 
				this.IsTrue(_response.ok);

			}
		}
	}
}

