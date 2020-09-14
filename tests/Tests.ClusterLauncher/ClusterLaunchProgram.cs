// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elastic.Elasticsearch.Ephemeral;
using Elastic.Elasticsearch.Managed;
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

			// Force TestConfiguration to load as if started from the command line even if we are actually starting
			// from the IDE. Also force configuration mode to integration test so the seeders run
			Environment.SetEnvironmentVariable("NEST_COMMAND_LINE_BUILD", "1", EnvironmentVariableTarget.Process);
			Environment.SetEnvironmentVariable("NEST_INTEGRATION_TEST", "1", EnvironmentVariableTarget.Process);

			if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("NEST_YAML_FILE")))
			{
				// build always sets previous argument, assume we are running from the IDE or dotnet run
                var yamlFile = TestConfiguration.LocateTestYamlFile();
                Environment.SetEnvironmentVariable("NEST_YAML_FILE", yamlFile, EnvironmentVariableTarget.Process);
			}

			// if version is passed this will take precedence over the version in the yaml file
			// in the constructor of EnvironmentConfiguration
			var clusterName = arguments[0];
			if (arguments.Length > 1)
				Environment.SetEnvironmentVariable("NEST_INTEGRATION_VERSION", arguments[1], EnvironmentVariableTarget.Process);

			var cluster = clusters.FirstOrDefault(c => c.Name.StartsWith(clusterName, StringComparison.OrdinalIgnoreCase));
			if (cluster == null)
			{
				Console.Error.WriteLine($"No cluster found that starts with '{clusterName}");
				Console.Out.WriteLine("Available clusters:");
				foreach (var c in clusters)
					Console.Out.WriteLine($"  - {c.Name.Replace("Cluster", "")}");
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
				types = typeof(INestTestCluster).Assembly.GetTypes();
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
