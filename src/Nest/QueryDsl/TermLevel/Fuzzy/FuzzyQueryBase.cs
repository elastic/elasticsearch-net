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
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(FuzzyQueryFormatter))]
	public interface IFuzzyQuery : IFieldNameQuery
	{
		[DataMember(Name ="max_expansions")]
		int? MaxExpansions { get; set; }

		[DataMember(Name ="prefix_length")]
		int? PrefixLength { get; set; }

		[DataMember(Name ="rewrite")]
		MultiTermQueryRewrite Rewrite { get; set; }

		[DataMember(Name ="transpositions")]
		bool? Transpositions { get; set; }
	}

	public interface IFuzzyQuery<TValue, TFuzziness> : IFuzzyQuery
	{
		[DataMember(Name ="fuzziness")]
		TFuzziness Fuzziness { get; set; }

		[DataMember(Name ="value")]
		TValue Value { get; set; }
	}

	internal static class FuzzyQueryBase
	{
		internal static bool IsConditionless<TValue, TFuzziness>(IFuzzyQuery<TValue, TFuzziness> fuzzy) =>
			fuzzy == null || fuzzy.Value == null || fuzzy.Field == null;
	}

	public abstract class FuzzyQueryBase<TValue, TFuzziness> : FieldNameQueryBase, IFuzzyQuery<TValue, TFuzziness>
	{
		public TFuzziness Fuzziness { get; set; }

		public int? MaxExpansions { get; set; }
		public int? PrefixLength { get; set; }

		public MultiTermQueryRewrite Rewrite { get; set; }

		public bool? Transpositions { get; set; }

		public TValue Value { get; set; }

		protected override bool Conditionless => FuzzyQueryBase.IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Fuzzy = this;
	}

	public abstract class FuzzyQueryDescriptorBase<TDescriptor, T, TValue, TFuzziness>
		: FieldNameQueryDescriptorBase<TDescriptor, IFuzzyQuery<TValue, TFuzziness>, T>, IFuzzyQuery<TValue, TFuzziness>
		where T : class
		where TDescriptor : FieldNameQueryDescriptorBase<TDescriptor, IFuzzyQuery<TValue, TFuzziness>, T>, IFuzzyQuery<TValue, TFuzziness>
	{
		protected override bool Conditionless => FuzzyQueryBase.IsConditionless(this);
		TFuzziness IFuzzyQuery<TValue, TFuzziness>.Fuzziness { get; set; }
		int? IFuzzyQuery.MaxExpansions { get; set; }
		int? IFuzzyQuery.PrefixLength { get; set; }
		MultiTermQueryRewrite IFuzzyQuery.Rewrite { get; set; }
		bool? IFuzzyQuery.Transpositions { get; set; }
		TValue IFuzzyQuery<TValue, TFuzziness>.Value { get; set; }

		public TDescriptor MaxExpansions(int? maxExpansions) => Assign(maxExpansions, (a, v) => a.MaxExpansions = v);

		public TDescriptor PrefixLength(int? prefixLength) => Assign(prefixLength, (a, v) => a.PrefixLength = v);

		public TDescriptor Transpositions(bool? enable = true) => Assign(enable, (a, v) => a.Transpositions = v);

		public TDescriptor Rewrite(MultiTermQueryRewrite rewrite) => Assign(rewrite, (a, v) => a.Rewrite = v);
	}
}
