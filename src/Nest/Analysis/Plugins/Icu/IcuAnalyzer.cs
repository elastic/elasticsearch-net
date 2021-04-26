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
	/// An ICU analyzer that performs basic normalization, tokenization and character folding,
	/// using the <see cref="IIcuNormalizationCharFilter" /> char filter,
	/// <see cref="IIcuTokenizer" /> and <see cref="IcuNormalizationTokenFilter" /> token filter
	/// </summary>
	/// <remarks>
	/// Requires analysis-icu plugin to be installed
	/// </remarks>
	public interface IIcuAnalyzer : IAnalyzer
	{
		/// <summary>
		/// Normalization method. Default is <see cref="IcuNormalizationType.CompatibilityCaseFold" />
		/// </summary>
		[DataMember(Name = "method")]
		IcuNormalizationType? Method { get; set; }

		/// <summary>
		/// Normalization mode. Default is <see cref="IcuNormalizationMode.Compose" />
		/// </summary>
		[DataMember(Name = "mode")]
		IcuNormalizationMode? Mode { get; set; }
	}

	/// <inheritdoc cref="IIcuAnalyzer" />
	public class IcuAnalyzer : AnalyzerBase, IIcuAnalyzer
	{
		public IcuAnalyzer() : base("icu_analyzer") { }

		/// <inheritdoc />
		public IcuNormalizationType? Method { get; set; }

		/// <inheritdoc />
		public IcuNormalizationMode? Mode { get; set; }
	}

	/// <inheritdoc cref="IIcuAnalyzer" />
	public class IcuAnalyzerDescriptor : AnalyzerDescriptorBase<IcuAnalyzerDescriptor, IIcuAnalyzer>, IIcuAnalyzer
	{
		protected override string Type => "icu_analyzer";

		IcuNormalizationType? IIcuAnalyzer.Method { get; set; }
		IcuNormalizationMode? IIcuAnalyzer.Mode { get; set; }

		/// <inheritdoc cref="IIcuAnalyzer.Method" />
		public IcuAnalyzerDescriptor Method(IcuNormalizationType? method) => Assign(method, (a, v) => a.Method = v);

		/// <inheritdoc cref="IIcuAnalyzer.Mode" />
		public IcuAnalyzerDescriptor Mode(IcuNormalizationMode? mode) => Assign(mode, (a, v) => a.Mode = v);
	}
}
