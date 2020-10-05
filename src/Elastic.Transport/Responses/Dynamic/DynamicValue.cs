// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.CSharp.RuntimeBinder;
using Elastic.Transport.Extensions;
using Binder = Microsoft.CSharp.RuntimeBinder.Binder;

// ReSharper disable ArrangeConstructorOrDestructorBody
// ReSharper disable ArrangeAccessorOwnerBody
// ReSharper disable RemoveRedundantBraces
// ReSharper disable ArrangeMethodOrOperatorBody

namespace Elastic.Transport
{
	public class DynamicValue : DynamicObject, IEquatable<DynamicValue>, IConvertible, IReadOnlyCollection<DynamicValue>
	{
		private readonly object _value;

		/// <summary>
		/// Initializes a new instance of the <see cref="DynamicValue" /> class.
		/// </summary>
		/// <param name="value">The value to store in the instance</param>
		public DynamicValue(object value) => _value = value is DynamicValue av ? av.Value : value;

		/// <summary>
		/// Gets a value indicating whether this instance has value.
		/// </summary>
		/// <value><c>true</c> if this instance has value; otherwise, <c>false</c>.</value>
		/// <remarks><see langword="null" /> is considered as not being a value.</remarks>
		public bool HasValue
		{
			get { return _value != null; }
		}

		public DynamicValue this[string name]
		{
			get
			{
				object r;
				Dispatch(out r, name);
				return (DynamicValue)r;
			}
		}

		public T Get<T>(string path)
		{
			var dynamicDictionary = Value switch
			{
				DynamicDictionary v => v,
				IDictionary<string, object> v => DynamicDictionary.Create(v),
				JsonElement e => DynamicDictionary.Create(e),
				_ => null
			};
			return dynamicDictionary == null ? default : dynamicDictionary.Get<T>(path);
		}

		public static DynamicValue NullValue { get; } = new DynamicValue(null);

		public static DynamicValue SelfOrNew(object v) => v is DynamicValue av ? av : new DynamicValue(v);

		/// <summary>
		/// Gets the inner value
		/// </summary>
		public object Value
		{
			get { return _value; }
		}

		/// <summary>
		/// Returns the <see cref="T:System.TypeCode" /> for this instance.
		/// </summary>
		/// <returns>
		/// The enumerated constant that is the <see cref="T:System.TypeCode" /> of the class or value type that implements this interface.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public TypeCode GetTypeCode()
		{
			if (_value == null) return TypeCode.Empty;

			return Type.GetTypeCode(_value.GetType());
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent Boolean value using the specified culture-specific formatting information.
		/// </summary>
		/// <returns>
		/// A Boolean value equivalent to the value of this instance.
		/// </returns>
		/// <param name="provider">
		/// An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
		/// information.
		/// </param>
		/// <filterpriority>2</filterpriority>
		public bool ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(_value, provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent 8-bit unsigned integer using the specified culture-specific formatting information.
		/// </summary>
		/// <returns>
		/// An 8-bit unsigned integer equivalent to the value of this instance.
		/// </returns>
		/// <param name="provider">
		/// An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
		/// information.
		/// </param>
		/// <filterpriority>2</filterpriority>
		public byte ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(_value, provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent Unicode character using the specified culture-specific formatting information.
		/// </summary>
		/// <returns>
		/// A Unicode character equivalent to the value of this instance.
		/// </returns>
		/// <param name="provider">
		/// An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
		/// information.
		/// </param>
		/// <filterpriority>2</filterpriority>
		public char ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(_value, provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent <see cref="T:System.DateTime" /> using the specified culture-specific formatting
		/// information.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.DateTime" /> instance equivalent to the value of this instance.
		/// </returns>
		/// <param name="provider">
		/// An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
		/// information.
		/// </param>
		/// <filterpriority>2</filterpriority>
		public DateTime ToDateTime(IFormatProvider provider)
		{
			return Convert.ToDateTime(_value, provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent <see cref="T:System.Decimal" /> number using the specified culture-specific formatting
		/// information.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Decimal" /> number equivalent to the value of this instance.
		/// </returns>
		/// <param name="provider">
		/// An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
		/// information.
		/// </param>
		/// <filterpriority>2</filterpriority>
		public decimal ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(_value, provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent double-precision floating-point number using the specified culture-specific formatting
		/// information.
		/// </summary>
		/// <returns>
		/// A double-precision floating-point number equivalent to the value of this instance.
		/// </returns>
		/// <param name="provider">
		/// An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
		/// information.
		/// </param>
		/// <filterpriority>2</filterpriority>
		public double ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(_value, provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent 16-bit signed integer using the specified culture-specific formatting information.
		/// </summary>
		/// <returns>
		/// An 16-bit signed integer equivalent to the value of this instance.
		/// </returns>
		/// <param name="provider">
		/// An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
		/// information.
		/// </param>
		/// <filterpriority>2</filterpriority>
		public short ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(_value, provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent 32-bit signed integer using the specified culture-specific formatting information.
		/// </summary>
		/// <returns>
		/// An 32-bit signed integer equivalent to the value of this instance.
		/// </returns>
		/// <param name="provider">
		/// An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
		/// information.
		/// </param>
		/// <filterpriority>2</filterpriority>
		public int ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(_value, provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent 64-bit signed integer using the specified culture-specific formatting information.
		/// </summary>
		/// <returns>
		/// An 64-bit signed integer equivalent to the value of this instance.
		/// </returns>
		/// <param name="provider">
		/// An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
		/// information.
		/// </param>
		/// <filterpriority>2</filterpriority>
		public long ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(_value, provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent 8-bit signed integer using the specified culture-specific formatting information.
		/// </summary>
		/// <returns>
		/// An 8-bit signed integer equivalent to the value of this instance.
		/// </returns>
		/// <param name="provider">
		/// An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
		/// information.
		/// </param>
		/// <filterpriority>2</filterpriority>
		[CLSCompliant(false)]
		public sbyte ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(_value, provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent single-precision floating-point number using the specified culture-specific formatting
		/// information.
		/// </summary>
		/// <returns>
		/// A single-precision floating-point number equivalent to the value of this instance.
		/// </returns>
		/// <param name="provider">
		/// An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
		/// information.
		/// </param>
		/// <filterpriority>2</filterpriority>
		public float ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(_value, provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent <see cref="T:System.String" /> using the specified culture-specific formatting
		/// information.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String" /> instance equivalent to the value of this instance.
		/// </returns>
		/// <param name="provider">
		/// An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
		/// information.
		/// </param>
		/// <filterpriority>2</filterpriority>
		public string ToString(IFormatProvider provider)
		{
			return Convert.ToString(_value, provider)!;
		}

		/// <summary>
		/// Converts the value of this instance to an <see cref="T:System.Object" /> of the specified <see cref="T:System.Type" /> that has an
		/// equivalent value, using the specified culture-specific formatting information.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Object" /> instance of type <paramref name="conversionType" /> whose value is equivalent to the value of this
		/// instance.
		/// </returns>
		/// <param name="conversionType">The <see cref="T:System.Type" /> to which the value of this instance is converted. </param>
		/// <param name="provider">
		/// An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
		/// information.
		/// </param>
		/// <filterpriority>2</filterpriority>
		public object ToType(Type conversionType, IFormatProvider provider)
		{
			return Convert.ChangeType(_value, conversionType, provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent 16-bit unsigned integer using the specified culture-specific formatting information.
		/// </summary>
		/// <returns>
		/// An 16-bit unsigned integer equivalent to the value of this instance.
		/// </returns>
		/// <param name="provider">
		/// An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
		/// information.
		/// </param>
		/// <filterpriority>2</filterpriority>
		[CLSCompliant(false)]
		public ushort ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(_value, provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent 32-bit unsigned integer using the specified culture-specific formatting information.
		/// </summary>
		/// <returns>
		/// An 32-bit unsigned integer equivalent to the value of this instance.
		/// </returns>
		/// <param name="provider">
		/// An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
		/// information.
		/// </param>
		/// <filterpriority>2</filterpriority>
		[CLSCompliant(false)]
		public uint ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(_value, provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent 64-bit unsigned integer using the specified culture-specific formatting information.
		/// </summary>
		/// <returns>
		/// An 64-bit unsigned integer equivalent to the value of this instance.
		/// </returns>
		/// <param name="provider">
		/// An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
		/// information.
		/// </param>
		/// <filterpriority>2</filterpriority>
		[CLSCompliant(false)]
		public ulong ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(_value, provider);
		}

		/// <summary>
		/// Returns the value as a dictionary if the current value represents an object.
		/// Otherwise returns null.
		/// </summary>
		public IDictionary<string, DynamicValue> ToDictionary()
		{
			if (_value is IDictionary<string, object> dict) return DynamicDictionary.Create(dict);
			else if (_value is JsonElement e && e.ValueKind == JsonValueKind.Object)
			{
				var d = e.EnumerateObject()
					.Aggregate(new Dictionary<string, object>(), (dictionary, je) =>
					{
						dictionary.Add(je.Name, je.Value);
						return dictionary;
					});
				return DynamicDictionary.Create(d);
			}

			return null;
		}


		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the current object is equal to the <paramref name="compareValue" /> parameter; otherwise, <c>false</c>.
		/// </returns>
		/// <param name="compareValue">An <see cref="DynamicValue" /> to compare with this instance.</param>
		public bool Equals(DynamicValue compareValue)
		{
			if (ReferenceEquals(null, compareValue))
			{
				return false;
			}

			return ReferenceEquals(this, compareValue) || Equals(compareValue._value, _value);
		}

		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			var name = binder.Name;

			return Dispatch(out result, name);
		}


		private bool Dispatch(out object result, string name)
		{
			if (!HasValue)
			{
				result = NullValue;
				return true;
			}

			if (Value is IDictionary<string, object> d)
			{
				result = d.TryGetValue(name, out var r) ? SelfOrNew(r) : NullValue;
				return true;
			}
			if (Value is IDynamicMetaObjectProvider)
			{
				var dm = GetDynamicMember(Value, name);
				result = SelfOrNew(dm);
				return true;
			}
			if (Value is IDictionary ds)
			{
				result = ds.Contains(name) ? SelfOrNew(ds[name]) : NullValue;
				return true;
			}
			if (Value is IList l)
			{
				var projected = l
					.Cast<object>()
					.Select(i => SelfOrNew(i).Dispatch(out var o, name) ? o : null)
					.Where(i => i != null)
					.ToArray();
				result = SelfOrNew(projected);
				return projected.Length > 0;
			}
			if (Value is IList<object> lo)
			{
				var projected = lo
					.Select(i => SelfOrNew(i).Dispatch(out var o, name) ? o : null)
					.Where(i => i != null)
					.ToArray();
				result = SelfOrNew(projected);
				return projected.Length > 0;
			}
			if (Value is JsonElement e && e.ValueKind == JsonValueKind.Object)
			{
				if (e.TryGetProperty(name, out var r))
				{
					result = SelfOrNew(r);
					return true;
				}
			}
			if (Value is JsonElement a && a.ValueKind == JsonValueKind.Array)
			{
				var projected = a.EnumerateArray()
					.Select(i => SelfOrNew(i).Dispatch(out var o, name) ? o : null)
					.Where(i => i != null)
					.ToArray();
				result = SelfOrNew(projected);
				return projected.Length > 0;
			}

			result = NullValue;
			return true;
		}

		private static object GetDynamicMember(object obj, string memberName)
		{
			var binder = Binder.GetMember(CSharpBinderFlags.None, memberName, null,
				new[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) });
			var callsite = CallSite<Func<CallSite, object, object>>.Create(binder);
			return callsite.Target(callsite, obj);
		}

		/// <summary>
		/// Returns a default value if Value is null
		/// </summary>
		/// <typeparam name="T">When no default value is supplied, required to supply the default type</typeparam>
		/// <param name="defaultValue">Optional parameter for default value, if not given it returns default of type T</param>
		/// <returns>If value is not null, value is returned, else default value is returned</returns>
		public T Default<T>(T defaultValue = default(T))
		{
			if (HasValue)
			{
				try
				{
					return (T)_value;
				}
				catch
				{
					var typeName = _value.GetType().Name;
					var message = string.Format("Cannot convert value of type '{0}' to type '{1}'",
						typeName, typeof(T).Name);

					throw new InvalidCastException(message);
				}
			}

			return defaultValue;
		}

		/// <summary>
		/// Attempts to convert the value to type of T, failing to do so will return the defaultValue.
		/// </summary>
		/// <typeparam name="T">When no default value is supplied, required to supply the default type</typeparam>
		/// <param name="defaultValue">Optional parameter for default value, if not given it returns default of type T</param>
		/// <returns>If value is not null, value is returned, else default value is returned</returns>
		public T TryParse<T>(T defaultValue = default)
		{
			if (!HasValue) return defaultValue;

			try
			{
				return TryParse(defaultValue, typeof(T), _value, out var o) ? (T)o : defaultValue;
			}
			catch
			{
				return defaultValue;
			}
		}

		public static object ConsumeJsonElement(Type targetReturnType, JsonElement e)
		{
			// ReSharper disable once HeapView.BoxingAllocation
			object ParseNumber(JsonElement el)
			{
				if (el.TryGetInt64(out var l))
					return l;

				return el.GetDouble();
			}

			return targetReturnType switch
			{
				_ when targetReturnType == typeof(bool) => e.GetBoolean(),
				_ when targetReturnType == typeof(byte) => e.GetByte(),
				_ when targetReturnType == typeof(decimal) => e.GetDecimal(),
				_ when targetReturnType == typeof(double) => e.GetDouble(),
				_ when targetReturnType == typeof(Guid) => e.GetGuid(),
				_ when targetReturnType == typeof(short) => e.GetInt16(),
				_ when targetReturnType == typeof(int) => e.GetInt32(),
				_ when targetReturnType == typeof(long) => e.GetInt64(),
				_ when targetReturnType == typeof(float) => e.GetSingle(),
				_ when targetReturnType == typeof(string) => e.GetString(),
				_ when targetReturnType == typeof(DateTime) => e.GetDateTime(),
				_ when targetReturnType == typeof(DateTimeOffset) => e.GetDateTimeOffset(),
				_ when targetReturnType == typeof(ushort) => e.GetUInt16(),
				_ when targetReturnType == typeof(uint) => e.GetUInt32(),
				_ when targetReturnType == typeof(ulong) => e.GetUInt64(),
				_ when targetReturnType == typeof(sbyte) => e.GetSByte(),
				_ when targetReturnType == typeof(DynamicDictionary) => DynamicDictionary.Create(e),
				_ when targetReturnType == typeof(object) && e.ValueKind == JsonValueKind.Array =>
					e.EnumerateArray().Select(je => ConsumeJsonElement(targetReturnType, je)).ToArray(),
				_ when targetReturnType == typeof(object) && e.ValueKind == JsonValueKind.Object => e.ToDictionary(),
				_ when targetReturnType == typeof(object) && e.ValueKind == JsonValueKind.True => true,
				_ when targetReturnType == typeof(object) && e.ValueKind == JsonValueKind.False => false,
				_ when targetReturnType == typeof(object) && e.ValueKind == JsonValueKind.Null => null,
				_ when targetReturnType == typeof(object) && e.ValueKind == JsonValueKind.String => e.GetString(),
				_ when targetReturnType == typeof(object) && e.ValueKind == JsonValueKind.Number => ParseNumber(e),
				_ => null
			};
		}

		internal bool TryParse(object defaultValue, Type targetReturnType, object value, out object newObject)
		{
			newObject = defaultValue;
			if (value == null) return false;

			if (targetReturnType.IsGenericType && targetReturnType.GetGenericTypeDefinition() == typeof(Nullable<>))
				targetReturnType = targetReturnType.GenericTypeArguments[0];

			try
			{
				if (value is JsonElement e)
				{

					// ReSharper disable once HeapView.BoxingAllocation
					newObject = ConsumeJsonElement(targetReturnType, e);
					return true;
				}

				var valueType = value.GetType();
				if (targetReturnType.IsArray && value is DynamicValue v)
				{
					value = v.Value;
					valueType = value.GetType();
				}
				if (targetReturnType.IsArray)
				{
					if (!valueType.IsArray)
					{
						return false;
					}
					var ar = (object[])value;
					var t = targetReturnType.GetElementType();
					var objectArray = ar
						.Select(a => TryParse(defaultValue, t, a, out var o) ? o : null)
						.Where(a => a != null)
						.ToArray();

					var arr = Array.CreateInstance(t, objectArray.Length);
					Array.Copy(objectArray, arr, objectArray.Length);
					newObject = arr;
					return true;
				}

				if (valueType.IsAssignableFrom(targetReturnType))
				{
					newObject = value;
					return true;
				}

				var stringValue = value as string;

				if (targetReturnType == typeof(DateTime)
					&& DateTime.TryParse(stringValue, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
				{
					newObject = result;
					return true;
				}
				if (stringValue != null)
				{
					if (targetReturnType == typeof(object))
					{
						newObject = Convert.ChangeType(value, targetReturnType);
						return true;
					}

					var converter = TypeDescriptor.GetConverter(targetReturnType);
					if (converter.IsValid(stringValue))
					{
						newObject = converter.ConvertFromInvariantString(stringValue);
						return true;
					}
				}
				else if (value is DynamicValue dv)
					return dv.TryParse(defaultValue, targetReturnType, dv.Value, out newObject);
				else if (targetReturnType == typeof(string))
				{
					newObject = Convert.ChangeType(value, TypeCode.String, CultureInfo.InvariantCulture);
					return true;
				}
				else if (valueType.IsValueType)
				{
					newObject = Convert.ChangeType(_value, targetReturnType);
					return true;
				}
				else if (targetReturnType == typeof(DynamicDictionary) && valueType == typeof(Dictionary<string, object>))
				{
					newObject = DynamicDictionary.Create(value as Dictionary<string, object>);
					return true;
				}

				else if (targetReturnType == typeof(object))
				{
					newObject = value;
					return true;
				}
			}
			catch
			{
				return false;
			}
			return false;
		}



		public static bool operator ==(DynamicValue dynamicValue, object compareValue)
		{
			if (dynamicValue._value == null && compareValue == null)
			{
				return true;
			}

			return dynamicValue._value != null && dynamicValue._value.Equals(compareValue);
		}

		public static bool operator !=(DynamicValue dynamicValue, object compareValue)
		{
			return !(dynamicValue == compareValue);
		}

		/// <summary>
		/// Determines whether the specified <see cref="object" /> is equal to the current <see cref="object" />.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the specified <see cref="object" /> is equal to the current <see cref="DynamicValue" />; otherwise,
		/// <c>false</c>.
		/// </returns>
		/// <param name="compareValue">The <see cref="object" /> to compare with the current <see cref="DynamicValue" />.</param>
		public override bool Equals(object compareValue)
		{
			if (ReferenceEquals(null, compareValue))
			{
				return false;
			}

			if (ReferenceEquals(this, compareValue)
				|| ReferenceEquals(_value, compareValue)
				|| Equals(_value, compareValue)
			)
			{
				return true;
			}

			return compareValue.GetType() == typeof(DynamicValue) && Equals((DynamicValue)compareValue);
		}

		/// <summary>
		/// Serves as a hash function for a particular type.
		/// </summary>
		/// <returns>A hash code for the current instance.</returns>
		public override int GetHashCode()
		{
			return _value != null ? _value.GetHashCode() : 0;
		}

		/// <summary>
		/// Provides implementation for binary operations. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override
		/// this method to specify dynamic behavior for operations such as addition and multiplication.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the operation is successful; otherwise, <c>false</c>. If this method returns <c>false</c>, the run-time binder of
		/// the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)
		/// </returns>
		/// <param name="binder">
		/// Provides information about the binary operation. The binder.Operation property returns an
		/// <see cref="T:System.Linq.Expressions.ExpressionType" /> object. For example, for the sum = first + second statement, where first and second
		/// are derived from the DynamicObject class, binder.Operation returns ExpressionType.Add.
		/// </param>
		/// <param name="arg">
		/// The right operand for the binary operation. For example, for the sum = first + second statement, where first and second
		/// are derived from the DynamicObject class, <paramref name="arg" /> is equal to second.
		/// </param>
		/// <param name="result">The result of the binary operation.</param>
		public override bool TryBinaryOperation(BinaryOperationBinder binder, object arg, out object result)
		{
			object resultOfCast;
			result = null;

			if (binder.Operation != ExpressionType.Equal)
			{
				return false;
			}

			var convert =
				Binder.Convert(CSharpBinderFlags.None, arg.GetType(), typeof(DynamicValue));

			if (!TryConvert((ConvertBinder)convert, out resultOfCast))
			{
				return false;
			}

			result = resultOfCast == null ? Equals(arg, resultOfCast) : resultOfCast.Equals(arg);

			return true;
		}

		/// <summary>
		/// Provides implementation for type conversion operations. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can
		/// override this method to specify dynamic behavior for operations that convert an object from one type to another.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the operation is successful; otherwise, <c>false</c>. If this method returns <c>false</c>, the run-time binder of
		/// the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)
		/// </returns>
		/// <param name="binder">
		/// Provides information about the conversion operation. The binder.Type property provides the type to which the object
		/// must be converted. For example, for the statement (String)sampleObject in C# (CType(sampleObject, Type) in Visual Basic), where
		/// sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, binder.Type returns the
		/// <see cref="T:System.String" /> type. The binder.Explicit property provides information about the kind of conversion that occurs. It returns
		/// true for explicit conversion and false for implicit conversion.
		/// </param>
		/// <param name="result">The result of the type conversion operation.</param>
		public override bool TryConvert(ConvertBinder binder, out object result)
		{
			result = null;

			if (_value == null)
			{
				return true;
			}

			var binderType = binder.Type;

			if (binderType == typeof(List<DynamicValue>))
			{
				result = ToEnumerable().ToList();
				return true;
			}
			if (binderType == typeof(List<object>))
			{
				result = ToEnumerable().Cast<object>().ToList();
				return true;
			}

			if (binderType == typeof(string))
			{
				result = Convert.ToString(_value);
				return true;
			}

			if (binderType == typeof(Guid) || binderType == typeof(Guid?))
			{
				Guid guid;
				if (Guid.TryParse(Convert.ToString(_value), out guid))
				{
					result = guid;
					return true;
				}
			}
			else if (binderType == typeof(TimeSpan) || binderType == typeof(TimeSpan?))
			{
				TimeSpan timespan;
				if (TimeSpan.TryParse(Convert.ToString(_value), out timespan))
				{
					result = timespan;
					return true;
				}
			}
			else
			{
				if (binderType.IsGenericType && binderType.GetGenericTypeDefinition() == typeof(Nullable<>))
					binderType = binderType.GetGenericArguments()[0];

				var typeCode = Type.GetTypeCode(binderType);

				if (typeCode == TypeCode.Object)
				{
					if (binderType.IsAssignableFrom(_value.GetType()))
					{
						result = _value;
						return true;
					}
					else
						return false;
				}

				result = Convert.ChangeType(_value, typeCode);
				return true;
			}
			return base.TryConvert(binder, out result);
		}

		public override string ToString()
		{
			return _value == null ? base.ToString() : Convert.ToString(_value);
		}

		public static implicit operator bool(DynamicValue dynamicValue)
		{
			if (!dynamicValue.HasValue) return false;
			if (dynamicValue._value is JsonElement e) return e.GetBoolean();

			if (dynamicValue._value.GetType().IsValueType) return Convert.ToBoolean(dynamicValue._value);

			if (bool.TryParse(dynamicValue.ToString(CultureInfo.InvariantCulture), out var result)) return result;

			return true;
		}

		public static implicit operator string(DynamicValue dynamicValue)
		{
			if (!dynamicValue.HasValue) return null;
			if (dynamicValue._value is JsonElement e) return e.GetString();

			return Convert.ToString(dynamicValue._value);
		}

		public static implicit operator int(DynamicValue dynamicValue)
		{
			if (dynamicValue._value is JsonElement e && e.TryGetInt32(out var v)) return v;
			if (dynamicValue._value.GetType().IsValueType) return Convert.ToInt32(dynamicValue._value);

			return int.Parse(dynamicValue.ToString(CultureInfo.InvariantCulture));
		}

		public static implicit operator Guid(DynamicValue dynamicValue)
		{
			if (dynamicValue._value is JsonElement e && e.TryGetGuid(out var v)) return v;
			if (dynamicValue._value is Guid)
			{
				return (Guid)dynamicValue._value;
			}

			return Guid.Parse(dynamicValue.ToString(CultureInfo.InvariantCulture));
		}

		public static implicit operator DateTime(DynamicValue dynamicValue)
		{
			if (dynamicValue._value is JsonElement e && e.TryGetDateTime(out var v)) return v;
			if (dynamicValue._value is DateTime)
			{
				return (DateTime)dynamicValue._value;
			}

			return DateTime.Parse(dynamicValue.ToString(CultureInfo.InvariantCulture));
		}

		public static implicit operator DateTimeOffset(DynamicValue dynamicValue)
		{
			if (dynamicValue._value is JsonElement e && e.TryGetDateTimeOffset(out var v)) return v;
			if (dynamicValue._value is DateTimeOffset)
			{
				return (DateTimeOffset)dynamicValue._value;
			}
			return DateTimeOffset.Parse(dynamicValue);
		}

		public static implicit operator TimeSpan(DynamicValue dynamicValue)
		{
			if (dynamicValue._value is TimeSpan)
			{
				return (TimeSpan)dynamicValue._value;
			}

			return TimeSpan.Parse(dynamicValue.ToString(CultureInfo.InvariantCulture));
		}

		public static implicit operator long(DynamicValue dynamicValue)
		{
			if (dynamicValue._value is JsonElement e && e.TryGetInt64(out var v)) return v;
			if (dynamicValue._value.GetType().IsValueType) return Convert.ToInt64(dynamicValue._value);

			return long.Parse(dynamicValue.ToString(CultureInfo.InvariantCulture));
		}

		public static implicit operator float(DynamicValue dynamicValue)
		{
			if (dynamicValue._value is JsonElement e && e.TryGetSingle(out var v)) return v;
			if (dynamicValue._value.GetType().IsValueType) return Convert.ToSingle(dynamicValue._value);

			return float.Parse(dynamicValue.ToString(CultureInfo.InvariantCulture));
		}

		public static implicit operator decimal(DynamicValue dynamicValue)
		{
			if (dynamicValue._value is JsonElement e && e.TryGetDecimal(out var v)) return v;
			if (dynamicValue._value.GetType().IsValueType) return Convert.ToDecimal(dynamicValue._value);

			return decimal.Parse(dynamicValue.ToString(CultureInfo.InvariantCulture));
		}

		public static implicit operator double(DynamicValue dynamicValue)
		{
			if (dynamicValue._value is JsonElement e && e.TryGetDouble(out var v)) return v;
			if (dynamicValue._value.GetType().IsValueType) return Convert.ToDouble(dynamicValue._value);

			return double.Parse(dynamicValue.ToString(CultureInfo.InvariantCulture));
		}

		public IEnumerable<DynamicValue> ToEnumerable()
		{
			using var e = GetEnumerator();
			while(e.MoveNext())
				yield return e.Current;
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public IEnumerator<DynamicValue> GetEnumerator()
		{
			if (Value is ICollection c) return c.OfType<object>().Select(v => SelfOrNew(v)).GetEnumerator();
			else if (Value is IList l) return l.OfType<object>().Select(v => SelfOrNew(v)).GetEnumerator();
			else if (Value is IDictionary<string, object> d) return d.Select(kv=> SelfOrNew(kv.Value)).GetEnumerator();
			else if (Value is IDictionary<string, DynamicValue> dv) return dv.Values.GetEnumerator();
			else if (Value is JsonElement e && e.ValueKind == JsonValueKind.Array) return e.EnumerateArray().Select(a=> SelfOrNew(a)).GetEnumerator();
			else if (Value is JsonElement el && el.ValueKind == JsonValueKind.Object)
				return ToDictionary().Values.Select(v => SelfOrNew(v)).GetEnumerator();

			return Value == null
				? Enumerable.Empty<DynamicValue>().GetEnumerator()
				: new List<DynamicValue>() { this }.GetEnumerator();
		}

		public int Count
		{
			get
			{
				if (Value is ICollection c) return c.Count;
				else if (Value is IList l) return l.Count;
				else if (Value is IDictionary<string, object> d) return d.Count;
				else if (Value is IDictionary<string, DynamicValue> dv) return dv.Count;
				else if (Value is JsonElement e && e.ValueKind == JsonValueKind.Array) return e.GetArrayLength();
				else if (Value is JsonElement el && el.ValueKind == JsonValueKind.Object) return el.EnumerateObject().Count();

				return Value == null ? 0 : 1;
			}
		}

		public DynamicValue this[int i]
		{
			get
			{
				if (!HasValue) return NullValue;

				var v = Value;

				if (v is IList l && l.Count - 1 >= i)
					return SelfOrNew(l[i]);
				if (v is IList<object> o && o.Count - 1 >= i)
					return SelfOrNew(o[i]);

				if (v is IDictionary<string, object> d)
				{
					if (d.TryGetValue(i.ToString(CultureInfo.InvariantCulture), out v))
						return SelfOrNew(v);

					if (i >= d.Count) return new DynamicValue(null);
					var at = d[d.Keys.ElementAt(i)];
					return SelfOrNew(at);
				}
				if (v is IDictionary<string, DynamicValue> dv)
				{
					if (dv.TryGetValue(i.ToString(CultureInfo.InvariantCulture), out var dvv))
						return dvv;

					if (i >= dv.Count) return new DynamicValue(null);
					var at = dv[dv.Keys.ElementAt(i)];
					return at;
				}
				if (v is JsonElement e && e.ValueKind == JsonValueKind.Array)
				{
					if (e.GetArrayLength() -1 >= i)
						return SelfOrNew(e[i]);
				}
				if (v is JsonElement el && el.ValueKind == JsonValueKind.Object)
				{
					return SelfOrNew(el.GetProperty(i.ToString(CultureInfo.InvariantCulture)));
				}

				return NullValue;
			}
		}




	}
}
