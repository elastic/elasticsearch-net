// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	public class StartTrialLicenseResponse : AcknowledgedResponseBase
	{
		[DataMember(Name ="error_message")]
		public string ErrorMessage { get; internal set; }

		[DataMember(Name ="trial_was_started")]
		public bool TrialWasStarted { get; internal set; }
	}
}
