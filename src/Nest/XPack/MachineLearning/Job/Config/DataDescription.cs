// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Defines the format of the input data when you send data to the machine learning job.
	/// Note that when configure a datafeed, these properties are automatically set.
	/// </summary>
	[ReadAs(typeof(DataDescription))]
	public interface IDataDescription
	{
		/// <summary>
		/// Only JSON format is supported at this time.
		/// </summary>
		[DataMember(Name ="format")]
		string Format { get; set; }

		/// <summary>
		/// The name of the field that contains the timestamp. The default value is time.
		/// </summary>
		[DataMember(Name ="time_field")]
		Field TimeField { get; set; }

		/// <summary>
		/// The time format, which can be epoch, epoch_ms, or a custom pattern.
		/// </summary>
		[DataMember(Name ="time_format")]
		string TimeFormat { get; set; }
	}

	/// <inheritdoc />
	public class DataDescription : IDataDescription
	{
		/// <inheritdoc />
		public string Format { get; set; }

		/// <inheritdoc />
		public Field TimeField { get; set; }

		/// <inheritdoc />
		public string TimeFormat { get; set; }
	}

	/// <inheritdoc />
	public class DataDescriptionDescriptor<T> : DescriptorBase<DataDescriptionDescriptor<T>, IDataDescription>, IDataDescription
	{
		string IDataDescription.Format { get; set; }
		Field IDataDescription.TimeField { get; set; }
		string IDataDescription.TimeFormat { get; set; }

		/// <inheritdoc />
		public DataDescriptionDescriptor<T> Format(string format) => Assign(format, (a, v) => a.Format = v);

		/// <inheritdoc />
		public DataDescriptionDescriptor<T> TimeField(Field timeField) => Assign(timeField, (a, v) => a.TimeField = v);

		/// <inheritdoc />
		public DataDescriptionDescriptor<T> TimeField<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.TimeField = v);

		/// <inheritdoc />
		public DataDescriptionDescriptor<T> TimeFormat(string timeFormat) => Assign(timeFormat, (a, v) => a.TimeFormat = v);
	}
}
