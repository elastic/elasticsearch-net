using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	[JsonConverter(typeof(LazyDocumentJsonConverter))]
	public interface ILazyDocument
	{
		/// <summary>
		/// Creates an instance of <typeparamref name="T"/> from this
		/// <see cref="ILazyDocument"/> instance
		/// </summary>
		/// <typeparam name="T">The type</typeparam>
		T As<T>() where T : class;

		/// <summary>
		/// Creates an instance of <paramref name="objectType"/> from this
		/// <see cref="ILazyDocument"/> instance
		/// </summary>
		/// <param name="objectType">The type</param>
		object As(Type objectType);
	}

	public class LazyDocument : ILazyDocument
	{
		internal JToken _Value { get; set; }
		internal JsonSerializer _Serializer { get; set; }

		/// <inheritdoc />
		public T As<T>() where T : class
		{
			var jToken = this._Value;
			return jToken?.ToObject<T>(_Serializer);
		}

		/// <inheritdoc />
		public object As(Type objectType)
		{
			var jToken = this._Value;
			return jToken?.ToObject(objectType, _Serializer);
		}
	}
}
