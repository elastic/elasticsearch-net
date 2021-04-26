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
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// For numeric fields it is also possible to cast the values from one type to another using this option. This can be useful for cross-index
	/// search if the sort field is mapped differently on some indices.
	/// </summary>
	[StringEnum]
	public enum NumericType
	{
		[EnumMember(Value = "long")]
		Long,

		[EnumMember(Value = "double")]
		Double,

		[EnumMember(Value = "date")]
		Date,

		[EnumMember(Value = "date_nanos")]
		DateNanos
	}
}
