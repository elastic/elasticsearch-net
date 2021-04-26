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

using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("sql.clear_cursor.json")]
	[ReadAs(typeof(ClearSqlCursorRequest))]
	public partial interface IClearSqlCursorRequest
	{
		/// <summary>
		/// <para>
		/// You’ve reached the last page when there is no cursor returned in the results. Like Elasticsearch’s scroll,
		/// SQL may keep state in Elasticsearch to support the cursor.
		/// Unlike scroll, receiving the last page is enough to guarantee that the Elasticsearch state is cleared.
		/// </para>
		/// </summary>
		[DataMember(Name ="cursor")]
		string Cursor { get; set; }
	}

	public partial class ClearSqlCursorRequest
	{
		/// <inheritdoc cref="IQuerySqlRequest.Cursor" />
		/// >
		public string Cursor { get; set; }
	}

	public partial class ClearSqlCursorDescriptor
	{
		string IClearSqlCursorRequest.Cursor { get; set; }

		/// <inheritdoc cref="IQuerySqlRequest.Cursor" />
		/// >
		public ClearSqlCursorDescriptor Cursor(string cursor) => Assign(cursor, (a, v) => a.Cursor = v);
	}
}
