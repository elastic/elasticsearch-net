namespace Nest
{
	public interface IElasticCoreType : IElasticType
	{
		string IndexName { get; set; }
	}
}
