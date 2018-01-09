using System;
using System.Collections.Generic;
using System.IO;
using static Elasticsearch.Net.ElasticsearchUrlFormatter;

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

		public Func<IApiCallDetails, Stream, object> DeserializationOverride { get; set; }
		public IRequestConfiguration RequestConfiguration { get; set; }

		public abstract HttpMethod DefaultHttpMethod { get; }
		public Dictionary<string, object> QueryString { get; set; } = new Dictionary<string, object>();

		//These exists solely so the generated code can call these shortened methods
		protected TOut Q<TOut>(string name) => this.GetQueryStringValue<TOut>(name);
		protected void Q(string name, object value) => this.SetQueryString(name, value);

		/// <inheritdoc />
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
		/// <inheritdoc />
		public bool ContainsQueryString(string name) => Self.QueryString != null && Self.QueryString.ContainsKey(name);

		/// <inheritdoc />
		public TOut GetQueryStringValue<TOut>(string name)
		{
			if (!this.ContainsQueryString(name))
				return default(TOut);
			var value = Self.QueryString[name];
			if (value == null)
				return default(TOut);
			return (TOut)value;
		}

		/// <inheritdoc />
		public string GetResolvedQueryStringValue(string n, IConnectionConfigurationValues s) =>
			CreateString(GetQueryStringValue<object>(n), s);
	}

}
