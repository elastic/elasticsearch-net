using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Connection.Configuration;

namespace Elasticsearch.Net
{

	/// <summary>
	/// Used by the raw client to compose querystring parameters in a matter that still exposes some xmldocs
	/// You can always pass a simple NameValueCollection if you want.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class FluentRequestParameters<T> : IRequestParameters 
		where T : FluentRequestParameters<T>
	{

		private IRequestParameters Self { get { return this; } }

		IDictionary<string, object> IRequestParameters.QueryString { get; set; }
		Func<IElasticsearchResponse, Stream, object> IRequestParameters.DeserializationState { get; set; }
		IRequestConfiguration IRequestParameters.RequestConfiguration { get; set; }

		public FluentRequestParameters()
		{
			Self.QueryString = new Dictionary<string, object>();
		}


		public T CopyQueryStringValuesFrom(IRequestParameters requestParameters)
		{
			var from = requestParameters.QueryString;
			foreach (var k in from.Keys)
				Self.QueryString[k] = from[k];
			return (T)this;
		}

		public T AddQueryString(string name, object value)
		{
			Self.QueryString[name] = value;
			return (T)this;
		}

		public T RequestConfiguration(Func<IRequestConfiguration, RequestConfigurationDescriptor> selector)
		{
			selector.ThrowIfNull("selector");
			Self.RequestConfiguration = selector(Self.RequestConfiguration ?? new RequestConfigurationDescriptor());
			return (T)this;
		}
		
		public T DeserializationState(Func<IElasticsearchResponse, Stream, object> customResponseCreator)
		{
			Self.DeserializationState = customResponseCreator;
			return (T)this;
		}

		public bool ContainsKey(string name)
		{
			return Self.QueryString != null && Self.QueryString.ContainsKey(name);
		}

		public T RemoveQueryString(string name)
		{
			Self.QueryString.Remove(name);
			return (T)this;
		}

		public TOut GetQueryStringValue<TOut>(string name)
		{
			if (!this.ContainsKey(name))
				return default(TOut);
			var value = Self.QueryString[name];
			if (value == null)
				return default(TOut);
			return (TOut)value;
		}

	}

}
