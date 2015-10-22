using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Framework.Configuration
{
	public class LocalConfiguration
	{
		public bool IntegrationOverride { get; private set; } = false;
		public string ManualOverrideVersion { get; private set; } = "2.0.0-rc1";

		public LocalConfiguration(string configurationFile)
		{
			if (!File.Exists(configurationFile)) return;

			var config = File.ReadAllLines(configurationFile)
				.ToDictionary(l => l.Split(':')[0], l => l.Split(':')[1]);

			this.IntegrationOverride = bool.Parse(config["integration_override"]);
			this.ManualOverrideVersion = config["override_version"];
		}
	}
}
