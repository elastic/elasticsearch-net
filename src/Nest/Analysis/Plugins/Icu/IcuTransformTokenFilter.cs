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
	/// Transforms are used to process Unicode text in many different ways, such as case mapping,
	/// normalization, transliteration and bidirectional text handling.
	/// </summary>
	/// <remarks>
	/// Requires analysis-icu plugin to be installed
	/// </remarks>
	public interface IIcuTransformTokenFilter : ITokenFilter
	{
		/// <summary>
		/// Specify text direction with the dir parameter which accepts forward (default) for LTR and reverse for RTL.
		/// </summary>
		[DataMember(Name ="dir")]
		IcuTransformDirection? Direction { get; set; }

		[DataMember(Name ="id")]
		string Id { get; set; }
	}

	/// <inheritdoc />
	public class IcuTransformTokenFilter : TokenFilterBase, IIcuTransformTokenFilter
	{
		public IcuTransformTokenFilter() : base("icu_transform") { }

		/// <inheritdoc />
		public IcuTransformDirection? Direction { get; set; }

		public string Id { get; set; }
	}

	/// <inheritdoc />
	public class IcuTransformTokenFilterDescriptor
		: TokenFilterDescriptorBase<IcuTransformTokenFilterDescriptor, IIcuTransformTokenFilter>, IIcuTransformTokenFilter
	{
		protected override string Type => "icu_transform";

		IcuTransformDirection? IIcuTransformTokenFilter.Direction { get; set; }
		string IIcuTransformTokenFilter.Id { get; set; }

		/// <inheritdoc />
		public IcuTransformTokenFilterDescriptor Direction(IcuTransformDirection? direction) => Assign(direction, (a, v) => a.Direction = v);

		/// <inheritdoc />
		public IcuTransformTokenFilterDescriptor Id(string id) => Assign(id, (a, v) => a.Id = v);
	}
}
