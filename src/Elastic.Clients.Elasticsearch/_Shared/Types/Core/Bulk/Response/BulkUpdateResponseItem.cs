// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;

using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Core.Bulk;

public sealed class BulkUpdateResponseItem : ResponseItem
{
	public override string Operation => "update";

	[SetsRequiredMembers]
	internal BulkUpdateResponseItem(JsonConstructorSentinel sentinel) : base(sentinel)
	{
	}
}
