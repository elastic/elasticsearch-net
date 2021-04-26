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

namespace Nest
{
	public class QueryProfile
	{
		/// <summary>
		/// Detailed stats about how the time was spent
		/// </summary>
		[DataMember(Name ="breakdown")]
		public QueryBreakdown Breakdown { get; internal set; }

		/// <summary>
		/// Sub-queries of this query
		/// </summary>
		[DataMember(Name ="children")]
		public IEnumerable<QueryProfile> Children { get; internal set; }

		/// <summary>
		/// The lucene explanation text for the query
		/// </summary>
		[DataMember(Name ="description")]
		public string Description { get; internal set; }

		/// <summary>
		/// The time that this query took in nanoseconds
		/// </summary>
		[DataMember(Name ="time_in_nanos")]
		public long TimeInNanoseconds { get; internal set; }

		/// <summary>
		/// The lucene class name for the type of query
		/// </summary>
		[DataMember(Name ="type")]
		public string Type { get; internal set; }
	}
}
