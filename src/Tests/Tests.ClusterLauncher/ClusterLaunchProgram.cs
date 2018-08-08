using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elastic.Managed.Ephemeral;
using FluentAssertions.Common;
using Tests.Core.ManagedElasticsearch.Clusters;

namespace Tests.ClusterLauncher
{
	public static class ClusterLaunchProgram
	{
		public static int Main(string[] arguments)
		{
			if (arguments.Length < 1)
			{
				Console.Error.WriteLine("cluster command needs atleast one argument to indicate the cluster to start");
				return 3;
			}

			var clusterName = arguments[0];
			if 	(arguments.Length > 1)
				Environment.SetEnvironmentVariable("NEST_INTEGRATION_VERSION", arguments[1], EnvironmentVariableTarget.Process);
			Environment.SetEnvironmentVariable("NEST_INTEGRATION_SHOW_OUTPUT_AFTER_START", "1", EnvironmentVariableTarget.Process);

			var cluster = GetClusters().FirstOrDefault(c => c.Name.StartsWith(clusterName, StringComparison.OrdinalIgnoreCase));
			if (cluster == null)
			{
				Console.Error.WriteLine($"No cluster found that starts with '{clusterName}");
				return 4;
			}

			if (!TryStartClientTestClusterBaseImplementation(cluster) && !TryStartXPackClusterImplementation(cluster))
			{
				Console.Error.WriteLine($"Could not create an instance of '{cluster.FullName}");
				return 1;
			}
			return 0;
		}
		private static bool TryStartXPackClusterImplementation(Type cluster)
		{
			if (!(Activator.CreateInstance(cluster) is XPackCluster instance)) return false;
			using (instance)
			{
				instance.Start();
				Console.WriteLine("Press any key to shutdown the running cluster");
				Console.ReadKey();
				instance.Dispose();
			}

			return true;
		}

		private static bool TryStartClientTestClusterBaseImplementation(Type cluster)
		{
			if (!(Activator.CreateInstance(cluster) is ClientTestClusterBase instance)) return false;
			using (instance)
			{
				instance.Start();
				Console.WriteLine("Press any key to shutdown the running cluster");
				Console.ReadKey();
				instance.Dispose();
			}
			return true;
		}


		private static Type[] GetClusters()
		{
			IEnumerable<Type> types;

			try
			{
				types = typeof(INestTestCluster).GetTypeInfo().Assembly.GetTypes();
			}
			catch (ReflectionTypeLoadException e)
			{
				types = e.Types.Where(t => t != null);
			}

			return types
				.Where(t => t.Implements(typeof(IEphemeralCluster)))
				.ToArray();
		}
	}
}
