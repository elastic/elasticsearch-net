using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;
using System.Reflection;

namespace Tests.Framework
{
	public abstract class NestTestDiscoverer : IXunitTestCaseDiscoverer
	{
		protected IMessageSink DiagnosticMessageSink;
		private readonly bool _condition;

		protected NestTestDiscoverer(IMessageSink diagnosticMessageSink, bool condition)
		{
			this._condition = condition;
			this.DiagnosticMessageSink = diagnosticMessageSink;
		}

		protected virtual bool SkipMethod(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute) => false;

		public IEnumerable<IXunitTestCase> Discover(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute) =>
			!_condition || SkipMethod(discoveryOptions, testMethod, factAttribute)
				? Enumerable.Empty<IXunitTestCase>()
				: new[] { new XunitTestCase(DiagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), testMethod) };

		protected static bool SkipWhenNeedingTypedKeys(Type classOfMethod) =>
			(!TestClient.Configuration.Random.TypedKeys && classOfMethod.GetAttributes<NeedsTypedKeysAttribute>().Any());

	}
}
