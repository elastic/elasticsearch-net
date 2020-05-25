// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Linq.Expressions;

namespace Nest
{
	public abstract class FieldNameQueryDescriptorBase<TDescriptor, TInterface, T>
		: QueryDescriptorBase<TDescriptor, TInterface>, IFieldNameQuery
		where TDescriptor : FieldNameQueryDescriptorBase<TDescriptor, TInterface, T>, TInterface
		where TInterface : class, IFieldNameQuery
		where T : class
	{
		Field IFieldNameQuery.Field { get; set; }

		bool IQuery.IsStrict { get; set; }

		bool IQuery.IsVerbatim { get; set; }

		public TDescriptor Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public TDescriptor Field<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);
	}
}
