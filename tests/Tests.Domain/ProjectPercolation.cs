// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Elastic.Clients.Elasticsearch.QueryDsl;

namespace Tests.Domain;

public class ProjectPercolation : Project
{
	public string Id { get; set; }
	public QueryContainer Query { get; set; }
}
