// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

//public interface IScript { }

///// <summary>
///// Base class for scripts
///// </summary>
//[JsonConverter(typeof(ScriptBaseConverter))]
//public abstract class Script : IScript
//{
//}

//internal sealed class ScriptBaseConverter : JsonConverter<Script>
//{
//	public override Script? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
//	{
//		var readAheadCopy = reader;

//		if (readAheadCopy.TokenType == JsonTokenType.String)
//		{
//			var source = reader.GetString();
//			return new InlineScript() { Source = source };
//		}

//		readAheadCopy.Read(); // {

//		if (readAheadCopy.TokenType != JsonTokenType.PropertyName)
//			throw new JsonException("Unexpected token type");

//		if (readAheadCopy.ValueTextEquals("params"))
//		{
//			while (readAheadCopy.Read() && readAheadCopy.TokenType != JsonTokenType.EndObject)
//			{
//			}

//			readAheadCopy.Read();
//		}

//		if (readAheadCopy.ValueTextEquals("id"))
//			return JsonSerializer.Deserialize<StoredScriptId>(ref reader, options);

//		return JsonSerializer.Deserialize<InlineScript>(ref reader, options);
//	}

//	public override void Write(Utf8JsonWriter writer, Script value, JsonSerializerOptions options)
//	{
//		if (value is InlineScript scriptSort)
//			JsonSerializer.Serialize<InlineScript>(writer, scriptSort, options);

//		else if (value is StoredScriptId storedScript)
//			JsonSerializer.Serialize<StoredScriptId>(writer, storedScript, options);

//		else
//			throw new JsonException("Unsupported script implementation");
//	}
//}
