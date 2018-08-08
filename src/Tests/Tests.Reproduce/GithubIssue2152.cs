using System;
using System.IO;
using System.Linq;
using System.Text;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;

namespace Tests.Reproduce
{
	public class GithubIssue2152
	{
		[U]
		public void CanDeserializeNestedBulkError()
		{
			var nestedCausedByError = @"{
			   ""took"": 4,
			   ""errors"": true,
			   ""items"": [{
					""update"": {
						""_index"": ""index-name"",
						""_type"": ""type-name"",
						""_id"": ""1"",
						""status"": 400,
						""error"": {
							""type"": ""illegal_argument_exception"",
							""reason"": ""failed to execute script"",
							""caused_by"": {
								""type"": ""script_exception"",
								""reason"": ""failed to run inline script [use(java.lang.Exception) {throw new Exception(\""Customized Exception\"")}] using lang [groovy]"",
								""caused_by"": {
									""type"": ""privileged_action_exception"",
									""reason"": null,
									""caused_by"": {
										""type"": ""exception"",
										""reason"": ""Custom Exception""
									}
								}
							}
						}
					}
				}]
			}";

			var bytes = Encoding.UTF8.GetBytes(nestedCausedByError);
			var connection = new InMemoryConnection(bytes);
			var settings = new ConnectionSettings(new SingleNodeConnectionPool(new Uri("http://localhost:9200")), connection);
			var client = new ElasticClient(settings);

			var bulkResponse = client.Bulk(new BulkDescriptor());

			bulkResponse.Errors.Should().BeTrue();

			var firstOperation = bulkResponse.ItemsWithErrors.First();

			firstOperation.Error.Should().NotBeNull();
			firstOperation.Error.CausedBy.Should().NotBeNull();
			firstOperation.Error.CausedBy.InnerCausedBy.Should().NotBeNull();
			firstOperation.Error.CausedBy.InnerCausedBy.InnerCausedBy.Should().NotBeNull();
		}

		[U]
		public void CanDeserializeNestedError()
		{
			var nestedCausedByError = @"{
				""status"": 400,
				""error"": {
					""type"": ""illegal_argument_exception"",
					""reason"": ""failed to execute script"",
					""caused_by"": {
						""type"": ""script_exception"",
						""reason"": ""failed to run inline script [use(java.lang.Exception) {throw new Exception(\""Customized Exception\"")}] using lang [groovy]"",
						""caused_by"": {
							""type"": ""privileged_action_exception"",
							""reason"": null,
							""caused_by"": {
								""type"": ""exception"",
								""reason"": ""Custom Exception""
							}
						}
					}
				}
			}";

			var bytes = Encoding.UTF8.GetBytes(nestedCausedByError);
			var connection = new InMemoryConnection(bytes, 400);
			var settings = new ConnectionSettings(new SingleNodeConnectionPool(new Uri("http://localhost:9200")), connection);
			var client = new ElasticClient(settings);

			var searchResponse = client.Search<object>(s => s.Index("index"));

			searchResponse.ShouldNotBeValid();
			searchResponse.ServerError.Should().NotBeNull();
			searchResponse.ServerError.Error.Should().NotBeNull();
			searchResponse.ServerError.Error.CausedBy.Should().NotBeNull();
			searchResponse.ServerError.Error.CausedBy.InnerCausedBy.Should().NotBeNull();
			searchResponse.ServerError.Error.CausedBy.InnerCausedBy.InnerCausedBy.Should().NotBeNull();
		}
	}
}
