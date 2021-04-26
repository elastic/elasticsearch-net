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

using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class QuerySqlResponse : ResponseBase
	{
		/// <summary>
		/// Describes the columns being returned, this property will only be set on the first page of results.
		/// </summary>
		[DataMember(Name = "columns")]
		public IReadOnlyCollection<SqlColumn> Columns { get; internal set; } = EmptyReadOnly<SqlColumn>.Collection;

		/// <summary>
		/// You’ve reached the last page when there is no cursor returned in the results. Like Elasticsearch’s scroll,
		/// SQL may keep state in Elasticsearch to support the cursor.
		/// Unlike scroll, receiving the last page is enough to guarantee that the Elasticsearch state is cleared.
		/// </summary>
		[DataMember(Name = "cursor")]
		public string Cursor { get; internal set; }

		/// <summary>
		/// If <see cref="IQuerySqlRequest.Columnar"/> has been set to false, this property will contain the row values
		/// </summary>
		[DataMember(Name = "rows")]
		public IReadOnlyCollection<SqlRow> Rows { get; internal set; } = EmptyReadOnly<SqlRow>.Collection;

		/// <summary>
		/// If <see cref="IQuerySqlRequest.Columnar"/> has been set to true, this property will contain the column values
		/// </summary>
		[DataMember(Name = "values")]
		public IReadOnlyCollection<SqlRow> Values { get; internal set; } = EmptyReadOnly<SqlRow>.Collection;
	}
}
