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
	/// String distance implementation to use for comparing how similar suggested terms are
	/// </summary>
	[StringEnum]
	public enum StringDistance
	{
		/// <summary>
		/// The default based on damerau_levenshtein but highly optimized for comparing string distance for terms inside the index.
		/// </summary>
		[EnumMember(Value = "internal")]
		Internal,

		/// <summary>
		/// String distance algorithm based on Damerau-Levenshtein algorithm.
		/// </summary>
		[EnumMember(Value = "damerau_levenshtein")]
		DamerauLevenshtein,

		/// <summary>
		/// String distance algorithm based on Levenshtein edit distance algorithm.
		/// </summary>
		[EnumMember(Value = "levenshtein")]
		Levenshtein,

		/// <summary>
		/// String distance algorithm based on Jaro-Winkler algorithm.
		/// </summary>
		[EnumMember(Value = "jaro_winkler")]
		Jarowinkler,

		/// <summary>
		/// String distance algorithm based on character n-grams.
		/// </summary>
		[EnumMember(Value = "ngram")]
		Ngram
	}
}
