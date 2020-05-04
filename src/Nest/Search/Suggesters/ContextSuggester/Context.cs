// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(ContextFormatter))]
	public class Context : Union<string, GeoLocation>
	{
		public Context(string category) : base(category) { }

		public Context(GeoLocation geo) : base(geo) { }

		public string Category => Item1;
		public GeoLocation Geo => Item2;

		public static implicit operator Context(string context) => new Context(context);

		public static implicit operator Context(GeoLocation context) => new Context(context);
	}

	internal class ContextFormatter : IJsonFormatter<Context>
	{
		public Context Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var formatter = formatterResolver.GetFormatter<Union<string, GeoLocation>>();
			var union = formatter.Deserialize(ref reader, formatterResolver);
			switch (union.Tag)
			{
				case 0:
					return new Context(union.Item1);
				case 1:
					return new Context(union.Item2);
				default:
					return null;
			}
		}

		public void Serialize(ref JsonWriter writer, Context value, IJsonFormatterResolver formatterResolver)
		{
			var formatter = formatterResolver.GetFormatter<Union<string, GeoLocation>>();
			formatter.Serialize(ref writer, value, formatterResolver);
		}
	}
}
