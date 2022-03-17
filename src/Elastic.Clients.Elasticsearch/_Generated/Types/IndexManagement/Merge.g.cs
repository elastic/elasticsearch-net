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

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.IndexManagement
{
	public partial class Merge
	{
		[JsonInclude]
		[JsonPropertyName("scheduler")]
		public Elastic.Clients.Elasticsearch.IndexManagement.MergeScheduler? Scheduler { get; set; }
	}

	public sealed partial class MergeDescriptor : DescriptorBase<MergeDescriptor>
	{
		internal MergeDescriptor(Action<MergeDescriptor> configure) => configure.Invoke(this);
		public MergeDescriptor() : base()
		{
		}

		private Elastic.Clients.Elasticsearch.IndexManagement.MergeScheduler? SchedulerValue { get; set; }

		private MergeSchedulerDescriptor SchedulerDescriptor { get; set; }

		private Action<MergeSchedulerDescriptor> SchedulerDescriptorAction { get; set; }

		public MergeDescriptor Scheduler(Elastic.Clients.Elasticsearch.IndexManagement.MergeScheduler? scheduler)
		{
			SchedulerDescriptor = null;
			SchedulerDescriptorAction = null;
			SchedulerValue = scheduler;
			return Self;
		}

		public MergeDescriptor Scheduler(IndexManagement.MergeSchedulerDescriptor descriptor)
		{
			SchedulerValue = null;
			SchedulerDescriptorAction = null;
			SchedulerDescriptor = descriptor;
			return Self;
		}

		public MergeDescriptor Scheduler(Action<IndexManagement.MergeSchedulerDescriptor> configure)
		{
			SchedulerValue = null;
			SchedulerDescriptorAction = null;
			SchedulerDescriptorAction = configure;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (SchedulerDescriptor is not null)
			{
				writer.WritePropertyName("scheduler");
				JsonSerializer.Serialize(writer, SchedulerDescriptor, options);
			}
			else if (SchedulerDescriptorAction is not null)
			{
				writer.WritePropertyName("scheduler");
				JsonSerializer.Serialize(writer, new IndexManagement.MergeSchedulerDescriptor(SchedulerDescriptorAction), options);
			}
			else if (SchedulerValue is not null)
			{
				writer.WritePropertyName("scheduler");
				JsonSerializer.Serialize(writer, SchedulerValue, options);
			}

			writer.WriteEndObject();
		}
	}
}