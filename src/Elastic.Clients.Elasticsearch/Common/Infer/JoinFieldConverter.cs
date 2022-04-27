// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Elastic.Clients.Elasticsearch
{
	internal sealed class JoinFieldConverter : JsonConverter<JoinField>
	{
		public override JoinField? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
		public override void Write(Utf8JsonWriter writer, JoinField value, JsonSerializerOptions options)
		{
			switch (value.Tag)
			{
				case 0:
					JsonSerializer.Serialize(writer, value.ParentOption.Name, options);
					break; 
			}

			// TODO - Finish this
		}
	}
}
