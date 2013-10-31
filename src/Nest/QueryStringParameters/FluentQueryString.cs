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
		internal readonly NameValueCollection NameValueCollection = new NameValueCollection();

		public T Add(string name, string value)
		{
			NameValueCollection.Add(name, value);
			return (T)this;
		}

		protected string CreateString(object s)
		{
			return RawElasticClient.Stringify(s);
		}

	}

	public class FluentQueryString : FluentQueryString<FluentQueryString>
	{

	}
}
