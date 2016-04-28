using System;
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
			var collectionType = testMethod.TestClass?.TestCollection?.CollectionDefinition?.Name;
			//in mixed mode we do not want to run any api tests for plugins when running against a snapshot
			//because the client is "hot"
			return TestClient.Configuration.RunIntegrationTests && RequiresPluginButRunningAgainstSnapshot(classOfMethod, collectionType);
		}
	}
}
