using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public class ServerError
	{
		public Error Error { get; set; }
		public int Status { get; set; }

		public static ServerError Create(Stream stream) => ElasticsearchDefaultSerializer.Instance.Deserialize<ServerError>(stream);
		public static Task<ServerError> CreateAsync(Stream stream, CancellationToken token) => 
			ElasticsearchDefaultSerializer.Instance.DeserializeAsync<ServerError>(stream, token);

		/// <summary>
		/// Creating the server error might fail in cases where a proxy returns an http response which is not json at all
		/// </summary>
		public static bool TryCreate(Stream stream, out ServerError serverError)
		{
			serverError = null;
			try { serverError = Create(stream); }
			catch { return false; }
			return true;
		}

		/// <summary>
		/// Creating the server error might fail in cases where a proxy returns an http response which is not json at all
		/// </summary>
		public static Task<ServerError> TryCreateAsync(Stream stream, CancellationToken token)
		{
			try { return CreateAsync(stream, token); }
			catch { // ignored
			}
			return null;
		}

		internal static ServerError Create(IDictionary<string, object> dict, IJsonSerializerStrategy strategy)
		{
			object status, error;
			int statusCode = -1;
			if (dict.TryGetValue("status", out status))
				statusCode = Convert.ToInt32(status);

			if (!dict.TryGetValue("error", out error)) return null;

			return new ServerError
			{
				Status = statusCode,
				Error = (Error)strategy.DeserializeObject(error, typeof(Error))
			};
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
			object rootCause;
			if (!dict.TryGetValue("root_cause", out rootCause)) return error;

			var os = rootCause as object[];
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
			object index;
			if (dict.TryGetValue("index", out index)) rootCause.Index = Convert.ToString(index);
			object reason;
			if (dict.TryGetValue("reason", out reason)) rootCause.Reason = Convert.ToString(reason);
			object resourceId;
			if (dict.TryGetValue("resource.id", out resourceId)) rootCause.ResourceId = Convert.ToString(resourceId);
			object resourceType;
			if (dict.TryGetValue("resource.type", out resourceType)) rootCause.ResourceType = Convert.ToString(resourceType);
			object type;
			if (dict.TryGetValue("type", out type)) rootCause.Type = Convert.ToString(type);
		}
	}
}
