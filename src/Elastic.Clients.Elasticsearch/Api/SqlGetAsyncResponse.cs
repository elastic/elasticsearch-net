using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Sql;

public partial class SqlGetAsyncResponse
{
	[JsonInclude]
	[JsonPropertyName("rows")]
	public IReadOnlyCollection<SqlRow> Rows { get; init; }
}
