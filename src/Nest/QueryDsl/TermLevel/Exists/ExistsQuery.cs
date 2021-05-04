// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(ExistsQuery))]
	public interface IExistsQuery : IQuery
	{
		[DataMember(Name = "field")]
		Field Field { get; set; }
	}

	public class ExistsQuery : QueryBase, IExistsQuery
	{
		public Field Field { get; set; }
		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Exists = this;

		internal static bool IsConditionless(IExistsQuery q) => q.Field.IsConditionless();
	}

	public class ExistsQueryDescriptor<T>
		: QueryDescriptorBase<ExistsQueryDescriptor<T>, IExistsQuery>
			, IExistsQuery where T : class
	{
		protected override bool Conditionless => ExistsQuery.IsConditionless(this);

		Field IExistsQuery.Field { get; set; }

		public ExistsQueryDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public ExistsQueryDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.Field = v);
	}
}
