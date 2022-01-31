// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;

namespace Nest
{
	public class IpAttribute : ElasticsearchDocValuesPropertyAttributeBase, IIpProperty
	{
		public IpAttribute() : base(FieldType.Ip) { }

		[Obsolete("The server always treated this as a noop and has been removed in 7.10")]
		public double Boost
		{
			get => Self.Boost.GetValueOrDefault();
			set => Self.Boost = value;
		}

		public bool Index
		{
			get => Self.Index.GetValueOrDefault();
			set => Self.Index = value;
		}

		public string NullValue
		{
			get => Self.NullValue;
			set => Self.NullValue = value;
		}

		public bool IgnoreMalformed
		{
			get => Self.IgnoreMalformed.GetValueOrDefault();
			set => Self.IgnoreMalformed = value;
		}

		double? IIpProperty.Boost { get; set; }
		bool? IIpProperty.Index { get; set; }
		string IIpProperty.NullValue { get; set; }
		private IIpProperty Self => this;
		bool? IIpProperty.IgnoreMalformed { get; set; }
		IInlineScript IIpProperty.Script { get; set; }
		OnScriptError? IIpProperty.OnScriptError { get; set; }
	}
}
