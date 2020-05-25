// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Nest
{
	[ReadAs(typeof(SlicedScroll))]
	public interface ISlicedScroll
	{
		[DataMember(Name ="field")]
		Field Field { get; set; }

		[DataMember(Name ="id")]
		int? Id { get; set; }

		[DataMember(Name ="max")]
		int? Max { get; set; }
	}

	public class SlicedScroll : ISlicedScroll
	{
		public Field Field { get; set; }
		public int? Id { get; set; }
		public int? Max { get; set; }
	}

	public class SlicedScrollDescriptor<T> : DescriptorBase<SlicedScrollDescriptor<T>, ISlicedScroll>, ISlicedScroll
		where T : class
	{
		Field ISlicedScroll.Field { get; set; }
		int? ISlicedScroll.Id { get; set; }
		int? ISlicedScroll.Max { get; set; }

		public SlicedScrollDescriptor<T> Id(int? id) => Assign(id, (a, v) => a.Id = v);

		public SlicedScrollDescriptor<T> Max(int? max) => Assign(max, (a, v) => a.Max = v);

		public SlicedScrollDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public SlicedScrollDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.Field = v);
	}
}
