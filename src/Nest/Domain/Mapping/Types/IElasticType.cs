namespace Nest
{
	public interface IElasticType : IFieldMapping
	{
		PropertyNameMarker Name { get; set; }
		TypeNameMarker Type { get; }
	}
}
