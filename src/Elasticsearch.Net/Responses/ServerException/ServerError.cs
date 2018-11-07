using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public class ServerError
	{
		public ServerError(Error error, int? statusCode)
		{
			Error = error;
			Status = statusCode.GetValueOrDefault();
		}

		public Error Error { get; }
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

		internal static ServerError Create(IDictionary<string, object> dict, IJsonSerializerStrategy strategy)
		{
			var statusCode = -1;

			if (dict.TryGetValue("status", out var status))
				statusCode = Convert.ToInt32(status);

			if (!dict.TryGetValue("error", out var error)) return null;

			Error err;
			if (error is string s)
				err = new Error { Reason = s };
			else err = (Error)strategy.DeserializeObject(error, typeof(Error));

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
}
