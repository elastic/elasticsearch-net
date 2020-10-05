// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Elasticsearch.Net
{
	public class BytesResponse : ElasticsearchResponse<byte[]>
	{
		public BytesResponse() { }

		public BytesResponse(byte[] body) => Body = body;

		public override bool TryGetServerError(out ServerError serverError)
		{
			serverError = null;
			if (Body == null || Body.Length == 0 || ResponseMimeType != RequestData.MimeType)
				return false;

			using(var stream = ConnectionConfiguration.MemoryStreamFactory.Create(Body))
				return ServerError.TryCreate(stream, out serverError);
		}
	}
}
