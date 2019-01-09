using System;
using Utf8Json;

namespace Nest
{
	/// <summary>
	/// A lazily deserialized document
	/// </summary>
	[JsonFormatter(typeof(LazyDocumentInterfaceFormatter))]
	public interface ILazyDocument
	{
		/// <summary>
		/// Creates an instance of <typeparamref name="T" /> from this
		/// <see cref="ILazyDocument" /> instance
		/// </summary>
		/// <typeparam name="T">The type</typeparam>
		T As<T>();

		/// <summary>
		/// Creates an instance of <paramref name="objectType" /> from this
		/// <see cref="ILazyDocument" /> instance
		/// </summary>
		/// <param name="objectType">The type</param>
		object As(Type objectType);
	}

	/// <inheritdoc />
	[JsonFormatter(typeof(LazyDocumentFormatter))]
	public class LazyDocument : ILazyDocument
	{
		private readonly IJsonFormatterResolver _formatterResolver;

		internal LazyDocument(byte[] bytes, IJsonFormatterResolver formatterResolver)
		{
			Bytes = bytes;
			_formatterResolver = formatterResolver;
		}

		internal byte[] Bytes { get; }

		/// <inheritdoc />
		public T As<T>()
		{
			var reader = new JsonReader(Bytes);
			var formatter = new SourceFormatter<T>();
			return formatter.Deserialize(ref reader, _formatterResolver);
		}

		/// <inheritdoc />
		public object As(Type objectType)
		{
			var reader = new JsonReader(Bytes);
			// TODO: Non generic SourceFormatter equivalent
			return JsonSerializer.NonGeneric.Deserialize(objectType, ref reader, _formatterResolver);
		}
	}
}
