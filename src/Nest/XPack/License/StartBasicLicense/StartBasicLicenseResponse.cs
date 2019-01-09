using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	public interface IStartBasicLicenseResponse : IAcknowledgedResponse
	{
		[JsonProperty("basic_was_started")]
		bool BasicWasStarted { get; }

		[JsonProperty("error_message")]
		string ErrorMessage { get; }

		[JsonProperty("acknowledge")]
		StartBasicLicenseFeatureAcknowledgements Acknowledge { get; }
	}

	public class StartBasicLicenseResponse : AcknowledgedResponseBase, IStartBasicLicenseResponse
	{
		//TODO: make this the default on base class for 7.0 ?
		public override bool IsValid => base.IsValid && Acknowledged;

		public bool BasicWasStarted { get; internal set; }

		public string ErrorMessage { get; internal set; }

		public StartBasicLicenseFeatureAcknowledgements Acknowledge { get; internal set; }
	}

	[JsonConverter(typeof(StartBasicLicenseFeatureAcknowledgementsJsonConverter))]
	public class StartBasicLicenseFeatureAcknowledgements : ReadOnlyDictionary<string, string[]>
	{
		internal StartBasicLicenseFeatureAcknowledgements(IDictionary<string, string[]> dictionary)
			: base(dictionary) { }

		[JsonProperty("message")]
		public string Message { get; internal set; }

	}

	internal class StartBasicLicenseFeatureAcknowledgementsJsonConverter : VerbatimDictionaryKeysJsonConverter<string, object>
	{
		public override bool CanRead => true;

		public override bool CanConvert(Type t) => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var jObject = JObject.Load(reader);
			string message = null;
			if (jObject.TryGetValue("message", out var mToken))
			{
				message = mToken.Value<string>();
				jObject.Remove("message");
			}

			var dictionary = serializer.Deserialize<Dictionary<string, string[]>>(jObject.CreateReader());
			var d = new StartBasicLicenseFeatureAcknowledgements(dictionary);
			d.Message = message;
			return d;
		}
	}

}
