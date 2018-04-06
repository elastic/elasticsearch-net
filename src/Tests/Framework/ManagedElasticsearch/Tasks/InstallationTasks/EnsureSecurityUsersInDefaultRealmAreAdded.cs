using System.IO;
using Elastic.Managed.Ephemeral;
using Elastic.Managed.Ephemeral.Tasks;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.XPack.Security;

namespace Tests.Framework.ManagedElasticsearch.Tasks.InstallationTasks
{
	public class EnsureSecurityUsersInDefaultRealmAreAdded : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			if (!(cluster.ClusterConfiguration is XPackClusterConfiguration config)) return;
			var v = cluster.ClusterConfiguration.Version;
			var fileSystem = cluster.FileSystem;
			var folder = v.Major >= 5 ? "x-pack" : "shield";
			var plugin = v.Major >= 5 ? "users" : "esusers";

			if (v.Major == 6 && !File.Exists(config.XPackEnvBinary))
			{
				File.WriteAllText(config.XPackEnvBinary, "set ES_CLASSPATH=!ES_CLASSPATH!;!ES_HOME!/plugins/x-pack/*");
			}

			var pluginBat = Path.Combine(fileSystem.ElasticsearchHome, "bin", folder, plugin) + BinarySuffix;
			foreach (var cred in ShieldInformation.AllUsers)
				ExecuteBinary(cluster.Writer, pluginBat, $"adding user {cred.Username}",$"useradd {cred.Username} -p {cred.Password} -r {cred.Role}");
		}
	}
}
