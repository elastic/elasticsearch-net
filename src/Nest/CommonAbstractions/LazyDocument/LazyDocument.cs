using System;
using Utf8Json;

namespace Nest
{
	/// <summary>
	/// A lazily deserialized document
	/// </summary>
	[JsonFormatter(typeof(LazyDocumentJsonFormatter))]
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
	public class LazyDocument : ILazyDocument
	{
		private readonly IJsonFormatterResolver _formatterResolver;

		internal LazyDocument(ArraySegment<byte> arraySegment, IJsonFormatterResolver formatterResolver)
		{
			ArraySegment = arraySegment;
			_formatterResolver = formatterResolver;
		}

		internal ArraySegment<byte> ArraySegment { get; }

		/// <inheritdoc />
		public T As<T>()
		{
			var reader = new JsonReader(ArraySegment.Array, ArraySegment.Offset);
			var formatter = new SourceFormatter<T>();
			return formatter.Deserialize(ref reader, _formatterResolver);
		}

		/// <inheritdoc />
		public object As(Type objectType)
		{
			var reader = new JsonReader(ArraySegment.Array, ArraySegment.Offset);
			// TODO: Non generic SourceFormatter equivalent
			return JsonSerializer.NonGeneric.Deserialize(objectType, ref reader, _formatterResolver);
		}
	}
}
