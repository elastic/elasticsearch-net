using System;
using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("transform.preview_transform.json")]
	public partial interface IPreviewTransformRequest
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

		/// <inheritdoc cref="ITransformPivot"/>
		[DataMember(Name = "pivot")]
		public ITransformPivot Pivot { get; set; }

		/// <inheritdoc cref="ITransformSync"/>
		[DataMember(Name = "sync")]
		public ITransformSync Sync { get; set; }
	}

	public partial class PreviewTransformRequest
	{
		/// <inheritdoc cref="IPreviewTransformRequest.Description"/>
		public string Description { get; set; }

		/// <inheritdoc cref="IPreviewTransformRequest.Source"/>
		public ITransformSource Source { get; set; }

		/// <inheritdoc cref="IPreviewTransformRequest.Destination"/>
		public ITransformDestination Destination { get; set; }

		/// <inheritdoc cref="IPreviewTransformRequest.Frequency"/>
		public Time Frequency { get; set; }

		/// <inheritdoc cref="IPreviewTransformRequest.Pivot"/>
		public ITransformPivot Pivot { get; set; }

		/// <inheritdoc cref="IPreviewTransformRequest.Sync"/>
		public ITransformSync Sync { get; set; }
	}

	public partial class PreviewTransformDescriptor<TDocument> : IPreviewTransformRequest where TDocument : class
	{
		string IPreviewTransformRequest.Description { get; set; }
		ITransformSource IPreviewTransformRequest.Source { get; set; }
		ITransformDestination IPreviewTransformRequest.Destination { get; set; }
		Time IPreviewTransformRequest.Frequency { get; set; }
		ITransformPivot IPreviewTransformRequest.Pivot { get; set; }
		ITransformSync IPreviewTransformRequest.Sync { get; set; }

		/// <inheritdoc cref="IPreviewTransformRequest.Description"/>
		public PreviewTransformDescriptor<TDocument> Description(string description) =>
			Assign(description, (a, v) => a.Description = v);

		/// <inheritdoc cref="IPreviewTransformRequest.Source"/>
		public PreviewTransformDescriptor<TDocument> Source(Func<TransformSourceDescriptor<TDocument>, ITransformSource> selector) =>
			Assign(selector.InvokeOrDefault(new TransformSourceDescriptor<TDocument>()), (a, v) => a.Source = v);

		/// <inheritdoc cref="IPreviewTransformRequest.Destination"/>
		public PreviewTransformDescriptor<TDocument> Destination(Func<TransformDestinationDescriptor, ITransformDestination> selector) =>
			Assign(selector.InvokeOrDefault(new TransformDestinationDescriptor()), (a, v) => a.Destination = v);

		/// <inheritdoc cref="IPreviewTransformRequest.Frequency"/>
		public PreviewTransformDescriptor<TDocument> Frequency(Time frequency) => Assign(frequency, (a, v) => a.Frequency = v);

		/// <inheritdoc cref="IPreviewTransformRequest.Pivot"/>
		public PreviewTransformDescriptor<TDocument> Pivot(Func<TransformPivotDescriptor<TDocument>, ITransformPivot> selector) =>
			Assign(selector.InvokeOrDefault(new TransformPivotDescriptor<TDocument>()), (a, v) => a.Pivot = v);

		/// <inheritdoc cref="IPreviewTransformRequest.Sync"/>
		public PreviewTransformDescriptor<TDocument> Sync(Func<TransformSyncDescriptor<TDocument>, ITransformSync> selector) =>
			Assign(selector.InvokeOrDefault(new TransformSyncDescriptor<TDocument>()), (a, v) => a.Sync = v);
	}
}
