using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Elasticsearch.Net;

namespace Nest
{
	internal partial class LowLevelDispatch
	{
		protected IElasticLowLevelClient _lowLevel;

		public LowLevelDispatch(IElasticLowLevelClient rawElasticClient)
		{
			this._lowLevel = rawElasticClient;
		}

		internal bool AllSetNoFallback(params string[] pathVariables) => pathVariables.All(p => !p.IsNullOrEmpty());

		internal bool AllSet(params string[] pathVariables) => pathVariables.All(p => !p.IsNullOrEmpty()) && !pathVariables.All(p => p == "_all");

		internal static Exception InvalidDispatch(string apiCall, IRequest provided, HttpMethod[] methods, params string[] endpoints)
		{
			var sb = new StringBuilder();
			sb.AppendLine($"Dispatching {apiCall}() from NEST into to Elasticsearch.NET failed");
			sb.AppendLine($"Received a request marked as {provided.HttpMethod.GetStringValue()}");
			sb.AppendLine($"This endpoint accepts {string.Join(",", methods.Select(p=>p.GetStringValue()))}");
			sb.AppendLine($"The request might not have enough information provided to make any of these endpoints:");
			foreach (var endpoint in endpoints)
				sb.AppendLine($"  - {PrettyPrintEndpoint(provided, endpoint)}");

			return new ArgumentException(sb.ToString());
		}

		private static readonly Regex ReplaceParams = new Regex(@"\{(.+?)\}");

		internal static string PrettyPrintEndpoint(IRequest request, string endpoint)
		{
			var pretty = ReplaceParams.Replace(endpoint, (m) =>
			{
				var key = m.Groups[1].Value.ToLowerInvariant();
				switch(key)
				{
					case "index" :  return PrettyParam(key, request.RouteValues.Index);
					case "name" :  return PrettyParam(key, request.RouteValues.Name);
					case "feature" :  return PrettyParam(key, request.RouteValues.Feature);
					case "field" :  return PrettyParam(key, request.RouteValues.Field);
					case "fields" :  return PrettyParam(key, request.RouteValues.Fields);
					case "id" :  return PrettyParam(key, request.RouteValues.Id);
					case "index_metric" :  return PrettyParam(key, request.RouteValues.IndexMetric);
					case "lang" :  return PrettyParam(key, request.RouteValues.Lang);
					case "metric" :  return PrettyParam(key, request.RouteValues.Metric);
					case "node_id" :  return PrettyParam(key, request.RouteValues.NodeId);
					case "repository" :  return PrettyParam(key, request.RouteValues.Repository);
					case "scroll_id" :  return PrettyParam(key, request.RouteValues.ScrollId);
					case "snapshot" :  return PrettyParam(key, request.RouteValues.Snapshot);
					case "type" :  return PrettyParam(key, request.RouteValues.Type);
					default:  return PrettyParam(key, "<Unknown route variable>");
				}
			});
			return pretty;
		}

		private static string PrettyParam(string key, string value) => $"{{{key}={(value.IsNullOrEmpty() ? "<NULL>" : value)}}}";

	}
}
