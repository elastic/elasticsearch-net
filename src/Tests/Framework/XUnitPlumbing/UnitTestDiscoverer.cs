using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Tests.Framework
{
	public class UnitTestDiscoverer : NestTestDiscoverer
	{
		public UnitTestDiscoverer(IMessageSink diagnosticMessageSink)
			: base(diagnosticMessageSink, TestClient.Configuration.RunUnitTests)
		{
		}

		protected override bool SkipMethod(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
		{
			var classOfMethod = Type.GetType(testMethod.TestClass.Class.Name, true, true);
			return !TestClient.Configuration.RunUnitTests || ClassIsIntegrationOnly(classOfMethod);
		}

		private static bool ClassIsIntegrationOnly(Type classOfMethod)
		{
			var attributes = classOfMethod.GetAttributes<IntegrationOnly>();
			return (attributes.Any());
		}
	}
}
