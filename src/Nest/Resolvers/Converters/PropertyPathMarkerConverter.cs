using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;


namespace Nest.Resolvers.Converters
{
	public class PropertyPathMarkerConverter : JsonConverter
	{
		private readonly ElasticInferrer _infer;
		public PropertyPathMarkerConverter(IConnectionSettings connectionSettings)
		{
			_infer = new ElasticInferrer(connectionSettings);
		}

		public override bool CanRead { get { return false; } }

		public override bool CanWrite { get { return true; } }

		public override bool CanConvert(Type objectType)
		{
			return true; //only to be used with attribute or contract registration.
		}
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var marker = value as PropertyPathMarker;
			if (marker == null)
			{
				writer.WriteNull();
				return;
			}
			writer.WriteValue(this._infer.PropertyPath(marker));
		}
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return null;
		}

	}
	public class PropertyNameMarkerConverter : JsonConverter
	{
		private readonly ElasticInferrer _infer;
		public PropertyNameMarkerConverter(IConnectionSettings connectionSettings)
		{
			_infer = new ElasticInferrer(connectionSettings);
		}

		public override bool CanRead { get { return false; } }

		public override bool CanWrite { get { return true; } }

		public override bool CanConvert(Type objectType)
		{
			return true; //only to be used with attribute or contract registration.
		}
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var marker = value as PropertyNameMarker;
			if (marker == null)
			{
				writer.WriteNull();
				return;
			}
			writer.WriteValue(this._infer.PropertyName(marker));
		}
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return null;
		}

	}
}

