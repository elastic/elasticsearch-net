using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	public interface ISchedule { }

	public abstract class ScheduleBase : ISchedule
	{
		internal abstract void WrapInContainer(IScheduleContainer container);
	}
}
