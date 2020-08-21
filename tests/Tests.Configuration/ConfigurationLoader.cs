// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.IO;
using System.Threading;

namespace Tests.Configuration
{
	public static class TestConfiguration
	{
		private static readonly Lazy<TestConfigurationBase> Lazy
			= new Lazy<TestConfigurationBase>(LoadConfiguration, LazyThreadSafetyMode.ExecutionAndPublication);

		public static TestConfigurationBase Instance => Lazy.Value;

		private static TestConfigurationBase LoadConfiguration() =>
			!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("NEST_COMMAND_LINE_BUILD"))
				? (TestConfigurationBase)LoadCommandLineConfiguration()
				: LoadYamlConfiguration();

		/// <summary>
		/// Loads configuration by reading from the yaml and overriding specific configuration settings through
		/// environment variables set by the command line build.
		/// </summary>
		private static EnvironmentConfiguration LoadCommandLineConfiguration()
		{
			var yamlFile = Environment.GetEnvironmentVariable("NEST_YAML_FILE");
			if (string.IsNullOrWhiteSpace(yamlFile))
				throw new Exception("expected NEST_YAML_FILE to be set when calling build.bat or build.sh");
			if (!File.Exists(yamlFile))
				throw new Exception($"expected {yamlFile} to exist on disk NEST_YAML_FILE seems misconfigured");

			//load the test seed from the explicitly passed yaml file when running from FAKE
			var tempYamlConfiguration = new YamlConfiguration(yamlFile);
			return new EnvironmentConfiguration(tempYamlConfiguration);
		}

		public static string LocateTestYamlFile()
		{
			var directory = new DirectoryInfo(Directory.GetCurrentDirectory());
			var testsConfigurationFolder = FindTestsConfigurationFolder(directory);
			if (testsConfigurationFolder == null)
				throw new Exception($"Tried to locate a parent test folder starting from  pwd:{directory.FullName}");

			var localYamlFile = Path.Combine(testsConfigurationFolder.FullName, "tests.yaml");
			var defaultYamlFile = Path.Combine(testsConfigurationFolder.FullName, "tests.default.yaml");
			if (File.Exists(localYamlFile)) return localYamlFile;

			if (File.Exists(defaultYamlFile)) return defaultYamlFile;

			throw new Exception($"Tried to load a yaml file from {testsConfigurationFolder.FullName}");

		}

		/// <summary>
		/// The test configuration loaded when you run the tests
		/// <para> - from the IDE </para>
		/// <para> - when calling dotnet test in the tests directory </para>
		/// </summary>
		private static YamlConfiguration LoadYamlConfiguration()
		{
			var yamlFile = LocateTestYamlFile();
			return new YamlConfiguration(yamlFile);
		}

		private static DirectoryInfo FindTestsConfigurationFolder(DirectoryInfo directoryInfo)
		{
			do
			{
				var yamlConfigDir = Path.Combine(directoryInfo.FullName, "Tests.Configuration");
				if (Directory.Exists(yamlConfigDir))
					return new DirectoryInfo(yamlConfigDir);

				// traverse up
				directoryInfo = directoryInfo.Parent;
			} while (directoryInfo != null);
			return null;
		}
	}
}
