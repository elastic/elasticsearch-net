// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Elastic.Transport.Products.Elasticsearch.Failures;
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
			firstOperation.Status.Should().Be(400);

			ShouldDeserialize(firstOperation.Error);
			ShouldDeserialize(firstOperation.Error.CausedBy);
			ShouldDeserialize(firstOperation.Error.CausedBy.CausedBy, true);
			ShouldDeserialize(firstOperation.Error.CausedBy.CausedBy.CausedBy);
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
			var se = searchResponse.ServerError;
			se.Status.Should().Be(400);
			se.Should().NotBeNull();
			ShouldDeserialize(se.Error);
			ShouldDeserialize(se.Error.CausedBy);
			ShouldDeserialize(se.Error.CausedBy.CausedBy, true);
			ShouldDeserialize(se.Error.CausedBy.CausedBy.CausedBy);
		}

		private static void ShouldDeserialize(ErrorCause error, bool nullReason = false)
		{
			error.Should().NotBeNull();
			error.Type.Should().NotBeNullOrEmpty();
			if (nullReason)
				error.Reason.Should().BeNull();
			else
				error.Reason.Should().NotBeNullOrEmpty();
		}
	}
}
