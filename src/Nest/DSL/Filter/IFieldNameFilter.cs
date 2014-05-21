namespace Nest
{
	public interface IFieldNameFilter : IFilter
	{
		string GetFieldName();
	}
}