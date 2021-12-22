using Newtonsoft.Json;

namespace Playground
{
	public class Person
	{
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
