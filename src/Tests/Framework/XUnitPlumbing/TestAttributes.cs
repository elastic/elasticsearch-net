using Xunit;
using Xunit.Sdk;
using System.Reflection;

namespace Tests.Framework
{
    /// <summary>
    /// An integration test
    /// </summary>
    [XunitTestCaseDiscoverer("Tests.Framework.IntegrationTestDiscoverer", "Tests")]
	public class I : FactAttribute { }

    /// <summary>
    /// A unit test
    /// </summary>
    [XunitTestCaseDiscoverer("Tests.Framework.UnitTestDiscoverer", "Tests")]
	public class U : FactAttribute { }
}
