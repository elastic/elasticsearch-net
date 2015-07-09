namespace Nest
{
	//TODO belongs to writer should probably live over there as well
	public interface IElasticPropertyVisitor
	{
		void Visit(ElasticPropertyAttribute attribute);
	}
}