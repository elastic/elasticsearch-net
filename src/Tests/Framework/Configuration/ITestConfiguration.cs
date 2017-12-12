using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework.Versions;

namespace Tests.Framework.Configuration
{
	public interface ITestConfiguration
	{
		int Seed { get; }
		TestMode Mode { get; }
		ElasticsearchVersion ElasticsearchVersion { get; }
		string ClusterFilter { get; }
		string TestFilter { get; }
		bool ForceReseed { get; }
		bool TestAgainstAlreadyRunningElasticsearch { get; }

		bool RunIntegrationTests { get; }
		bool RunUnitTests { get; }
	}

}
