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
using System.Linq;
using System.Text;
using Elasticsearch.Net.Extensions;
using static Elasticsearch.Net.ResponseStatics;

namespace Elasticsearch.Net
{
	public class ElasticsearchClientException : Exception
	{
		public ElasticsearchClientException(string message) : base(message) => FailureReason = PipelineFailure.Unexpected;

		public ElasticsearchClientException(PipelineFailure failure, string message, Exception innerException)
			: base(message, innerException) => FailureReason = failure;

		public ElasticsearchClientException(PipelineFailure failure, string message, IApiCallDetails apiCall)
			: this(message)
		{
			Response = apiCall;
			FailureReason = failure;
			AuditTrail = apiCall?.AuditTrail;
		}

		public List<Audit> AuditTrail { get; internal set; }

		public string DebugInformation
		{
			get
			{
				var sb = new StringBuilder();
				var failureReason = FailureReason.GetStringValue();
				if (FailureReason == PipelineFailure.Unexpected && AuditTrail.HasAny())
					failureReason = "Unrecoverable/Unexpected " + AuditTrail.Last().Event.GetStringValue();

				sb.Append("# FailureReason: ")
					.Append(failureReason)
					.Append(" while attempting ");

				if (Request != null)
				{
					sb.Append(Request.Method.GetStringValue()).Append(" on ");
					if (Request.Uri != null)
						sb.AppendLine(Request.Uri.ToString());
					else
						sb.Append(Request.PathAndQuery)
							.AppendLine(" on an empty node, likely a node predicate on ConnectionSettings not matching ANY nodes");
				}
				else if (Response != null)
					sb.Append(Response.HttpMethod.GetStringValue())
						.Append(" on ")
						.AppendLine(Response.Uri.ToString());
				else
					sb.AppendLine("a request");

				if (Response != null)
					DebugInformationBuilder(Response, sb);
				else
				{
					DebugAuditTrail(AuditTrail, sb);
					DebugAuditTrailExceptions(AuditTrail, sb);
				}

				if (InnerException != null)
					sb.Append("# Inner Exception: ")
						.AppendLine(InnerException.Message)
						.AppendLine(InnerException.ToString());

				sb.AppendLine("# Exception:")
					.AppendLine(ToString());
				return sb.ToString();
			}
		}

		public PipelineFailure? FailureReason { get; }

		public RequestData Request { get; internal set; }

		public IApiCallDetails Response { get; internal set; }
	}
}
