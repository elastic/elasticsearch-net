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

namespace Nest
{
	/// <summary>
	/// A token filter that drops identical tokens at the same position
	/// </summary>
	public interface IRemoveDuplicatesTokenFilter : ITokenFilter { }

	/// <inheritdoc cref="IRemoveDuplicatesTokenFilter" />
	public class RemoveDuplicatesTokenFilter : TokenFilterBase, IRemoveDuplicatesTokenFilter
	{
		internal const string TokenType = "remove_duplicates";

		public RemoveDuplicatesTokenFilter() : base(TokenType) { }
	}

	/// <inheritdoc cref="IRemoveDuplicatesTokenFilter" />
	public class RemoveDuplicatesTokenFilterDescriptor
		: TokenFilterDescriptorBase<RemoveDuplicatesTokenFilterDescriptor, IRemoveDuplicatesTokenFilter>, IRemoveDuplicatesTokenFilter
	{
		protected override string Type => RemoveDuplicatesTokenFilter.TokenType;
	}
}
