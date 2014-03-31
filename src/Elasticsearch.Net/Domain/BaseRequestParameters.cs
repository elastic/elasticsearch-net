using System.Collections.Generic;
using System.Collections.Specialized;
using Elasticsearch.Net.Connection;

namespace Elasticsearch.Net
{
	public abstract class BaseRequestParameters : IRequestParameters
	{
		internal readonly IDictionary<string, object> _QueryStringDictionary = new Dictionary<string, object>();
		
		internal object _DeserializationState = null;

		internal IRequestConfiguration _RequestConfiguration = null;
		internal NameValueCollection _queryString;

		NameValueCollection IRequestParameters.QueryString { get { return _queryString;}  }

		object IRequestParameters.DeserializationState
		{
			get { return _DeserializationState; }
		}

		IRequestConfiguration IRequestParameters.RequestConfiguration
		{
			get { return _RequestConfiguration; }
		}
	}
}