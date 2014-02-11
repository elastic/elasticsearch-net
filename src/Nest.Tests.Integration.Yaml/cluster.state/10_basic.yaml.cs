using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.ClusterState
{
	public partial class ClusterStateTests
	{	


		public class ClusterStateTestTests : YamlTestsBase
		{
			[Test]
			public void ClusterStateTestTest()
			{	

				//do cluster.state 
				_status = this._client.ClusterStateGet();
				_response = _status.Deserialize<dynamic>();

				//is_true .master_node; 
				this.IsTrue(_response.master_node);

			}
		}
	}
}

