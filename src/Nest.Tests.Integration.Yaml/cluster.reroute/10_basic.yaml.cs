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
				_status = this._client.ClusterReroutePost(null);
				_response = _status.Deserialize<dynamic>();

				//is_true .ok; 
				this.IsTrue(_response.ok);

			}
		}
	}
}

