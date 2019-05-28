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
		public abstract HttpMethod DefaultHttpMethod { get; }

		public Func<IApiCallDetails, Stream, object> DeserializationOverride { get; set; }
		public Dictionary<string, object> QueryString { get; set; } = new Dictionary<string, object>();
		public IRequestConfiguration RequestConfiguration { get; set; }
		private IRequestParameters Self => this;

		/// <inheritdoc />
		public bool ContainsQueryString(string name) => Self.QueryString != null && Self.QueryString.ContainsKey(name);

		/// <inheritdoc />
		public TOut GetQueryStringValue<TOut>(string name)
		{
			if (!ContainsQueryString(name))
				return default(TOut);

			var value = Self.QueryString[name];
			if (value == null)
				return default(TOut);

			return (TOut)value;
		}

		/// <inheritdoc />
		public string GetResolvedQueryStringValue(string n, IConnectionConfigurationValues s) =>
			CreateString(GetQueryStringValue<object>(n), s);

		/// <inheritdoc />
		public void SetQueryString(string name, object value)
		{
			if (value == null) RemoveQueryString(name);
			else Self.QueryString[name] = value;
		}

		//These exists solely so the generated code can call these shortened methods
		protected TOut Q<TOut>(string name) => GetQueryStringValue<TOut>(name);

		protected void Q(string name, object value) => SetQueryString(name, value);

		private void RemoveQueryString(string name)
		{
			if (!Self.QueryString.ContainsKey(name)) return;

			Self.QueryString.Remove(name);
		}
	}
}
