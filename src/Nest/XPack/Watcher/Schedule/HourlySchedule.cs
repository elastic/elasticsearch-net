// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(HourlySchedule))]
	public interface IHourlySchedule : ISchedule
	{
		[DataMember(Name = "minute")]
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

		public HourlyScheduleDescriptor Minute(params int[] minutes) => Assign(minutes, (a, v) => a.Minute = v);

		public HourlyScheduleDescriptor Minute(IEnumerable<int> minutes) => Assign(minutes, (a, v) => a.Minute = v);
	}
}
