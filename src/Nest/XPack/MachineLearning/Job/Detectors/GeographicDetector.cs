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
	public enum GeographicFunction
	{
		LatLong
	}

	public static class GeographicFunctionsExtensions
	{
		public static string GetStringValue(this GeographicFunction geographicFunction)
		{
			switch (geographicFunction)
			{
				case GeographicFunction.LatLong:
					return "lat_long";
				default:
					throw new ArgumentOutOfRangeException(nameof(geographicFunction), geographicFunction, null);
			}
		}
	}

	public interface IGeographicDetector
		: IDetector, IByFieldNameDetector, IOverFieldNameDetector,
			IPartitionFieldNameDetector, IFieldNameDetector { }

	public class LatLongDetector : DetectorBase, IGeographicDetector
	{
		public LatLongDetector() : base(GeographicFunction.LatLong.GetStringValue()) { }

		public Field ByFieldName { get; set; }
		public Field FieldName { get; set; }
		public Field OverFieldName { get; set; }
		public Field PartitionFieldName { get; set; }
	}

	public class LatLongDetectorDescriptor<T> : DetectorDescriptorBase<LatLongDetectorDescriptor<T>, IGeographicDetector>, IGeographicDetector
		where T : class
	{
		public LatLongDetectorDescriptor() : base(GeographicFunction.LatLong.GetStringValue()) { }

		Field IByFieldNameDetector.ByFieldName { get; set; }
		Field IFieldNameDetector.FieldName { get; set; }
		Field IOverFieldNameDetector.OverFieldName { get; set; }
		Field IPartitionFieldNameDetector.PartitionFieldName { get; set; }

		public LatLongDetectorDescriptor<T> FieldName(Field fieldName) => Assign(fieldName, (a, v) => a.FieldName = v);

		public LatLongDetectorDescriptor<T> FieldName<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.FieldName = v);

		public LatLongDetectorDescriptor<T> ByFieldName(Field byFieldName) => Assign(byFieldName, (a, v) => a.ByFieldName = v);

		public LatLongDetectorDescriptor<T> ByFieldName<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.ByFieldName = v);

		public LatLongDetectorDescriptor<T> OverFieldName(Field overFieldName) => Assign(overFieldName, (a, v) => a.OverFieldName = v);

		public LatLongDetectorDescriptor<T> OverFieldName<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.OverFieldName = v);

		public LatLongDetectorDescriptor<T> PartitionFieldName(Field partitionFieldName) => Assign(partitionFieldName, (a, v) => a.PartitionFieldName = v);

		public LatLongDetectorDescriptor<T> PartitionFieldName<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.PartitionFieldName = v);
	}
}
