using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IPropertiesDescriptor<T, out TReturnType>
		where T : class
		where TReturnType : class
	{
#pragma warning disable CS3001 // Argument type is not CLS-compliant
		TReturnType Scalar(Expression<Func<T, int>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector);

		TReturnType Scalar(Expression<Func<T, int?>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector);

		TReturnType Scalar(Expression<Func<T, float>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector);

		TReturnType Scalar(Expression<Func<T, float?>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector);

		TReturnType Scalar(Expression<Func<T, sbyte>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector);

		TReturnType Scalar(Expression<Func<T, sbyte?>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector);

		TReturnType Scalar(Expression<Func<T, short>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector);

		TReturnType Scalar(Expression<Func<T, short?>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector);

		TReturnType Scalar(Expression<Func<T, byte>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector);

		TReturnType Scalar(Expression<Func<T, byte?>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector);

		TReturnType Scalar(Expression<Func<T, long>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector);

		TReturnType Scalar(Expression<Func<T, long?>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector);

		TReturnType Scalar(Expression<Func<T, uint>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector);

		TReturnType Scalar(Expression<Func<T, uint?>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector);

		TReturnType Scalar(Expression<Func<T, TimeSpan>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector);

		TReturnType Scalar(Expression<Func<T, TimeSpan?>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector);

		TReturnType Scalar(Expression<Func<T, decimal>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector);

		TReturnType Scalar(Expression<Func<T, decimal?>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector);

		TReturnType Scalar(Expression<Func<T, ulong>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector);

		TReturnType Scalar(Expression<Func<T, ulong?>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector);

		TReturnType Scalar(Expression<Func<T, double>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector);

		TReturnType Scalar(Expression<Func<T, double?>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector);

		TReturnType Scalar(Expression<Func<T, DateTime>> field, Func<DatePropertyDescriptor<T>, IDateProperty> selector);

		TReturnType Scalar(Expression<Func<T, DateTime?>> field, Func<DatePropertyDescriptor<T>, IDateProperty> selector);

		TReturnType Scalar(Expression<Func<T, DateTimeOffset>> field, Func<DatePropertyDescriptor<T>, IDateProperty> selector);

		TReturnType Scalar(Expression<Func<T, DateTimeOffset?>> field, Func<DatePropertyDescriptor<T>, IDateProperty> selector);

		TReturnType Scalar(Expression<Func<T, bool>> field, Func<BooleanPropertyDescriptor<T>, IBooleanProperty> selector);

		TReturnType Scalar(Expression<Func<T, bool?>> field, Func<BooleanPropertyDescriptor<T>, IBooleanProperty> selector);

		TReturnType Scalar(Expression<Func<T, char>> field, Func<KeywordPropertyDescriptor<T>, IKeywordProperty> selector);

		TReturnType Scalar(Expression<Func<T, char?>> field, Func<KeywordPropertyDescriptor<T>, IKeywordProperty> selector);

		TReturnType Scalar(Expression<Func<T, Guid>> field, Func<KeywordPropertyDescriptor<T>, IKeywordProperty> selector);

		TReturnType Scalar(Expression<Func<T, Guid?>> field, Func<KeywordPropertyDescriptor<T>, IKeywordProperty> selector);

		TReturnType Scalar(Expression<Func<T, string>> field, Func<TextPropertyDescriptor<T>, ITextProperty> selector);
#pragma warning restore CS3001 // Argument type is not CLS-compliant
	}

	public partial class PropertiesDescriptor<T> : IsADictionaryDescriptorBase<PropertiesDescriptor<T>, IProperties, PropertyName, IProperty>, IPropertiesDescriptor<T, PropertiesDescriptor<T>>
		where T : class
	{
#pragma warning disable CS3001 // Argument type is not CLS-compliant
		public PropertiesDescriptor<T> Scalar(Expression<Func<T, int>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector) =>
			SetProperty(selector?.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Integer)));

		public PropertiesDescriptor<T> Scalar(Expression<Func<T, int?>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector) =>
			SetProperty(selector?.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Integer)));

		public PropertiesDescriptor<T> Scalar(Expression<Func<T, float>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector) =>
			SetProperty(selector?.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Float)));

		public PropertiesDescriptor<T> Scalar(Expression<Func<T, float?>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector) =>
			SetProperty(selector?.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Float)));

		public PropertiesDescriptor<T> Scalar(Expression<Func<T, sbyte>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector) =>
			SetProperty(selector?.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Byte)));

		public PropertiesDescriptor<T> Scalar(Expression<Func<T, sbyte?>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector) =>
			SetProperty(selector?.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Byte)));

		public PropertiesDescriptor<T> Scalar(Expression<Func<T, short>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector) =>
			SetProperty(selector?.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Short)));

		public PropertiesDescriptor<T> Scalar(Expression<Func<T, short?>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector) =>
			SetProperty(selector?.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Short)));

		public PropertiesDescriptor<T> Scalar(Expression<Func<T, byte>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector) =>
			SetProperty(selector?.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Short)));

		public PropertiesDescriptor<T> Scalar(Expression<Func<T, byte?>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector) =>
			SetProperty(selector?.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Short)));

		public PropertiesDescriptor<T> Scalar(Expression<Func<T, long>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector) =>
			SetProperty(selector?.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Long)));

		public PropertiesDescriptor<T> Scalar(Expression<Func<T, long?>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector) =>
			SetProperty(selector?.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Long)));

		public PropertiesDescriptor<T> Scalar(Expression<Func<T, uint>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector) =>
			SetProperty(selector?.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Long)));

		public PropertiesDescriptor<T> Scalar(Expression<Func<T, uint?>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector) =>
			SetProperty(selector?.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Long)));

		public PropertiesDescriptor<T> Scalar(Expression<Func<T, TimeSpan>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector) =>
			SetProperty(selector?.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Long)));

		public PropertiesDescriptor<T> Scalar(Expression<Func<T, TimeSpan?>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector) =>
			SetProperty(selector?.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Long)));

		public PropertiesDescriptor<T> Scalar(Expression<Func<T, decimal>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector) =>
			SetProperty(selector?.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Double)));

		public PropertiesDescriptor<T> Scalar(Expression<Func<T, decimal?>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector) =>
			SetProperty(selector?.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Double)));

		public PropertiesDescriptor<T> Scalar(Expression<Func<T, ulong>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector) =>
			SetProperty(selector?.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Double)));

		public PropertiesDescriptor<T> Scalar(Expression<Func<T, ulong?>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector) =>
			SetProperty(selector?.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Double)));

		public PropertiesDescriptor<T> Scalar(Expression<Func<T, double>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector) =>
			SetProperty(selector?.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Double)));

		public PropertiesDescriptor<T> Scalar(Expression<Func<T, double?>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector) =>
			SetProperty(selector?.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Double)));

		public PropertiesDescriptor<T> Scalar(Expression<Func<T, DateTime>> field, Func<DatePropertyDescriptor<T>, IDateProperty> selector) =>
			SetProperty(selector?.InvokeOrDefault(new DatePropertyDescriptor<T>().Name(field)));

		public PropertiesDescriptor<T> Scalar(Expression<Func<T, DateTime?>> field, Func<DatePropertyDescriptor<T>, IDateProperty> selector) =>
			SetProperty(selector?.InvokeOrDefault(new DatePropertyDescriptor<T>().Name(field)));

		public PropertiesDescriptor<T> Scalar(Expression<Func<T, DateTimeOffset>> field, Func<DatePropertyDescriptor<T>, IDateProperty> selector) =>
			SetProperty(selector?.InvokeOrDefault(new DatePropertyDescriptor<T>().Name(field)));

		public PropertiesDescriptor<T> Scalar(Expression<Func<T, DateTimeOffset?>> field, Func<DatePropertyDescriptor<T>, IDateProperty> selector) =>
			SetProperty(selector?.InvokeOrDefault(new DatePropertyDescriptor<T>().Name(field)));

		public PropertiesDescriptor<T> Scalar(Expression<Func<T, bool>> field, Func<BooleanPropertyDescriptor<T>, IBooleanProperty> selector) =>
			SetProperty(selector?.InvokeOrDefault(new BooleanPropertyDescriptor<T>().Name(field)));

		public PropertiesDescriptor<T> Scalar(Expression<Func<T, bool?>> field, Func<BooleanPropertyDescriptor<T>, IBooleanProperty> selector) =>
			SetProperty(selector?.InvokeOrDefault(new BooleanPropertyDescriptor<T>().Name(field)));

		public PropertiesDescriptor<T> Scalar(Expression<Func<T, char>> field, Func<KeywordPropertyDescriptor<T>, IKeywordProperty> selector) =>
			SetProperty(selector?.InvokeOrDefault(new KeywordPropertyDescriptor<T>().Name(field)));

		public PropertiesDescriptor<T> Scalar(Expression<Func<T, char?>> field, Func<KeywordPropertyDescriptor<T>, IKeywordProperty> selector) =>
			SetProperty(selector?.InvokeOrDefault(new KeywordPropertyDescriptor<T>().Name(field)));

		public PropertiesDescriptor<T> Scalar(Expression<Func<T, Guid>> field, Func<KeywordPropertyDescriptor<T>, IKeywordProperty> selector) =>
			SetProperty(selector?.InvokeOrDefault(new KeywordPropertyDescriptor<T>().Name(field)));

		public PropertiesDescriptor<T> Scalar(Expression<Func<T, Guid?>> field, Func<KeywordPropertyDescriptor<T>, IKeywordProperty> selector) =>
			SetProperty(selector?.InvokeOrDefault(new KeywordPropertyDescriptor<T>().Name(field)));

		public PropertiesDescriptor<T> Scalar(Expression<Func<T, string>> field, Func<TextPropertyDescriptor<T>, ITextProperty> selector) =>
			SetProperty(selector?.InvokeOrDefault(new TextPropertyDescriptor<T>().Name(field)));
#pragma warning restore CS3001 // Argument type is not CLS-compliant

	}
}
