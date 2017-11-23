using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IObjectProperty : ICoreProperty
	{
		[JsonProperty("dynamic")]
		Union<bool, DynamicMapping> Dynamic { get; set; }

		[JsonProperty("enabled")]
		bool? Enabled { get; set; }

		[JsonProperty("include_in_all")]
		bool? IncludeInAll { get; set; }

		[JsonProperty("properties", TypeNameHandling = TypeNameHandling.None)]
		IProperties Properties { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class ObjectProperty : CorePropertyBase, IObjectProperty
	{
		public ObjectProperty() : base(FieldType.Object) { }

		[Obsolete("Please use overload taking FieldType")]
		protected ObjectProperty(string type) : base(type) { }

#pragma warning disable 618
		protected ObjectProperty(FieldType type) : this(type.GetStringValue()) { }
#pragma warning restore 618

		public Union<bool, DynamicMapping> Dynamic { get; set; }
		public bool? Enabled { get; set; }
		public bool? IncludeInAll { get; set; }
		public IProperties Properties { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class ObjectTypeDescriptor<TParent, TChild>
		: ObjectPropertyDescriptorBase<ObjectTypeDescriptor<TParent, TChild>, IObjectProperty, TParent, TChild>, IObjectProperty
		where TParent : class
		where TChild : class
	{
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public abstract class ObjectPropertyDescriptorBase<TDescriptor, TInterface, TParent, TChild>
		: CorePropertyDescriptorBase<TDescriptor, TInterface, TParent>, IObjectProperty
		where TDescriptor : ObjectPropertyDescriptorBase<TDescriptor, TInterface, TParent, TChild>, TInterface
		where TInterface : class, IObjectProperty
		where TParent : class
		where TChild : class
	{
		internal TypeName _TypeName { get; set; }

		Union<bool, DynamicMapping> IObjectProperty.Dynamic { get; set; }
		bool? IObjectProperty.Enabled { get; set; }
		bool? IObjectProperty.IncludeInAll { get; set; }
		IProperties IObjectProperty.Properties { get; set; }

		protected ObjectPropertyDescriptorBase() : this(FieldType.Object) { }

		[Obsolete("Please use overload taking FieldType")]
		protected ObjectPropertyDescriptorBase(string type) : base(type)
		{
			_TypeName = TypeName.Create<TChild>();
		}

#pragma warning disable 618
		protected ObjectPropertyDescriptorBase(FieldType type) : this(type.GetStringValue()) { }
#pragma warning restore 618

		public TDescriptor Dynamic(Union<bool, DynamicMapping> dynamic) =>
			Assign(a => a.Dynamic = dynamic);

		public TDescriptor Dynamic(bool dynamic = true) =>
			Assign(a => a.Dynamic = dynamic);

		public TDescriptor Enabled(bool enabled = true) =>
			Assign(a => a.Enabled = enabled);

		public TDescriptor IncludeInAll(bool includeInAll = true) =>
			Assign(a => a.IncludeInAll = includeInAll);

		public TDescriptor Properties(Func<PropertiesDescriptor<TChild>, IPromise<IProperties>> selector) =>
			Assign(a => a.Properties = selector?.Invoke(new PropertiesDescriptor<TChild>(a.Properties))?.Value);

		/// <summary>
		/// Convenience method to map as much as it can based on <see cref="ElasticsearchTypeAttribute"/> attributes set on the type.
		/// This particular overload is useful for automapping any children
		/// <pre>This method also automatically sets up mappings for known values types (int, long, double, datetime, etc)</pre>
		/// <pre>Class types default to object and Enums to int</pre>
		/// <pre>Later calls can override whatever is set is by this call.</pre>
		/// </summary>
		/// <param name="visitor">Use a visitor implementation to control defaults</param>
		/// <param name="maxRecursion">By default this will only recurse 20 levels deep to prevent circular references blowing up</param>
		public TDescriptor AutoMap(IPropertyVisitor visitor = null, int maxRecursion = 20) =>
			Assign(a => a.Properties = a.Properties.AutoMap<TChild>(visitor, maxRecursion));

		/// <summary>
		/// Convenience method to map as much as it can based on <see cref="ElasticsearchTypeAttribute"/> attributes set on the type.
		/// <pre>This method also automatically sets up mappings for known values types (int, long, double, datetime, etc)</pre>
		/// <pre>Class types default to object and Enums to int</pre>
		/// <pre>Later calls can override whatever is set is by this call.</pre>
		/// </summary>
		/// <param name="maxRecursion">Limit how many levels we are allowed to recurse into</param>
		public TDescriptor AutoMap(int maxRecursion) => AutoMap(null, maxRecursion);
	}
}
