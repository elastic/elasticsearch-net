using System;

namespace Nest
{
	public interface IDocument
	{
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		T OfType<T>() where T : class;
	}

	public class Document : IDocument
	{
		private Lazy<object> _value;

		public Document(Func deserializer)
		{
			_value = new Lazy<object>(deserializer);
		}

		public T OfType<T>() where T : class
		{
			return this._value.Value as T;
		}
	}
}