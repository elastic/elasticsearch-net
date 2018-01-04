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
	public abstract class RequestParameters<T> : IRequestParameters where T : RequestParameters<T>
	{
		private IRequestParameters Self => this;

		IDictionary<string, object> IRequestParameters.QueryString { get; set; }
		Func<IApiCallDetails, Stream, object> IRequestParameters.DeserializationOverride { get; set; }
		IRequestConfiguration IRequestParameters.RequestConfiguration { get; set; }

		public abstract HttpMethod DefaultHttpMethod { get; }

		protected RequestParameters()
		{
			Self.QueryString = new Dictionary<string, object>();
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

		public void SetQueryString(string name, object value)
		{
			if (value == null) this.RemoveQueryString(name);
			else Self.QueryString[name] = value;
		}
		private void RemoveQueryString(string name)
		{
			if (!Self.QueryString.ContainsKey(name)) return;
			Self.QueryString.Remove(name);
		}

		protected TOut Q<TOut>(string name) => this.GetQueryStringValue<TOut>(name);

		protected void Q(string name, object value) => this.SetQueryString(name, value);

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
