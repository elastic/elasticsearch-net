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
using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("sql.query.json")]
	[ReadAs(typeof(QuerySqlRequest))]
	public partial interface IQuerySqlRequest : ISqlRequest
	{
		/// <summary>
		/// Continue to the next page by sending back the cursor field returned in the previous response.
		/// <para>
		/// You’ve reached the last page when there is no cursor returned in the results. Like Elasticsearch’s scroll,
		/// SQL may keep state in Elasticsearch to support the cursor.
		/// Unlike scroll, receiving the last page is enough to guarantee that the Elasticsearch state is cleared.
		/// </para>
		/// </summary>
		[DataMember(Name="cursor")]
		string Cursor { get; set; }

		/// <summary>
		/// Return the results in a columnar fashion: one row represents all the values of a certain column from the current page of results.
		/// The following formats can be returned in columnar orientation: json, yaml, cbor and smile.
		/// </summary>
		[DataMember(Name="columnar")]
		bool? Columnar { get; set; }
	}

	public partial class QuerySqlRequest
	{
		/// <inheritdoc cref="IQuerySqlRequest.Cursor" />
		/// >
		public string Cursor { get; set; }

		/// <inheritdoc cref="IQuerySqlRequest.Columnar" />
		/// >
		public bool? Columnar { get; set; }

		/// <inheritdoc cref="ISqlRequest.FetchSize" />
		/// >
		public int? FetchSize { get; set; }

		/// <inheritdoc cref="ISqlRequest.Filter" />
		/// >
		public QueryContainer Filter { get; set; }

		/// <inheritdoc cref="ISqlRequest.Query" />
		/// >
		public string Query { get; set; }

		/// <inheritdoc cref="ISqlRequest.TimeZone" />
		/// >
		public string TimeZone { get; set; }

		/// <inheritdoc />
		public IRuntimeFields RuntimeFields { get; set; }
	}

	public partial class QuerySqlDescriptor
	{
		string IQuerySqlRequest.Cursor { get; set; }
		bool? IQuerySqlRequest.Columnar { get; set; }
		int? ISqlRequest.FetchSize { get; set; }
		QueryContainer ISqlRequest.Filter { get; set; }
		string ISqlRequest.Query { get; set; }
		string ISqlRequest.TimeZone { get; set; }
		IRuntimeFields ISqlRequest.RuntimeFields { get; set; }

		/// <inheritdoc cref="ISqlRequest.Query" />
		/// >
		public QuerySqlDescriptor Query(string query) => Assign(query, (a, v) => a.Query = v);

		/// <inheritdoc cref="ISqlRequest.TimeZone" />
		/// >
		public QuerySqlDescriptor TimeZone(string timeZone) => Assign(timeZone, (a, v) => a.TimeZone = v);

		/// <inheritdoc cref="ISqlRequest.FetchSize" />
		/// >
		public QuerySqlDescriptor FetchSize(int? fetchSize) => Assign(fetchSize, (a, v) => a.FetchSize = v);

		/// <inheritdoc cref="ISqlRequest.Filter" />
		/// >
		public QuerySqlDescriptor Filter<T>(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector)
			where T : class => Assign(querySelector, (a, v) => a.Filter = v?.Invoke(new QueryContainerDescriptor<T>()));

		/// <inheritdoc cref="IQuerySqlRequest.Cursor" />
		/// >
		public QuerySqlDescriptor Cursor(string cursor) => Assign(cursor, (a, v) => a.Cursor = v);

		/// <inheritdoc cref="IQuerySqlRequest.Columnar" />
		/// >
		public QuerySqlDescriptor Columnar(bool? columnar = true) => Assign(columnar, (a, v) => a.Columnar = v);

		/// <inheritdoc cref="ISqlRequest.RuntimeFields" />
		public QuerySqlDescriptor RuntimeFields(Func<RuntimeFieldsDescriptor, IPromise<IRuntimeFields>> runtimeFieldsSelector) =>
			Assign(runtimeFieldsSelector, (a, v) => a.RuntimeFields = v?.Invoke(new RuntimeFieldsDescriptor())?.Value);
	}
}
