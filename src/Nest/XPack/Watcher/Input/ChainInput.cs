using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	///  input to load data from multiple sources into the watch execution context when the watch is triggered.
	/// </summary>
	[JsonObject]
	[JsonConverter(typeof(ChainInputJsonConverter))]
	public interface IChainInput : IInput
	{
		/// <summary>
		/// The input sources
		/// </summary>
		IDictionary<string, InputContainer> Inputs { get; set; }
	}

	/// <inheritdoc />
	public class ChainInput : InputBase, IChainInput
	{
		public ChainInput() {}

		public ChainInput(IDictionary<string, InputContainer> inputs)
		{
			this.Inputs = inputs;
		}

		/// <inheritdoc />
		public IDictionary<string, InputContainer> Inputs { get; set; }

		internal override void WrapInContainer(IInputContainer container) => container.Chain = this;
	}

	/// <inheritdoc />
	public class ChainInputDescriptor : DescriptorBase<ChainInputDescriptor, IChainInput>, IChainInput
	{
		public ChainInputDescriptor() {}

		public ChainInputDescriptor(IDictionary<string, InputContainer> inputs)
		{
			Self.Inputs = inputs;
		}

		IDictionary<string, InputContainer> IChainInput.Inputs { get; set; }

		/// <inheritdoc />
		public ChainInputDescriptor Input(string name, Func<InputDescriptor, InputContainer> selector)
		{
			if (Self.Inputs == null) Self.Inputs = new Dictionary<string, InputContainer>();

			if (Self.Inputs.ContainsKey(name))
				throw new InvalidOperationException($"An input named '{name}' has already been specified. Choose a different name");

			Self.Inputs.Add(name, selector.InvokeOrDefault(new InputDescriptor()));
			return this;
		}
	}

	internal class ChainInputJsonConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var chainInput = value as IChainInput;
			if (chainInput?.Inputs == null) return;

			writer.WriteStartObject();
			writer.WritePropertyName("inputs");
			writer.WriteStartArray();
			foreach (var input in chainInput.Inputs)
			{
				writer.WriteStartObject();
				writer.WritePropertyName(input.Key);
				serializer.Serialize(writer, input.Value);
				writer.WriteEndObject();
			}
			writer.WriteEndArray();
			writer.WriteEndObject();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.StartObject) return null;

			// inputs property
			reader.Read();

			// opening array
			reader.Read();

			var inputs = new Dictionary<string, InputContainer>();
			while (reader.Read())
			{
				if (reader.TokenType == JsonToken.StartObject)
				{
					reader.Read();
					var name = (string)reader.Value;
					reader.Read();
					var input = (InputContainer)serializer.Deserialize<IInputContainer>(reader);

					inputs.Add(name, input);
					reader.Read();
				}
				else if (reader.TokenType == JsonToken.EndArray)
				{
					reader.Read();
					break;
				}
			}

			return new ChainInput(inputs);
		}

		public override bool CanConvert(Type objectType) => true;
	}
}
