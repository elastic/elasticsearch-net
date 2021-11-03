// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;

namespace Tests.Domain;

public class ProjectTransform
{
	public double? AverageCommits { get; set; }

	public long WeekStartedOnMillis { get; set; }

	public DateTime WeekStartedOnDate { get; set; }

	public long SumIntoMaster { get; set; }
}
