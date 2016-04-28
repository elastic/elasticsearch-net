using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Framework.Configuration
{
	public interface ITestConfiguration
	{
		TestMode Mode { get; }
		string ElasticsearchVersion { get; }
		bool ForceReseed { get; } 
		bool DoNotSpawnIfAlreadyRunning { get; }

		bool RunIntegrationTests { get; }
		bool RunUnitTests { get; }
	}
}
