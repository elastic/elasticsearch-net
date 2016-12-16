using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;
using System.Reflection;
using Version = SemVer.Version;

namespace Tests.Framework
{
	[XunitTestCaseDiscoverer("Tests.Framework.IntegrationTestDiscoverer", "Tests")]
	public class I : FactAttribute
	{
	}

	[XunitTestCaseDiscoverer("Tests.Framework.UnitTestDiscoverer", "Tests")]
	public class U : FactAttribute
	{
	}

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
	}

	public class IntegrationTestDiscoverer : NestTestDiscoverer
	{
		public IntegrationTestDiscoverer(IMessageSink diagnosticMessageSink)
			: base(diagnosticMessageSink, TestClient.Configuration.RunIntegrationTests)
		{
		}

		protected override bool SkipMethod(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
		{

			var classOfMethod = Type.GetType(testMethod.TestClass.Class.Name, true, true);
#if !DOTNETCORE
			var attributes = Attribute.GetCustomAttributes(classOfMethod, typeof(SkipVersionAttribute));
#else

			var attributes =  classOfMethod.GetTypeInfo().GetCustomAttributes(typeof(SkipVersionAttribute), false);
#endif
			if (!attributes.Any()) return false;

			var version = new Version(TestClient.Configuration.ElasticsearchVersion);
			var ranges = attributes.Cast<SkipVersionAttribute>()
				.SelectMany(a=>a.Ranges);

			return ranges.Any(range => range.IsSatisfied(version));
		}
	}


	public class UnitTestDiscoverer : NestTestDiscoverer
	{
		public UnitTestDiscoverer(IMessageSink diagnosticMessageSink)
			: base(diagnosticMessageSink, TestClient.Configuration.RunUnitTests)
		{
		}
	}

	public class SkipVersionAttribute : Attribute
	{
		public IList<SemVer.Range> Ranges { get; }

		public SkipVersionAttribute(string skipVersionRangesSeparatedByComma, string reason)
		{
			this.Ranges = skipVersionRangesSeparatedByComma.Split(',')
				.Select(r => new SemVer.Range(r))
				.ToList();
		}
	}
}
