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
	/// Normalizes as defined here: http://userguide.icu-project.org/transforms/normalization
	/// </summary>
	/// <remarks>
	/// Requires analysis-icu plugin to be installed
	/// </remarks>
	public interface IIcuNormalizationTokenFilter : ITokenFilter
	{
		/// <summary>
		/// The type of normalization
		/// </summary>
		[DataMember(Name ="name")]
		IcuNormalizationType? Name { get; set; }
	}

	/// <inheritdoc />
	public class IcuNormalizationTokenFilter : TokenFilterBase, IIcuNormalizationTokenFilter
	{
		public IcuNormalizationTokenFilter() : base("icu_normalizer") { }

		/// <inheritdoc />
		public IcuNormalizationType? Name { get; set; }
	}

	/// <inheritdoc />
	public class IcuNormalizationTokenFilterDescriptor
		: TokenFilterDescriptorBase<IcuNormalizationTokenFilterDescriptor, IIcuNormalizationTokenFilter>, IIcuNormalizationTokenFilter
	{
		protected override string Type => "icu_normalizer";

		IcuNormalizationType? IIcuNormalizationTokenFilter.Name { get; set; }

		/// <inheritdoc />
		public IcuNormalizationTokenFilterDescriptor Name(IcuNormalizationType? name) => Assign(name, (a, v) => a.Name = v);
	}
}
