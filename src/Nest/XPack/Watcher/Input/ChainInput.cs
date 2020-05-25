// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Collections.Generic;
using Elasticsearch.Net.Utf8Json;

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
			if (Self.Inputs != null)
			{
				if (Self.Inputs.ContainsKey(name))
					throw new InvalidOperationException($"An input named '{name}' has already been specified. Choose a different name");
			}
			else
				Self.Inputs = new Dictionary<string, InputContainer>();

			Self.Inputs.Add(name, selector.InvokeOrDefault(new InputDescriptor()));
			return this;
		}
	}

	internal class ChainInputFormatter : IJsonFormatter<IChainInput>
	{
		public IChainInput Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
			{
				reader.ReadNextBlock();
				return null;
			}

			// inputs property
			reader.ReadNext(); // {
			reader.ReadNext(); // "inputs"
			reader.ReadNext(); // :

			var count = 0;
			var inputs = new Dictionary<string, InputContainer>();
			var inputContainerFormatter = formatterResolver.GetFormatter<InputContainer>();
			while (reader.ReadIsInArray(ref count))
			{
				reader.ReadNext(); // {
				var name = reader.ReadPropertyName();
				var input = inputContainerFormatter.Deserialize(ref reader, formatterResolver);
				reader.ReadNext(); // }
				inputs.Add(name, input);
			}

			reader.ReadNext(); // }

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
			var inputContainerFormatter = formatterResolver.GetFormatter<IInputContainer>();

			foreach (var input in value.Inputs)
			{
				if (count > 0)
					writer.WriteValueSeparator();

				writer.WriteBeginObject();
				writer.WritePropertyName(input.Key);
				inputContainerFormatter.Serialize(ref writer, input.Value, formatterResolver);
				writer.WriteEndObject();
				count++;
			}
			writer.WriteEndArray();
			writer.WriteEndObject();
		}
	}
}
