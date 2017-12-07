using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit.Abstractions;

namespace Tests.Framework
{
	public class IntegrationTestDiscoverer : NestTestDiscoverer
	{
		private bool RunningOnTeamCity { get; } = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TEAMCITY_VERSION"));

		public IntegrationTestDiscoverer(IMessageSink diagnosticMessageSink)
			: base(diagnosticMessageSink, TestClient.Configuration.RunIntegrationTests) { }

		protected override bool SkipMethod(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
		{
			var classOfMethod = Type.GetType(testMethod.TestClass.Class.Name, true, true);
			var method = classOfMethod.GetMethod(testMethod.Method.Name, BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.Public)
				?? testMethod.Method.ToRuntimeMethod();

			return TypeSkipVersionAttributeSatisfies(classOfMethod)
			       || MethodSkipVersionAttributeSatisfies(method)
			       || SkipWhenRunOnTeamCity(classOfMethod, method);
		}

		private bool SkipWhenRunOnTeamCity(Type classOfMethod, MethodInfo info)
		{
			if (!this.RunningOnTeamCity) return false;

			var attributes = classOfMethod.GetAttributes<SkipOnTeamCityAttribute>().Concat(info.GetAttributes<SkipOnTeamCityAttribute>());
			return attributes.Any();
		}

		private static bool TypeSkipVersionAttributeSatisfies(Type classOfMethod) =>
			VersionUnderTestMatchesAttribute(classOfMethod.GetAttributes<SkipVersionAttribute>());

		private static bool MethodSkipVersionAttributeSatisfies(MethodInfo methodInfo) =>
			VersionUnderTestMatchesAttribute(methodInfo.GetAttributes<SkipVersionAttribute>());

		private static bool VersionUnderTestMatchesAttribute(IEnumerable<SkipVersionAttribute> attributes)
		{
			if (!attributes.Any()) return false;

			return attributes
				.SelectMany(a => a.Ranges)
				.Any(range => TestClient.VersionUnderTestSatisfiedBy(range.ToString()));
		}

	}
}
