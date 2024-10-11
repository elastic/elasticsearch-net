// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json.Serialization;

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless.Serialization;
#else
namespace Elastic.Clients.Elasticsearch.Serialization;
#endif

internal sealed class SourceConverterAttribute : JsonConverterAttribute
{
	public override JsonConverter? CreateConverter(Type typeToConvert) => (JsonConverter)Activator.CreateInstance(typeof(IntermediateSourceConverter<>).MakeGenericType(typeToConvert));
}
