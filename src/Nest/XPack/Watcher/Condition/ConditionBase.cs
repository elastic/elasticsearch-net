using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	public interface ICondition { }

	public abstract class ConditionBase : ICondition
	{
		internal abstract void WrapInContainer(IConditionContainer container);
	}
}
