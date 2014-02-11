using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesStats
{
	public partial class IndicesStatsTests
	{	


		public class StatsTestTests : YamlTestsBase
		{
			[Test]
			public void StatsTestTest()
			{	

				//do indices.stats 
				_status = this._client.IndicesStatsGet();
				_response = _status.Deserialize<dynamic>();

				//is_true .ok; 
				this.IsTrue(_response.ok);

			}
		}
	}
}

