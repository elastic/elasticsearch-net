// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.


#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless;
#else
namespace Elastic.Clients.Elasticsearch;
#endif

internal static class HelperIdentifiers
{
	public const string SnapshotHelper = "sn";
	public const string ScrollHelper = "s";
	public const string ReindexHelper = "r";
	public const string BulkHelper = "b";
	public const string RestoreHelper = "sr";
}
