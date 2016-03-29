using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Framework.Configuration
{
	public abstract class TestConfigurationBase : ITestConfiguration
	{
		public abstract bool DoNotSpawnIfAlreadyRunning { get; protected set; }
		public abstract string ElasticsearchVersion { get; protected set; }
		public abstract bool ForceReseed { get; protected set; } 
		public abstract TestMode Mode { get; protected set; }

		public virtual bool RunIntegrationTests => Mode == TestMode.Mixed || Mode == TestMode.Integration;
		public virtual bool RunUnitTests => Mode == TestMode.Mixed || Mode == TestMode.Unit;
	}
}
