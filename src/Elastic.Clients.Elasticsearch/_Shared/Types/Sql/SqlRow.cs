// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Sql;

[JsonConverter(typeof(Json.SqlRowConverter))]
public sealed class SqlRow : ReadOnlyCollection<SqlValue>
{
	public SqlRow(IList<SqlValue> list) : base(list)
	{
	}
}
