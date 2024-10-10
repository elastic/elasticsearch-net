// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless.Core;
#else
namespace Elastic.Clients.Elasticsearch.Core;
#endif

internal interface IComplexUnion<TEnum> where TEnum : Enum
{
	internal TEnum ValueKind { get; }
	internal object Value { get; }
}
