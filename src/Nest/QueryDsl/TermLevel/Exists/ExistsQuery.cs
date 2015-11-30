using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(FieldNameAsValueJsonConverter<ExistsQuery>))]
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
