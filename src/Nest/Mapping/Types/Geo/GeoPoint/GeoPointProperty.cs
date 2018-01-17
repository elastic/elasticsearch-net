using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IGeoPointProperty : IDocValuesProperty
	{
		[JsonProperty("ignore_malformed")]
		bool? IgnoreMalformed { get; set;  }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class GeoPointProperty : DocValuesPropertyBase, IGeoPointProperty
	{
		public GeoPointProperty() : base(FieldType.GeoPoint) { }

		public bool? IgnoreMalformed { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class GeoPointPropertyDescriptor<T>
		: DocValuesPropertyDescriptorBase<GeoPointPropertyDescriptor<T>, IGeoPointProperty, T>, IGeoPointProperty
		where T : class
	{
		bool? IGeoPointProperty.IgnoreMalformed { get; set; }

		public GeoPointPropertyDescriptor() : base(FieldType.GeoPoint) { }

		public GeoPointPropertyDescriptor<T> IgnoreMalformed(bool? ignoreMalformed = true) => Assign(a => a.IgnoreMalformed = ignoreMalformed);
	}
}
