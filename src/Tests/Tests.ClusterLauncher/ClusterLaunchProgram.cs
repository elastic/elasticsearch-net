using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elastic.Managed;
using Elastic.Managed.Ephemeral;
using FluentAssertions.Common;
using Tests.Configuration;
using Tests.Core.ManagedElasticsearch.Clusters;

namespace Tests.ClusterLauncher
{
	public static class ClusterLaunchProgram
	{
		private static ICluster<EphemeralClusterConfiguration> Instance { get; set; }

		public static int Main(string[] arguments)
		{
			var clusters = GetClusters();
			if (arguments.Length < 1)
			{
				Console.Error.WriteLine("cluster command needs atleast one argument to indicate the cluster to start");
				foreach (var c in clusters)
					Console.WriteLine(" - " + c.Name.Replace("Cluster", "").ToLowerInvariant());

				return 3;
			}

			var clusterName = arguments[0];
			if (arguments.Length > 1)
				Environment.SetEnvironmentVariable("NEST_INTEGRATION_VERSION", arguments[1], EnvironmentVariableTarget.Process);
			Environment.SetEnvironmentVariable("NEST_INTEGRATION_SHOW_OUTPUT_AFTER_START", "1", EnvironmentVariableTarget.Process);

			var cluster = clusters.FirstOrDefault(c => c.Name.StartsWith(clusterName, StringComparison.OrdinalIgnoreCase));
			if (cluster == null)
			{
				Console.Error.WriteLine($"No cluster found that starts with '{clusterName}");
				return 4;
			}

			//best effort, wont catch all the things
			//https://github.com/dotnet/coreclr/issues/8565
			//Don't want to make this windows only by registering a SetConsoleCtrlHandler  though P/Invoke.
			AppDomain.CurrentDomain.ProcessExit += (s, ev) => Instance?.Dispose();
			Console.CancelKeyPress += (s, ev) => Instance?.Dispose();

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

			Instance = instance;
			using (instance)
				return Run(instance);
		}


		private static bool TryStartClientTestClusterBaseImplementation(Type cluster)
		{
			if (!(Activator.CreateInstance(cluster) is ClientTestClusterBase instance)) return false;

			Instance = instance;
			using (instance)
				return Run(instance);
		}

		private static bool Run(ICluster<EphemeralClusterConfiguration> instance)
		{
			TestConfiguration.Instance.DumpConfiguration();
			instance.Start();
			if (!instance.Started)
			{
				Console.Error.WriteLine($"Failed to start cluster: '{instance.GetType().FullName}");
				return false;
			}
			Console.WriteLine("Press any key to shutdown the running cluster");
			var c = default(ConsoleKeyInfo);
			while (c.Key != ConsoleKey.Q) c = Console.ReadKey();
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
				.Where(t=> !t.IsAbstract)
				.ToArray();
		}
	}
}
