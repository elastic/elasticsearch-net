using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Nest.QueryStringParameters
{
	/// <summary>
	/// Used by the raw client to compose querystring parameters in a matter that still exposes some xmldocs
	/// You can always pass a simple NameValueCollection if you want.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class FluentQueryString<T> : NameValueCollection where T : FluentQueryString<T>
	{
		 public new T Add(string name, string value)
		 {
			 base.Add(name, value);
			 return (T)this;
		 }
	}
}
