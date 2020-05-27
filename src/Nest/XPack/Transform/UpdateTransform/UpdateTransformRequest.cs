using System;
using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("transform.update_transform.json")]
	public partial interface IUpdateTransformRequest
	{
		/// <inheritdoc cref="Transform.Description"/>
		[DataMember(Name = "description")]
		public string Description { get; set; }

		/// <inheritdoc cref="ITransformSource"/>
		[DataMember(Name = "source")]
		public ITransformSource Source { get; set; }

		/// <inheritdoc cref="ITransformDestination"/>
		[DataMember(Name = "dest")]
		public ITransformDestination Destination { get; set; }

		/// <inheritdoc cref="Transform.Frequency"/>
		[DataMember(Name = "frequency")]
		public Time Frequency { get; set; }

		/// <inheritdoc cref="ITransformSyncContainer"/>
		[DataMember(Name = "sync")]
		public ITransformSyncContainer Sync { get; set; }
	}

	public partial class UpdateTransformRequest
	{
		/// <inheritdoc cref="IPutTransformRequest.Description"/>
		public string Description { get; set; }

		/// <inheritdoc cref="IPutTransformRequest.Source"/>
		public ITransformSource Source { get; set; }

		/// <inheritdoc cref="IPutTransformRequest.Destination"/>
		public ITransformDestination Destination { get; set; }

		/// <inheritdoc cref="IPutTransformRequest.Frequency"/>
		public Time Frequency { get; set; }

		/// <inheritdoc cref="IPutTransformRequest.Sync"/>
		public ITransformSyncContainer Sync { get; set; }
	}

	public partial class UpdateTransformDescriptor<TDocument> : IUpdateTransformRequest where TDocument : class
	{
		string IUpdateTransformRequest.Description { get; set; }
		ITransformSource IUpdateTransformRequest.Source { get; set; }
		ITransformDestination IUpdateTransformRequest.Destination { get; set; }
		Time IUpdateTransformRequest.Frequency { get; set; }
		ITransformSyncContainer IUpdateTransformRequest.Sync { get; set; }

		/// <inheritdoc cref="IUpdateTransformRequest.Description"/>
		public UpdateTransformDescriptor<TDocument> Description(string description) =>
			Assign(description, (a, v) => a.Description = v);

		/// <inheritdoc cref="IUpdateTransformRequest.Source"/>
		public UpdateTransformDescriptor<TDocument> Source(Func<TransformSourceDescriptor<TDocument>, ITransformSource> selector) =>
			Assign(selector.InvokeOrDefault(new TransformSourceDescriptor<TDocument>()), (a, v) => a.Source = v);

		/// <inheritdoc cref="IUpdateTransformRequest.Destination"/>
		public UpdateTransformDescriptor<TDocument> Destination(Func<TransformDestinationDescriptor, ITransformDestination> selector) =>
			Assign(selector.InvokeOrDefault(new TransformDestinationDescriptor()), (a, v) => a.Destination = v);

		/// <inheritdoc cref="IUpdateTransformRequest.Frequency"/>
		public UpdateTransformDescriptor<TDocument> Frequency(Time frequency) => Assign(frequency, (a, v) => a.Frequency = v);

		/// <inheritdoc cref="IUpdateTransformRequest.Sync"/>
		public UpdateTransformDescriptor<TDocument> Sync(Func<TransformSyncContainerDescriptor<TDocument>, ITransformSyncContainer> selector) =>
			Assign(selector?.Invoke(new TransformSyncContainerDescriptor<TDocument>()), (a, v) => a.Sync = v);
	}
}
