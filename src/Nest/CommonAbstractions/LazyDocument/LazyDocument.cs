using System;
using System.IO;
using System.Text;
using Elasticsearch.Net;
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
		internal JToken Token { get; }

		private readonly IElasticsearchSerializer _serializer;

		internal LazyDocument(JToken token, IElasticsearchSerializer serializer)
		{
			Token = token;
			_serializer = serializer;
		}

		/// <inheritdoc />
		public T As<T>() where T : class
		{
			if (Token == null) return null;
			using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(Token.ToString())))
				return _serializer.Deserialize<T>(ms);
		}

		/// <inheritdoc />
		public object As(Type objectType)
		{
			if (Token == null) return null;
			using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(Token.ToString())))
				return _serializer.Deserialize(objectType, ms);
		}
	}
}
