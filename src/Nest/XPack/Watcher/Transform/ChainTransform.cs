using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(ChainTransformJsonConverter))]
	public interface IChainTransform : ITransform
	{
		ICollection<TransformContainer> Transforms { get; set; }
	}

	public class ChainTransform : TransformBase, IChainTransform
	{
		public ChainTransform() {}

		public ChainTransform(IEnumerable<TransformContainer> transforms)
		{
			this.Transforms = transforms?.ToList();
		}

		public ICollection<TransformContainer> Transforms { get; set; }

		internal override void WrapInContainer(ITransformContainer container) => container.Chain = this;
	}

	public class ChainTransformDescriptor : DescriptorBase<ChainTransformDescriptor, IChainTransform>, IChainTransform
	{
		public ChainTransformDescriptor() { }

		public ChainTransformDescriptor(ICollection<TransformContainer> transforms)
		{
			Self.Transforms = transforms;
		}

		ICollection<TransformContainer> IChainTransform.Transforms { get; set; }

		/// <inheritdoc />
		public ChainTransformDescriptor Transform(Func<TransformDescriptor, TransformContainer> selector)
		{
			if (Self.Transforms == null) Self.Transforms = new List<TransformContainer>();
			Self.Transforms.AddIfNotNull(selector?.Invoke(new TransformDescriptor()));
			return this;
		}
	}

	internal class ChainTransformJsonConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			writer.WriteStartArray();
			var chainTransform = (IChainTransform)value;

			if (chainTransform != null)
			{
				foreach (var transform in chainTransform.Transforms)
				{
					serializer.Serialize(writer, transform);
				}
			}

			writer.WriteEndArray();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.StartArray) return null;
			var transforms = serializer.Deserialize<ICollection<TransformContainer>>(reader);
			return new ChainTransform(transforms);
		}

		public override bool CanConvert(Type objectType) => true;
	}
}
