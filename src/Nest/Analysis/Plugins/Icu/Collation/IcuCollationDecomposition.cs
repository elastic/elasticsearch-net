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
	/// Setting this decomposition property to canonical allows the Collator to handle unnormalized
	/// text properly, producing the same results as if the text were normalized. If no is set, it
	/// is the user’s responsibility to insure that all text is already in the appropriate form
	/// before a comparison or before getting a CollationKey. Adjusting decomposition mode
	/// allows the user to select between faster and more complete collation behavior. Since a
	/// great many of the world’s languages do not require text normalization, most locales
	/// set no as the default decomposition mode.
	/// </summary>
	/// <remarks>
	/// Requires analysis-icu plugin to be installed
	/// </remarks>
	[StringEnum]
	public enum IcuCollationDecomposition
	{
		[EnumMember(Value = "no")] No,
		[EnumMember(Value = "identical")] Canonical
	}
}
