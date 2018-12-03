using System;
using System.Collections.Generic;
using Utf8Json;

namespace Nest
{
	/// <summary>
	///  input to load data from multiple sources into the watch execution context when the watch is triggered.
	/// </summary>
	[InterfaceDataContract]
	[JsonFormatter(typeof(ChainInputFormatter))]
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
		public ChainInput() { }

		public ChainInput(IDictionary<string, InputContainer> inputs) => Inputs = inputs;

		/// <inheritdoc />
		public IDictionary<string, InputContainer> Inputs { get; set; }

		internal override void WrapInContainer(IInputContainer container) => container.Chain = this;
	}

	/// <inheritdoc />
	public class ChainInputDescriptor : DescriptorBase<ChainInputDescriptor, IChainInput>, IChainInput
	{
		public ChainInputDescriptor() { }

		public ChainInputDescriptor(IDictionary<string, InputContainer> inputs) => Self.Inputs = inputs;

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

	internal class ChainInputFormatter : IJsonFormatter<IChainInput>
	{
		public IChainInput Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject) return null;

			// inputs property
			reader.ReadNext();

			// property separator
			reader.ReadNext();

			var count = 0;
			var inputs = new Dictionary<string, InputContainer>();
			while (reader.ReadIsInArray(ref count))
			{
				var token = reader.GetCurrentJsonToken();
				if (token == JsonToken.BeginObject)
				{
					reader.ReadNext();
					var name = reader.ReadPropertyName();

					var inputContainerFormatter = formatterResolver.GetFormatter<InputContainer>();
					var input = inputContainerFormatter.Deserialize(ref reader, formatterResolver);

					inputs.Add(name, input);
				}
			}

			return new ChainInput(inputs);
		}

		public void Serialize(ref JsonWriter writer, IChainInput value, IJsonFormatterResolver formatterResolver)
		{
			if (value?.Inputs == null)
				return;

			writer.WriteBeginObject();
			writer.WritePropertyName("inputs");
			writer.WriteBeginArray();

			var count = 0;

			foreach (var input in value.Inputs)
			{
				if (count > 0)
					writer.WriteValueSeparator();

				writer.WriteBeginObject();
				writer.WritePropertyName(input.Key);
				var inputContainerFormatter = formatterResolver.GetFormatter<IInputContainer>();
				inputContainerFormatter.Serialize(ref writer, input.Value, formatterResolver);
				writer.WriteEndObject();

				count++;
			}
			writer.WriteEndArray();
			writer.WriteEndObject();
		}
	}
}
