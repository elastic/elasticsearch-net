// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Text;

namespace Elastic.Transport
{
	public class StringResponse : TransportResponseBase<string>
	{
		public StringResponse() { }

		public StringResponse(string body) => Body = body;

		public override bool TryGetServerError(out ServerError serverError)
		{
			serverError = null;
			if (string.IsNullOrEmpty(Body) || ResponseMimeType != RequestData.MimeType)
				return false;

			using(var stream = ConnectionConfiguration.MemoryStreamFactory.Create(Encoding.UTF8.GetBytes(Body)))
				return ServerError.TryCreate(stream, out serverError);
		}
	}
}
