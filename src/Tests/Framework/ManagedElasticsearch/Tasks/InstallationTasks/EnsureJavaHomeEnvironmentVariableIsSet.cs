using System;
using Tests.Framework.ManagedElasticsearch.Nodes;

namespace Tests.Framework.ManagedElasticsearch.Tasks.InstallationTasks
{
	public class EnsureJavaHomeEnvironmentVariableIsSet : InstallationTaskBase
	{
		public override void Run(NodeConfiguration config, NodeFileSystem fileSystem)
		{
			var javaHome = Environment.GetEnvironmentVariable("JAVA_HOME");
			if (string.IsNullOrWhiteSpace(javaHome))
				throw new Exception("The elasticsearch bat files are resillient to JAVA_HOME not being set, however the shield tooling is not");
		}
	}
}
