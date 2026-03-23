// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Elastic.Esql.Core;
using Elastic.Esql.QueryModel;

namespace Elastic.Clients.Elasticsearch.Esql;

internal sealed class EsqlSourceInferenceInterceptor : IEsqlQueryInterceptor
{
	private readonly Inferrer _inferrer;

	public EsqlSourceInferenceInterceptor(Inferrer inferrer) => _inferrer = inferrer;

	public EsqlQuery Intercept(EsqlQuery query)
	{
		if (query.Source is not null)
			return query;

		var indexName = _inferrer.IndexName(query.ElementType);
		return query.WithSource(indexName);
	}
}
