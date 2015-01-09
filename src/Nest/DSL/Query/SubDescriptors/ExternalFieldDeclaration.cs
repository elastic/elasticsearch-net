namespace Nest
{
	public class ExternalFieldDeclaration : IExternalFieldDeclaration
	{
		public IndexNameMarker Index { get; set; }
		public TypeNameMarker Type { get; set; }
		public string Id { get; set; }
		public PropertyPathMarker Path { get; set; }
	}
}