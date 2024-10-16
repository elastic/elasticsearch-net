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

#nullable restore

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Ingest;

public sealed partial class CommunityIDProcessor
{
	/// <summary>
	/// <para>
	/// Description of the processor.
	/// Useful for describing the purpose of the processor or its configuration.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("description")]
	public string? Description { get; set; }

	/// <summary>
	/// <para>
	/// Field containing the destination IP address.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("destination_ip")]
	public Elastic.Clients.Elasticsearch.Field? DestinationIp { get; set; }

	/// <summary>
	/// <para>
	/// Field containing the destination port.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("destination_port")]
	public Elastic.Clients.Elasticsearch.Field? DestinationPort { get; set; }

	/// <summary>
	/// <para>
	/// Field containing the IANA number.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("iana_number")]
	public Elastic.Clients.Elasticsearch.Field? IanaNumber { get; set; }

	/// <summary>
	/// <para>
	/// Field containing the ICMP code.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("icmp_code")]
	public Elastic.Clients.Elasticsearch.Field? IcmpCode { get; set; }

	/// <summary>
	/// <para>
	/// Field containing the ICMP type.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("icmp_type")]
	public Elastic.Clients.Elasticsearch.Field? IcmpType { get; set; }

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("if")]
	public string? If { get; set; }

	/// <summary>
	/// <para>
	/// Ignore failures for the processor.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("ignore_failure")]
	public bool? IgnoreFailure { get; set; }

	/// <summary>
	/// <para>
	/// If true and any required fields are missing, the processor quietly exits
	/// without modifying the document.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("ignore_missing")]
	public bool? IgnoreMissing { get; set; }

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("on_failure")]
	public ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? OnFailure { get; set; }

	/// <summary>
	/// <para>
	/// Seed for the community ID hash. Must be between 0 and 65535 (inclusive). The
	/// seed can prevent hash collisions between network domains, such as a staging
	/// and production network that use the same addressing scheme.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("seed")]
	public int? Seed { get; set; }

	/// <summary>
	/// <para>
	/// Field containing the source IP address.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("source_ip")]
	public Elastic.Clients.Elasticsearch.Field? SourceIp { get; set; }

	/// <summary>
	/// <para>
	/// Field containing the source port.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("source_port")]
	public Elastic.Clients.Elasticsearch.Field? SourcePort { get; set; }

	/// <summary>
	/// <para>
	/// Identifier for the processor.
	/// Useful for debugging and metrics.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("tag")]
	public string? Tag { get; set; }

	/// <summary>
	/// <para>
	/// Output field for the community ID.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("target_field")]
	public Elastic.Clients.Elasticsearch.Field? TargetField { get; set; }

	/// <summary>
	/// <para>
	/// Field containing the transport protocol name or number. Used only when the
	/// iana_number field is not present. The following protocol names are currently
	/// supported: eigrp, gre, icmp, icmpv6, igmp, ipv6-icmp, ospf, pim, sctp, tcp, udp
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("transport")]
	public Elastic.Clients.Elasticsearch.Field? Transport { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.Ingest.Processor(CommunityIDProcessor communityIDProcessor) => Elastic.Clients.Elasticsearch.Ingest.Processor.CommunityId(communityIDProcessor);
}

public sealed partial class CommunityIDProcessorDescriptor<TDocument> : SerializableDescriptor<CommunityIDProcessorDescriptor<TDocument>>
{
	internal CommunityIDProcessorDescriptor(Action<CommunityIDProcessorDescriptor<TDocument>> configure) => configure.Invoke(this);

	public CommunityIDProcessorDescriptor() : base()
	{
	}

	private string? DescriptionValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field? DestinationIpValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field? DestinationPortValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field? IanaNumberValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field? IcmpCodeValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field? IcmpTypeValue { get; set; }
	private string? IfValue { get; set; }
	private bool? IgnoreFailureValue { get; set; }
	private bool? IgnoreMissingValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? OnFailureValue { get; set; }
	private Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument> OnFailureDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument>> OnFailureDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument>>[] OnFailureDescriptorActions { get; set; }
	private int? SeedValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field? SourceIpValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field? SourcePortValue { get; set; }
	private string? TagValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field? TargetFieldValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field? TransportValue { get; set; }

	/// <summary>
	/// <para>
	/// Description of the processor.
	/// Useful for describing the purpose of the processor or its configuration.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor<TDocument> Description(string? description)
	{
		DescriptionValue = description;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the destination IP address.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor<TDocument> DestinationIp(Elastic.Clients.Elasticsearch.Field? destinationIp)
	{
		DestinationIpValue = destinationIp;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the destination IP address.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor<TDocument> DestinationIp<TValue>(Expression<Func<TDocument, TValue>> destinationIp)
	{
		DestinationIpValue = destinationIp;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the destination IP address.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor<TDocument> DestinationIp(Expression<Func<TDocument, object>> destinationIp)
	{
		DestinationIpValue = destinationIp;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the destination port.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor<TDocument> DestinationPort(Elastic.Clients.Elasticsearch.Field? destinationPort)
	{
		DestinationPortValue = destinationPort;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the destination port.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor<TDocument> DestinationPort<TValue>(Expression<Func<TDocument, TValue>> destinationPort)
	{
		DestinationPortValue = destinationPort;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the destination port.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor<TDocument> DestinationPort(Expression<Func<TDocument, object>> destinationPort)
	{
		DestinationPortValue = destinationPort;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the IANA number.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor<TDocument> IanaNumber(Elastic.Clients.Elasticsearch.Field? ianaNumber)
	{
		IanaNumberValue = ianaNumber;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the IANA number.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor<TDocument> IanaNumber<TValue>(Expression<Func<TDocument, TValue>> ianaNumber)
	{
		IanaNumberValue = ianaNumber;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the IANA number.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor<TDocument> IanaNumber(Expression<Func<TDocument, object>> ianaNumber)
	{
		IanaNumberValue = ianaNumber;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the ICMP code.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor<TDocument> IcmpCode(Elastic.Clients.Elasticsearch.Field? icmpCode)
	{
		IcmpCodeValue = icmpCode;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the ICMP code.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor<TDocument> IcmpCode<TValue>(Expression<Func<TDocument, TValue>> icmpCode)
	{
		IcmpCodeValue = icmpCode;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the ICMP code.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor<TDocument> IcmpCode(Expression<Func<TDocument, object>> icmpCode)
	{
		IcmpCodeValue = icmpCode;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the ICMP type.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor<TDocument> IcmpType(Elastic.Clients.Elasticsearch.Field? icmpType)
	{
		IcmpTypeValue = icmpType;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the ICMP type.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor<TDocument> IcmpType<TValue>(Expression<Func<TDocument, TValue>> icmpType)
	{
		IcmpTypeValue = icmpType;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the ICMP type.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor<TDocument> IcmpType(Expression<Func<TDocument, object>> icmpType)
	{
		IcmpTypeValue = icmpType;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor<TDocument> If(string? value)
	{
		IfValue = value;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Ignore failures for the processor.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor<TDocument> IgnoreFailure(bool? ignoreFailure = true)
	{
		IgnoreFailureValue = ignoreFailure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// If true and any required fields are missing, the processor quietly exits
	/// without modifying the document.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor<TDocument> IgnoreMissing(bool? ignoreMissing = true)
	{
		IgnoreMissingValue = ignoreMissing;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor<TDocument> OnFailure(ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? onFailure)
	{
		OnFailureDescriptor = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = null;
		OnFailureValue = onFailure;
		return Self;
	}

	public CommunityIDProcessorDescriptor<TDocument> OnFailure(Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument> descriptor)
	{
		OnFailureValue = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = null;
		OnFailureDescriptor = descriptor;
		return Self;
	}

	public CommunityIDProcessorDescriptor<TDocument> OnFailure(Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument>> configure)
	{
		OnFailureValue = null;
		OnFailureDescriptor = null;
		OnFailureDescriptorActions = null;
		OnFailureDescriptorAction = configure;
		return Self;
	}

	public CommunityIDProcessorDescriptor<TDocument> OnFailure(params Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument>>[] configure)
	{
		OnFailureValue = null;
		OnFailureDescriptor = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Seed for the community ID hash. Must be between 0 and 65535 (inclusive). The
	/// seed can prevent hash collisions between network domains, such as a staging
	/// and production network that use the same addressing scheme.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor<TDocument> Seed(int? seed)
	{
		SeedValue = seed;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the source IP address.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor<TDocument> SourceIp(Elastic.Clients.Elasticsearch.Field? sourceIp)
	{
		SourceIpValue = sourceIp;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the source IP address.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor<TDocument> SourceIp<TValue>(Expression<Func<TDocument, TValue>> sourceIp)
	{
		SourceIpValue = sourceIp;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the source IP address.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor<TDocument> SourceIp(Expression<Func<TDocument, object>> sourceIp)
	{
		SourceIpValue = sourceIp;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the source port.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor<TDocument> SourcePort(Elastic.Clients.Elasticsearch.Field? sourcePort)
	{
		SourcePortValue = sourcePort;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the source port.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor<TDocument> SourcePort<TValue>(Expression<Func<TDocument, TValue>> sourcePort)
	{
		SourcePortValue = sourcePort;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the source port.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor<TDocument> SourcePort(Expression<Func<TDocument, object>> sourcePort)
	{
		SourcePortValue = sourcePort;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Identifier for the processor.
	/// Useful for debugging and metrics.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor<TDocument> Tag(string? tag)
	{
		TagValue = tag;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Output field for the community ID.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor<TDocument> TargetField(Elastic.Clients.Elasticsearch.Field? targetField)
	{
		TargetFieldValue = targetField;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Output field for the community ID.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor<TDocument> TargetField<TValue>(Expression<Func<TDocument, TValue>> targetField)
	{
		TargetFieldValue = targetField;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Output field for the community ID.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor<TDocument> TargetField(Expression<Func<TDocument, object>> targetField)
	{
		TargetFieldValue = targetField;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the transport protocol name or number. Used only when the
	/// iana_number field is not present. The following protocol names are currently
	/// supported: eigrp, gre, icmp, icmpv6, igmp, ipv6-icmp, ospf, pim, sctp, tcp, udp
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor<TDocument> Transport(Elastic.Clients.Elasticsearch.Field? transport)
	{
		TransportValue = transport;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the transport protocol name or number. Used only when the
	/// iana_number field is not present. The following protocol names are currently
	/// supported: eigrp, gre, icmp, icmpv6, igmp, ipv6-icmp, ospf, pim, sctp, tcp, udp
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor<TDocument> Transport<TValue>(Expression<Func<TDocument, TValue>> transport)
	{
		TransportValue = transport;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the transport protocol name or number. Used only when the
	/// iana_number field is not present. The following protocol names are currently
	/// supported: eigrp, gre, icmp, icmpv6, igmp, ipv6-icmp, ospf, pim, sctp, tcp, udp
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor<TDocument> Transport(Expression<Func<TDocument, object>> transport)
	{
		TransportValue = transport;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(DescriptionValue))
		{
			writer.WritePropertyName("description");
			writer.WriteStringValue(DescriptionValue);
		}

		if (DestinationIpValue is not null)
		{
			writer.WritePropertyName("destination_ip");
			JsonSerializer.Serialize(writer, DestinationIpValue, options);
		}

		if (DestinationPortValue is not null)
		{
			writer.WritePropertyName("destination_port");
			JsonSerializer.Serialize(writer, DestinationPortValue, options);
		}

		if (IanaNumberValue is not null)
		{
			writer.WritePropertyName("iana_number");
			JsonSerializer.Serialize(writer, IanaNumberValue, options);
		}

		if (IcmpCodeValue is not null)
		{
			writer.WritePropertyName("icmp_code");
			JsonSerializer.Serialize(writer, IcmpCodeValue, options);
		}

		if (IcmpTypeValue is not null)
		{
			writer.WritePropertyName("icmp_type");
			JsonSerializer.Serialize(writer, IcmpTypeValue, options);
		}

		if (!string.IsNullOrEmpty(IfValue))
		{
			writer.WritePropertyName("if");
			writer.WriteStringValue(IfValue);
		}

		if (IgnoreFailureValue.HasValue)
		{
			writer.WritePropertyName("ignore_failure");
			writer.WriteBooleanValue(IgnoreFailureValue.Value);
		}

		if (IgnoreMissingValue.HasValue)
		{
			writer.WritePropertyName("ignore_missing");
			writer.WriteBooleanValue(IgnoreMissingValue.Value);
		}

		if (OnFailureDescriptor is not null)
		{
			writer.WritePropertyName("on_failure");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, OnFailureDescriptor, options);
			writer.WriteEndArray();
		}
		else if (OnFailureDescriptorAction is not null)
		{
			writer.WritePropertyName("on_failure");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument>(OnFailureDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (OnFailureDescriptorActions is not null)
		{
			writer.WritePropertyName("on_failure");
			writer.WriteStartArray();
			foreach (var action in OnFailureDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument>(action), options);
			}

			writer.WriteEndArray();
		}
		else if (OnFailureValue is not null)
		{
			writer.WritePropertyName("on_failure");
			JsonSerializer.Serialize(writer, OnFailureValue, options);
		}

		if (SeedValue.HasValue)
		{
			writer.WritePropertyName("seed");
			writer.WriteNumberValue(SeedValue.Value);
		}

		if (SourceIpValue is not null)
		{
			writer.WritePropertyName("source_ip");
			JsonSerializer.Serialize(writer, SourceIpValue, options);
		}

		if (SourcePortValue is not null)
		{
			writer.WritePropertyName("source_port");
			JsonSerializer.Serialize(writer, SourcePortValue, options);
		}

		if (!string.IsNullOrEmpty(TagValue))
		{
			writer.WritePropertyName("tag");
			writer.WriteStringValue(TagValue);
		}

		if (TargetFieldValue is not null)
		{
			writer.WritePropertyName("target_field");
			JsonSerializer.Serialize(writer, TargetFieldValue, options);
		}

		if (TransportValue is not null)
		{
			writer.WritePropertyName("transport");
			JsonSerializer.Serialize(writer, TransportValue, options);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class CommunityIDProcessorDescriptor : SerializableDescriptor<CommunityIDProcessorDescriptor>
{
	internal CommunityIDProcessorDescriptor(Action<CommunityIDProcessorDescriptor> configure) => configure.Invoke(this);

	public CommunityIDProcessorDescriptor() : base()
	{
	}

	private string? DescriptionValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field? DestinationIpValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field? DestinationPortValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field? IanaNumberValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field? IcmpCodeValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field? IcmpTypeValue { get; set; }
	private string? IfValue { get; set; }
	private bool? IgnoreFailureValue { get; set; }
	private bool? IgnoreMissingValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? OnFailureValue { get; set; }
	private Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor OnFailureDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor> OnFailureDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor>[] OnFailureDescriptorActions { get; set; }
	private int? SeedValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field? SourceIpValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field? SourcePortValue { get; set; }
	private string? TagValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field? TargetFieldValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field? TransportValue { get; set; }

	/// <summary>
	/// <para>
	/// Description of the processor.
	/// Useful for describing the purpose of the processor or its configuration.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor Description(string? description)
	{
		DescriptionValue = description;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the destination IP address.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor DestinationIp(Elastic.Clients.Elasticsearch.Field? destinationIp)
	{
		DestinationIpValue = destinationIp;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the destination IP address.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor DestinationIp<TDocument, TValue>(Expression<Func<TDocument, TValue>> destinationIp)
	{
		DestinationIpValue = destinationIp;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the destination IP address.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor DestinationIp<TDocument>(Expression<Func<TDocument, object>> destinationIp)
	{
		DestinationIpValue = destinationIp;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the destination port.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor DestinationPort(Elastic.Clients.Elasticsearch.Field? destinationPort)
	{
		DestinationPortValue = destinationPort;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the destination port.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor DestinationPort<TDocument, TValue>(Expression<Func<TDocument, TValue>> destinationPort)
	{
		DestinationPortValue = destinationPort;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the destination port.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor DestinationPort<TDocument>(Expression<Func<TDocument, object>> destinationPort)
	{
		DestinationPortValue = destinationPort;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the IANA number.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor IanaNumber(Elastic.Clients.Elasticsearch.Field? ianaNumber)
	{
		IanaNumberValue = ianaNumber;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the IANA number.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor IanaNumber<TDocument, TValue>(Expression<Func<TDocument, TValue>> ianaNumber)
	{
		IanaNumberValue = ianaNumber;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the IANA number.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor IanaNumber<TDocument>(Expression<Func<TDocument, object>> ianaNumber)
	{
		IanaNumberValue = ianaNumber;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the ICMP code.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor IcmpCode(Elastic.Clients.Elasticsearch.Field? icmpCode)
	{
		IcmpCodeValue = icmpCode;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the ICMP code.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor IcmpCode<TDocument, TValue>(Expression<Func<TDocument, TValue>> icmpCode)
	{
		IcmpCodeValue = icmpCode;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the ICMP code.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor IcmpCode<TDocument>(Expression<Func<TDocument, object>> icmpCode)
	{
		IcmpCodeValue = icmpCode;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the ICMP type.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor IcmpType(Elastic.Clients.Elasticsearch.Field? icmpType)
	{
		IcmpTypeValue = icmpType;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the ICMP type.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor IcmpType<TDocument, TValue>(Expression<Func<TDocument, TValue>> icmpType)
	{
		IcmpTypeValue = icmpType;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the ICMP type.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor IcmpType<TDocument>(Expression<Func<TDocument, object>> icmpType)
	{
		IcmpTypeValue = icmpType;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor If(string? value)
	{
		IfValue = value;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Ignore failures for the processor.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor IgnoreFailure(bool? ignoreFailure = true)
	{
		IgnoreFailureValue = ignoreFailure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// If true and any required fields are missing, the processor quietly exits
	/// without modifying the document.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor IgnoreMissing(bool? ignoreMissing = true)
	{
		IgnoreMissingValue = ignoreMissing;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor OnFailure(ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? onFailure)
	{
		OnFailureDescriptor = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = null;
		OnFailureValue = onFailure;
		return Self;
	}

	public CommunityIDProcessorDescriptor OnFailure(Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor descriptor)
	{
		OnFailureValue = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = null;
		OnFailureDescriptor = descriptor;
		return Self;
	}

	public CommunityIDProcessorDescriptor OnFailure(Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor> configure)
	{
		OnFailureValue = null;
		OnFailureDescriptor = null;
		OnFailureDescriptorActions = null;
		OnFailureDescriptorAction = configure;
		return Self;
	}

	public CommunityIDProcessorDescriptor OnFailure(params Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor>[] configure)
	{
		OnFailureValue = null;
		OnFailureDescriptor = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Seed for the community ID hash. Must be between 0 and 65535 (inclusive). The
	/// seed can prevent hash collisions between network domains, such as a staging
	/// and production network that use the same addressing scheme.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor Seed(int? seed)
	{
		SeedValue = seed;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the source IP address.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor SourceIp(Elastic.Clients.Elasticsearch.Field? sourceIp)
	{
		SourceIpValue = sourceIp;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the source IP address.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor SourceIp<TDocument, TValue>(Expression<Func<TDocument, TValue>> sourceIp)
	{
		SourceIpValue = sourceIp;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the source IP address.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor SourceIp<TDocument>(Expression<Func<TDocument, object>> sourceIp)
	{
		SourceIpValue = sourceIp;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the source port.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor SourcePort(Elastic.Clients.Elasticsearch.Field? sourcePort)
	{
		SourcePortValue = sourcePort;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the source port.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor SourcePort<TDocument, TValue>(Expression<Func<TDocument, TValue>> sourcePort)
	{
		SourcePortValue = sourcePort;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the source port.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor SourcePort<TDocument>(Expression<Func<TDocument, object>> sourcePort)
	{
		SourcePortValue = sourcePort;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Identifier for the processor.
	/// Useful for debugging and metrics.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor Tag(string? tag)
	{
		TagValue = tag;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Output field for the community ID.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor TargetField(Elastic.Clients.Elasticsearch.Field? targetField)
	{
		TargetFieldValue = targetField;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Output field for the community ID.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor TargetField<TDocument, TValue>(Expression<Func<TDocument, TValue>> targetField)
	{
		TargetFieldValue = targetField;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Output field for the community ID.
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor TargetField<TDocument>(Expression<Func<TDocument, object>> targetField)
	{
		TargetFieldValue = targetField;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the transport protocol name or number. Used only when the
	/// iana_number field is not present. The following protocol names are currently
	/// supported: eigrp, gre, icmp, icmpv6, igmp, ipv6-icmp, ospf, pim, sctp, tcp, udp
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor Transport(Elastic.Clients.Elasticsearch.Field? transport)
	{
		TransportValue = transport;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the transport protocol name or number. Used only when the
	/// iana_number field is not present. The following protocol names are currently
	/// supported: eigrp, gre, icmp, icmpv6, igmp, ipv6-icmp, ospf, pim, sctp, tcp, udp
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor Transport<TDocument, TValue>(Expression<Func<TDocument, TValue>> transport)
	{
		TransportValue = transport;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing the transport protocol name or number. Used only when the
	/// iana_number field is not present. The following protocol names are currently
	/// supported: eigrp, gre, icmp, icmpv6, igmp, ipv6-icmp, ospf, pim, sctp, tcp, udp
	/// </para>
	/// </summary>
	public CommunityIDProcessorDescriptor Transport<TDocument>(Expression<Func<TDocument, object>> transport)
	{
		TransportValue = transport;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(DescriptionValue))
		{
			writer.WritePropertyName("description");
			writer.WriteStringValue(DescriptionValue);
		}

		if (DestinationIpValue is not null)
		{
			writer.WritePropertyName("destination_ip");
			JsonSerializer.Serialize(writer, DestinationIpValue, options);
		}

		if (DestinationPortValue is not null)
		{
			writer.WritePropertyName("destination_port");
			JsonSerializer.Serialize(writer, DestinationPortValue, options);
		}

		if (IanaNumberValue is not null)
		{
			writer.WritePropertyName("iana_number");
			JsonSerializer.Serialize(writer, IanaNumberValue, options);
		}

		if (IcmpCodeValue is not null)
		{
			writer.WritePropertyName("icmp_code");
			JsonSerializer.Serialize(writer, IcmpCodeValue, options);
		}

		if (IcmpTypeValue is not null)
		{
			writer.WritePropertyName("icmp_type");
			JsonSerializer.Serialize(writer, IcmpTypeValue, options);
		}

		if (!string.IsNullOrEmpty(IfValue))
		{
			writer.WritePropertyName("if");
			writer.WriteStringValue(IfValue);
		}

		if (IgnoreFailureValue.HasValue)
		{
			writer.WritePropertyName("ignore_failure");
			writer.WriteBooleanValue(IgnoreFailureValue.Value);
		}

		if (IgnoreMissingValue.HasValue)
		{
			writer.WritePropertyName("ignore_missing");
			writer.WriteBooleanValue(IgnoreMissingValue.Value);
		}

		if (OnFailureDescriptor is not null)
		{
			writer.WritePropertyName("on_failure");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, OnFailureDescriptor, options);
			writer.WriteEndArray();
		}
		else if (OnFailureDescriptorAction is not null)
		{
			writer.WritePropertyName("on_failure");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor(OnFailureDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (OnFailureDescriptorActions is not null)
		{
			writer.WritePropertyName("on_failure");
			writer.WriteStartArray();
			foreach (var action in OnFailureDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor(action), options);
			}

			writer.WriteEndArray();
		}
		else if (OnFailureValue is not null)
		{
			writer.WritePropertyName("on_failure");
			JsonSerializer.Serialize(writer, OnFailureValue, options);
		}

		if (SeedValue.HasValue)
		{
			writer.WritePropertyName("seed");
			writer.WriteNumberValue(SeedValue.Value);
		}

		if (SourceIpValue is not null)
		{
			writer.WritePropertyName("source_ip");
			JsonSerializer.Serialize(writer, SourceIpValue, options);
		}

		if (SourcePortValue is not null)
		{
			writer.WritePropertyName("source_port");
			JsonSerializer.Serialize(writer, SourcePortValue, options);
		}

		if (!string.IsNullOrEmpty(TagValue))
		{
			writer.WritePropertyName("tag");
			writer.WriteStringValue(TagValue);
		}

		if (TargetFieldValue is not null)
		{
			writer.WritePropertyName("target_field");
			JsonSerializer.Serialize(writer, TargetFieldValue, options);
		}

		if (TransportValue is not null)
		{
			writer.WritePropertyName("transport");
			JsonSerializer.Serialize(writer, TransportValue, options);
		}

		writer.WriteEndObject();
	}
}