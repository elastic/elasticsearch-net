using System.IO;
using Tests.Framework.Configuration;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Nodes;
using Tests.XPack.Security;

namespace Tests.Framework.ManagedElasticsearch.Tasks.InstallationTasks
{
	public class EnsureSecurityUsersInDefaultRealmAreAdded : InstallationTaskBase
	{
		public override void Run(NodeConfiguration config, NodeFileSystem fileSystem)
		{
			var v = config.ElasticsearchVersion;
			var folder = v.Major >= 5 ? "x-pack" : "shield";
			var plugin = v.Major >= 5 ? "users" : "esusers";

			if (v.Major == 6 && !File.Exists(fileSystem.XPackEnvBinary))
			{
				File.WriteAllText(fileSystem.XPackEnvBinary, "set ES_CLASSPATH=!ES_CLASSPATH!;!ES_HOME!/plugins/x-pack/*");
			}

			var pluginBat = Path.Combine(fileSystem.ElasticsearchHome, "bin", folder, plugin) + BinarySuffix;
			foreach (var cred in ShieldInformation.AllUsers)
				this.ExecuteBinary(pluginBat, $"adding user {cred.Username}",$"useradd {cred.Username} -p {cred.Password} -r {cred.Role}");
		}
	}
}
