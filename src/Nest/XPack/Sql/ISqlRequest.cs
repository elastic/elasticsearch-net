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
	/// <summary> Shared properties of <see cref="IQuerySqlRequest" /> and <see cref="ITranslateSqlRequest" /> </summary>
	public interface ISqlRequest
	{
		/// <summary>
		/// fetch_size is a hint for how many results to return in each page. SQL might chose to return more or fewer results though.
		/// </summary>
		[DataMember(Name ="fetch_size")]
		int? FetchSize { get; set; }

		/// <summary> Further filter the results returned by the SQL query provided on <see cref="Query" /> </summary>
		[DataMember(Name ="filter")]
		QueryContainer Filter { get; set; }

		/// <summary> The SQL query you want Elasticsearch to execute </summary>
		[DataMember(Name ="query")]
		string Query { get; set; }

		/// <summary>
		/// time_zone is the time zone to use for date functions and date parsing. time_zone defaults to utc
		/// and can take any values as documented on Joda time's DateTimeZone enum.
		/// </summary>
		[DataMember(Name ="time_zone")]
		string TimeZone { get; set; }

		/// <summary>
		/// Specifies runtime fields which exist only as part of the query.
		/// </summary>
		[DataMember(Name = "runtime_mappings")]
		IRuntimeFields RuntimeFields { get; set; }
	}
}
