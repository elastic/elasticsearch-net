using System;
using System.IO;
using Tests.Framework.Configuration;
using Tests.Framework.MockData;

namespace Tests.Framework
{
	public static class ConfigurationLoader
	{
		public static ITestConfiguration LoadConfiguration()
		{
			// The build script sets a FAKEBUILD env variable, so if it exists then
			// we must be running tests from the build script
			if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("FAKEBUILD")))
				return new EnvironmentConfiguration();

			var directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());

			// If running the classic .NET solution, tests run from bin/{config} directory,
			// but when running DNX solution, tests run from the test project root
			var yamlConfigurationPath = (directoryInfo.Name == "Tests"
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

			throw new Exception($"Tried to load a yaml file from {yamlConfigurationPath} but it does not exist : pwd:{directoryInfo.FullName}");
		}
	}
}
