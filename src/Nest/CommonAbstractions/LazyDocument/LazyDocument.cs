using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest_5_2_0
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

		public T As<T>() where T : class
		{
			var jToken = this._Value;
			return jToken?.ToObject<T>();
		}
	}
}
