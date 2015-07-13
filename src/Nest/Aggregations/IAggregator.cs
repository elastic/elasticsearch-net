namespace Nest
{
	/// <summary>
	/// Describes an aggregation at request time when its being build
	/// </summary>
	public interface IAggregator { }

	/// <summary>
	/// Base for the OIS Aggregation DSL
	/// </summary>
	public interface IAggregatorBase : IAggregator
	{
		string Name { get; set; }
	}

	public abstract class AggregatorBase : IAggregatorBase
	{
		string IAggregatorBase.Name { get; set; }
		
		protected AggregatorBase(string name)
		{
			((IAggregatorBase) this).Name = name;
		}
	}
}