using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<HourlySchedule>))]
	public interface IHourlySchedule : ISchedule
	{
		[JsonProperty("minute")]
		IEnumerable<int> Minute { get; set; }
	}

	public class HourlySchedule : ScheduleBase, IHourlySchedule
	{
		public IEnumerable<int> Minute { get; set; }

		internal override void WrapInContainer(IScheduleContainer container) => container.Hourly = this;
	}

	public class HourlyScheduleDescriptor : DescriptorBase<HourlyScheduleDescriptor, IHourlySchedule>, IHourlySchedule
	{
		IEnumerable<int> IHourlySchedule.Minute { get; set; }

		public HourlyScheduleDescriptor Minute(params int[] minutes) => Assign(a => a.Minute = minutes);

		public HourlyScheduleDescriptor Minute(IEnumerable<int> minutes) => Assign(a => a.Minute = minutes);
	}
}
