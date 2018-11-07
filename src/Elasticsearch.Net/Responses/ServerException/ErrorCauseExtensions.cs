using System;
using System.Collections.Generic;

namespace Elasticsearch.Net
{
	internal static class ErrorCauseExtensions
	{
		public static void FillValues(this ErrorCause rootCause, IDictionary<string, object> dict)
		{
			if (dict == null) return;

			if (dict.TryGetValue("reason", out var reason)) rootCause.Reason = Convert.ToString(reason);
			if (dict.TryGetValue("type", out var type)) rootCause.Type = Convert.ToString(type);
			if (dict.TryGetValue("stack_trace", out var stackTrace)) rootCause.StackTrace = Convert.ToString(stackTrace);

//			if (dict.TryGetValue("index", out var index)) rootCause.Index = Convert.ToString(index);
//			if (dict.TryGetValue("resource.id", out var resourceId)) rootCause.ResourceId = Convert.ToString(resourceId);
//			if (dict.TryGetValue("resource.type", out var resourceType)) rootCause.ResourceType = Convert.ToString(resourceType);
		}
	}
}
