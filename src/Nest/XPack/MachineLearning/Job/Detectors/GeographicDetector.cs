using System;
using System.Linq.Expressions;

namespace Nest
{
	public enum GeographicFunction
	{
		LatLong
	}

	public static class GeographicFunctionsExtensions
	{
		public static string GetStringValue(this GeographicFunction geographicFunction)
		{
			switch (geographicFunction)
			{
				case GeographicFunction.LatLong:
					return "lat_long";
				default:
					throw new ArgumentOutOfRangeException(nameof(geographicFunction), geographicFunction, null);
			}
		}
	}

	public interface IGeographicDetector : IDetector, IByFieldNameDetector, IOverFieldNameDetector,
		IPartitionFieldNameDetector, IFieldNameDetector
	{
	}

	public class LatLongDetector : DetectorBase, IGeographicDetector
	{
		public LatLongDetector() : base(GeographicFunction.LatLong.GetStringValue()) {}

		public Field ByFieldName { get; set; }
		public Field OverFieldName { get; set; }
		public Field PartitionFieldName { get; set; }
		public Field FieldName { get; set; }
	}

	public class LatLongDetectorDescriptor<T> : DetectorDescriptorBase<LatLongDetectorDescriptor<T>, IGeographicDetector>, IGeographicDetector where T : class
	{
		Field IByFieldNameDetector.ByFieldName { get; set; }
		Field IOverFieldNameDetector.OverFieldName { get; set; }
		Field IPartitionFieldNameDetector.PartitionFieldName { get; set; }
		Field IFieldNameDetector.FieldName { get; set; }

		[Obsolete("Use parameterless constructor")]
		public LatLongDetectorDescriptor(string function) : base(function) {}

		public LatLongDetectorDescriptor() : base(GeographicFunction.LatLong.GetStringValue()) {}

		public LatLongDetectorDescriptor<T> FieldName(Field fieldName) => Assign(a => a.FieldName = fieldName);

		public LatLongDetectorDescriptor<T> FieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.FieldName = objectPath);

		public LatLongDetectorDescriptor<T> ByFieldName(Field byFieldName) => Assign(a => a.ByFieldName = byFieldName);

		public LatLongDetectorDescriptor<T> ByFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.ByFieldName = objectPath);

		public LatLongDetectorDescriptor<T> OverFieldName(Field overFieldName) => Assign(a => a.OverFieldName = overFieldName);

		public LatLongDetectorDescriptor<T> OverFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.OverFieldName = objectPath);

		public LatLongDetectorDescriptor<T> PartitionFieldName(Field partitionFieldName) => Assign(a => a.PartitionFieldName = partitionFieldName);

		public LatLongDetectorDescriptor<T> PartitionFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.PartitionFieldName = objectPath);
	}
}
