using System;
using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("transform.put_transform.json")]
	public partial interface IPutTransformRequest
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

		/// <inheritdoc cref="ITransformSyncContainer"/>
		[DataMember(Name = "sync")]
		public ITransformSyncContainer Sync { get; set; }

		/// <inheritdoc cref="ITransformSettings"/>
		[DataMember(Name = "settings")]
		public ITransformSettings Settings { get; set; }
	}

	/// <inheritdoc cref="IPutTransformRequest"/>
	public partial class PutTransformRequest
	{
		/// <inheritdoc cref="IPutTransformRequest.Description"/>
		public string Description { get; set; }

		/// <inheritdoc cref="IPutTransformRequest.Source"/>
		public ITransformSource Source { get; set; }

		/// <inheritdoc cref="IPutTransformRequest.Destination"/>
		public ITransformDestination Destination { get; set; }

		/// <inheritdoc cref="IPutTransformRequest.Frequency"/>
		public Time Frequency { get; set; }

		/// <inheritdoc cref="IPutTransformRequest.Pivot"/>
		public ITransformPivot Pivot { get; set; }

		/// <inheritdoc cref="IPutTransformRequest.Sync"/>
		public ITransformSyncContainer Sync { get; set; }

		/// <inheritdoc cref="IPutTransformRequest.Settings"/>
		public ITransformSettings Settings { get; set; }
	}

	public partial class PutTransformDescriptor<TDocument> : IPutTransformRequest where TDocument : class
	{
		string IPutTransformRequest.Description { get; set; }
		ITransformSource IPutTransformRequest.Source { get; set; }
		ITransformDestination IPutTransformRequest.Destination { get; set; }
		Time IPutTransformRequest.Frequency { get; set; }
		ITransformPivot IPutTransformRequest.Pivot { get; set; }
		ITransformSyncContainer IPutTransformRequest.Sync { get; set; }
		ITransformSettings IPutTransformRequest.Settings { get; set; }

		/// <inheritdoc cref="IPutTransformRequest.Description"/>
		public PutTransformDescriptor<TDocument> Description(string description) =>
			Assign(description, (a, v) => a.Description = v);

		/// <inheritdoc cref="IPutTransformRequest.Source"/>
		public PutTransformDescriptor<TDocument> Source(Func<TransformSourceDescriptor<TDocument>, ITransformSource> selector) =>
			Assign(selector.InvokeOrDefault(new TransformSourceDescriptor<TDocument>()), (a, v) => a.Source = v);

		/// <inheritdoc cref="IPutTransformRequest.Destination"/>
		public PutTransformDescriptor<TDocument> Destination(Func<TransformDestinationDescriptor, ITransformDestination> selector) =>
			Assign(selector.InvokeOrDefault(new TransformDestinationDescriptor()), (a, v) => a.Destination = v);

		/// <inheritdoc cref="IPutTransformRequest.Frequency"/>
		public PutTransformDescriptor<TDocument> Frequency(Time frequency) => Assign(frequency, (a, v) => a.Frequency = v);

		/// <inheritdoc cref="IPutTransformRequest.Pivot"/>
		public PutTransformDescriptor<TDocument> Pivot(Func<TransformPivotDescriptor<TDocument>, ITransformPivot> selector) =>
			Assign(selector.InvokeOrDefault(new TransformPivotDescriptor<TDocument>()), (a, v) => a.Pivot = v);

		/// <inheritdoc cref="IPutTransformRequest.Sync"/>
		public PutTransformDescriptor<TDocument> Sync(Func<TransformSyncContainerDescriptor<TDocument>, ITransformSyncContainer> selector) =>
			Assign(selector?.Invoke(new TransformSyncContainerDescriptor<TDocument>()), (a, v) => a.Sync = v);

		/// <inheritdoc cref="IPutTransformRequest.Settings"/>
		public PutTransformDescriptor<TDocument> Settings(Func<TransformSettingsDescriptor, ITransformSettings> selector) =>
			Assign(selector?.Invoke(new TransformSettingsDescriptor()), (a, v) => a.Settings = v);
	}
}
