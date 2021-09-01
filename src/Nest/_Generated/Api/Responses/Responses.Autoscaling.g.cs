// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
//
// ------------------------------------------------

using OneOf;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable restore
namespace Nest.Autoscaling
{
	public partial class DeleteAutoscalingPolicyResponse : AcknowledgedResponseBase
	{
	}

	public partial class GetAutoscalingCapacityResponse : ResponseBase
	{
		[JsonPropertyName("policies")]
		public Dictionary<string, Nest.Autoscaling.GetAutoscalingCapacity.AutoscalingDeciders> Policies
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class GetAutoscalingPolicyResponse : ResponseBase
	{
	}

	public partial class PutAutoscalingPolicyResponse : AcknowledgedResponseBase
	{
	}
}