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

using System;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// A values source that is equivalent to a simple terms aggregation.
	/// The values are extracted from a field or a script exactly like the terms aggregation.
	/// </summary>
	public interface ITermsCompositeAggregationSource : ICompositeAggregationSource
	{
		/// <summary>
		/// A script to create the values for the composite buckets
		/// </summary>
		[DataMember(Name ="script")]
		IScript Script { get; set; }
	}

	/// <inheritdoc cref="ITermsCompositeAggregationSource" />
	public class TermsCompositeAggregationSource : CompositeAggregationSourceBase, ITermsCompositeAggregationSource
	{
		public TermsCompositeAggregationSource(string name) : base(name) { }

		/// <inheritdoc />
		public IScript Script { get; set; }

		/// <inheritdoc />
		protected override string SourceType => "terms";
	}

	/// <inheritdoc cref="ITermsCompositeAggregationSource" />
	public class TermsCompositeAggregationSourceDescriptor<T>
		: CompositeAggregationSourceDescriptorBase<TermsCompositeAggregationSourceDescriptor<T>, ITermsCompositeAggregationSource, T>,
			ITermsCompositeAggregationSource
	{
		public TermsCompositeAggregationSourceDescriptor(string name) : base(name, "terms") { }

		IScript ITermsCompositeAggregationSource.Script { get; set; }

		/// <inheritdoc cref="ITermsCompositeAggregationSource.Script" />
		public TermsCompositeAggregationSourceDescriptor<T> Script(Func<ScriptDescriptor, IScript> selector) =>
			Assign(selector, (a, v) => a.Script = v?.Invoke(new ScriptDescriptor()));
	}
}
