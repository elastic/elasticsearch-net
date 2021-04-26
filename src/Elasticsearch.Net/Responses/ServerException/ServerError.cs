/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
		public ServerError() { }

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
