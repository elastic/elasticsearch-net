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

namespace Nest
{
	/// <summary>
	/// A filter that stems words using a Snowball-generated stemmer.
	/// </summary>
	public interface ISnowballTokenFilter : ITokenFilter
	{
		[DataMember(Name ="language")]
		SnowballLanguage? Language { get; set; }
	}

	/// <inheritdoc />
	public class SnowballTokenFilter : TokenFilterBase, ISnowballTokenFilter
	{
		public SnowballTokenFilter() : base("snowball") { }

		/// <inheritdoc />
		[DataMember(Name ="language")]
		public SnowballLanguage? Language { get; set; }
	}

	/// <inheritdoc />
	public class SnowballTokenFilterDescriptor
		: TokenFilterDescriptorBase<SnowballTokenFilterDescriptor, ISnowballTokenFilter>, ISnowballTokenFilter
	{
		protected override string Type => "snowball";

		SnowballLanguage? ISnowballTokenFilter.Language { get; set; }

		/// <inheritdoc />
		public SnowballTokenFilterDescriptor Language(SnowballLanguage? language) => Assign(language, (a, v) => a.Language = v);
	}
}
