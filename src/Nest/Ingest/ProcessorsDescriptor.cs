// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;

namespace Nest
{
	public class ProcessorsDescriptor : DescriptorPromiseBase<ProcessorsDescriptor, IList<IProcessor>>
	{
		public ProcessorsDescriptor() : base(new List<IProcessor>()) { }

		/// <summary>
		/// The ingest attachment plugin lets Elasticsearch extract file attachments in common formats
		/// (such as PPT, XLS, and PDF) by using the Apache text extraction library Tika.
		/// You can use the ingest attachment plugin as a replacement for the mapper attachment plugin.
		/// </summary>
		/// <remarks>
		/// Requires the Ingest Attachment Processor Plugin to be installed on the cluster.
		/// </remarks>
		public ProcessorsDescriptor Attachment<T>(Func<AttachmentProcessorDescriptor<T>, IAttachmentProcessor> selector) where T : class =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new AttachmentProcessorDescriptor<T>())));

		/// <inheritdoc cref="IAppendProcessor"/>
		public ProcessorsDescriptor Append<T>(Func<AppendProcessorDescriptor<T>, IAppendProcessor> selector) where T : class =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new AppendProcessorDescriptor<T>())));

		/// <inheritdoc cref="ICsvProcessor"/>
		public ProcessorsDescriptor Csv<T>(Func<CsvProcessorDescriptor<T>, ICsvProcessor> selector) where T : class =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new CsvProcessorDescriptor<T>())));


		/// <inheritdoc cref="IConvertProcessor"/>
		public ProcessorsDescriptor Convert<T>(Func<ConvertProcessorDescriptor<T>, IConvertProcessor> selector) where T : class =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new ConvertProcessorDescriptor<T>())));

		/// <inheritdoc cref="IDateProcessor"/>
		public ProcessorsDescriptor Date<T>(Func<DateProcessorDescriptor<T>, IDateProcessor> selector) where T : class =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new DateProcessorDescriptor<T>())));

		/// <summary>
		/// Point documents to the right time-based index based on a date or timestamp field in a document
		/// by using the date math index name support.
		/// </summary>
		public ProcessorsDescriptor DateIndexName<T>(Func<DateIndexNameProcessorDescriptor<T>, IDateIndexNameProcessor> selector) where T : class =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new DateIndexNameProcessorDescriptor<T>())));

		/// <summary>
		/// Expands a field with dots into an object field.
		/// This processor allows fields with dots in the name to be accessible by other processors in the pipeline.
		/// Otherwise these fields can’t be accessed by any processor.
		/// </summary>
		public ProcessorsDescriptor DotExpander<T>(Func<DotExpanderProcessorDescriptor<T>, IDotExpanderProcessor> selector) where T : class =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new DotExpanderProcessorDescriptor<T>())));

		/// <inheritdoc cref="IEnrichProcessor"/>
		public ProcessorsDescriptor Enrich<T>(Func<EnrichProcessorDescriptor<T>, IEnrichProcessor> selector) where T : class =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new EnrichProcessorDescriptor<T>())));

		/// <inheritdoc cref="IFailProcessor"/>
		public ProcessorsDescriptor Fail(Func<FailProcessorDescriptor, IFailProcessor> selector) =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new FailProcessorDescriptor())));

		public ProcessorsDescriptor Foreach<T>(Func<ForeachProcessorDescriptor<T>, IForeachProcessor> selector) where T : class =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new ForeachProcessorDescriptor<T>())));

		/// <summary>
		/// Adds information about the geographical location of IP addresses,
		/// based on data from the Maxmind databases.
		/// This processor adds this information by default under the geoip field.
		/// The geoip processor can resolve both IPv4 and IPv6 addresses.
		/// </summary>
		/// <remarks>
		/// Requires the Ingest Geoip Processor Plugin to be installed on the cluster.
		/// </remarks>
		public ProcessorsDescriptor GeoIp<T>(Func<GeoIpProcessorDescriptor<T>, IGeoIpProcessor> selector) where T : class =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new GeoIpProcessorDescriptor<T>())));

		public ProcessorsDescriptor Grok<T>(Func<GrokProcessorDescriptor<T>, IGrokProcessor> selector) where T : class =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new GrokProcessorDescriptor<T>())));

		public ProcessorsDescriptor Gsub<T>(Func<GsubProcessorDescriptor<T>, IGsubProcessor> selector) where T : class =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new GsubProcessorDescriptor<T>())));

		/// <inheritdoc cref="IJoinProcessor"/>
		public ProcessorsDescriptor Join<T>(Func<JoinProcessorDescriptor<T>, IJoinProcessor> selector) where T : class =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new JoinProcessorDescriptor<T>())));

		/// <inheritdoc cref="ILowercaseProcessor"/>
		public ProcessorsDescriptor Lowercase<T>(Func<LowercaseProcessorDescriptor<T>, ILowercaseProcessor> selector) where T : class =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new LowercaseProcessorDescriptor<T>())));

		/// <inheritdoc cref="IRemoveProcessor"/>
		public ProcessorsDescriptor Remove<T>(Func<RemoveProcessorDescriptor<T>, IRemoveProcessor> selector) where T : class =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new RemoveProcessorDescriptor<T>())));

		/// <inheritdoc cref="IRenameProcessor"/>
		public ProcessorsDescriptor Rename<T>(Func<RenameProcessorDescriptor<T>, IRenameProcessor> selector) where T : class =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new RenameProcessorDescriptor<T>())));

		/// <summary>
		/// Allows inline, stored, and file scripts to be executed within ingest pipelines.
		/// </summary>
		public ProcessorsDescriptor Script(Func<ScriptProcessorDescriptor, IScriptProcessor> selector) =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new ScriptProcessorDescriptor())));

		public ProcessorsDescriptor Set<T>(Func<SetProcessorDescriptor<T>, ISetProcessor> selector) where T : class =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new SetProcessorDescriptor<T>())));

		/// <summary>
		/// The Sort processor sorts the elements of an array ascending or descending. Homogeneous arrays of numbers
		/// will be sorted numerically, while arrays of strings or heterogeneous arrays
		///  of strings and numbers will be sorted lexicographically.
		/// </summary>
		public ProcessorsDescriptor Sort<T>(Func<SortProcessorDescriptor<T>, ISortProcessor> selector) where T : class =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new SortProcessorDescriptor<T>())));

		/// <inheritdoc cref="ISplitProcessor"/>
		public ProcessorsDescriptor Split<T>(Func<SplitProcessorDescriptor<T>, ISplitProcessor> selector) where T : class =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new SplitProcessorDescriptor<T>())));

		/// <inheritdoc cref="ITrimProcessor"/>
		public ProcessorsDescriptor Trim<T>(Func<TrimProcessorDescriptor<T>, ITrimProcessor> selector) where T : class =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new TrimProcessorDescriptor<T>())));

		/// <inheritdoc cref="IUppercaseProcessor"/>
		public ProcessorsDescriptor Uppercase<T>(Func<UppercaseProcessorDescriptor<T>, IUppercaseProcessor> selector) where T : class =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new UppercaseProcessorDescriptor<T>())));

		/// <summary>
		/// Converts a JSON string into a structured JSON object.
		/// </summary>
		public ProcessorsDescriptor Json<T>(Func<JsonProcessorDescriptor<T>, IJsonProcessor> selector) where T : class =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new JsonProcessorDescriptor<T>())));

		/// <summary>
		/// The user_agent processor extracts details from the user agent string a browser sends with its web requests.
		/// This processor adds this information by default under the user_agent field.
		/// The ingest-user-agent plugin ships by default with the regexes.yaml made available by
		/// uap-java with an Apache 2.0 license.
		/// </summary>
		/// <remarks>
		/// Requires the UserAgent Processor Plugin to be installed on the cluster.
		/// </remarks>
		public ProcessorsDescriptor UserAgent<T>(Func<UserAgentProcessorDescriptor<T>, IUserAgentProcessor> selector) where T : class =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new UserAgentProcessorDescriptor<T>())));

		/// <inheritdoc cref="IKeyValueProcessor" />
		public ProcessorsDescriptor Kv<T>(Func<KeyValueProcessorDescriptor<T>, IKeyValueProcessor> selector) where T : class =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new KeyValueProcessorDescriptor<T>())));

		/// <inheritdoc cref="IUrlDecodeProcessor"/>
		public ProcessorsDescriptor UrlDecode<T>(Func<UrlDecodeProcessorDescriptor<T>, IUrlDecodeProcessor> selector) where T : class =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new UrlDecodeProcessorDescriptor<T>())));

		/// <inheritdoc cref="IBytesProcessor" />
		public ProcessorsDescriptor Bytes<T>(Func<BytesProcessorDescriptor<T>, IBytesProcessor> selector) where T : class =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new BytesProcessorDescriptor<T>())));

		/// <inheritdoc cref="IDissectProcessor" />
		public ProcessorsDescriptor Dissect<T>(Func<DissectProcessorDescriptor<T>, IDissectProcessor> selector) where T : class =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new DissectProcessorDescriptor<T>())));

		/// <inheritdoc cref="IDropProcessor" />
		public ProcessorsDescriptor Drop(Func<DropProcessorDescriptor, IDropProcessor> selector) =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new DropProcessorDescriptor())));

		/// <inheritdoc cref="ISetSecurityUserProcessor" />
		public ProcessorsDescriptor SetSecurityUser<T>(Func<SetSecurityUserProcessorDescriptor<T>, ISetSecurityUserProcessor> selector) where T : class =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new SetSecurityUserProcessorDescriptor<T>())));

		/// <inheritdoc cref="IPipelineProcessor" />
		public ProcessorsDescriptor Pipeline(Func<PipelineProcessorDescriptor, IPipelineProcessor> selector) =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new PipelineProcessorDescriptor())));

		/// <inheritdoc cref="IPipelineProcessor" />
		public ProcessorsDescriptor Circle<T>(Func<CircleProcessorDescriptor<T>, ICircleProcessor> selector) where T : class  =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new CircleProcessorDescriptor<T>())));

		/// <inheritdoc cref="IUriPartsProcessor"/>
		public ProcessorsDescriptor UriParts<T>(Func<UriPartsProcessorDescriptor<T>, IUriPartsProcessor> selector) where T : class =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new UriPartsProcessorDescriptor<T>())));

		/// <inheritdoc cref="IFingerprintProcessor"/>
		public ProcessorsDescriptor Fingerprint<T>(Func<FingerprintProcessorDescriptor<T>, IFingerprintProcessor> selector) where T : class =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new FingerprintProcessorDescriptor<T>())));

		/// <inheritdoc cref="INetworkCommunityIdProcessor"/>
		public ProcessorsDescriptor NetworkCommunityId<T>(Func<NetworkCommunityIdProcessorDescriptor<T>, INetworkCommunityIdProcessor> selector) where T : class =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new NetworkCommunityIdProcessorDescriptor<T>())));

		/// <inheritdoc cref="INetworkDirectionProcessor"/>
		public ProcessorsDescriptor NetworkDirection<T>(Func<NetworkDirectionProcessorDescriptor<T>, INetworkDirectionProcessor> selector) where T : class =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new NetworkDirectionProcessorDescriptor<T>())));
	}
}
