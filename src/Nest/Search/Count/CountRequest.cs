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
using Elasticsearch.Net;
using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("count.json")]
	[ReadAs(typeof(CountRequest))]
	public partial interface ICountRequest
	{
		[DataMember(Name ="query")]
		QueryContainer Query { get; set; }
	}

	// ReSharper disable once UnusedTypeParameter
	public partial interface ICountRequest<TDocument> where TDocument : class { }

	public partial class CountRequest
	{
		public QueryContainer Query { get; set; }

		protected override HttpMethod HttpMethod =>
			Self.RequestParameters.ContainsQueryString("source") || Self.RequestParameters.ContainsQueryString("q") || Self.Query == null
			|| Self.Query.IsConditionless()
				? HttpMethod.GET
				: HttpMethod.POST;
	}

	// ReSharper disable once UnusedTypeParameter
	public partial class CountRequest<TDocument> where TDocument : class { }

	public partial class CountDescriptor<TDocument> where TDocument : class
	{
		protected override HttpMethod HttpMethod =>
			Self.RequestParameters.ContainsQueryString("source") || Self.RequestParameters.ContainsQueryString("q") || Self.Query == null
			|| Self.Query.IsConditionless()
				? HttpMethod.GET
				: HttpMethod.POST;

		QueryContainer ICountRequest.Query { get; set; }

		public CountDescriptor<TDocument> Query(Func<QueryContainerDescriptor<TDocument>, QueryContainer> querySelector) =>
			Assign(querySelector, (a, v) => a.Query = v?.Invoke(new QueryContainerDescriptor<TDocument>()));
	}
}
