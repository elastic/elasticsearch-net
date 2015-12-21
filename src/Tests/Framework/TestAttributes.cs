using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

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
		readonly IMessageSink _diagnosticMessageSink;
		private readonly bool _condition;

		protected NestTestDiscoverer(IMessageSink diagnosticMessageSink, bool condition)
		{
			this._condition = condition;
			this._diagnosticMessageSink = diagnosticMessageSink;
		}

		public IEnumerable<IXunitTestCase> Discover(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute) => 
			!_condition 
				? Enumerable.Empty<IXunitTestCase>() 
				: new[] { new XunitTestCase(_diagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), testMethod) };
	}

	public class IntegrationTestDiscoverer : NestTestDiscoverer
	{
		public IntegrationTestDiscoverer(IMessageSink diagnosticMessageSink) 
			: base(diagnosticMessageSink, TestClient.Configuration.RunIntegrationTests)
		{
		}
	}


	public class UnitTestDiscoverer : NestTestDiscoverer
	{
		public UnitTestDiscoverer(IMessageSink diagnosticMessageSink) 
			: base(diagnosticMessageSink, TestClient.Configuration.RunUnitTests)
		{
		}
	}


}