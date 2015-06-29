using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<ExistsQuery>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IExistsQuery : IFieldNameQuery
	{
	}

	public class ExistsQuery : FieldNameQuery, IExistsQuery
	{
		bool IQuery.Conditionless { get { return QueryCondition.IsConditionless(this); } }

		protected override void WrapInContainer(IQueryContainer container)
		{
			container.Exists = this;
		}
	}

	public class ExistsQueryDescriptor<T> : IExistsQuery where T : class
	{
		private IExistsQuery Self => this;
		string IQuery.Name { get; set; }
		bool IQuery.Conditionless { get { return QueryCondition.IsConditionless(this); } }
		PropertyPathMarker IFieldNameQuery.Field { get; set;}

		public ExistsQueryDescriptor<T> Field (string field)
		{
			Self.Field = field;
			return this;
		}

		public ExistsQueryDescriptor<T> Field(Expression<Func<T, object>> field)
		{
			Self.Field = field;
			return this;
		}

		public ExistsQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}
	}
}
