// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

 using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	[DataContract]
	public class ServerError
	{
		internal ServerError() {}

		public ServerError(Error error, int? statusCode)
		{
			Error = error;
			Status = statusCode.GetValueOrDefault(-1);
		}

		[DataMember(Name = "error")]
		public Error Error { get; internal set; }

		[DataMember(Name = "status")]
		public int Status { get; internal set; } = -1;

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

		// ReSharper disable once UnusedMember.Global
		public static Task<ServerError> CreateAsync(Stream stream, CancellationToken token = default) =>
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
}
