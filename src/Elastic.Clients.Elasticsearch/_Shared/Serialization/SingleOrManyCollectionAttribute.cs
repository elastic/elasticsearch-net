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

[AttributeUsage(AttributeTargets.Property)]
internal class SingleOrManyCollectionConverterAttribute : JsonConverterAttribute
{
	public SingleOrManyCollectionConverterAttribute(Type convertType) => ConvertType = convertType;

	public Type ConvertType { get; }

	public override JsonConverter? CreateConverter(Type typeToConvert) => (JsonConverter)Activator.CreateInstance(typeof(SingleOrManyCollectionConverter<>).MakeGenericType(ConvertType));
}
