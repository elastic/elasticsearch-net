// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless.OpenTelemetry;
#else
namespace Elastic.Clients.Elasticsearch.OpenTelemetry;
#endif

internal static class SemanticConventions
{
	public const string DbOperation = "db.operation";
}
