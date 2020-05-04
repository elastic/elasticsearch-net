// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Formatters;

namespace Nest
{
	public class StartBasicLicenseResponse : AcknowledgedResponseBase
	{
		[DataMember(Name = "acknowledge")]
		public StartBasicLicenseFeatureAcknowledgements Acknowledge { get; internal set; }

		[DataMember(Name = "basic_was_started")]
		public bool BasicWasStarted { get; internal set; }

		[DataMember(Name = "error_message")]
		public string ErrorMessage { get; internal set; }
	}

	[JsonFormatter(typeof(StartBasicLicenseFeatureAcknowledgementsFormatter))]
	public class StartBasicLicenseFeatureAcknowledgements : ReadOnlyDictionary<string, string[]>
	{
		internal StartBasicLicenseFeatureAcknowledgements(IDictionary<string, string[]> dictionary)
			: base(dictionary) { }

		[DataMember(Name = "message")]
		public string Message { get; internal set; }
	}

	internal class StartBasicLicenseFeatureAcknowledgementsFormatter : IJsonFormatter<StartBasicLicenseFeatureAcknowledgements>
	{
		private static readonly ArrayFormatter<string> StringArrayFormatter = new ArrayFormatter<string>();

		public StartBasicLicenseFeatureAcknowledgements Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.ReadIsNull())
				return null;

			var count = 0;
			string message = null;
			var dict = new Dictionary<string, string[]>();
			while (reader.ReadIsInObject(ref count))
			{
				var propertyName = reader.ReadPropertyName();
				if (propertyName == "message")
					message = reader.ReadString();
				else
					dict.Add(propertyName, StringArrayFormatter.Deserialize(ref reader, formatterResolver));
			}

			return new StartBasicLicenseFeatureAcknowledgements(dict) { Message = message };
		}

		public void Serialize(ref JsonWriter writer, StartBasicLicenseFeatureAcknowledgements value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteBeginObject();
			var count = 0;
			if (!string.IsNullOrEmpty(value.Message))
			{
				writer.WritePropertyName("message");
				writer.WriteString(value.Message);
				count++;
			}
			foreach (var kv in value)
			{
				if (count > 0)
					writer.WriteValueSeparator();

				writer.WritePropertyName(kv.Key);
				StringArrayFormatter.Serialize(ref writer, kv.Value, formatterResolver);
				count++;
			}
			writer.WriteEndObject();
		}
	}
}
