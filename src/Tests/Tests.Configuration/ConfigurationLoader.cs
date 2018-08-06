using System;
using System.IO;
using System.Threading;
using Tests.Framework.Configuration;

namespace Tests.Framework
{
	public sealed class TestConfiguration
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

			var directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
			Console.WriteLine(directoryInfo.FullName);

			var yamlConfigurationPath = "../../../../Tests.Configuration";
			var directory = Path.GetFullPath(yamlConfigurationPath);

			var localYamlFile = Path.Combine(directory, "tests.yaml");
			if (File.Exists(localYamlFile))
				return new YamlConfiguration(localYamlFile);

			var defaultYamlFile = Path.Combine(directory, "tests.default.yaml");
			if (File.Exists(defaultYamlFile))
				return new YamlConfiguration(defaultYamlFile);

			throw new Exception($"Tried to load a yaml file from {directory} but it does not exist : pwd:{directoryInfo.FullName}");
		}
	}
}
