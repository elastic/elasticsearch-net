import { resolve } from "node:path";
import { pathToFileURL } from "node:url";

const bundleDir = process.argv[2];
if (!bundleDir) {
  console.error("Usage: node smoke.mjs <appBundleOrPackageDir>");
  process.exit(1);
}

const { boot } = await import(pathToFileURL(resolve(bundleDir, "main.mjs")).href);
const { check, convert } = await boot();

const payload = {
  requests: [
    {
      api: "search",
      body: { query: { term: { "user.id": { value: "kimchy" } } } },
      method: "GET",
      params: { index: "my-index-000001" },
      path: "/my-index-000001/_search",
      query: { from: "40", size: "20" },
      raw_path: "/my-index-000001/_search",
      source: "GET /my-index-000001/_search?from=40&size=20",
      url: "/my-index-000001/_search?from=40&size=20",
    },
  ],
  options: {
    document_type_name: "MyDocument",
    syntax_mode: "descriptor",
    use_strongly_typed_document: true,
  },
};

const checked = JSON.parse(check(JSON.stringify(payload)));
if (checked.error || checked.return !== true) {
  console.error("check failed:", JSON.stringify(checked));
  process.exit(1);
}

const converted = JSON.parse(convert(JSON.stringify(payload)));
if (converted.error) {
  console.error("convert failed:", converted.error);
  process.exit(1);
}
if (!converted.return.includes("new SearchRequestDescriptor<MyDocument>()")) {
  console.error("unexpected output:\n" + converted.return);
  process.exit(1);
}
console.log("smoke OK");
