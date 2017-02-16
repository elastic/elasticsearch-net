using System;
using Xunit;
using Xunit.Abstractions;

namespace Tests.Framework
{
	public class UnitTestDiscoverer : NestTestDiscoverer
	{
		public UnitTestDiscoverer(IMessageSink diagnosticMessageSink)
			: base(diagnosticMessageSink, TestClient.Configuration.RunUnitTests) { }

		protected override bool SkipMethod(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
		{
			var classOfMethod = Type.GetType(testMethod.TestClass.Class.Name, true, true);
			//in mixed mode we do not want to run any api tests for plugins when running against a snapshot
			//because the client is "hot"
			var collectionType = TestAssemblyRunner.GetClusterForCollection(testMethod.TestClass?.TestCollection);
			return TestClient.Configuration.RunIntegrationTests;
		}
	}
}
