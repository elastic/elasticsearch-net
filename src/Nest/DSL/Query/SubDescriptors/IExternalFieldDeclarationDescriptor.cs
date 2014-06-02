using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<ExternalFieldDeclarationDescriptor<object>>))]
	public interface IExternalFieldDeclarationDescriptor
	{
		IndexNameMarker _Index { get; set; }
		TypeNameMarker _Type { get; set; }
		string _Id { get; set; }
		PropertyPathMarker _Path { get; set; }
	}
}