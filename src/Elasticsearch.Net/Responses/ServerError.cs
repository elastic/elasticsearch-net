using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
		public static async Task<ServerError> TryCreateAsync(Stream stream, CancellationToken token)
		{
			try { return await CreateAsync(stream, token).ConfigureAwait(false); }
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

		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.Append($"ServerError: {Status}");
			if (Error != null)
				sb.Append(Error);
			return sb.ToString();
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
		public CausedBy CausedBy { get; set; }

		internal static Error Create(IDictionary<string, object> dict, IJsonSerializerStrategy strategy)
		{
			var error = new Error();
			error.FillValues(dict);

			object causedBy;
			if (dict.TryGetValue("caused_by", out causedBy))
				error.CausedBy = (CausedBy) strategy.DeserializeObject(causedBy, typeof(CausedBy));

			object rootCause;
			if (!dict.TryGetValue("root_cause", out rootCause)) return error;

			var os = rootCause as object[];
			if (os == null) return error;
			error.RootCause = os.Select(o => (RootCause)strategy.DeserializeObject(o, typeof(RootCause))).ToList();
			return error;
		}

		public override string ToString() => CausedBy == null
			? $"Type: {this.Type} Reason: \"{this.Reason}\""
			: $"Type: {this.Type} Reason: \"{this.Reason}\" CausedBy: \"{this.CausedBy}\"";
	}

	public class CausedBy
	{
		public string Reason { get; set; }
		public string Type { get; set; }

		internal static CausedBy Create(IDictionary<string, object> dict, IJsonSerializerStrategy strategy)
		{
			var causedBy = new CausedBy();
			object reason;
			if (dict.TryGetValue("reason", out reason)) causedBy.Reason = Convert.ToString(reason);
			object type;
			if (dict.TryGetValue("type", out type)) causedBy.Type = Convert.ToString(type);
			return causedBy;
		}

		public override string ToString() => $"Type: {this.Type} Reason: \"{this.Reason}\"";
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

		public override string ToString() => $"Type: {this.Type} Reason: \"{this.Reason}\"";
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
