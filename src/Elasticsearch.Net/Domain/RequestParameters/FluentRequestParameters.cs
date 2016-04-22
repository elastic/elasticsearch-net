using System;
using System.Collections.Generic;
using System.IO;

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
		private IRequestParameters Self => this;

		public abstract HttpMethod DefaultHttpMethod { get; }

		IDictionary<string, object> IRequestParameters.QueryString { get; set; }
		Func<IApiCallDetails, Stream, object> IRequestParameters.DeserializationOverride { get; set; }
		IRequestConfiguration IRequestParameters.RequestConfiguration { get; set; }

		protected FluentRequestParameters()
		{
			Self.QueryString = new Dictionary<string, object>();
		}

		void IRequestParameters.AddQueryStringValue(string name, object value)
		{
			if (value == null || name.IsNullOrEmpty()) return;
			Self.QueryString[name] = value;
		}

		public T AddQueryString(string name, object value)
		{
			Self.AddQueryStringValue(name, value);
			return (T)this;
		}

		public T RemoveQueryString(string name)
		{
			Self.QueryString.Remove(name);
			return (T)this;
		}

		public T RequestConfiguration(Func<RequestConfigurationDescriptor, IRequestConfiguration> selector)
		{
			Self.RequestConfiguration = selector?.Invoke(new RequestConfigurationDescriptor(Self.RequestConfiguration)) ?? Self.RequestConfiguration;
			return (T)this;
		}

		public T DeserializationOverride(Func<IApiCallDetails, Stream, object> customResponseCreator)
		{
			Self.DeserializationOverride = customResponseCreator;
			return (T)this;
		}

		public bool ContainsKey(string name)
		{
			return Self.QueryString != null && Self.QueryString.ContainsKey(name);
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
