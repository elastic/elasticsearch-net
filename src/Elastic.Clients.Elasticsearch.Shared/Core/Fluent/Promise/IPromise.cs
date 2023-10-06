// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless.Fluent;
#else
namespace Elastic.Clients.Elasticsearch.Fluent;
#endif

internal interface IPromise<out TValue> where TValue : class
{
	TValue Value { get; }
}
