// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Elastic.Clients.Elasticsearch.Serialization;

using System.Diagnostics.CodeAnalysis;

namespace Elastic.Clients.Elasticsearch.Core.Bulk;

public sealed class CreateResponseItem : ResponseItem
{
	public override string Operation => "create";

	[SetsRequiredMembers]
	internal CreateResponseItem(JsonConstructorSentinel sentinel) : base(sentinel)
	{
	}
}
