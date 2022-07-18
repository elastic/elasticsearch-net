// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	public class GeoPointAttribute : ElasticsearchDocValuesPropertyAttributeBase, IGeoPointProperty
	{
		public GeoPointAttribute() : base(FieldType.GeoPoint) { }

		/// <inheritdoc cref="IGeoPointProperty.IgnoreMalformed" />
		public bool IgnoreMalformed
		{
			get => Self.IgnoreMalformed.GetValueOrDefault();
			set => Self.IgnoreMalformed = value;
		}

		/// <inheritdoc cref="IGeoPointProperty.IgnoreZValue" />
		public bool IgnoreZValue
		{
			get => Self.IgnoreZValue.GetValueOrDefault(true);
			set => Self.IgnoreZValue = value;
		}

		public IInlineScript Script
		{
			get => Self.Script;
			set => Self.Script = value;
		}

		public OnScriptError OnScriptError
		{
			get => Self.OnScriptError.GetValueOrDefault();
			set => Self.OnScriptError = value;
		}

		bool? IGeoPointProperty.IgnoreMalformed { get; set; }
		bool? IGeoPointProperty.IgnoreZValue { get; set; }
		GeoLocation IGeoPointProperty.NullValue { get; set; }
		IInlineScript IGeoPointProperty.Script { get; set; }
		OnScriptError? IGeoPointProperty.OnScriptError { get; set; }
		private IGeoPointProperty Self => this;
	}
}
