// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest.Utf8Json;
namespace Nest
{
	internal class ConcreteBulkIndexResponseItemFormatter<T> : IJsonFormatter<T>
		where T : BulkResponseItemBase
	{
		public void Serialize(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver) =>
			throw new System.NotImplementedException();

		public T Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var formatter = DynamicObjectResolver.AllowPrivateExcludeNullCamelCase.GetFormatter<T>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}
	}
}
