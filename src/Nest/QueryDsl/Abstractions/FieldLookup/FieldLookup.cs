// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
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
