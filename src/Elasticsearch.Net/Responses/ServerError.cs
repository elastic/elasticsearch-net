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
		public ServerError(Error error, int? statusCode)
		{
			this.Error = error;
			this.Status = statusCode.GetValueOrDefault();
		}

		public Error Error { get; private set; }
		public int Status { get; private set; }

		public static ServerError Create(Stream stream) =>
			LowLevelRequestResponseSerializer.Instance.Deserialize<ServerError>(stream);

		public static Task<ServerError> CreateAsync(Stream stream, CancellationToken token) =>
			LowLevelRequestResponseSerializer.Instance.DeserializeAsync<ServerError>(stream, token);

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
			var statusCode = -1;

			if (dict.TryGetValue("status", out var status))
				statusCode = Convert.ToInt32(status);

			if (!dict.TryGetValue("error", out var error)) return null;
			Error err;
			if (error is string s)
			{
				err = new Error {Reason = s};
			}
			else err = (Error) strategy.DeserializeObject(error, typeof(Error));

			return new ServerError(err, statusCode);
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
		public IReadOnlyCollection<RootCause> RootCause { get; set; }
		public CausedBy CausedBy { get; set; }

		internal static Error Create(IDictionary<string, object> dict, IJsonSerializerStrategy strategy)
		{
			var error = new Error();
			error.FillValues(dict);

			if (dict.TryGetValue("caused_by", out var causedBy))
				error.CausedBy = (CausedBy) strategy.DeserializeObject(causedBy, typeof(CausedBy));

			if (!dict.TryGetValue("root_cause", out var rootCause)) return error;

			if (!(rootCause is object[] os)) return error;
			error.RootCause = os.Select(o => (RootCause)strategy.DeserializeObject(o, typeof(RootCause))).ToList().AsReadOnly();
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
		public CausedBy InnerCausedBy { get; set; }

		internal static CausedBy Create(IDictionary<string, object> dict, IJsonSerializerStrategy strategy)
		{
			var causedBy = new CausedBy();
			if (dict.TryGetValue("reason", out var reason)) causedBy.Reason = Convert.ToString(reason);
			if (dict.TryGetValue("type", out var type)) causedBy.Type = Convert.ToString(type);
			if (dict.TryGetValue("caused_by", out var innerCausedBy))
				causedBy.InnerCausedBy = (CausedBy)strategy.DeserializeObject(innerCausedBy, typeof(CausedBy));

			return causedBy;
		}

		public override string ToString() => this.InnerCausedBy == null
			? $"Type: {this.Type} Reason: \"{this.Reason}\""
			: $"Type: {this.Type} Reason: \"{this.Reason}\" CausedBy: \"{this.InnerCausedBy}\"";
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
			if (dict == null) return;
			if (dict.TryGetValue("index", out var index)) rootCause.Index = Convert.ToString(index);
			if (dict.TryGetValue("reason", out var reason)) rootCause.Reason = Convert.ToString(reason);
			if (dict.TryGetValue("resource.id", out var resourceId)) rootCause.ResourceId = Convert.ToString(resourceId);
			if (dict.TryGetValue("resource.type", out var resourceType)) rootCause.ResourceType = Convert.ToString(resourceType);
			if (dict.TryGetValue("type", out var type)) rootCause.Type = Convert.ToString(type);
		}
	}
}
