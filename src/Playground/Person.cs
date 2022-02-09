// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Newtonsoft.Json;

namespace Playground
{
	public class Person
	{
		public int Id { get; set; }
		public string? FirstName { get; init; }
		public string? LastName { get; init; }
		public int? Age { get; init; }
		public string? Email { get; init; }
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
