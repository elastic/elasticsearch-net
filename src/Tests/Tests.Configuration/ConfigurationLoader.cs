using System;
using System.IO;
using System.Threading;

namespace Tests.Configuration
{
	public static class TestConfiguration
	{
		private static readonly Lazy<ITestConfiguration> Lazy
			= new Lazy<ITestConfiguration>(LoadConfiguration, LazyThreadSafetyMode.ExecutionAndPublication);

		public static ITestConfiguration Instance => Lazy.Value;

		private static ITestConfiguration LoadConfiguration()
		{
			// The build script sets a FAKEBUILD env variable, so if it exists then
			// we must be running tests from the build script
			if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("FAKEBUILD")))
				return new EnvironmentConfiguration();

			var directory = new DirectoryInfo(Directory.GetCurrentDirectory());
			var testsConfigurationFolder = FindTestsConfigurationFolder(directory);
			if (testsConfigurationFolder == null)
				throw new Exception($"Tried to locate a parent test folder starting from  pwd:{directory.FullName}");

			var localYamlFile = Path.Combine(testsConfigurationFolder.FullName, "tests.yaml");
			if (File.Exists(localYamlFile))
				return new YamlConfiguration(localYamlFile);

			var defaultYamlFile = Path.Combine(testsConfigurationFolder.FullName, "tests.default.yaml");
			if (File.Exists(defaultYamlFile))
				return new YamlConfiguration(defaultYamlFile);

			throw new Exception($"Tried to load a yaml file from {testsConfigurationFolder.FullName}");
		}

		private static DirectoryInfo FindTestsConfigurationFolder(DirectoryInfo directoryInfo)
		{
			do
			{
				var yamlConfigDir = Path.Combine(directoryInfo.FullName, "Tests.Configuration");
				if (directoryInfo.Name == "Tests" && Directory.Exists(yamlConfigDir))
					return new DirectoryInfo(yamlConfigDir);
				directoryInfo = directoryInfo.Parent;
			} while (directoryInfo != null);
			return null;
		}
	}
}
