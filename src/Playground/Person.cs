// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Runtime.Serialization;
using Elastic.Clients.Elasticsearch;
using Newtonsoft.Json;

namespace Playground
{
	public class Person
	{
		public int Id { get; set; }

		[System.Text.Json.Serialization.JsonPropertyName("id2")]
		public Guid SecondaryId { get; set; } = Guid.NewGuid();
		public string? FirstName { get; init; }
		public string? LastName { get; init; }
		public int? Age { get; init; }
		public bool IsDeleted { get; init; }	
		public Routing? Routing { get; init; }

		public Id Idv3 => "testing";
		//public Guid Routing { get; init; } = Guid.NewGuid();

		[System.Text.Json.Serialization.JsonIgnore]
		public string? Email { get; init; }

		[DataMember(Name = "STEVE")]
		[IgnoreDataMember]
		public string Data { get; init; } = "NOTHING";
	}

	public class PersonV3
	{
		public Guid SecondaryId { get; set; } = Guid.NewGuid();
	}

	public class PersonV2
	{
		[JsonProperty("foreName")]
		public string? FirstName { get; init; }

		public string? LastName { get; init; }

		public int? Age { get; init; }

		public string? Email { get; init; }

		[JsonProperty("customisedName")]
		public string ShouldNotSeeThisName => "Test";

		[JsonIgnore]
		public string Ignored => "SHOULD BE IGNORED";
	}
}
