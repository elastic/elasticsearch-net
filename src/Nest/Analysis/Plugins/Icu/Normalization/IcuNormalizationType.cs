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
	/// Normalization forms https://en.wikipedia.org/wiki/Unicode_equivalence#Normal_forms
	/// </summary>
	/// <remarks>
	/// Requires analysis-icu plugin to be installed
	/// </remarks>
	[StringEnum]
	public enum IcuNormalizationType
	{
		/// <summary>
		/// Characters are decomposed and then recomposed by canonical equivalence.
		/// </summary>
		[EnumMember(Value = "nfc")]
		Canonical,

		/// <summary>
		/// Characters are decomposed by compatibility, then recomposed by canonical equivalence.
		/// </summary>
		[EnumMember(Value = "nfkc")]
		Compatibility,

		/// <summary>
		/// Characters are decomposed by compatibility, then recomposed by canonical equivalence with case folding
		/// </summary>
		[EnumMember(Value = "nfkc_cf")]
		CompatibilityCaseFold
	}
}
