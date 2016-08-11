using System;
using System.Reflection;
using Tests.Framework;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit
{
	class TestFramework : XunitTestFramework
	{
		public TestFramework(IMessageSink messageSink)
			: base(messageSink)
		{ }

		protected override ITestFrameworkExecutor CreateExecutor(AssemblyName assemblyName)
		{
			var config = TestClient.Configuration;
			Console.WriteLine("Starting tests using config:");
			Console.WriteLine($" - {nameof(config.DoNotSpawnIfAlreadyRunning)}: {config.DoNotSpawnIfAlreadyRunning}");
			Console.WriteLine($" - {nameof(config.ElasticsearchVersion)}: {config.ElasticsearchVersion}");
			Console.WriteLine($" - {nameof(config.ForceReseed)}: {config.ForceReseed}");
			Console.WriteLine($" - {nameof(config.Mode)}: {config.Mode.ToString()}");
			Console.WriteLine($" - {nameof(config.RunIntegrationTests)}: {config.RunIntegrationTests}");
			Console.WriteLine($" - {nameof(config.RunUnitTests)}: {config.RunUnitTests}");

			return new TestFrameworkExecutor(assemblyName, SourceInformationProvider, DiagnosticMessageSink);
		}
	}
}
