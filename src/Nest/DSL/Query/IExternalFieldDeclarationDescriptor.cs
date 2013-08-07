using Nest.Resolvers;

namespace Nest
{
	public interface IExternalFieldDeclarationDescriptor
	{
		IndexNameMarker _Index { get; set; }
		TypeNameMarker _Type { get; set; }
		string _Id { get; set; }
		string _Path { get; set; }
	}
}