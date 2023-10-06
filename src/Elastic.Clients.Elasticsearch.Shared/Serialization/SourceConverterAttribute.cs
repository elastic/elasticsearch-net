// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serialization;

internal sealed class SourceConverterAttribute : JsonConverterAttribute
{
	public override JsonConverter? CreateConverter(Type typeToConvert) => (JsonConverter)Activator.CreateInstance(typeof(IntermediateSourceConverter<>).MakeGenericType(typeToConvert));
}
