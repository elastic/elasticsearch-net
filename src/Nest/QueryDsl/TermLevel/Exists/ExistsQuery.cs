using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<ExistsQuery>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IExistsQuery : IFieldNameQuery
	{
	}

	public class ExistsQuery : FieldNameQueryBase, IExistsQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);

		protected override void WrapInContainer(IQueryContainer c) => c.Exists = this;
		internal static bool IsConditionless(IExistsQuery q) => q.Field.IsConditionless();
	}

	public class ExistsQueryDescriptor<T> 
		: FieldNameQueryDescriptorBase<ExistsQueryDescriptor<T>, IExistsQuery, T>
		, IExistsQuery where T : class
	{
		bool IQuery.Conditionless => ExistsQuery.IsConditionless(this);
	}
}
