using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface INetworkDirectionProcessor : IProcessor
	{
		[DataMember(Name = "destination_ip")]
		Field DestinationIp { get; set; }

		[DataMember(Name = "ignore_missing")]
		bool? IgnoreMissing { get; set; }
		
		[DataMember(Name = "internal_networks")]
		IEnumerable<string> InternalNetworks { get; set; }
		
		[DataMember(Name = "source_ip")]
		Field SourceIp { get; set; }
		
		[DataMember(Name = "target_field")]
		Field TargetField { get; set; }
	}

	public class NetworkDirectionProcessor : ProcessorBase, INetworkDirectionProcessor
	{
		protected override string Name => "network_direction";

		/// <inheritdoc />
		public Field DestinationIp { get; set; }
		/// <inheritdoc />
		public bool? IgnoreMissing { get; set; }
		/// <inheritdoc />
		public IEnumerable<string> InternalNetworks { get; set; }
		/// <inheritdoc />
		public Field SourceIp { get; set; }
		/// <inheritdoc />
		public Field TargetField { get; set; }
	}

	/// <inheritdoc cref="IFingerprintProcessor" />
	public class NetworkDirectionProcessorDescriptor<T>
		: ProcessorDescriptorBase<NetworkDirectionProcessorDescriptor<T>, INetworkDirectionProcessor>, INetworkDirectionProcessor
		where T : class
	{
		protected override string Name => "network_direction";

		Field INetworkDirectionProcessor.DestinationIp { get; set; }
		bool? INetworkDirectionProcessor.IgnoreMissing { get; set; }
		IEnumerable<string> INetworkDirectionProcessor.InternalNetworks { get; set; }
		Field INetworkDirectionProcessor.SourceIp { get; set; }
		Field INetworkDirectionProcessor.TargetField { get; set; }
		
		/// <inheritdoc cref="INetworkDirectionProcessor.DestinationIp" />
		public NetworkDirectionProcessorDescriptor<T> DestinationIp(Field field) => Assign(field, (a, v) => a.DestinationIp = v);

		/// <inheritdoc cref="INetworkDirectionProcessor.DestinationIp" />
		public NetworkDirectionProcessorDescriptor<T> DestinationIp<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.DestinationIp = v);

		/// <inheritdoc cref="INetworkDirectionProcessor.IgnoreMissing" />
		public NetworkDirectionProcessorDescriptor<T> IgnoreMissing(bool? ignoreMissing = true) => Assign(ignoreMissing, (a, v) => a.IgnoreMissing = v);

		/// <inheritdoc cref="INetworkDirectionProcessor.InternalNetworks" />
		public NetworkDirectionProcessorDescriptor<T> InternalNetworks(IEnumerable<string> internalNetworks) => Assign(internalNetworks, (a, v) => a.InternalNetworks = v);

		/// <inheritdoc cref="INetworkDirectionProcessor.InternalNetworks" />
		public NetworkDirectionProcessorDescriptor<T> InternalNetworks(params string[] internalNetworks) => Assign(internalNetworks, (a, v) => a.InternalNetworks = v);

		/// <inheritdoc cref="INetworkDirectionProcessor.SourceIp" />
		public NetworkDirectionProcessorDescriptor<T> SourceIp(Field field) => Assign(field, (a, v) => a.SourceIp = v);

		/// <inheritdoc cref="INetworkDirectionProcessor.SourceIp" />
		public NetworkDirectionProcessorDescriptor<T> SourceIp<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.SourceIp = v);

		/// <inheritdoc cref="INetworkDirectionProcessor.TargetField" />
		public NetworkDirectionProcessorDescriptor<T> TargetField(Field field) => Assign(field, (a, v) => a.TargetField = v);

		/// <inheritdoc cref="INetworkDirectionProcessor.TargetField" />
		public NetworkDirectionProcessorDescriptor<T> TargetField<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.TargetField = v);
	}
}
