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

		protected static bool RequiresPluginButRunningAgainstSnapshot(Type classOfMethod, Type collectionType)
		{
			Attribute[] classAttributes, collectionAttributes = { };
#if !DOTNETCORE
			classAttributes = Attribute.GetCustomAttributes(classOfMethod, typeof(RequiresPluginAttribute), true);
#else
			classAttributes =  classOfMethod.GetTypeInfo().GetCustomAttributes(typeof(RequiresPluginAttribute), true).ToArray();
#endif
			if (collectionType != null)
			{
#if !DOTNETCORE
				collectionAttributes = Attribute.GetCustomAttributes(collectionType, typeof(RequiresPluginAttribute), true);
#else
				collectionAttributes =  collectionType.GetTypeInfo().GetCustomAttributes(typeof(RequiresPluginAttribute), true).ToArray();
#endif
			}
			if (!classAttributes.Concat(collectionAttributes).Any()) return false;

			var elasticsearchVersion = TestClient.Configuration.ElasticsearchVersion;
			//test class requires a pluging but we are running against a snapshot
			//we can not as of yet install plugins for snapshots reliably
			return elasticsearchVersion.IsSnapshot;
		}
	}
}
