// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
