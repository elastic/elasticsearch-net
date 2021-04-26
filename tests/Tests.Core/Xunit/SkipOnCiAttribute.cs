/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;

namespace Tests.Core.Xunit
{
	public class SkipOnCiAttribute : SkipTestAttributeBase
	{
		public override string Reason { get; } = "Skip running this test on TeamCity, this is usually a sign this test is flakey?";

		public static bool RunningOnTeamCity => !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TEAMCITY_VERSION"));
		public static bool RunningOnAzureDevops => !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TF_BUILD"));
		public static bool RunningOnAppVeyor => !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("APPVEYOR_BUILD_VERSION"));
		public override bool Skip => RunningOnTeamCity || RunningOnAppVeyor || RunningOnAzureDevops;
	}
	//

	//TODO 7.0: this attribute and all its usages have to be scrubbed before we can do a 7.x release
	/// <summary> Indicates test can not run because of a presumed upstream bug </summary>
	public class BlockedByIssueAttribute : SkipTestAttributeBase
	{
		public BlockedByIssueAttribute(string url) => Url = url;

		public override string Reason => $"Blocked temporarily by {Url}";

		public override bool Skip => true;

		private string Url { get; }
	}

	public class SkipAttribute : SkipTestAttributeBase
	{
		public SkipAttribute(string reason) => Reason = reason;

		public override bool Skip => true;
		public override string Reason { get; }
	}
}
