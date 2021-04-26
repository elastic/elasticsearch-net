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

using Elastic.Transport;
using Elastic.Transport.Products.Elasticsearch;

namespace Nest
{
	public class NestElasticsearchProductRegistration : ElasticsearchProductRegistration
	{
		public static NestElasticsearchProductRegistration DefaultForNest { get; } = new NestElasticsearchProductRegistration();

		/// <summary>
		/// NEST handles 404 in its <see cref="ResponseBase.IsValid"/>, we do not want the low level client throwing exceptions
		/// when <see cref="ITransportConfiguration.ThrowExceptions"/> is enabled for 404's. The client is in charge of composing paths
		/// so a 404 never signals a wrong url but a missing entity.
		/// </summary>
		public override bool HttpStatusCodeClassifier(HttpMethod method, int statusCode) =>
			statusCode >= 200 && statusCode < 300
			|| statusCode == 404;


		/// <summary>
		/// Makes the low level transport aware of NEST's <see cref="ResponseBase"/>
		/// So that it can peek in to its exposed error when reporting failures
		/// </summary>
		public override bool TryGetServerErrorReason<TResponse>(TResponse response, out string reason)
		{
			if (response is ResponseBase r)
			{
				reason = r.ServerError?.Error?.ToString();
				return !reason.IsNullOrEmpty();
			}
			return base.TryGetServerErrorReason(response, out reason);
		}
	}
}
