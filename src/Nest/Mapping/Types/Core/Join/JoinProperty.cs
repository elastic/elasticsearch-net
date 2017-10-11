using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IJoinProperty : IProperty
	{
		/// <summary>
		/// Should the field be searchable? Accepts true (default) and false.
		/// </summary>
		[JsonProperty("relations")]
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
