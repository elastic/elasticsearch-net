// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq.Expressions;

namespace Nest
{
	public enum InfoContentFunction
	{
		InfoContent,
		HighInfoContent,
		LowInfoContent
	}

	public static class InfoContentFunctions
	{
		public static string GetStringValue(this InfoContentFunction infoContentFunction)
		{
			switch (infoContentFunction)
			{
				case InfoContentFunction.InfoContent:
					return "info_content";
				case InfoContentFunction.HighInfoContent:
					return "high_info_content";
				case InfoContentFunction.LowInfoContent:
					return "low_info_content";
				default:
					throw new ArgumentOutOfRangeException(nameof(infoContentFunction), infoContentFunction, null);
			}
		}
	}

	public interface IInfoContentDetector
		: IDetector, IByFieldNameDetector, IOverFieldNameDetector,
			IPartitionFieldNameDetector, IFieldNameDetector { }

	public abstract class InfoContentDetectorBase : DetectorBase, IInfoContentDetector
	{
		protected InfoContentDetectorBase(InfoContentFunction function) : base(function.GetStringValue()) { }

		public Field ByFieldName { get; set; }
		public Field FieldName { get; set; }
		public Field OverFieldName { get; set; }
		public Field PartitionFieldName { get; set; }
	}

	public class InfoContentDetector : InfoContentDetectorBase
	{
		public InfoContentDetector() : base(InfoContentFunction.InfoContent) { }
	}

	public class HighInfoContentDetector : InfoContentDetectorBase
	{
		public HighInfoContentDetector() : base(InfoContentFunction.HighInfoContent) { }
	}

	public class LowInfoContentDetector : InfoContentDetectorBase
	{
		public LowInfoContentDetector() : base(InfoContentFunction.LowInfoContent) { }
	}

	public class InfoContentDetectorDescriptor<T>
		: DetectorDescriptorBase<InfoContentDetectorDescriptor<T>, IInfoContentDetector>, IInfoContentDetector where T : class
	{
		public InfoContentDetectorDescriptor(InfoContentFunction function) : base(function.GetStringValue()) { }

		Field IByFieldNameDetector.ByFieldName { get; set; }
		Field IFieldNameDetector.FieldName { get; set; }
		Field IOverFieldNameDetector.OverFieldName { get; set; }
		Field IPartitionFieldNameDetector.PartitionFieldName { get; set; }

		public InfoContentDetectorDescriptor<T> FieldName(Field fieldName) => Assign(fieldName, (a, v) => a.FieldName = v);

		public InfoContentDetectorDescriptor<T> FieldName<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.FieldName = v);

		public InfoContentDetectorDescriptor<T> ByFieldName(Field byFieldName) => Assign(byFieldName, (a, v) => a.ByFieldName = v);

		public InfoContentDetectorDescriptor<T> ByFieldName<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.ByFieldName = v);

		public InfoContentDetectorDescriptor<T> OverFieldName(Field overFieldName) => Assign(overFieldName, (a, v) => a.OverFieldName = v);

		public InfoContentDetectorDescriptor<T> OverFieldName<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.OverFieldName = v);

		public InfoContentDetectorDescriptor<T> PartitionFieldName(Field partitionFieldName) =>
			Assign(partitionFieldName, (a, v) => a.PartitionFieldName = v);

		public InfoContentDetectorDescriptor<T> PartitionFieldName<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.PartitionFieldName = v);
	}
}
