using Elastic.Xunit;
using Tests.Core.Xunit;

[assembly: Xunit.TestFrameworkAttribute("Elastic.Xunit.Sdk.ElasticTestFramework", "Elastic.Xunit")]
[assembly: ElasticXunitConfiguration(typeof(NestXunitRunOptions))]
