using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public interface ISchedule {}

	public abstract class ScheduleBase
	{
		internal abstract void WrapInContainer(IScheduleContainer container);
	}
}
