// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Linq;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

public partial class MultiSearchResponse<TDocument>
{
	public override bool IsValidResponse => base.IsValidResponse && (Responses?.All(b => b.Item1 is not null && b.Item1.Status == 200) ?? true);

	[JsonIgnore]
	public int TotalResponses => Responses.HasAny() ? Responses.Count() : 0;
}

public sealed partial class MultiSearchRequestDescriptor<TDocument>
{
	internal override void BeforeRequest() => TypedKeys(true);
}

public sealed partial class MultiSearchRequestDescriptor
{
	internal override void BeforeRequest() => TypedKeys(true);
}

public partial class MultiSearchRequest
{
	internal override void BeforeRequest() => TypedKeys = true;
}
