// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface INetworkCommunityIdProcessor : IProcessor
	{
		[DataMember(Name = "destination_ip")]
		Field DestinationIp { get; set; }

		[DataMember(Name = "destination_port")]
		Field DestinationPort { get; set; }

		[DataMember(Name = "iana_number")]
		Field IanaNumber { get; set; }

		[DataMember(Name = "icmp_type")]
		Field IcmpType { get; set; }

		[DataMember(Name = "icmp_code")]
		Field IcmpCode { get; set; }

		[DataMember(Name = "ignore_missing")]
		bool? IgnoreMissing { get; set; }

		[DataMember(Name = "seed")]
		int? Seed { get; set; }

		[DataMember(Name = "source_ip")]
		Field SourceIp { get; set; }

		[DataMember(Name = "source_port")]
		Field SourcePort { get; set; }

		[DataMember(Name = "target_field")]
		Field TargetField { get; set; }

		[DataMember(Name = "transport")]
		Field Transport { get; set; }
	}

	public class NetworkCommunityIdProcessor : ProcessorBase, INetworkCommunityIdProcessor
	{
		protected override string Name => "community_id";

		/// <inheritdoc />
		public Field DestinationIp { get; set; }
		/// <inheritdoc />
		public Field DestinationPort { get; set; }
		/// <inheritdoc />
		public Field IanaNumber { get; set; }
		/// <inheritdoc />
		public Field IcmpType { get; set; }
		/// <inheritdoc />
		public Field IcmpCode { get; set; }
		/// <inheritdoc />
		public bool? IgnoreMissing { get; set; }
		/// <inheritdoc />
		public int? Seed { get; set; }
		/// <inheritdoc />
		public Field SourceIp { get; set; }
		/// <inheritdoc />
		public Field SourcePort { get; set; }
		/// <inheritdoc />
		public Field TargetField { get; set; }
		/// <inheritdoc />
		public Field Transport { get; set; }
	}

	/// <inheritdoc cref="IFingerprintProcessor" />
	public class NetworkCommunityIdProcessorDescriptor<T>
		: ProcessorDescriptorBase<NetworkCommunityIdProcessorDescriptor<T>, INetworkCommunityIdProcessor>, INetworkCommunityIdProcessor
		where T : class
	{
		protected override string Name => "community_id";

		Field INetworkCommunityIdProcessor.DestinationIp { get; set; }

		Field INetworkCommunityIdProcessor.DestinationPort { get; set; }

		Field INetworkCommunityIdProcessor.IanaNumber { get; set; }

		Field INetworkCommunityIdProcessor.IcmpType { get; set; }

		Field INetworkCommunityIdProcessor.IcmpCode { get; set; }

		bool? INetworkCommunityIdProcessor.IgnoreMissing { get; set; }

		int? INetworkCommunityIdProcessor.Seed { get; set; }

		Field INetworkCommunityIdProcessor.SourceIp { get; set; }

		Field INetworkCommunityIdProcessor.SourcePort { get; set; }

		Field INetworkCommunityIdProcessor.TargetField { get; set; }

		Field INetworkCommunityIdProcessor.Transport { get; set; }

		/// <inheritdoc cref="INetworkCommunityIdProcessor.DestinationIp" />
		public NetworkCommunityIdProcessorDescriptor<T> DestinationIp(Field field) => Assign(field, (a, v) => a.DestinationIp = v);

		/// <inheritdoc cref="INetworkCommunityIdProcessor.DestinationIp" />
		public NetworkCommunityIdProcessorDescriptor<T> DestinationIp<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.DestinationIp = v);

		/// <inheritdoc cref="INetworkCommunityIdProcessor.DestinationPort" />
		public NetworkCommunityIdProcessorDescriptor<T> DestinationPort(Field field) => Assign(field, (a, v) => a.DestinationPort = v);

		/// <inheritdoc cref="INetworkCommunityIdProcessor.DestinationPort" />
		public NetworkCommunityIdProcessorDescriptor<T> DestinationPort<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.DestinationPort = v);

		/// <inheritdoc cref="INetworkCommunityIdProcessor.IanaNumber" />
		public NetworkCommunityIdProcessorDescriptor<T> IanaNumber(Field field) => Assign(field, (a, v) => a.IanaNumber = v);

		/// <inheritdoc cref="INetworkCommunityIdProcessor.IanaNumber" />
		public NetworkCommunityIdProcessorDescriptor<T> IanaNumber<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.IanaNumber = v);

		/// <inheritdoc cref="INetworkCommunityIdProcessor.IcmpType" />
		public NetworkCommunityIdProcessorDescriptor<T> IcmpType(Field field) => Assign(field, (a, v) => a.IcmpType = v);

		/// <inheritdoc cref="INetworkCommunityIdProcessor.IcmpType" />
		public NetworkCommunityIdProcessorDescriptor<T> IcmpType<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.IcmpType = v);

		/// <inheritdoc cref="INetworkCommunityIdProcessor.IcmpCode" />
		public NetworkCommunityIdProcessorDescriptor<T> IcmpCode(Field field) => Assign(field, (a, v) => a.IcmpCode = v);

		/// <inheritdoc cref="INetworkCommunityIdProcessor.IcmpType" />
		public NetworkCommunityIdProcessorDescriptor<T> IcmpCode<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.IcmpCode = v);

		/// <inheritdoc cref="INetworkCommunityIdProcessor.IgnoreMissing" />
		public NetworkCommunityIdProcessorDescriptor<T> IgnoreMissing(bool? ignoreMissing = true) => Assign(ignoreMissing, (a, v) => a.IgnoreMissing = v);

		/// <inheritdoc cref="INetworkCommunityIdProcessor.Seed" />
		public NetworkCommunityIdProcessorDescriptor<T> Seed(int? seed = null) => Assign(seed, (a, v) => a.Seed = v);

		/// <inheritdoc cref="INetworkCommunityIdProcessor.SourceIp" />
		public NetworkCommunityIdProcessorDescriptor<T> SourceIp(Field field) => Assign(field, (a, v) => a.SourceIp = v);

		/// <inheritdoc cref="INetworkCommunityIdProcessor.SourceIp" />
		public NetworkCommunityIdProcessorDescriptor<T> SourceIp<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.SourceIp = v);

		/// <inheritdoc cref="INetworkCommunityIdProcessor.SourcePort" />
		public NetworkCommunityIdProcessorDescriptor<T> SourcePort(Field field) => Assign(field, (a, v) => a.SourcePort = v);

		/// <inheritdoc cref="INetworkCommunityIdProcessor.SourcePort" />
		public NetworkCommunityIdProcessorDescriptor<T> SourcePort<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.SourcePort = v);

		/// <inheritdoc cref="INetworkCommunityIdProcessor.TargetField" />
		public NetworkCommunityIdProcessorDescriptor<T> TargetField(Field field) => Assign(field, (a, v) => a.TargetField = v);

		/// <inheritdoc cref="INetworkCommunityIdProcessor.TargetField" />
		public NetworkCommunityIdProcessorDescriptor<T> TargetField<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.TargetField = v);

		/// <inheritdoc cref="INetworkCommunityIdProcessor.Transport" />
		public NetworkCommunityIdProcessorDescriptor<T> Transport(Field field) => Assign(field, (a, v) => a.Transport = v);

		/// <inheritdoc cref="INetworkCommunityIdProcessor.Transport" />
		public NetworkCommunityIdProcessorDescriptor<T> Transport<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.Transport = v);
	}
}
