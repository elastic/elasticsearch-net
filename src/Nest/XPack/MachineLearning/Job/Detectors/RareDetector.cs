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

namespace Nest
{
	public enum RareFunction
	{
		Rare,
		FreqRare
	}

	public static class RareFunctionsExtensions
	{
		public static string GetStringValue(this RareFunction rareFunction)
		{
			switch (rareFunction)
			{
				case RareFunction.Rare:
					return "rare";
				case RareFunction.FreqRare:
					return "freq_rare";
				default:
					throw new ArgumentOutOfRangeException(nameof(rareFunction), rareFunction, null);
			}
		}
	}

	public interface IRareDetector
		: IDetector, IByFieldNameDetector, IOverFieldNameDetector,
			IPartitionFieldNameDetector { }

	public abstract class RareDetectorBase : DetectorBase, IRareDetector
	{
		protected RareDetectorBase(RareFunction function) : base(function.GetStringValue()) { }

		public Field ByFieldName { get; set; }
		public Field OverFieldName { get; set; }
		public Field PartitionFieldName { get; set; }
	}

	public class RareDetectorDescriptor<T> : DetectorDescriptorBase<RareDetectorDescriptor<T>, IRareDetector>, IRareDetector where T : class
	{
		public RareDetectorDescriptor(RareFunction function) : base(function.GetStringValue()) { }

		Field IByFieldNameDetector.ByFieldName { get; set; }
		Field IOverFieldNameDetector.OverFieldName { get; set; }
		Field IPartitionFieldNameDetector.PartitionFieldName { get; set; }

		public RareDetectorDescriptor<T> ByFieldName(Field byFieldName) => Assign(byFieldName, (a, v) => a.ByFieldName = v);

		public RareDetectorDescriptor<T> ByFieldName<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.ByFieldName = v);

		public RareDetectorDescriptor<T> OverFieldName(Field overFieldName) => Assign(overFieldName, (a, v) => a.OverFieldName = v);

		public RareDetectorDescriptor<T> OverFieldName<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.OverFieldName = v);

		public RareDetectorDescriptor<T> PartitionFieldName(Field partitionFieldName) => Assign(partitionFieldName, (a, v) => a.PartitionFieldName = v);

		public RareDetectorDescriptor<T> PartitionFieldName<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.PartitionFieldName = v);
	}

	public class RareDetector : RareDetectorBase
	{
		public RareDetector() : base(RareFunction.Rare) { }
	}

	public class FreqRareDetector : RareDetectorBase
	{
		public FreqRareDetector() : base(RareFunction.FreqRare) { }
	}
}
