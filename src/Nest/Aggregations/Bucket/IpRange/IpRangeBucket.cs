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

namespace Nest
{
	/// <summary>
	/// A bucket for an <see cref="IpRangeAggregation"/>
	/// </summary>
	public class IpRangeBucket : BucketBase
	{
		public IpRangeBucket(IReadOnlyDictionary<string, IAggregate> dict) : base(dict) { }

		/// <summary>
		/// The count of documents in the bucket
		/// </summary>
		public long DocCount { get; set; }

		/// <summary>
		/// The IP address from
		/// </summary>
		public string From { get; set; }

		/// <summary>
		/// The key for the bucket
		/// </summary>
		public string Key { get; set; }

		/// <summary>
		/// The IP address to
		/// </summary>
		public string To { get; set; }
	}
}
