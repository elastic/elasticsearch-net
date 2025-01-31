// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

namespace Elastic.Clients.Elasticsearch.QueryDsl;

// This converter is generated for the Query container type. We add the CanConvert override here (for now)
// as the Query type may be used in source POCOs.

internal sealed partial class QueryConverter
{
	public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(Query);
}
