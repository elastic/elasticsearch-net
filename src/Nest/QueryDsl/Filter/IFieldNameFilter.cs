namespace Nest
{
	public interface IFieldNameFilter : IFilter
	{
		PropertyPathMarker Field { get; set; }
	}
}