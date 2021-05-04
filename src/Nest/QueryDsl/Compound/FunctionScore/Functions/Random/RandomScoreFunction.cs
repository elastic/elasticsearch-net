// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Nest.Utf8Json;

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
