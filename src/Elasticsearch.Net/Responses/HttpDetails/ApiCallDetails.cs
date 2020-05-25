// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Elasticsearch.Net.Extensions;

namespace Elasticsearch.Net
{
	public class ApiCallDetails : IApiCallDetails
	{
		public List<Audit> AuditTrail { get; set; }

		public string DebugInformation
		{
			get
			{
				var sb = new System.Text.StringBuilder();
				sb.AppendLine(ToString());
				return ResponseStatics.DebugInformationBuilder(this, sb);
			}
		}

		public IEnumerable<string> DeprecationWarnings { get; set; }
		public HttpMethod HttpMethod { get; set; }
		public int? HttpStatusCode { get; set; }
		public Exception OriginalException { get; set; }
		public byte[] RequestBodyInBytes { get; set; }
		public byte[] ResponseBodyInBytes { get; set; }
		public string ResponseMimeType { get; set; }
		public bool Success { get; set; }

		public bool SuccessOrKnownError =>
			Success || HttpStatusCode >= 400 && HttpStatusCode < 599
			&& HttpStatusCode != 504 //Gateway timeout needs to be retried
			&& HttpStatusCode != 503 //service unavailable needs to be retried
			&& HttpStatusCode != 502;

		public Uri Uri { get; set; }

		public IConnectionConfigurationValues ConnectionConfiguration { get; set; }

		public override string ToString() =>
			$"{(Success ? "S" : "Uns")}uccessful ({HttpStatusCode}) low level call on {EnumExtensions.GetStringValue(HttpMethod)}: {Uri.PathAndQuery}";
	}
}
