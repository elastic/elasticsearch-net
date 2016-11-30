using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Json converter that deserializes into an <see cref="ReadOnlyCollection{TInterface}"/> or <see cref="IReadOnlyCollection{TInterface}"/>
	/// where <typeparamref name="TInterface"/> does not have a custom deserializer and should be deserialized
	/// into concrete types of <typeparamref name="TDocument"/>
	/// </summary>
	/// <remarks>
	/// <typeparamref name="TInterface"/> may not have a deserializer for valid reasons, for example, an interface may be implemented by two
	/// concrete types that need to be deserialized. In this case, a deserializer would not know which concrete type to deserialize to in
	/// a given context.
	/// </remarks>
	/// <typeparam name="TDocument">The concrete type to deserialize</typeparam>
	/// <typeparam name="TInterface">The interface for the deserialized readonly collection</typeparam>
	internal class ReadOnlyCollectionJsonConverter<TDocument, TInterface> : JsonConverter
		where TDocument : TInterface
		where TInterface : class
	{
		public override bool CanWrite => false;
		public override bool CanRead => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.StartArray)
				return EmptyReadOnly<TInterface>.Collection;

			var list = new List<TInterface>();
			reader.Read();
			while (reader.TokenType != JsonToken.EndArray)
			{
				var item = serializer.Deserialize<TDocument>(reader);
				list.Add(item);
				reader.Read();
			}

			return new ReadOnlyCollection<TInterface>(list);
		}

		public override bool CanConvert(Type objectType) => true;
	}
}
