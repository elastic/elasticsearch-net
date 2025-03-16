// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text;
using System.Linq;
using Elastic.Clients.Elasticsearch.Core.Bulk;

namespace Elastic.Clients.Elasticsearch;

public partial class BulkResponse
{
	[JsonConverter(typeof(BulkResponseItemConverter)), JsonPropertyName("items")]
	public IReadOnlyList<ResponseItem> Items { get; init; }

	[JsonIgnore]
	public IEnumerable<ResponseItem> ItemsWithErrors => !Items.HasAny()
		? Enumerable.Empty<ResponseItem>()
		: Items.Where(i => !i.IsValid);

	public override bool IsValidResponse => base.IsValidResponse && !Errors && !ItemsWithErrors.HasAny();

	protected override void DebugIsValid(StringBuilder sb)
	{
		if (Items == null)
			return;

		sb.AppendLine($"# Invalid Bulk items:");
		foreach (var i in Items.Select((item, i) => new { item, i }).Where(i => !i.item.IsValid))
			sb.AppendLine($"  operation[{i.i}]: {i.item}");
	}
}
