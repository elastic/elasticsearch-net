using System;
using System.Linq;

namespace Nest
{
	internal static class CovariantSearch
	{
		/// <summary>
		/// Based on the type information present in this descriptor create method that takes
		/// the returned _source and hit and returns the ClrType it should deserialize too.
		/// This is so that Documents[A] can contain actual instances of subclasses B, C as well.
		/// If you specify types using .Type(typeof(B), typeof(C)) then NEST can automagically
		/// create a TypeSelector based on the hits _type property.
		/// </summary>
		public static void CloseOverAutomagicCovariantResultSelector(Inferrer infer, ICovariantSearchRequest self)
		{
			if (infer == null || self == null) return;
			var returnType = self.ClrType;

			if (returnType == null) return;

			var types = self.ElasticsearchTypes?.Match(all => new TypeName[] { "_all" }, many => many.Types);
			types = (types ?? Enumerable.Empty<TypeName>()).Where(t => t.Type != null).ToList();

			if (self.TypeSelector != null || !types.HasAny(t => t.Type != returnType))
				return;

			var typeDictionary = types.ToDictionary(infer.TypeName, t => t.Type);
			self.TypeSelector = (o, h) =>
			{
				Type t;
				return !typeDictionary.TryGetValue(h.Type, out t) ? returnType : t;
			};
		}
	}

	public interface ICovariantSearchRequest
	{
		Type ClrType { get; }
		Types ElasticsearchTypes { get; }
		Func<dynamic, Hit<dynamic>, Type> TypeSelector { get; set;}
	}
}