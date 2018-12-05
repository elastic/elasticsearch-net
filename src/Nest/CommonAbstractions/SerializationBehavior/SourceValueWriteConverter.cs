using System;
using Elasticsearch.Net;
using System.Runtime.Serialization;
using System.Xml;
using Utf8Json;

namespace Nest
{
	internal class SourceWriteFormatter<T> : SourceFormatter<T>
	{
		public override void Serialize(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver)
		{
			var nestType = value.GetType().Assembly() == typeof(SourceWriteFormatter<>).Assembly();
			if (nestType)
				formatterResolver.GetFormatter<T>().Serialize(ref writer, value, formatterResolver);
			else
				base.Serialize(ref writer, value, formatterResolver);
		}
	}
}
