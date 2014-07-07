using Nest.Resolvers;

namespace Nest
{
	public interface IElasticType 
	{
		PropertyNameMarker Name { get; set; }
		TypeNameMarker Type { get; }
	}
}
