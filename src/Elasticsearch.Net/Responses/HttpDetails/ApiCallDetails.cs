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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.NetworkInformation;
using System.Text;
using Elasticsearch.Net.Diagnostics;

namespace Elasticsearch.Net
{
	public class ApiCallDetails : IApiCallDetails
	{
		private string _debugInformation;

		public List<Audit> AuditTrail { get; set; }
		public ReadOnlyDictionary<string, ThreadPoolStatistics> ThreadPoolStats { get; set; }
		public ReadOnlyDictionary<TcpState, int> TcpStats { get; set; }

		public string DebugInformation
		{
			get
			{
				if (_debugInformation != null)
					return _debugInformation;

				var sb = new StringBuilder();
				sb.AppendLine(ToString());
				_debugInformation = ResponseStatics.DebugInformationBuilder(this, sb);

				return _debugInformation;
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
			$"{(Success ? "S" : "Uns")}uccessful ({HttpStatusCode}) low level call on {HttpMethod.GetStringValue()}: {Uri.PathAndQuery}";
	}
}
