using System.IO;
using Tests.Framework.Integration;
using Tests.XPack.Security;

namespace Tests.Framework.ManagedElasticsearch.InstallationTasks
{
	public class EnsureSecurityUsersInDefaultRealmAreAdded : InstallationTaskBase
	{
		public override void Run(NodeConfiguration config, INodeFileSystem fileSystem)
		{
			if (!config.XPackEnabled) return;

			var v = config.ElasticsearchVersion;
			var folder = v.Major >= 5 ? "x-pack" : "shield";
			var plugin = v.Major >= 5 ? "users" : "esusers";

			var pluginBat = Path.Combine(fileSystem.ElasticsearchHome, "bin", folder, plugin) + ".bat";
			foreach (var cred in ShieldInformation.AllUsers)
				this.ExecuteBinary(pluginBat, $"adding user {cred.Username}",$"useradd {cred.Username} -p {cred.Password} -r {cred.Role}");
		}
	}
}