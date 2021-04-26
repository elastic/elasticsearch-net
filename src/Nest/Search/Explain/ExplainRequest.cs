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
	[MapsApi("explain.json")]
	public partial interface IExplainRequest
	{
		[DataMember(Name ="query")]
		QueryContainer Query { get; set; }
	}

	// ReSharper disable once UnusedTypeParameter
	public partial interface IExplainRequest<TDocument> where TDocument : class { }

	public partial class ExplainRequest
	{
		public QueryContainer Query { get; set; }
		public Fields StoredFields { get; set; }
	}

	// ReSharper disable once UnusedTypeParameter
	public partial class ExplainRequest<TDocument> where TDocument : class
	{
		protected override HttpMethod HttpMethod =>
			RequestState.RequestParameters?.ContainsQueryString("source") == true || RequestState.RequestParameters?.ContainsQueryString("q") == true
				? HttpMethod.GET
				: HttpMethod.POST;

	}

	public partial class ExplainDescriptor<TDocument> where TDocument : class
	{
		protected override HttpMethod HttpMethod =>
			RequestState.RequestParameters?.ContainsQueryString("source") == true || RequestState.RequestParameters?.ContainsQueryString("q") == true
				? HttpMethod.GET
				: HttpMethod.POST;

		Fields IExplainRequest.StoredFields { get; set; }
		QueryContainer IExplainRequest.Query { get; set; }

		public ExplainDescriptor<TDocument> Query(Func<QueryContainerDescriptor<TDocument>, QueryContainer> querySelector) =>
			Assign(querySelector, (a, v) => a.Query = v?.Invoke(new QueryContainerDescriptor<TDocument>()));

		//TODO write a code standards tests for Field/Fields descriptors (if not already exists)
		/// <summary>
		/// Allows to selectively load specific fields for each document
		/// represented by a search hit. Defaults to load the internal _source field.
		/// </summary>
		public ExplainDescriptor<TDocument> StoredFields(Func<FieldsDescriptor<TDocument>, IPromise<Fields>> fields) =>
			Assign(fields, (a, v) => a.StoredFields = v?.Invoke(new FieldsDescriptor<TDocument>())?.Value);

		public ExplainDescriptor<TDocument> StoredFields(Fields fields) => Assign(fields, (a, v) => a.StoredFields = v);
	}
}
