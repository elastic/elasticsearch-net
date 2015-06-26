namespace Nest
{
	public interface IMultiTermVectorDocumentDescriptor
	{
		MultiTermVectorDocument Document { get; set; }
		MultiTermVectorDocument GetDocument(); 
	}
}