using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Shared.Extensions;
using Elasticsearch.Net.Connection;

namespace Elasticsearch.Net
{
	/// <summary>
	/// Used by the raw client to compose querystring parameters in a matter that still exposes some xmldocs
	/// You can always pass a simple NameValueCollection if you want.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class FluentRequestParameters<T> : BaseRequestParameters 
		where T : FluentRequestParameters<T>
	{

		public T CopyQueryStringValuesFrom(BaseRequestParameters requestParameters)
		{
			var from = requestParameters._QueryStringDictionary;
			foreach (var k in from.Keys)
				this._QueryStringDictionary[k] = from[k];
			return (T)this;
		}

		public T AddQueryString(string name, object value)
		{
			this._QueryStringDictionary[name] = value;
			return (T)this;
		}

		public T RequestConfiguration(Func<RequestConfiguration, RequestConfiguration> selector)
		{
			selector.ThrowIfNull("selector");
			this._RequestConfiguration = selector(this._RequestConfiguration ?? new RequestConfiguration());
			return (T)this;
		}
		
		public T DeserializationState(object customObjectCreator)
		{
			_DeserializationState = customObjectCreator;
			return (T)this;
		}

		public bool ContainsKey(string name)
		{
			return this._QueryStringDictionary != null && this._QueryStringDictionary.ContainsKey(name);
		}

		public T RemoveQueryString(string name)
		{
			this._QueryStringDictionary.Remove(name);
			return (T)this;
		}

		public TOut GetQueryStringValue<TOut>(string name)
		{
			if (!this.ContainsKey(name))
				return default(TOut);
			var value = this._QueryStringDictionary[name];
			if (value == null)
				return default(TOut);
			return (TOut)value;
		}

	}

	public class FluentRequestParameters : FluentRequestParameters<FluentRequestParameters>
	{

	}
}
