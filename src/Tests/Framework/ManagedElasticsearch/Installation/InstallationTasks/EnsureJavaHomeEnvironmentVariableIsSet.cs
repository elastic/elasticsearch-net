using System;
using Tests.Framework.Integration;

namespace Tests.Framework.ManagedElasticsearch.InstallationTasks
{
	public class EnsureJavaHomeEnvironmentVariableIsSet : InstallationTaskBase
	{
		public override void Run(NodeConfiguration config, INodeFileSystem fileSystem)
		{
#if DOTNETCORE
			var javaHome = Environment.GetEnvironmentVariable("JAVA_HOME");
#else
			var javaHome = Environment.GetEnvironmentVariable("JAVA_HOME", EnvironmentVariableTarget.Machine)
			               ?? Environment.GetEnvironmentVariable("JAVA_HOME", EnvironmentVariableTarget.User);
#endif
			if (string.IsNullOrWhiteSpace(javaHome))
				throw new Exception("The elasticsearch bat files are resillient to JAVA_HOME not being set, however the shield tooling is not");
		}
	}
}