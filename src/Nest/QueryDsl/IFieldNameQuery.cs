namespace Nest
{
	public interface IFieldNameQuery : IQuery
	{
		PropertyPathMarker Field { get; set; }
	}
}