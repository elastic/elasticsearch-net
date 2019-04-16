using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public interface IStartBasicLicenseResponse : IAcknowledgedResponse
	{
		[DataMember(Name = "acknowledge")]
		StartBasicLicenseFeatureAcknowledgements Acknowledge { get; }

		[DataMember(Name = "basic_was_started")]
		bool BasicWasStarted { get; }

		[DataMember(Name = "error_message")]
		string ErrorMessage { get; }
	}

	public class StartBasicLicenseResponse : AcknowledgedResponseBase, IStartBasicLicenseResponse
	{
		public StartBasicLicenseFeatureAcknowledgements Acknowledge { get; internal set; }

		public bool BasicWasStarted { get; internal set; }

		public string ErrorMessage { get; internal set; }

		//TODO: make this the default on base class for 7.0 ?
		public override bool IsValid => base.IsValid && Acknowledged;
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
