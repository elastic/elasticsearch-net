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
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(RandomScoreFunction))]
	public interface IRandomScoreFunction : IScoreFunction
	{
		[DataMember(Name ="field")]
		Field Field { get; set; }

		[DataMember(Name ="seed")]
		Union<long, string> Seed { get; set; }
	}

	public class RandomScoreFunction : FunctionScoreFunctionBase, IRandomScoreFunction
	{
		public Field Field { get; set; }
		public Union<long, string> Seed { get; set; }
	}

	public class RandomScoreFunctionDescriptor<T>
		: FunctionScoreFunctionDescriptorBase<RandomScoreFunctionDescriptor<T>, IRandomScoreFunction, T>, IRandomScoreFunction
		where T : class
	{
		Field IRandomScoreFunction.Field { get; set; }
		Union<long, string> IRandomScoreFunction.Seed { get; set; }

		public RandomScoreFunctionDescriptor<T> Seed(long? seed) => Assign(seed, (a, v) => a.Seed = v);

		public RandomScoreFunctionDescriptor<T> Seed(string seed) => Assign(seed, (a, v) => a.Seed = v);

		public RandomScoreFunctionDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public RandomScoreFunctionDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);
	}
}
