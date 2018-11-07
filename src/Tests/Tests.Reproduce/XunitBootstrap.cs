using Elastic.Xunit;
using Tests.Core.Xunit;
using Xunit;

[assembly: TestFramework("Elastic.Xunit.Sdk.ElasticTestFramework", "Elastic.Xunit")]
[assembly: ElasticXunitConfiguration(typeof(NestXunitRunOptions))]
