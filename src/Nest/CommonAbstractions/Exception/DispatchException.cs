using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// Occurs when an IElasticClient call does not have 
	/// enough information to dispatch into the raw client.
	/// </summary>
	[Serializable]
	public class DispatchException : System.Exception
	{
		public RouteValues Provided { get; }

		public DispatchException(string msg, RouteValues provided) : base(msg)
		{
			Provided = provided;
		}

		public static DispatchException InvalidDispatch(string apiCall, IRequest provided, HttpMethod[] methods, params string[] endpoints)
		{
			var sb = new StringBuilder();
			sb.AppendLine($"Dispatching {apiCall}() from NEST into to Elasticsearch.NET failed");
			sb.AppendLine($"Recieved a request marked as ${provided.HttpMethod.GetStringValue()}");
			sb.AppendLine($"This endpoint accepts ${string.Join(",", methods.Select(p=>p.GetStringValue()))}");
			sb.AppendLine($"The request might not have enough information provided to make any of these endpoints:");
			foreach (var endpoint in endpoints)
				sb.AppendLine($"  - {endpoint}");
			return new DispatchException(sb.ToString(), provided.RouteValues);
		}

	}
}