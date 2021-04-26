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
	/// Type of highlighter
	/// </summary>
	[StringEnum]
	public enum HighlighterType
	{
		/// <summary>
		/// Plain Highlighter.
		/// Uses the Lucene highlighter.
		/// It tries hard to reflect the query matching logic in terms of understanding word
		/// importance and any word positioning criteria in phrase queries.
		/// </summary>
		[EnumMember(Value = "plain")]
		Plain,

		/// <summary>
		/// Fast Vector Highlighter.
		/// If term_vector information is provided by setting term_vector to with_positions_offsets
		/// in the mapping then the fast vector highlighter will be used instead of the plain highlighter
		/// </summary>
		[EnumMember(Value = "fvh")]
		Fvh,

		/// <summary>
		/// Unified Highlighter.
		/// The default choice.
		/// The unified highlighter can extract offsets from either term vectors, or via re-analyzing text.
		/// Under the hood it uses Lucene UnifiedHighlighter which picks its strategy depending on the field and the query to highlight.
		/// Independently of the strategy this highlighter breaks the text into sentences and scores individual sentences as if
		/// they were documents in this corpus, using the BM25 algorithm. It supports accurate phrase and multi-term
		/// (fuzzy, prefix, regex) highlighting
		/// </summary>
		[EnumMember(Value = "unified")]
		Unified
	}
}
