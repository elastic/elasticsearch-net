// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Runtime.Serialization;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Playground;

[JsonSerializable(typeof(Person))]
internal partial class PlaygroundJsonSerializerContext : JsonSerializerContext;

public class Person
{
	public int Id { get; set; }

	[JsonPropertyName("id2")]
	public Guid SecondaryId { get; set; } = Guid.NewGuid();

	public string? FirstName { get; init; }
	public string? LastName { get; init; }
	public int? Age { get; init; }
	public bool IsDeleted { get; init; }

	[JsonConverter(typeof(RequestResponseConverter<Routing?>))]
	public Routing? Routing { get; init; }

	[JsonConverter(typeof(RequestResponseConverter<Id>))]
	public Id Idv3 => "testing";

	[JsonIgnore]
	public string? Email { get; init; }

	[DataMember(Name = "STEVE")]
	[IgnoreDataMember]
	public string Data { get; init; } = "NOTHING";

	public DateTimeKind Enum { get; init; }
}

public class PersonV3
{
	public Guid SecondaryId { get; set; } = Guid.NewGuid();
}
