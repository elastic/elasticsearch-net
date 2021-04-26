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
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(GeoLineAggregation))]
	public interface IGeoLineAggregation : IAggregation
	{
		[DataMember(Name ="point")]
		GeoLinePoint Point { get; set; }

		[DataMember(Name = "sort")]
		GeoLineSort Sort { get; set; }

		[DataMember(Name = "include_sort")]
		bool? IncludeSort { get; set; }

		[DataMember(Name = "sort_order")]
		string SortOrder { get; set; }

		[DataMember(Name = "size")]
		int? Size { get; set; }
	}

	public class GeoLineAggregation : AggregationBase, IGeoLineAggregation
	{
		internal GeoLineAggregation() { }

		public GeoLineAggregation(string name, Field point, Field sort) : base(name)
		{
			Point = new GeoLinePoint{ Field =  point };
			Sort = new GeoLineSort{ Field = sort };
		}
		
		internal override void WrapInContainer(AggregationContainer c) => c.GeoLine = this;

		public GeoLinePoint Point { get; set; }
		public GeoLineSort Sort { get; set; }
		public bool? IncludeSort { get; set; }
		public string SortOrder { get; set; }
		public int? Size { get; set; }
	}

	public class GeoLinePoint
	{
		public Field Field { get; set; }
	}

	public class GeoLineSort
	{
		public Field Field { get; set; }
	}

	public class GeoLineAggregationDescriptor<T>
		: DescriptorBase<GeoLineAggregationDescriptor<T>, IGeoLineAggregation>, IGeoLineAggregation
		where T : class
	{
		public GeoLineAggregationDescriptor() { }
		
		// Since we are not using MetricAggregationDescriptorBase we define these here
		IDictionary<string, object> IAggregation.Meta { get; set; }
		string IAggregation.Name { get; set; }
		
		GeoLinePoint IGeoLineAggregation.Point { get; set; }
		GeoLineSort IGeoLineAggregation.Sort { get; set; }
		bool? IGeoLineAggregation.IncludeSort { get; set; }
		string IGeoLineAggregation.SortOrder { get; set; }
		int? IGeoLineAggregation.Size { get; set; }
		

		/// <inheritdoc cref="IGeoLineAggregation.Point"/>
		public GeoLineAggregationDescriptor<T> Point(Field field) => Assign(field, (a, v) => a.Point = new GeoLinePoint { Field = v });

		/// <inheritdoc cref="IGeoLineAggregation.Point"/>
		public GeoLineAggregationDescriptor<T> Point<TValue>(Expression<Func<T, TValue>> field) => Assign(field, (a, v) => a.Point = new GeoLinePoint { Field = v });

		/// <inheritdoc cref="IGeoLineAggregation.Sort"/>
		public GeoLineAggregationDescriptor<T> Sort(Field field) => Assign(field, (a, v) => a.Sort = new GeoLineSort { Field = v });

		/// <inheritdoc cref="IGeoLineAggregation.Sort"/>
		public GeoLineAggregationDescriptor<T> Sort<TValue>(Expression<Func<T, TValue>> field) => Assign(field, (a, v) => a.Sort = new GeoLineSort { Field = v });

		/// <inheritdoc cref="IGeoLineAggregation.IncludeSort"/>
		public GeoLineAggregationDescriptor<T> IncludeSort(bool? includeSort = true) => Assign(includeSort, (a, v) => a.IncludeSort = v);

		/// <inheritdoc cref="IGeoLineAggregation.SortOrder"/>
		public GeoLineAggregationDescriptor<T> SortOrder(string sortOrder) => Assign(sortOrder, (a, v) => a.SortOrder = v);

		/// <inheritdoc cref="IGeoLineAggregation.Size"/>
		public GeoLineAggregationDescriptor<T> Size(int? size) => Assign(size, (a, v) => a.Size = v);
	}
}
