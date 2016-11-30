using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public interface ISchedule {}

	public abstract class ScheduleBase : ISchedule
	{
		internal abstract void WrapInContainer(IScheduleContainer container);
	}
}
