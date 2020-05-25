// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(BucketsPathFormatter))]
	public interface IBucketsPath { }

	public class SingleBucketsPath : IBucketsPath
	{
		public SingleBucketsPath(string bucketsPath) => BucketsPath = bucketsPath;

		public string BucketsPath { get; }

		public static implicit operator SingleBucketsPath(string bucketsPath) => new SingleBucketsPath(bucketsPath);
	}

	public interface IMultiBucketsPath : IIsADictionary<string, string>, IBucketsPath { }

	public class MultiBucketsPath : IsADictionaryBase<string, string>, IMultiBucketsPath
	{
		public MultiBucketsPath() { }

		public MultiBucketsPath(IDictionary<string, string> container) : base(container) { }

		public MultiBucketsPath(Dictionary<string, string> container) : base(container) { }

		public void Add(string name, string bucketsPath) => BackingDictionary.Add(name, bucketsPath);

		public static implicit operator MultiBucketsPath(Dictionary<string, string> bucketsPath) => new MultiBucketsPath(bucketsPath);
	}

	public class MultiBucketsPathDescriptor
		: IsADictionaryDescriptorBase<MultiBucketsPathDescriptor, IMultiBucketsPath, string, string>
	{
		public MultiBucketsPathDescriptor() : base(new MultiBucketsPath()) { }

		public MultiBucketsPathDescriptor Add(string name, string bucketsPath) => Assign(name, bucketsPath);
	}

	internal class BucketsPathFormatter : IJsonFormatter<IBucketsPath>
	{
		public IBucketsPath Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			switch (token)
			{
				case JsonToken.String:
					return new SingleBucketsPath(reader.ReadString());
				case JsonToken.BeginObject:
					var formatter = formatterResolver.GetFormatter<Dictionary<string, string>>();
					var dict = formatter.Deserialize(ref reader, formatterResolver);
					return new MultiBucketsPath(dict);
				default:
					return null;
			}
		}

		public void Serialize(ref JsonWriter writer, IBucketsPath value, IJsonFormatterResolver formatterResolver)
		{
			if (value is SingleBucketsPath single)
				writer.WriteString(single.BucketsPath);
			else if (value is MultiBucketsPath multi)
			{
				writer.WriteBeginObject();
				var count = 0;
				foreach (var kv in multi)
				{
					if (count != 0)
						writer.WriteValueSeparator();
					writer.WritePropertyName(kv.Key);
					writer.WriteString(kv.Value);
					count++;
				}
				writer.WriteEndObject();
			}
			else
				writer.WriteNull();
		}
	}
}
