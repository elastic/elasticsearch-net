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
	/// The kstem token filter is a high performance filter for english.
	/// <para> All terms must already be lowercased (use lowercase filter) for this filter to work correctly.</para>
	/// </summary>
	public interface IKStemTokenFilter : ITokenFilter { }

	/// <inheritdoc />
	public class KStemTokenFilter : TokenFilterBase, IKStemTokenFilter
	{
		public KStemTokenFilter() : base("kstem") { }
	}

	/// <inheritdoc />
	public class KStemTokenFilterDescriptor
		: TokenFilterDescriptorBase<KStemTokenFilterDescriptor, IKStemTokenFilter>, IKStemTokenFilter
	{
		protected override string Type => "kstem";
	}
}
