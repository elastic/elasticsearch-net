using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(AlwaysCondition))]
	public interface IAlwaysCondition : ICondition { }

	public class AlwaysCondition : ConditionBase, IAlwaysCondition
	{
		internal override void WrapInContainer(IConditionContainer container) => container.Always = this;
	}
}
