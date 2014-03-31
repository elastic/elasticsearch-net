using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Elasticsearch.Net.Connection;

namespace Elasticsearch.Net
{
	
	public interface IRequestParameters
	{
		NameValueCollection QueryString { get; }
		object DeserializationState { get; }
		IConnectionConfigurationOverrides RequestConfiguration { get; }

	}

	public abstract class BaseRequestParameters : IRequestParameters
	{
		internal readonly IDictionary<string, object> _QueryStringDictionary = new Dictionary<string, object>();
		
		internal object _DeserializationState = null;

		internal IConnectionConfigurationOverrides _RequestConfiguration = null;
		internal NameValueCollection _queryString;

		NameValueCollection IRequestParameters.QueryString { get { return _queryString;}  }

		object IRequestParameters.DeserializationState
		{
			get { return _DeserializationState; }
		}

		IConnectionConfigurationOverrides IRequestParameters.RequestConfiguration
		{
			get { return _RequestConfiguration; }
		}
	}

	/// <summary>
	/// Used by the raw client to compose querystring parameters in a matter that still exposes some xmldocs
	/// You can always pass a simple NameValueCollection if you want.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class FluentRequestParameters<T> : BaseRequestParameters 
		where T : FluentRequestParameters<T>
	{

		public T Add(string name, object value)
		{
			this._QueryStringDictionary[name] = value;
			return (T)this;
		}

		public T RequestConfiguration(IConnectionConfigurationOverrides requestConfiguration)
		{
			this._RequestConfiguration = requestConfiguration;
			return (T)this;
		}
		public T DeserializationState(object deserializationState)
		{
			_DeserializationState = deserializationState;
			return (T)this;
		}

		public bool ContainsKey(string name)
		{
			return this._QueryStringDictionary != null && this._QueryStringDictionary.ContainsKey(name);
		}

	}

	public class FluentRequestParameters : FluentRequestParameters<FluentRequestParameters>
	{

	}
}
