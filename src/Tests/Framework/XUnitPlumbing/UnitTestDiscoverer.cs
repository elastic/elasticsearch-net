using System;
using System.Linq;
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
			return !TestClient.Configuration.RunUnitTests
			       || ClassShouldSkipWhenPackageReference(classOfMethod)
			       || ClassIsIntegrationOnly(classOfMethod);
		}

		private static bool ClassShouldSkipWhenPackageReference(Type classOfMethod)
		{
#if TESTINGNUGETPACKAGE
			var attributes = classOfMethod.GetAttributes<ProjectReferenceOnlyAttribute>();
			return (attributes.Any());
#else
			return false;
#endif
		}

		private static bool ClassIsIntegrationOnly(Type classOfMethod)
		{
			var attributes = classOfMethod.GetAttributes<IntegrationOnlyAttribute>();
			return (attributes.Any());
		}
	}
}
