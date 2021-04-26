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
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IDecayFunction : IScoreFunction
	{
		[DataMember(Name ="decay")]
		double? Decay { get; set; }

		string DecayType { get; }

		Field Field { get; set; }

		[DataMember(Name ="multi_value_mode")]
		MultiValueMode? MultiValueMode { get; set; }
	}

	public interface IDecayFunction<TOrigin, TScale> : IDecayFunction
	{
		[DataMember(Name ="offset")]
		TScale Offset { get; set; }

		[DataMember(Name ="origin")]
		TOrigin Origin { get; set; }

		[DataMember(Name ="scale")]
		TScale Scale { get; set; }
	}


	public abstract class DecayFunctionBase<TOrigin, TScale> : FunctionScoreFunctionBase, IDecayFunction<TOrigin, TScale>
	{
		public double? Decay { get; set; }

		public Field Field { get; set; }

		public MultiValueMode? MultiValueMode { get; set; }

		public TScale Offset { get; set; }

		public TOrigin Origin { get; set; }

		public TScale Scale { get; set; }
		protected abstract string DecayType { get; }

		string IDecayFunction.DecayType => DecayType;
	}

	public abstract class DecayFunctionDescriptorBase<TDescriptor, TOrigin, TScale, T>
		: FunctionScoreFunctionDescriptorBase<TDescriptor, IDecayFunction<TOrigin, TScale>, T>, IDecayFunction<TOrigin, TScale>
		where TDescriptor : DecayFunctionDescriptorBase<TDescriptor, TOrigin, TScale, T>, IDecayFunction<TOrigin, TScale>
		where T : class
	{
		protected abstract string DecayType { get; }

		double? IDecayFunction.Decay { get; set; }

		string IDecayFunction.DecayType => DecayType;

		Field IDecayFunction.Field { get; set; }

		MultiValueMode? IDecayFunction.MultiValueMode { get; set; }

		TScale IDecayFunction<TOrigin, TScale>.Offset { get; set; }

		TOrigin IDecayFunction<TOrigin, TScale>.Origin { get; set; }

		TScale IDecayFunction<TOrigin, TScale>.Scale { get; set; }

		public TDescriptor Origin(TOrigin origin) => Assign(origin, (a, v) => a.Origin = v);

		public TDescriptor Scale(TScale scale) => Assign(scale, (a, v) => a.Scale = v);

		public TDescriptor Offset(TScale offset) => Assign(offset, (a, v) => a.Offset = v);

		public TDescriptor Decay(double? decay) => Assign(decay, (a, v) => a.Decay = v);

		public TDescriptor MultiValueMode(MultiValueMode? mode) => Assign(mode, (a, v) => a.MultiValueMode = v);

		public TDescriptor Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public TDescriptor Field<TValue>(Expression<Func<T, TValue>> field) => Assign(field, (a, v) => a.Field = v);
	}
}
