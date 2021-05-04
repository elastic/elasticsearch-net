// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;

namespace Tests.Reproduce
{
	public class GithubIssue2409
	{
		[U]
		public void CanDeserializeCopyTo()
		{
			var json =
				"{\"test-events-v1-201412\":{\"mappings\":{\"events\":{\"dynamic\":\"false\",\"_size\":{\"enabled\":true},\"properties\":{\"created_utc\":{\"type\":\"date\"},\"data\":{\"properties\":{\"@environment\":{\"properties\":{\"o_s_name\":{\"type\":\"text\",\"index\":false,\"copy_to\":[\"os\"]}}}}},\"id\":{\"type\":\"keyword\"},\"os\":{\"type\":\"text\",\"fields\":{\"keyword\":{\"type\":\"keyword\",\"ignore_above\":256}}}}}}},\"test-events-v1-201502\":{\"mappings\":{\"events\":{\"dynamic\":\"false\",\"_size\":{\"enabled\":true},\"properties\":{\"created_utc\":{\"type\":\"date\"},\"data\":{\"properties\":{\"@environment\":{\"properties\":{\"o_s_name\":{\"type\":\"text\",\"index\":false,\"copy_to\":[\"os\"]}}}}},\"id\":{\"type\":\"keyword\"},\"os\":{\"type\":\"text\",\"fields\":{\"keyword\":{\"type\":\"keyword\",\"ignore_above\":256}}}}}}}}";

			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));

			var connection = new InMemoryConnection(Encoding.UTF8.GetBytes(json));
			var settings = new ConnectionSettings(pool, connection).DefaultIndex("test-events-v1-201412");
			var client = new ElasticClient(settings);

			var mappingResponse = client.Indices.GetMapping<Events>();

			mappingResponse.ShouldBeValid();

			var mappingWalker = new MappingWalker(new CopyToVisitor());
			mappingWalker.Accept(mappingResponse);
		}

		public class CopyToVisitor : NoopMappingVisitor
		{
			public override void Visit(ITextProperty property)
			{
				if (property.Name == "o_s_name") property.CopyTo.Should().NotBeNull();
			}
		}

		public class Events { }
	}
}
