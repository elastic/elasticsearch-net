// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Text.Json.Serialization;

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless.IndexManagement;
#else
namespace Elastic.Clients.Elasticsearch.IndexManagement;
#endif

public partial class GetIndexResponse
{
	[JsonIgnore]
	public IReadOnlyDictionary<IndexName, IndexState> Indices => BackingDictionary;
}
