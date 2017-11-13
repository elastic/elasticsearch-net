using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	[JsonConverter(typeof(FieldValuesJsonConverter))]
	public class FieldValues : IsADictionaryBase<string, LazyDocument>
	{
		public static readonly FieldValues Empty = new FieldValues();

		private readonly Inferrer _inferrer;

		protected FieldValues() : base() { }

		internal FieldValues(Inferrer inferrer, IDictionary<string, LazyDocument> container)
			: base(container)
		{
			_inferrer = inferrer;
		}

		public TValue Value<TValue>(Field field)
		{
			var values = ValuesOf<TValue>(field);
			return values != null
				? values.FirstOrDefault()
				: default(TValue);
		}

		public TValue ValueOf<T, TValue>(Expression<Func<T, TValue>> objectPath)
			where T : class
		{
			var values = Values(objectPath);
			return values != null
				? values.FirstOrDefault()
				: default(TValue);
		}

		public TValue[] ValuesOf<TValue>(Field field)
		{
			if (this._inferrer == null) return new TValue[0];
			var path = this._inferrer.Field(field);
			return this.FieldArray<TValue>(path);
		}

		public TValue[] Values<T, TValue>(Expression<Func<T, TValue>> objectPath)
			where T : class
		{
			if (this._inferrer == null) return new TValue[0];
			var field = this._inferrer.Field(objectPath);
			return this.FieldArray<TValue>(field);
		}

		private static readonly HashSet<Type> NumericTypes = new HashSet<Type>
		{
			typeof(int),  typeof(double),  typeof(decimal),
			typeof(long), typeof(short),   typeof(sbyte),
			typeof(byte), typeof(ulong),   typeof(ushort),
			typeof(uint), typeof(float)
		};

		public static bool IsNumeric(Type myType) => NumericTypes.Contains(Nullable.GetUnderlyingType(myType) ?? myType);
		public static bool IsNullable(Type type) => type.IsGeneric() && type.GetGenericTypeDefinition() == typeof(Nullable<>);

		private TValue[] FieldArray<TValue>(string field)
		{
			//unknown field
			if (this.BackingDictionary == null || !this.BackingDictionary.TryGetValue(field, out var o))
				return null;

			//numerics are always returned as doubles by elasticsearch.
			if (!IsNumeric(typeof(TValue)))
				return o.As<TValue[]>();

			//here we support casting to the desired numeric type whether its nullable or not.
			if (!IsNullable(typeof(TValue)))
				return o.As<double[]>().Select(d => (TValue) Convert.ChangeType(d, typeof(TValue))).ToArray();

			var underlyingType = Nullable.GetUnderlyingType(typeof(TValue));
			return o.As<double?[]>().Select(d=>
			{
				if (d == null) return default(TValue);
				return (TValue) Convert.ChangeType(d, underlyingType);
			}).ToArray();

		}
	}
}
