using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utf8Json;

namespace Elasticsearch.Net
{
	[JsonFormatter(typeof(ServerErrorFormatter))]
	public class ServerError
	{
		public ServerError(Error error, int? statusCode)
		{
			Error = error;
			Status = statusCode.GetValueOrDefault();
		}

		[DataMember(Name = "error")]
		public Error Error { get; }

		[DataMember(Name = "status")]
		public int Status { get; }

		public static bool TryCreate(Stream stream, out ServerError serverError)
		{
			try
			{
				serverError = Create(stream);
				return true;
			}
			catch
			{
				serverError = null;
				return false;
			}
		}

		public static ServerError Create(Stream stream) =>
			LowLevelRequestResponseSerializer.Instance.Deserialize<ServerError>(stream);

		// TODO: make token default parameter in 7.x
		public static Task<ServerError> CreateAsync(Stream stream, CancellationToken token) =>
			LowLevelRequestResponseSerializer.Instance.DeserializeAsync<ServerError>(stream, token);

		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.Append($"ServerError: {Status}");
			if (Error != null)
				sb.Append(Error);
			return sb.ToString();
		}
	}

	internal class ServerErrorFormatter : IJsonFormatter<ServerError>
	{
		public void Serialize(ref JsonWriter writer, ServerError value, IJsonFormatterResolver formatterResolver) =>
			throw new NotSupportedException();

		// TODO: Optimize this
		public ServerError Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var formatter = formatterResolver.GetFormatter<Dictionary<string, object>>();
			var dict = formatter.Deserialize(ref reader, formatterResolver);

			var statusCode = -1;

			if (dict.TryGetValue("status", out var status))
				statusCode = Convert.ToInt32(status);

			if (!dict.TryGetValue("error", out var error)) return null;

			var err = formatterResolver.ReserializeAndDeserialize<Error>(error);

			return new ServerError(err, statusCode);
		}
	}
}
