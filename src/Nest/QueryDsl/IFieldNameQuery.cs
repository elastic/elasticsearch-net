namespace Nest
{
	public interface IFieldNameQuery : IQuery
	{
		PropertyPathMarker GetFieldName();
		void SetFieldName(string fieldName);
	}
}