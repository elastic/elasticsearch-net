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

using Elastic.Transport.Products.Elasticsearch.Failures;
using OneOf;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Ilm.MoveToStep
{
	[ConvertAs(typeof(StepKey))]
	public partial interface IStepKey
	{
		string Action { get; set; }

		string Name { get; set; }

		string Phase { get; set; }
	}

	public partial class StepKeyDescriptor : DescriptorBase<StepKeyDescriptor, IStepKey>, IStepKey
	{
		string IStepKey.Action { get; set; }

		string IStepKey.Name { get; set; }

		string IStepKey.Phase { get; set; }
	}

	public partial class StepKey : IStepKey
	{
		[JsonInclude]
		[JsonPropertyName("action")]
		public string Action { get; set; }

		[JsonInclude]
		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonInclude]
		[JsonPropertyName("phase")]
		public string Phase { get; set; }
	}
}