using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	[ReadAs(typeof(ExistsQuery))]
	public interface IExistsQuery : IQuery
	{
		[DataMember(Name ="field")]
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

		public ExistsQueryDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public ExistsQueryDescriptor<T> Field(Expression<Func<T, object>> objectPath) => Assign(a => a.Field = objectPath);
	}
}
