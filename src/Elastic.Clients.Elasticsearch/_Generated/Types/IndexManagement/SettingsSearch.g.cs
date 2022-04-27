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
	public partial class SettingsSearch
	{
		[JsonInclude]
		[JsonPropertyName("idle")]
		public Elastic.Clients.Elasticsearch.IndexManagement.SearchIdle Idle { get; set; }
	}

	public sealed partial class SettingsSearchDescriptor : SerializableDescriptorBase<SettingsSearchDescriptor>
	{
		internal SettingsSearchDescriptor(Action<SettingsSearchDescriptor> configure) => configure.Invoke(this);
		public SettingsSearchDescriptor() : base()
		{
		}

		private Elastic.Clients.Elasticsearch.IndexManagement.SearchIdle IdleValue { get; set; }

		private SearchIdleDescriptor IdleDescriptor { get; set; }

		private Action<SearchIdleDescriptor> IdleDescriptorAction { get; set; }

		public SettingsSearchDescriptor Idle(Elastic.Clients.Elasticsearch.IndexManagement.SearchIdle idle)
		{
			IdleDescriptor = null;
			IdleDescriptorAction = null;
			IdleValue = idle;
			return Self;
		}

		public SettingsSearchDescriptor Idle(SearchIdleDescriptor descriptor)
		{
			IdleValue = null;
			IdleDescriptorAction = null;
			IdleDescriptor = descriptor;
			return Self;
		}

		public SettingsSearchDescriptor Idle(Action<SearchIdleDescriptor> configure)
		{
			IdleValue = null;
			IdleDescriptorAction = null;
			IdleDescriptorAction = configure;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (IdleDescriptor is not null)
			{
				writer.WritePropertyName("idle");
				JsonSerializer.Serialize(writer, IdleDescriptor, options);
			}
			else if (IdleDescriptorAction is not null)
			{
				writer.WritePropertyName("idle");
				JsonSerializer.Serialize(writer, new SearchIdleDescriptor(IdleDescriptorAction), options);
			}
			else
			{
				writer.WritePropertyName("idle");
				JsonSerializer.Serialize(writer, IdleValue, options);
			}

			writer.WriteEndObject();
		}
	}
}