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
	/// Fragmenter can control how text should be broken up in highlight snippets. However, this option is
	/// applicable only for the Plain Highlighter
	/// </summary>
	[StringEnum]
	public enum HighlighterFragmenter
	{
		/// <summary>
		/// Breaks up text into same sized fragments.
		/// </summary>
		[EnumMember(Value = "simple")]
		Simple,

		/// <summary>
		/// Same as the simple fragmenter, but tries not to break up text between highlighted terms (this is applicable
		/// when using phrase like queries). This is the default.
		/// </summary>
		[EnumMember(Value = "span")]
		Span
	}
}
