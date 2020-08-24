// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	public class IpAttribute : ElasticsearchDocValuesPropertyAttributeBase, IIpProperty
	{
		public IpAttribute() : base(FieldType.Ip) { }

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

		bool? IIpProperty.Index { get; set; }
		string IIpProperty.NullValue { get; set; }
		private IIpProperty Self => this;
	}
}
