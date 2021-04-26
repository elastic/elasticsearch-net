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
