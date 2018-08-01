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

			// If running the classic .NET solution, tests run from bin/{config} directory,
			// but when running DNX solution, tests run from the test project root
			var yamlConfigurationPath = (directoryInfo.Name == "Tests.Configuration"
			                             && directoryInfo.Parent != null
			                             && directoryInfo.Parent.Name == "src")
				? "."
				: @"../../../";

			var localYamlFile = Path.GetFullPath(Path.Combine(yamlConfigurationPath, "tests.yaml"));
			if (File.Exists(localYamlFile))
				return new YamlConfiguration(localYamlFile);

			var defaultYamlFile = Path.GetFullPath(Path.Combine(yamlConfigurationPath, "tests.default.yaml"));
			if (File.Exists(defaultYamlFile))
				return new YamlConfiguration(defaultYamlFile);

			return new EnvironmentConfiguration();
			throw new Exception($"Tried to load a yaml file from {yamlConfigurationPath} but it does not exist : pwd:{directoryInfo.FullName}");
		}
	}
}
