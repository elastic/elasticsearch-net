namespace Nest_5_2_0
{
	public interface IFieldNameQuery : IQuery
	{
		Field Field { get; set; }
	}

	public abstract class FieldNameQueryBase : QueryBase, IFieldNameQuery
	{
		public Field Field { get; set; }
	}
}
