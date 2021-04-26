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
	/// The tokenization mode determines how the tokenizer handles compound and unknown words.
	/// Part of the `analysis-kuromoji` plugin:
	/// https://www.elastic.co/guide/en/elasticsearch/plugins/current/analysis-kuromoji.html
	/// </summary>
	[StringEnum]
	public enum KuromojiTokenizationMode
	{
		/// <summary>
		/// Normal segmentation, no decomposition for compounds
		/// </summary>
		[EnumMember(Value = "normal")]
		Normal,

		/// <summary>
		/// Segmentation geared towards search. This includes a decompounding process for long nouns,
		/// also including the full compound token as a synonym.
		/// </summary>
		[EnumMember(Value = "search")]
		Search,

		/// <summary>
		/// Extended mode outputs unigrams for unknown words.
		/// </summary>
		[EnumMember(Value = "extended")]
		Extended
	}
}
