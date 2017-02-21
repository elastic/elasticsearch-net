using System;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace Tests.Framework
{
	public class IntegrationTestDiscoverer : NestTestDiscoverer
	{
		public IntegrationTestDiscoverer(IMessageSink diagnosticMessageSink)
			: base(diagnosticMessageSink, TestClient.Configuration.RunIntegrationTests) { }

		protected override bool SkipMethod(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
		{
			var classOfMethod = Type.GetType(testMethod.TestClass.Class.Name, true, true);
			var method = classOfMethod.GetMethod(testMethod.Method.Name, BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.Public)
				?? testMethod.Method.ToRuntimeMethod();

			return TypeSkipVersionAttributeSatisfies(classOfMethod) ||
				   MethodSkipVersionAttributeSatisfies(method);
		}


		private static bool TypeSkipVersionAttributeSatisfies(Type classOfMethod)
		{
			var attributes = classOfMethod.GetAttributes<SkipVersionAttribute>();
			if (!attributes.Any()) return false;

			return attributes
				.SelectMany(a => a.Ranges)
				.Any(range => TestClient.VersionUnderTestSatisfiedBy(range.ToString()));
		}

		private static bool MethodSkipVersionAttributeSatisfies(MethodInfo methodInfo)
		{
			var attributes = methodInfo.GetAttributes<SkipVersionAttribute>();
			if (!attributes.Any()) return false;

			return attributes
				.SelectMany(a => a.Ranges)
				.Any(range => TestClient.VersionUnderTestSatisfiedBy(range.ToString()));
		}
	}
}
