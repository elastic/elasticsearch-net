/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(ChainTransformFormatter))]
	public interface IChainTransform : ITransform
	{
		ICollection<TransformContainer> Transforms { get; set; }
	}

	public class ChainTransform : TransformBase, IChainTransform
	{
		public ChainTransform() { }

		public ChainTransform(IEnumerable<TransformContainer> transforms) => Transforms = transforms?.ToList();

		public ICollection<TransformContainer> Transforms { get; set; }

		internal override void WrapInContainer(ITransformContainer container) => container.Chain = this;
	}

	public class ChainTransformDescriptor : DescriptorBase<ChainTransformDescriptor, IChainTransform>, IChainTransform
	{
		public ChainTransformDescriptor() { }

		public ChainTransformDescriptor(ICollection<TransformContainer> transforms) => Self.Transforms = transforms;

		ICollection<TransformContainer> IChainTransform.Transforms { get; set; }

		/// <inheritdoc />
		public ChainTransformDescriptor Transform(Func<TransformDescriptor, TransformContainer> selector)
		{
			if (Self.Transforms == null) Self.Transforms = new List<TransformContainer>();
			Self.Transforms.AddIfNotNull(selector?.Invoke(new TransformDescriptor()));
			return this;
		}
	}

	internal class ChainTransformFormatter : IJsonFormatter<IChainTransform>
	{
		public void Serialize(ref JsonWriter writer, IChainTransform value, IJsonFormatterResolver formatterResolver)
		{
			writer.WriteBeginArray();

			if (value != null)
			{
				var formatter = formatterResolver.GetFormatter<TransformContainer>();
				var count = 0;
				foreach (var transform in value.Transforms)
				{
					if (count > 0)
						writer.WriteValueSeparator();

					formatter.Serialize(ref writer, transform, formatterResolver);
					count++;
				}
			}

			writer.WriteEndArray();
		}

		public IChainTransform Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginArray)
				return null;

			var formatter = formatterResolver.GetFormatter<ICollection<TransformContainer>>();
			var transforms = formatter.Deserialize(ref reader, formatterResolver);
			return new ChainTransform(transforms);
		}
	}
}
