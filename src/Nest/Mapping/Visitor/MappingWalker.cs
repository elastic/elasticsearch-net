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
			if (response?.Indices == null) return;
			foreach (var indexMapping in response.Indices)
			{
				if (indexMapping.Value?.Mappings == null) continue;
				foreach (var typeMapping in indexMapping.Value.Mappings)
					this.Accept(typeMapping.Value);
			}
		}

		public void Accept(ITypeMapping mapping)
		{
			if (mapping == null) return;
			this._visitor.Visit(mapping);
			this.Accept(mapping.Properties);
		}


		private static void Visit<TProperty>(IProperty prop, Action<TProperty> act)
			where TProperty : class, IProperty
		{
			if (!(prop is TProperty t)) return;
			act(t);
		}

		public void Accept(IProperties properties)
		{
			if (properties == null) return;
			foreach (var kv in properties)
			{
				var field = kv.Value;
				var type = field.Type;
				var ft = type.ToEnum<FieldType>();
				switch (ft)
				{
					case FieldType.Text:
						Visit<ITextProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this.Accept(t.Fields);
						});
						break;
					case FieldType.Keyword:
						Visit<IKeywordProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this.Accept(t.Fields);
						});
						break;
					case FieldType.HalfFloat:
					case FieldType.ScaledFloat:
					case FieldType.Float:
					case FieldType.Double:
					case FieldType.Byte:
					case FieldType.Short:
					case FieldType.Integer:
					case FieldType.Long:
						Visit<INumberProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this.Accept(t.Fields);
						});
						break;
					case FieldType.Date:
						Visit<IDateProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this.Accept(t.Fields);
						});
						break;
					case FieldType.Boolean:
						Visit<IBooleanProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this.Accept(t.Fields);
						});
						break;
					case FieldType.Binary:
						Visit<IBinaryProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this.Accept(t.Fields);
						});
						break;
					case FieldType.Object:
						Visit<IObjectProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this._visitor.Depth += 1;
							this.Accept(t.Properties);
							this._visitor.Depth -= 1;
						});
						break;
					case FieldType.Nested:
						Visit<INestedProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this._visitor.Depth += 1;
							this.Accept(t.Properties);
							this._visitor.Depth -= 1;
						});
						break;
					case FieldType.Ip:
						Visit<IIpProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this.Accept(t.Fields);
						});
						break;
					case FieldType.GeoPoint:
						Visit<IGeoPointProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this.Accept(t.Fields);
						});
						break;
					case FieldType.GeoShape:
						Visit<IGeoShapeProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this.Accept(t.Fields);
						});
						break;
					case FieldType.Completion:
						Visit<ICompletionProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this.Accept(t.Fields);
						});
						break;
					case FieldType.Murmur3Hash:
						Visit<IMurmur3HashProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this.Accept(t.Fields);
						});
						break;
					case FieldType.TokenCount:
						Visit<ITokenCountProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this.Accept(t.Fields);
						});
						break;
					case FieldType.None:
						continue;
					case FieldType.Percolator:
						Visit<IPercolatorProperty>(field, t =>
						{
							this._visitor.Visit(t);
						});
						break;
					case FieldType.IntegerRange:
						Visit<IIntegerRangeProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this.Accept(t.Fields);
						});
						break;
					case FieldType.FloatRange:
						Visit<IFloatRangeProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this.Accept(t.Fields);
						});
						break;
					case FieldType.LongRange:
						Visit<ILongRangeProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this.Accept(t.Fields);
						});
						break;
					case FieldType.DoubleRange:
						Visit<IDoubleRangeProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this.Accept(t.Fields);
						});
						break;
					case FieldType.DateRange:
						Visit<IDateRangeProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this.Accept(t.Fields);
						});
						break;
					case FieldType.IpRange:
						Visit<IIpRangeProperty>(field, t =>
						{
							this._visitor.Visit(t);
							this.Accept(t.Fields);
						});
						break;
					case FieldType.Join:
						Visit<IJoinProperty>(field, t =>
						{
							this._visitor.Visit(t);
						});
						break;
				}
			}
		}
	}
}
