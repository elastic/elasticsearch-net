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
	/// <summary>
	/// A machine learning filter
	/// </summary>
	public class Filter
	{
		/// <summary>
		/// A description of the filter
		/// </summary>
		[DataMember(Name = "description")]
		public string Description { get; set; }

		/// <summary>
		/// The filter ID
		/// </summary>
		[DataMember(Name = "filter_id")]
		public string FilterId { get; set; }

		/// <summary>
		/// The items of the filter
		/// </summary>
		[DataMember(Name = "items")]
		public IReadOnlyCollection<string> Items { get; set; } = EmptyReadOnly<string>.Collection;
	}

	/// <summary>
	/// Retrieves configuration information for machine learning filters.
	/// </summary>
	public class GetFiltersResponse : ResponseBase
	{
		/// <summary>
		/// The count of filters.
		/// </summary>
		[DataMember(Name = "count")]
		public long Count { get; internal set; }

		/// <summary>
		/// An array of filters resources
		/// </summary>
		[DataMember(Name = "filters")]
		public IReadOnlyCollection<Filter> Filters { get; internal set; } = EmptyReadOnly<Filter>.Collection;
	}
}
