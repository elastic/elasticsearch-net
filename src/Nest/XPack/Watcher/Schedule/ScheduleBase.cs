using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonObject]
	public interface ISchedule {}

	public abstract class ScheduleBase : ISchedule
	{
		internal abstract void WrapInContainer(IScheduleContainer container);
	}
}
