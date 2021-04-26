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
using Elasticsearch.Net.Specification.SqlApi;

namespace Nest
{
	[MapsApi("sql.translate.json")]
	[ReadAs(typeof(TranslateSqlRequest))]
	public partial interface ITranslateSqlRequest : ISqlRequest { }

	public partial class TranslateSqlRequest
	{
		protected sealed override void RequestDefaults(TranslateSqlRequestParameters parameters) =>
			parameters.CustomResponseBuilder = TranslateSqlResponseBuilder.Instance;

		/// <inheritdoc cref="ISqlRequest.FetchSize" />
		public int? FetchSize { get; set; }

		/// <inheritdoc cref="ISqlRequest.Filter" />
		public QueryContainer Filter { get; set; }

		/// <inheritdoc cref="ISqlRequest.Query" />
		public string Query { get; set; }

		/// <inheritdoc cref="ISqlRequest.TimeZone" />
		public string TimeZone { get; set; }

		/// <inheritdoc cref="ISqlRequest.RuntimeFields" />
		public IRuntimeFields RuntimeFields { get; set; }
	}

	public partial class TranslateSqlDescriptor
	{
		protected sealed override void RequestDefaults(TranslateSqlRequestParameters parameters) =>
			parameters.CustomResponseBuilder = TranslateSqlResponseBuilder.Instance;

		int? ISqlRequest.FetchSize { get; set; }
		QueryContainer ISqlRequest.Filter { get; set; }
		string ISqlRequest.Query { get; set; }
		string ISqlRequest.TimeZone { get; set; }
		IRuntimeFields ISqlRequest.RuntimeFields { get; set; }

		/// <inheritdoc cref="ISqlRequest.Query" />
		/// >
		public TranslateSqlDescriptor Query(string query) => Assign(query, (a, v) => a.Query = v);

		/// <inheritdoc cref="ISqlRequest.TimeZone" />
		/// >
		public TranslateSqlDescriptor TimeZone(string timeZone) => Assign(timeZone, (a, v) => a.TimeZone = v);

		/// <inheritdoc cref="ISqlRequest.FetchSize" />
		/// >
		public TranslateSqlDescriptor FetchSize(int? fetchSize) => Assign(fetchSize, (a, v) => a.FetchSize = v);

		/// <inheritdoc cref="ISqlRequest.Filter" />
		/// >
		public TranslateSqlDescriptor Filter<T>(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector)
			where T : class => Assign(querySelector, (a, v) => a.Filter = v?.Invoke(new QueryContainerDescriptor<T>()));

		/// <inheritdoc cref="ISqlRequest.RuntimeFields" />
		public TranslateSqlDescriptor RuntimeFields<TSource>(Func<RuntimeFieldsDescriptor<TSource>, IPromise<IRuntimeFields>> runtimeFieldsSelector) where TSource : class =>
			Assign(runtimeFieldsSelector, (a, v) => a.RuntimeFields = v?.Invoke(new RuntimeFieldsDescriptor<TSource>())?.Value);
	}
}
