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
	/// <summary>
	/// A field mapped as an <see cref="ICompletionProperty" />. Convenience class to use when indexing completion
	/// fields.
	/// </summary>
	public class CompletionField
	{
		/// <summary>
		/// The contexts to associate with the input which can be used at query time to filter and boost suggestions
		/// </summary>
		[DataMember(Name = "contexts")]
		public IDictionary<string, IEnumerable<string>> Contexts { get; set; }

		/// <summary>
		/// The input to store. Can be a single or multiple inputs
		/// </summary>
		[DataMember(Name = "input")]
		public IEnumerable<string> Input { get; set; }

		/// <summary>
		/// A positive integer which defines a weight and allows you to rank your suggestions. This field is optional.
		/// </summary>
		[DataMember(Name = "weight")]
		public int? Weight { get; set; }
	}
}
