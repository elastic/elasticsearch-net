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
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(GeoDistanceAggregation))]
	public interface IGeoDistanceAggregation : IBucketAggregation
	{
		[DataMember(Name ="distance_type")]
		GeoDistanceType? DistanceType { get; set; }

		[DataMember(Name ="field")]
		Field Field { get; set; }

		[DataMember(Name ="origin")]
		GeoLocation Origin { get; set; }

		[DataMember(Name ="ranges")]
		IEnumerable<IAggregationRange> Ranges { get; set; }

		[DataMember(Name ="unit")]
		DistanceUnit? Unit { get; set; }
	}

	public class GeoDistanceAggregation : BucketAggregationBase, IGeoDistanceAggregation
	{
		internal GeoDistanceAggregation() { }

		public GeoDistanceAggregation(string name) : base(name) { }

		public GeoDistanceType? DistanceType { get; set; }
		public Field Field { get; set; }

		public GeoLocation Origin { get; set; }

		public IEnumerable<IAggregationRange> Ranges { get; set; }

		public DistanceUnit? Unit { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.GeoDistance = this;
	}

	public class GeoDistanceAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<GeoDistanceAggregationDescriptor<T>, IGeoDistanceAggregation, T>
			, IGeoDistanceAggregation
		where T : class
	{
		GeoDistanceType? IGeoDistanceAggregation.DistanceType { get; set; }
		Field IGeoDistanceAggregation.Field { get; set; }

		GeoLocation IGeoDistanceAggregation.Origin { get; set; }

		IEnumerable<IAggregationRange> IGeoDistanceAggregation.Ranges { get; set; }

		DistanceUnit? IGeoDistanceAggregation.Unit { get; set; }

		public GeoDistanceAggregationDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public GeoDistanceAggregationDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field) => Assign(field, (a, v) => a.Field = v);

		public GeoDistanceAggregationDescriptor<T> Origin(double lat, double lon) => Assign(new GeoLocation(lat, lon), (a, v) => a.Origin = v);

		public GeoDistanceAggregationDescriptor<T> Origin(GeoLocation geoLocation) => Assign(geoLocation, (a, v) => a.Origin = v);

		public GeoDistanceAggregationDescriptor<T> Unit(DistanceUnit? unit) => Assign(unit, (a, v) => a.Unit = v);

		public GeoDistanceAggregationDescriptor<T> DistanceType(GeoDistanceType? geoDistance) => Assign(geoDistance, (a, v) => a.DistanceType = v);

		public GeoDistanceAggregationDescriptor<T> Ranges(params Func<AggregationRangeDescriptor, IAggregationRange>[] ranges) =>
			Assign(ranges?.Select(r => r(new AggregationRangeDescriptor())), (a, v) => a.Ranges = v);
	}
}
