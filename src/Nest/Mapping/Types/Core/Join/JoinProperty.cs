using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public interface IJoinProperty : IProperty
	{
		/// <summary>
		/// Should the field be searchable? Accepts true (default) and false.
		/// </summary>
		[DataMember(Name ="relations")]
		IRelations Relations { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class JoinProperty : PropertyBase, IJoinProperty
	{
		public JoinProperty() : base(FieldType.Join) { }

		public IRelations Relations { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class JoinPropertyDescriptor<T> : PropertyDescriptorBase<JoinPropertyDescriptor<T>, IJoinProperty, T>, IJoinProperty
		where T : class
	{
		public JoinPropertyDescriptor() : base(FieldType.Join) { }

		IRelations IJoinProperty.Relations { get; set; }

		public JoinPropertyDescriptor<T> Relations(Func<RelationsDescriptor, IPromise<IRelations>> selector) =>
			Assign(a => a.Relations = selector?.Invoke(new RelationsDescriptor())?.Value);
	}
}
