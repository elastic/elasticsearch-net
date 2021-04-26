/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
		public TransformSyncContainer() { }

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

	public class TransformSyncContainerDescriptor<T> : DescriptorBase<TransformSyncContainerDescriptor<T>, ITransformSyncContainer>, ITransformSyncContainer
	{
		ITransformTimeSync ITransformSyncContainer.Time { get; set; }

		/// <inheritdoc cref="ITransformTimeSync"/>
		public TransformSyncContainerDescriptor<T> Time(Func<TransformTimeSyncDescriptor<T>, ITransformTimeSync> selector) =>
			Assign(selector?.Invoke(new TransformTimeSyncDescriptor<T>()), (a, v) => a.Time = v);
	}

	/// <summary>
	/// Defines the properties transforms require to run continuously.
	/// </summary>
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
	public class TransformTimeSyncDescriptor<T> : DescriptorBase<TransformTimeSyncDescriptor<T>, ITransformTimeSync>, ITransformTimeSync
	{
		Field ITransformTimeSync.Field { get; set; }
		Time ITransformTimeSync.Delay { get; set; }

		/// <inheritdoc cref="ITransformTimeSync.Field" />
		public TransformTimeSyncDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="ITransformTimeSync.Field" />
		public TransformTimeSyncDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);

		/// <inheritdoc cref="ITransformTimeSync.Delay" />
		public TransformTimeSyncDescriptor<T> Delay(Time delay) => Assign(delay, (a, v) => a.Delay = v);
	}
}
