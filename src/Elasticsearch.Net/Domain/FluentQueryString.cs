using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Nest
{
	/// <summary>
	/// Used by the raw client to compose querystring parameters in a matter that still exposes some xmldocs
	/// You can always pass a simple NameValueCollection if you want.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class FluentQueryString<T> where T : FluentQueryString<T>
	{
		internal readonly IDictionary<string, object> _QueryStringDictionary = new Dictionary<string, object>();

		public T Add(string name, object value)
		{
			_QueryStringDictionary[name] = value;
			return (T)this;
		}

		public bool ContainsKey(string name)
		{
			return this._QueryStringDictionary != null && this._QueryStringDictionary.ContainsKey(name);
		}

	}

	public class FluentQueryString : FluentQueryString<FluentQueryString>
	{

	}
}
