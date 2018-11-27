using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public interface ISchedule { }

	public abstract class ScheduleBase : ISchedule
	{
		internal abstract void WrapInContainer(IScheduleContainer container);
	}
}
