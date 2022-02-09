// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Runtime.Serialization;

namespace Tests.Domain;

public class ProjectRuntimeFields
{
	[DataMember(Name = "runtime_started_on_day_of_week")]
	public string StartedOnDayOfWeek { get; set; }

	[DataMember(Name = "runtime_thirty_days_after_started")]
	public string ThirtyDaysFromStarted { get; set; }
}
