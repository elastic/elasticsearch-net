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
	/// How suggestions should be sorted per suggest text term.
	/// </summary>
	[StringEnum]
	public enum SuggestSort
	{
		/// <summary>
		/// Sort by score first, then document frequency and then the term itself
		/// </summary>
		[EnumMember(Value = "score")]
		Score,

		/// <summary>
		/// Sort by document frequency first, then similarity score and then the term itself
		/// </summary>
		[EnumMember(Value = "frequency")]
		Frequency
	}
}
