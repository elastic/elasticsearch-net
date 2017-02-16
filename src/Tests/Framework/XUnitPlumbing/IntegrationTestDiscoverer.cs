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
			: base(diagnosticMessageSink, TestClient.Configuration.RunIntegrationTests)
		{
		}

		protected override bool SkipMethod(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
		{
			var classOfMethod = Type.GetType(testMethod.TestClass.Class.Name, true, true);
			var method = classOfMethod.GetMethod(testMethod.Method.Name, BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.Public)
				?? testMethod.Method.ToRuntimeMethod();

			var collectionType = TestAssemblyRunner.GetClusterForCollection(testMethod.TestClass?.TestCollection);

			return TypeSkipVersionAttributeSatisfies(classOfMethod) ||
				   MethodSkipVersionAttributeSatisfies(method);
		}


		private static bool TypeSkipVersionAttributeSatisfies(Type classOfMethod)
		{
#if !DOTNETCORE
			var attributes = Attribute.GetCustomAttributes(classOfMethod, typeof(SkipVersionAttribute), true);
#else
			var attributes =  classOfMethod.GetTypeInfo().GetCustomAttributes(typeof(SkipVersionAttribute), true);
#endif
			if (!attributes.Any()) return false;

			var elasticsearchVersion = TestClient.Configuration.ElasticsearchVersion;
			return attributes
				.Cast<SkipVersionAttribute>()
				.SelectMany(a => a.Ranges)
				.Any(range => range.IsSatisfied(elasticsearchVersion));
		}

		private static bool MethodSkipVersionAttributeSatisfies(MethodInfo methodInfo)
		{
#if !DOTNETCORE
			var attributes = Attribute.GetCustomAttributes(methodInfo, typeof(SkipVersionAttribute), true);
#else
			var attributes =  methodInfo.GetCustomAttributes(typeof(SkipVersionAttribute), true);
#endif
			if (!attributes.Any()) return false;

			var elasticsearchVersion = TestClient.Configuration.ElasticsearchVersion;
			return attributes
				.Cast<SkipVersionAttribute>()
				.SelectMany(a => a.Ranges)
				.Any(range => range.IsSatisfied(elasticsearchVersion));
		}
	}
}
