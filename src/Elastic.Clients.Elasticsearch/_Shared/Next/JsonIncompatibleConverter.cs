// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serialization;

/// <summary>
/// A <see cref="JsonConverterFactory"/> implementation that is used to indicate that a type is not compatible with
/// standard JSON serialization.
/// </summary>
internal sealed class JsonIncompatibleConverter :
	JsonConverterFactory
{
	public override bool CanConvert(Type typeToConvert)
	{
		return true;
	}

	public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
	{
		throw new NotSupportedException($"Type '{typeToConvert.FullName}' is not compatible with standard JSON serialization.");
	}
}
