using System;
using Elasticsearch.Net;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A object datatype mapping for an inner object
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	public interface IObjectProperty : ICoreProperty
	{
		/// <summary>
		/// Whether or not new properties should be added dynamically to an existing object.
		/// Default is <c>true</c>
		/// </summary>
		[JsonProperty("dynamic")]
		Union<bool, DynamicMapping> Dynamic { get; set; }

		/// <summary>
		/// Whether the JSON value given for this field should be parsed and indexed. Default is <c>true</c>
		/// </summary>
		[JsonProperty("enabled")]
		bool? Enabled { get; set; }

		/// <summary>
		/// The fields within the object
		/// </summary>
		[JsonProperty("properties", TypeNameHandling = TypeNameHandling.None)]
		IProperties Properties { get; set; }
	}

	/// <summary>
	/// A object datatype mapping for an inner object
	/// </summary>
	[DebuggerDisplay("{DebugDisplay}")]
	public class ObjectProperty : CorePropertyBase, IObjectProperty
	{
		public ObjectProperty() : base(FieldType.Object) { }

		protected ObjectProperty(FieldType type) : base(type) { }

		/// <inheritdoc />
		public Union<bool, DynamicMapping> Dynamic { get; set; }
		/// <inheritdoc />
		public bool? Enabled { get; set; }
		/// <inheritdoc />
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
		IProperties IObjectProperty.Properties { get; set; }

		protected ObjectPropertyDescriptorBase() : this(FieldType.Object) { }

		protected ObjectPropertyDescriptorBase(FieldType type) : base(type) =>
			_TypeName = TypeName.Create<TChild>();

		/// <summary>
		/// Whether or not new properties should be added dynamically to an existing object.
		/// Default is <c>true</c>
		/// </summary>
		public TDescriptor Dynamic(Union<bool, DynamicMapping> dynamic) => Assign(a => a.Dynamic = dynamic);

		/// <summary>
		/// Whether or not new properties should be added dynamically to an existing object.
		/// Default is <c>true</c>
		/// </summary>
		public TDescriptor Dynamic(bool dynamic = true) => Assign(a => a.Dynamic = dynamic);

		/// <summary>
		/// Whether the JSON value given for this field should be parsed and indexed. Default is <c>true</c>
		/// </summary>
		public TDescriptor Enabled(bool? enabled = true) => Assign(a => a.Enabled = enabled);

		/// <summary>
		/// The fields within the object
		/// </summary>
		public TDescriptor Properties(Func<PropertiesDescriptor<TChild>, IPromise<IProperties>> selector) =>
			Assign(a => a.Properties = selector?.Invoke(new PropertiesDescriptor<TChild>(a.Properties))?.Value);

		public TDescriptor AutoMap(IPropertyVisitor visitor = null, int maxRecursion = 0) =>
			Assign(a => a.Properties = a.Properties.AutoMap<TChild>(visitor, maxRecursion));

		public TDescriptor AutoMap(int maxRecursion) => AutoMap(null, maxRecursion);
	}
}
