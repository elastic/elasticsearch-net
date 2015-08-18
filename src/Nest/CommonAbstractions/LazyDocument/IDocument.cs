using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	[JsonConverter(typeof(LazyDocumentJsonConverter))]
	public interface IDocument
	{
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		T As<T>() where T : class;
	}

	public class Document : IDocument
	{
		internal JToken _Value { get; set; }

		public T As<T>() where T : class
		{
			var jToken = this._Value;
			return jToken != null ? jToken.ToObject<T>() : null;
		}
	}
}