using System;
using System.Collections.Generic;

namespace Nest
{
	public class MappingWalker
	{
		private readonly IMappingVisitor _visitor;

		public MappingWalker(IMappingVisitor visitor)
		{
			visitor.ThrowIfNull(nameof(visitor));
			_visitor = visitor;
		}

		public void Accept(IGetMappingResponse response)
		{
			if (response == null) return;

			foreach (var indexMapping in response.Mappings)
			foreach (var typeMapping in indexMapping.Value)
			{
				this.Accept(typeMapping.Value);
			}
		}

		public void Accept(ITypeMapping mapping)
		{
			if (mapping == null) return;
			this._visitor.Visit(mapping);
			this.Accept(mapping.Properties);
		}


		private void Visit<TProperty>(IProperty prop, Action<TProperty> act)
			where TProperty : class, IProperty
		{
			var t = prop as TProperty;
			if (t == null) return;
			act(t);
		}

		public void Accept(IProperties properties)
		{
			if (properties == null) return;
			foreach (var kv in properties)
			{
				var field = kv.Value;
				var type = field.Type;
				var ft = type.Name.ToEnum<FieldType>();
				switch (ft)
				{
					case FieldType.Text:
						this.Visit<ITextProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this.Accept(t.Fields);
						});
						break;
					case FieldType.Keyword:
						this.Visit<IKeywordProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this.Accept(t.Fields);
						});
						break;
					case FieldType.String:
#pragma warning disable 618
						this.Visit<IStringProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this.Accept(t.Fields);
						});
#pragma warning restore 618
						break;
					//TODO implement type specific visitors too!
					case FieldType.HalfFloat:
					case FieldType.ScaledFloat:
					case FieldType.Float:
					case FieldType.Double:
					case FieldType.Byte:
					case FieldType.Short:
					case FieldType.Integer:
					case FieldType.Long:
						this.Visit<INumberProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this.Accept(t.Fields);
						});
						break;
					case FieldType.Date:
						this.Visit<IDateProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this.Accept(t.Fields);
						});
						break;
					case FieldType.Boolean:
						this.Visit<IBooleanProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this.Accept(t.Fields);
						});
						break;
					case FieldType.Binary:
						this.Visit<IBinaryProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this.Accept(t.Fields);
						});
						break;
					case FieldType.Object:
						this.Visit<IObjectProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this._visitor.Depth += 1;
							this.Accept(t.Properties);
							this._visitor.Depth -= 1;
						});
						break;
					case FieldType.Nested:
						this.Visit<INestedProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this._visitor.Depth += 1;
							this.Accept(t.Properties);
							this._visitor.Depth -= 1;
						});
						break;
					case FieldType.Ip:
						this.Visit<IIpProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this.Accept(t.Fields);
						});
						break;
					case FieldType.GeoPoint:
						this.Visit<IGeoPointProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this.Accept(t.Fields);
						});
						break;
					case FieldType.GeoShape:
						this.Visit<IGeoShapeProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this.Accept(t.Fields);
						});
						break;
					case FieldType.Attachment:
						this.Visit<IAttachmentProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this.Accept(t.Fields);
						});
						break;
					case FieldType.Completion:
						this.Visit<ICompletionProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this.Accept(t.Fields);
						});
						break;
					case FieldType.Murmur3Hash:
						this.Visit<IMurmur3HashProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this.Accept(t.Fields);
						});
						break;
					case FieldType.TokenCount:
						this.Visit<ITokenCountProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this.Accept(t.Fields);
						});
						break;
					case FieldType.None:
						continue;
					case FieldType.Percolator:
						this.Visit<IPercolatorProperty>(field, t =>
						{
							this._visitor.Visit(t);
						});
						break;
					case FieldType.IntegerRange:
						this.Visit<IIntegerRangeProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this.Accept(t.Fields);
						});
						break;
					case FieldType.FloatRange:
						this.Visit<IFloatRangeProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this.Accept(t.Fields);
						});
						break;
					case FieldType.LongRange:
						this.Visit<ILongRangeProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this.Accept(t.Fields);
						});
						break;
					case FieldType.DoubleRange:
						this.Visit<IDoubleRangeProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this.Accept(t.Fields);
						});
						break;
					case FieldType.DateRange:
						this.Visit<IDateRangeProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this.Accept(t.Fields);
						});
						break;
				}
			}
		}
	}
}
