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
	[ReadAs(typeof(FieldLookup))]
	public interface IFieldLookup
	{
		[DataMember(Name ="id")]
		Id Id { get; set; }

		[DataMember(Name ="index")]
		IndexName Index { get; set; }

		[DataMember(Name ="path")]
		Field Path { get; set; }

		[DataMember(Name ="routing")]
		Routing Routing { get; set; }
	}

	public class FieldLookup : IFieldLookup
	{
		public Id Id { get; set; }
		public IndexName Index { get; set; }
		public Field Path { get; set; }
		public Routing Routing { get; set; }
	}

	public class FieldLookupDescriptor<T> : DescriptorBase<FieldLookupDescriptor<T>, IFieldLookup>, IFieldLookup
		where T : class
	{
		public FieldLookupDescriptor() => Self.Index = ClrType;

		private static Type ClrType => typeof(T);

		Id IFieldLookup.Id { get; set; }

		IndexName IFieldLookup.Index { get; set; }

		Field IFieldLookup.Path { get; set; }

		Routing IFieldLookup.Routing { get; set; }

		public FieldLookupDescriptor<T> Index(IndexName index) => Assign(index, (a, v) => a.Index = v);

		public FieldLookupDescriptor<T> Id(Id id) => Assign(id, (a, v) => a.Id = v);

		public FieldLookupDescriptor<T> Path(Field path) => Assign(path, (a, v) => a.Path = v);

		public FieldLookupDescriptor<T> Path<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.Path = v);

		public FieldLookupDescriptor<T> Routing(Routing routing) => Assign(routing, (a, v) => a.Routing = v);
	}
}
