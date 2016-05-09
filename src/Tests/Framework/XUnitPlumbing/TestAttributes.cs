using Xunit;
using Xunit.Sdk;
using System.Reflection;

namespace Tests.Framework
{
	[XunitTestCaseDiscoverer("Tests.Framework.IntegrationTestDiscoverer", "Tests")]
	public class I : FactAttribute { }

	[XunitTestCaseDiscoverer("Tests.Framework.UnitTestDiscoverer", "Tests")]
	public class U : FactAttribute { }
}
