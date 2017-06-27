using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	[JsonConverter(typeof(LazyDocumentJsonConverter))]
	public interface ILazyDocument
	{
		/// <summary>
		///
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		T As<T>() where T : class;
	}

	public class LazyDocument : ILazyDocument
	{
		internal JToken _Value { get; set; }
		internal JsonSerializer _Serializer { get; set; }

		public T As<T>() where T : class
		{
			var jToken = this._Value;
			return jToken?.ToObject<T>(_Serializer);
		}
	}
}
