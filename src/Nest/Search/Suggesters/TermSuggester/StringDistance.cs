// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
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
