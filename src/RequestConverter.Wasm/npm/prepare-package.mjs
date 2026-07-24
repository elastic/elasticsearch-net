import { cp, mkdir, readFile, rm, writeFile } from "node:fs/promises";
import { dirname, join, resolve } from "node:path";
import { fileURLToPath } from "node:url";
import { parseArgs } from "node:util";

const { values: args } = parseArgs({
  options: {
    "app-bundle": { type: "string" },
    "client-version": { type: "string" },
    commit: { type: "string" },
    out: { type: "string" },
    version: { type: "string" },
  },
});

for (const required of ["app-bundle", "client-version", "commit", "out", "version"]) {
  if (!args[required]) {
    console.error(`Missing required argument: --${required}`);
    process.exit(1);
  }
}

const here = dirname(fileURLToPath(import.meta.url));
const outDir = resolve(args.out);

await rm(outDir, { recursive: true, force: true });
await mkdir(outDir, { recursive: true });
await cp(resolve(args["app-bundle"]), outDir, { recursive: true });

const template = JSON.parse(await readFile(join(here, "package.template.json"), "utf8"));
template.version = args.version;
template.elasticsearch.clientVersion = args["client-version"];
template.elasticsearch.commit = args.commit;
// The AppBundle ships its own minimal package.json ({"type":"module"}); ours replaces it.
await writeFile(join(outDir, "package.json"), JSON.stringify(template, null, 2) + "\n");
await cp(join(here, "README.md"), join(outDir, "README.md"));

console.log(`Package assembled at ${outDir} (version ${args.version})`);
