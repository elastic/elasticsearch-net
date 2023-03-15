// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;

namespace Elastic.Clients.Elasticsearch.QueryDsl;

public partial class Query
{
	public bool TryGet<T>([NotNullWhen(true)]out T? query)
	{
		query = default(T);

		if (Variant is T variant)
		{
			query = variant;
			return true;
		}

		return false;
	}
}

