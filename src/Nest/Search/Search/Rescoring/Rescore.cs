using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(RescoreConverter))]
	public interface IRescore
	{
		[JsonProperty("window_size")]
		int? WindowSize { get; set; }

		[JsonProperty("query")]
		IRescoreQuery Query { get; set; }
	}

	public class Rescore : IRescore
	{
		public int? WindowSize { get; set; }

		public IRescoreQuery Query { get; set; }
	}

	public class RescoreDescriptor<T> : DescriptorBase<RescoreDescriptor<T>, IRescore>, IRescore 
		where T : class
	{
		int? IRescore.WindowSize { get; set; }
		IRescoreQuery IRescore.Query { get; set; }

		public virtual RescoreDescriptor<T> RescoreQuery(Func<RescoreQueryDescriptor<T>, IRescoreQuery> rescoreQuerySelector) =>
			Assign(a=>a.Query = rescoreQuerySelector?.Invoke(new RescoreQueryDescriptor<T>()));

		public virtual RescoreDescriptor<T> WindowSize(int? windowSize) => Assign(a => a.WindowSize = windowSize);
	}

	public class MultiRescore : IRescore, IEnumerable<IRescore>
	{
		private readonly List<IRescore> _rescore = new List<IRescore>();

		public MultiRescore() {}

		internal MultiRescore(IEnumerable<IRescore> recore)
		{
			_rescore = recore.ToList();
		}

		[JsonIgnore]
		int? IRescore.WindowSize
		{
			get { return _rescore.FirstOrDefault()?.WindowSize; }
			set
			{
				if (_rescore.Any())
				{
					_rescore.First().WindowSize = value;
				}
			}
		}

		[JsonIgnore]
		IRescoreQuery IRescore.Query
		{
			get { return _rescore.FirstOrDefault()?.Query; }
			set
			{
				if (_rescore.Any())
				{
					_rescore.First().Query = value;
				}
			}
		}

		public void Add(IRescore rescore)
		{
			var multiRescore = rescore as MultiRescore;
			if (multiRescore != null)
				_rescore.AddRange(multiRescore);
			else 
				_rescore.Add(rescore);
		}

		public IEnumerator<IRescore> GetEnumerator() => _rescore.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}

	public class RescoreConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var multiRescore = value as MultiRescore;
			if (multiRescore != null)
			{
				writer.WriteStartArray();
				foreach (var rescore in multiRescore)
					SerializeRescore(writer, rescore, serializer);
				writer.WriteEndArray();
			}
			else
			{
				var rescore = (IRescore)value;
				SerializeRescore(writer, rescore, serializer);
			}
		}

		private void SerializeRescore(JsonWriter writer, IRescore rescore, JsonSerializer serializer)
		{
			writer.WriteStartObject();
			if (rescore.WindowSize.HasValue)
			{
				writer.WritePropertyName("window_size");
				writer.WriteValue(rescore.WindowSize.Value);
			}
			if (rescore.Query != null)
			{
				writer.WritePropertyName("query");
				serializer.Serialize(writer, rescore.Query);
			}
			writer.WriteEndObject();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.StartArray)
			{
				var rescores = FromJson.ReadAs<List<Rescore>>(reader, objectType, existingValue, serializer);
				return new MultiRescore(rescores);
			}

			return FromJson.ReadAs<Rescore>(reader, objectType, existingValue, serializer);
		}

		public override bool CanConvert(Type objectType) => true;
	}
}
