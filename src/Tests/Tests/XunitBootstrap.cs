using Elastic.Elasticsearch.Xunit;
using Tests.Core.Xunit;
using Xunit;

[assembly: TestFramework("Elastic.Elasticsearch.Xunit.Sdk.ElasticTestFramework", "Elastic.Elasticsearch.Xunit")]
[assembly: ElasticXunitConfiguration(typeof(NestXunitRunOptions))]
