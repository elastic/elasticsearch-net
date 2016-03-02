using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Tests.Framework;
using Elasticsearch.Net;
using FluentAssertions;
using Xunit;

namespace Tests.ClientConcepts.ServerError
{
    public class ServerErrorTests : SerializationTestBase
    {
		[U]
	    public void CanDeserializeServerError()
	    {
		    var serverErrorJson = @"{
			   ""error"": {
				  ""root_cause"": [
					 {
						""type"": ""parse_exception"",
						""reason"": ""failed to parse source for create index""
					 }
				  ],
				  ""type"": ""parse_exception"",
				  ""reason"": ""failed to parse source for create index"",
				  ""caused_by"": {
					 ""type"": ""json_parse_exception"",
					 ""reason"": ""Unexpected character ('\""' (code 34)): was expecting a colon to separate field name and value\n at [Source: [B@1231dcb3; line: 6, column: 10]""
				  }
			   },
			   ""status"": 400
			}";

		    var serverError = this.Deserialize<Elasticsearch.Net.ServerError>(serverErrorJson);

		    serverError.Should().NotBeNull();
			serverError.Status.Should().Be(400);

			serverError.Error.Should().NotBeNull();
			serverError.Error.RootCause.Count.Should().Be(1);
			serverError.Error.CausedBy.Should().NotBeNull();
	    }
    }
}
