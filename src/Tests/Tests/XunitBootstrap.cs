using Elastic.Xunit;
using Tests.Core.Xunit;
using Tests.Framework.ManagedElasticsearch;

[assembly: Xunit.TestFrameworkAttribute("Elastic.Xunit.Sdk.ElasticTestFramework", "Elastic.Xunit")]
[assembly: ElasticXunitConfiguration(typeof(NestXunitRunOptions))]
