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
	/// Use a built-in tag schema
	/// </summary>
	[StringEnum]
	public enum HighlighterTagsSchema
	{
		/// <summary>
		/// Use a specific "tag" schemas.
		/// </summary>
		/// <remarks>
		/// <para>Currently a single schema called "styled" with the following pre_tags:</para>
		/// <para>&lt;em class="hlt1"&gt;, &lt;em class="hlt2"&gt;, &lt;em class="hlt3"&gt;,</para>
		/// <para>&lt;em class="hlt4"&gt;, &lt;em class="hlt5"&gt;, &lt;em class="hlt6"&gt;,</para>
		/// <para>&lt;em class="hlt7"&gt;, &lt;em class="hlt8"&gt;, &lt;em class="hlt9"&gt;,</para>
		/// <para>&lt;em class="hlt10"&gt;</para>
		/// </remarks>
		[EnumMember(Value = "styled")]
		Styled
	}
}
