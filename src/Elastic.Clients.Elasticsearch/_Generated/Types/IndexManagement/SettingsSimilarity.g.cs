// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
//
// ------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.IndexManagement
{
	public partial class SettingsSimilarity
	{
		[JsonInclude]
		[JsonPropertyName("bm25")]
		public Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityBm25? Bm25 { get; set; }

		[JsonInclude]
		[JsonPropertyName("dfi")]
		public Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityDfi? Dfi { get; set; }

		[JsonInclude]
		[JsonPropertyName("dfr")]
		public Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityDfr? Dfr { get; set; }

		[JsonInclude]
		[JsonPropertyName("ib")]
		public Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityIb? Ib { get; set; }

		[JsonInclude]
		[JsonPropertyName("lmd")]
		public Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityLmd? Lmd { get; set; }

		[JsonInclude]
		[JsonPropertyName("lmj")]
		public Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityLmj? Lmj { get; set; }

		[JsonInclude]
		[JsonPropertyName("scripted_tfidf")]
		public Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityScriptedTfidf? ScriptedTfidf { get; set; }
	}

	public sealed partial class SettingsSimilarityDescriptor : DescriptorBase<SettingsSimilarityDescriptor>
	{
		internal SettingsSimilarityDescriptor(Action<SettingsSimilarityDescriptor> configure) => configure.Invoke(this);
		public SettingsSimilarityDescriptor() : base()
		{
		}

		private Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityBm25? Bm25Value { get; set; }

		private SettingsSimilarityBm25Descriptor Bm25Descriptor { get; set; }

		private Action<SettingsSimilarityBm25Descriptor> Bm25DescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityDfi? DfiValue { get; set; }

		private SettingsSimilarityDfiDescriptor DfiDescriptor { get; set; }

		private Action<SettingsSimilarityDfiDescriptor> DfiDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityDfr? DfrValue { get; set; }

		private SettingsSimilarityDfrDescriptor DfrDescriptor { get; set; }

		private Action<SettingsSimilarityDfrDescriptor> DfrDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityIb? IbValue { get; set; }

		private SettingsSimilarityIbDescriptor IbDescriptor { get; set; }

		private Action<SettingsSimilarityIbDescriptor> IbDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityLmd? LmdValue { get; set; }

		private SettingsSimilarityLmdDescriptor LmdDescriptor { get; set; }

		private Action<SettingsSimilarityLmdDescriptor> LmdDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityLmj? LmjValue { get; set; }

		private SettingsSimilarityLmjDescriptor LmjDescriptor { get; set; }

		private Action<SettingsSimilarityLmjDescriptor> LmjDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityScriptedTfidf? ScriptedTfidfValue { get; set; }

		private SettingsSimilarityScriptedTfidfDescriptor ScriptedTfidfDescriptor { get; set; }

		private Action<SettingsSimilarityScriptedTfidfDescriptor> ScriptedTfidfDescriptorAction { get; set; }

		public SettingsSimilarityDescriptor Bm25(Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityBm25? bm25)
		{
			Bm25Descriptor = null;
			Bm25DescriptorAction = null;
			Bm25Value = bm25;
			return Self;
		}

		public SettingsSimilarityDescriptor Bm25(SettingsSimilarityBm25Descriptor descriptor)
		{
			Bm25Value = null;
			Bm25DescriptorAction = null;
			Bm25Descriptor = descriptor;
			return Self;
		}

		public SettingsSimilarityDescriptor Bm25(Action<SettingsSimilarityBm25Descriptor> configure)
		{
			Bm25Value = null;
			Bm25DescriptorAction = null;
			Bm25DescriptorAction = configure;
			return Self;
		}

		public SettingsSimilarityDescriptor Dfi(Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityDfi? dfi)
		{
			DfiDescriptor = null;
			DfiDescriptorAction = null;
			DfiValue = dfi;
			return Self;
		}

		public SettingsSimilarityDescriptor Dfi(SettingsSimilarityDfiDescriptor descriptor)
		{
			DfiValue = null;
			DfiDescriptorAction = null;
			DfiDescriptor = descriptor;
			return Self;
		}

		public SettingsSimilarityDescriptor Dfi(Action<SettingsSimilarityDfiDescriptor> configure)
		{
			DfiValue = null;
			DfiDescriptorAction = null;
			DfiDescriptorAction = configure;
			return Self;
		}

		public SettingsSimilarityDescriptor Dfr(Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityDfr? dfr)
		{
			DfrDescriptor = null;
			DfrDescriptorAction = null;
			DfrValue = dfr;
			return Self;
		}

		public SettingsSimilarityDescriptor Dfr(SettingsSimilarityDfrDescriptor descriptor)
		{
			DfrValue = null;
			DfrDescriptorAction = null;
			DfrDescriptor = descriptor;
			return Self;
		}

		public SettingsSimilarityDescriptor Dfr(Action<SettingsSimilarityDfrDescriptor> configure)
		{
			DfrValue = null;
			DfrDescriptorAction = null;
			DfrDescriptorAction = configure;
			return Self;
		}

		public SettingsSimilarityDescriptor Ib(Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityIb? ib)
		{
			IbDescriptor = null;
			IbDescriptorAction = null;
			IbValue = ib;
			return Self;
		}

		public SettingsSimilarityDescriptor Ib(SettingsSimilarityIbDescriptor descriptor)
		{
			IbValue = null;
			IbDescriptorAction = null;
			IbDescriptor = descriptor;
			return Self;
		}

		public SettingsSimilarityDescriptor Ib(Action<SettingsSimilarityIbDescriptor> configure)
		{
			IbValue = null;
			IbDescriptorAction = null;
			IbDescriptorAction = configure;
			return Self;
		}

		public SettingsSimilarityDescriptor Lmd(Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityLmd? lmd)
		{
			LmdDescriptor = null;
			LmdDescriptorAction = null;
			LmdValue = lmd;
			return Self;
		}

		public SettingsSimilarityDescriptor Lmd(SettingsSimilarityLmdDescriptor descriptor)
		{
			LmdValue = null;
			LmdDescriptorAction = null;
			LmdDescriptor = descriptor;
			return Self;
		}

		public SettingsSimilarityDescriptor Lmd(Action<SettingsSimilarityLmdDescriptor> configure)
		{
			LmdValue = null;
			LmdDescriptorAction = null;
			LmdDescriptorAction = configure;
			return Self;
		}

		public SettingsSimilarityDescriptor Lmj(Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityLmj? lmj)
		{
			LmjDescriptor = null;
			LmjDescriptorAction = null;
			LmjValue = lmj;
			return Self;
		}

		public SettingsSimilarityDescriptor Lmj(SettingsSimilarityLmjDescriptor descriptor)
		{
			LmjValue = null;
			LmjDescriptorAction = null;
			LmjDescriptor = descriptor;
			return Self;
		}

		public SettingsSimilarityDescriptor Lmj(Action<SettingsSimilarityLmjDescriptor> configure)
		{
			LmjValue = null;
			LmjDescriptorAction = null;
			LmjDescriptorAction = configure;
			return Self;
		}

		public SettingsSimilarityDescriptor ScriptedTfidf(Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityScriptedTfidf? scriptedTfidf)
		{
			ScriptedTfidfDescriptor = null;
			ScriptedTfidfDescriptorAction = null;
			ScriptedTfidfValue = scriptedTfidf;
			return Self;
		}

		public SettingsSimilarityDescriptor ScriptedTfidf(SettingsSimilarityScriptedTfidfDescriptor descriptor)
		{
			ScriptedTfidfValue = null;
			ScriptedTfidfDescriptorAction = null;
			ScriptedTfidfDescriptor = descriptor;
			return Self;
		}

		public SettingsSimilarityDescriptor ScriptedTfidf(Action<SettingsSimilarityScriptedTfidfDescriptor> configure)
		{
			ScriptedTfidfValue = null;
			ScriptedTfidfDescriptorAction = null;
			ScriptedTfidfDescriptorAction = configure;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (Bm25Descriptor is not null)
			{
				writer.WritePropertyName("bm25");
				JsonSerializer.Serialize(writer, Bm25Descriptor, options);
			}
			else if (Bm25DescriptorAction is not null)
			{
				writer.WritePropertyName("bm25");
				JsonSerializer.Serialize(writer, new SettingsSimilarityBm25Descriptor(Bm25DescriptorAction), options);
			}
			else if (Bm25Value is not null)
			{
				writer.WritePropertyName("bm25");
				JsonSerializer.Serialize(writer, Bm25Value, options);
			}

			if (DfiDescriptor is not null)
			{
				writer.WritePropertyName("dfi");
				JsonSerializer.Serialize(writer, DfiDescriptor, options);
			}
			else if (DfiDescriptorAction is not null)
			{
				writer.WritePropertyName("dfi");
				JsonSerializer.Serialize(writer, new SettingsSimilarityDfiDescriptor(DfiDescriptorAction), options);
			}
			else if (DfiValue is not null)
			{
				writer.WritePropertyName("dfi");
				JsonSerializer.Serialize(writer, DfiValue, options);
			}

			if (DfrDescriptor is not null)
			{
				writer.WritePropertyName("dfr");
				JsonSerializer.Serialize(writer, DfrDescriptor, options);
			}
			else if (DfrDescriptorAction is not null)
			{
				writer.WritePropertyName("dfr");
				JsonSerializer.Serialize(writer, new SettingsSimilarityDfrDescriptor(DfrDescriptorAction), options);
			}
			else if (DfrValue is not null)
			{
				writer.WritePropertyName("dfr");
				JsonSerializer.Serialize(writer, DfrValue, options);
			}

			if (IbDescriptor is not null)
			{
				writer.WritePropertyName("ib");
				JsonSerializer.Serialize(writer, IbDescriptor, options);
			}
			else if (IbDescriptorAction is not null)
			{
				writer.WritePropertyName("ib");
				JsonSerializer.Serialize(writer, new SettingsSimilarityIbDescriptor(IbDescriptorAction), options);
			}
			else if (IbValue is not null)
			{
				writer.WritePropertyName("ib");
				JsonSerializer.Serialize(writer, IbValue, options);
			}

			if (LmdDescriptor is not null)
			{
				writer.WritePropertyName("lmd");
				JsonSerializer.Serialize(writer, LmdDescriptor, options);
			}
			else if (LmdDescriptorAction is not null)
			{
				writer.WritePropertyName("lmd");
				JsonSerializer.Serialize(writer, new SettingsSimilarityLmdDescriptor(LmdDescriptorAction), options);
			}
			else if (LmdValue is not null)
			{
				writer.WritePropertyName("lmd");
				JsonSerializer.Serialize(writer, LmdValue, options);
			}

			if (LmjDescriptor is not null)
			{
				writer.WritePropertyName("lmj");
				JsonSerializer.Serialize(writer, LmjDescriptor, options);
			}
			else if (LmjDescriptorAction is not null)
			{
				writer.WritePropertyName("lmj");
				JsonSerializer.Serialize(writer, new SettingsSimilarityLmjDescriptor(LmjDescriptorAction), options);
			}
			else if (LmjValue is not null)
			{
				writer.WritePropertyName("lmj");
				JsonSerializer.Serialize(writer, LmjValue, options);
			}

			if (ScriptedTfidfDescriptor is not null)
			{
				writer.WritePropertyName("scripted_tfidf");
				JsonSerializer.Serialize(writer, ScriptedTfidfDescriptor, options);
			}
			else if (ScriptedTfidfDescriptorAction is not null)
			{
				writer.WritePropertyName("scripted_tfidf");
				JsonSerializer.Serialize(writer, new SettingsSimilarityScriptedTfidfDescriptor(ScriptedTfidfDescriptorAction), options);
			}
			else if (ScriptedTfidfValue is not null)
			{
				writer.WritePropertyName("scripted_tfidf");
				JsonSerializer.Serialize(writer, ScriptedTfidfValue, options);
			}

			writer.WriteEndObject();
		}
	}
}