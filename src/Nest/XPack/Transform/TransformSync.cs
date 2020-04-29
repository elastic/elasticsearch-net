using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(TransformSyncContainer))]
	public interface ITransformSyncContainer
	{
		[DataMember(Name = "time")]
		ITransformTimeSync Time { get; set; }
	}

	/// <summary>
	/// A transform time sync
	/// </summary>
	public class TransformSyncContainer : ITransformSyncContainer
	{
		internal TransformSyncContainer() { }

		public TransformSyncContainer(TransformSyncBase transform)
		{
			transform.ThrowIfNull(nameof(transform));
			transform.WrapInContainer(this);
		}

		public ITransformTimeSync Time { get; set; }

		public static implicit operator TransformSyncContainer(TransformSyncBase transform) => transform is null
			? null
			: new TransformSyncContainer(transform);
	}

	/// <summary>
	/// Defines the properties transforms require to run continuously.
	/// </summary>
	[InterfaceDataContract]
	public interface ITransformSync { }

	/// <inheritdoc />
	public abstract class TransformSyncBase : ITransformSync
	{
		internal abstract void WrapInContainer(ITransformSyncContainer container);
	}

	/// <summary>
	/// Specifies that the transform uses a time field to synchronize the source and destination indices.
	/// </summary>
	[InterfaceDataContract]
	[ReadAs(typeof(TransformTimeSync))]
	public interface ITransformTimeSync : ITransformSync
	{
		/// <summary>
		/// The date field that is used to identify new documents in the source.
		/// </summary>
		[DataMember(Name = "field")]
		Field Field { get; set; }

		/// <summary>
		/// The time delay between the current time and the latest input data time. The default value is 60s.
		/// </summary>
		[DataMember(Name = "delay")]
		Time Delay { get; set; }
	}

	/// <inheritdoc />
	public class TransformTimeSync : TransformSyncBase, ITransformTimeSync
	{
		/// <inheritdoc />
		public Field Field { get; set; }

		/// <inheritdoc />
		public Time Delay { get; set; }

		internal override void WrapInContainer(ITransformSyncContainer container) => container.Time = this;
	}

	/// <inheritdoc cref="ITransformTimeSync" />
	public class TransformSyncDescriptor<T> : DescriptorBase<TransformSyncDescriptor<T>, ITransformTimeSync>, ITransformTimeSync
	{
		Field ITransformTimeSync.Field { get; set; }
		Time ITransformTimeSync.Delay { get; set; }

		/// <inheritdoc cref="ITransformTimeSync.Field" />
		public TransformSyncDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="ITransformTimeSync.Field" />
		public TransformSyncDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);

		/// <inheritdoc cref="ITransformTimeSync.Delay" />
		public TransformSyncDescriptor<T> Delay(Time delay) => Assign(delay, (a, v) => a.Delay = v);
	}
}
