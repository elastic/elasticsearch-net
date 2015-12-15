using System;
using System.Collections.Generic;
using System.Linq;

namespace Elasticsearch.Net
{
	public class ServerError
	{
		public Error Error { get; set; }
		public int Status { get; set; }

		internal static ServerError Create(IDictionary<string, object> dict, IJsonSerializerStrategy strategy)
		{
			var serverError = new ServerError
			{
				Status = Convert.ToInt32(dict["status"]),
				Error = (Error)strategy.DeserializeObject(dict["error"], typeof(Error))
			};
			return serverError;
		}
	}

	public interface IRootCause
	{
		string Type { get; set; }
		string Reason { get; set; }
		string ResourceId { get; set; }
		string ResourceType { get; set; }
		string Index { get; set; }
	}

	public class Error : IRootCause
	{
		public string Index { get; set; }
		public string Reason { get; set; }
		public string ResourceId { get; set; }
		public string ResourceType { get; set; }
		public string Type { get; set; }
		public List<RootCause> RootCause { get; set; }

		internal static Error Create(IDictionary<string, object> dict, IJsonSerializerStrategy strategy)
		{
			var error = new Error();
			error.FillValues(dict);
			var os = dict["root_cause"] as object[];
			if (os == null) return error;
			error.RootCause = os.Select(o => (RootCause)strategy.DeserializeObject(o, typeof(RootCause))).ToList();
			return error;
		}
	}

	public class RootCause : IRootCause
	{
		public string Index { get; set; }
		public string Reason { get; set; }
		public string ResourceId { get; set; }
		public string ResourceType { get; set; }
		public string Type { get; set; }

		internal static RootCause Create(IDictionary<string, object> dict, IJsonSerializerStrategy strategy)
		{
			var rootCause = new RootCause();
			rootCause.FillValues(dict);
			return rootCause;
		}
	}

	internal static class RootCauseExtensions
	{
		public static void FillValues(this IRootCause rootCause, IDictionary<string, object> dict)
		{
			rootCause.Index = Convert.ToString(dict["index"]);
			rootCause.Reason = Convert.ToString(dict["reason"]);
			rootCause.ResourceId = Convert.ToString(dict["resource.id"]);
			rootCause.ResourceType = Convert.ToString(dict["resource.type"]);
			rootCause.Type = Convert.ToString(dict["type"]);
		}
	}
}
