---
name: release-runbook
description: Use when drafting, creating, or updating a GitHub release for elastic/elasticsearch-net (any 9.x or 8.19.x patch). Covers branch targeting, backport linking, per-PR sub-bullets, and how to summarize regenerate-client PRs with optional upstream correlation to elastic/elasticsearch-specification and elastic/client-generator-net.
---

# elasticsearch-net Release Runbook

How to draft a GitHub release for `elastic/elasticsearch-net`. Always work from the GitHub auto-generated baseline and enhance it — don't write release notes from scratch.

## Conventions (must follow)

1. **`targetCommitish` = the major.minor branch.** 9.3.x → `9.3`, 9.4.x → `9.4`, 8.19.x → `8.19`. NEVER `main`. *Most important rule.*
2. **Backports reference the original main PR only.** When the GH baseline lists a `[X.Y]` backport, replace it with the original main PR (drop the `[X.Y]` prefix). The backport PR is not linked at all — not the URL, not the number, not the bot author. *Rationale: attribute the work to where it was originally done; the backport is an internal mechanism, not user-facing.*
3. **Issue references.** `Fixes <full-issue-url>` for main bullets, `(#issue)` shorthand for sub-bullets.
4. **Regenerate PR uses the `[X.Y]` branch-prefixed PR**, never the main one. Summarize with up to 5 grounded sub-bullets.
5. **Multiple regenerate PRs in one release.** Combine into one line item; link both PRs.
6. **Always create the release as a draft.** Use `gh release create ... --draft`. Never publish — the user reviews and publishes manually. **Default to `--draft` even when the user's request doesn't explicitly say "draft"** — a request to "create release X.Y.Z" is not authorization to publish. *Rationale: publishing has user-visible consequences (notifications, package release pipelines, public visibility) the user wants to control manually after reviewing the generated notes.*
7. **Every non-regen PR gets up to 2 sub-bullets** describing what changed/fixed/improved. Skip when the title is already fully self-describing.
8. **Best-effort upstream correlation** for the regen PR — try to match diffs to PRs in `elastic/elasticsearch-specification` (spec changes) and `elastic/client-generator-net` (generator changes). Optional grounding; don't fabricate matches.

## Quick reference: version → target branch

| Version line | Target branch |
|--------------|---------------|
| 9.4.x        | `9.4`         |
| 9.3.x        | `9.3`         |
| 8.19.x       | `8.19`        |

Read the version from the user's request, pick the branch deterministically. If the version line is unfamiliar, ask before guessing.

## Workflow

Six phases: **gather → baseline → enhance non-regen → analyze regen → review → draft.**

### 1. Gather PRs and closed issues since the previous tag

Find the previous tag on the same major.minor:

```
gh release list -R elastic/elasticsearch-net --limit 10
```

PRs merged into the branch since the previous tag's date:

```
gh pr list -R elastic/elasticsearch-net --state merged --base <branch> \
  --search "merged:>=<prev-tag-date>" \
  --json number,title,author,baseRefName,body,url,closingIssuesReferences
```

Recently closed issues — used in step 4 to ground regen sub-bullets:

```
gh issue list -R elastic/elasticsearch-net --state closed \
  --search "closed:>=<prev-tag-date>" \
  --json number,title,closedAt,url --limit 50
```

### 2. Generate the GH baseline

```
gh api repos/elastic/elasticsearch-net/releases/generate-notes \
  -F tag_name=X.Y.Z -F target_commitish=X.Y -F previous_tag_name=X.Y.W \
  -q .body
```

This produces the canonical skeleton: `## What's Changed`, then bullets, optional `## New Contributors`, then `**Full Changelog**: .../compare/X.Y.W...X.Y.Z`. Use it as-is — do **not** add `### Bug Fixes` / `### Features` category headers (no prior release uses them).

### 3. Enhance each non-regenerate line

For every non-regen PR in the baseline:

**Detect backports.** Signals: title starts with `[X.Y]`, body mentions "backport"/"cherry-pick"/an original PR. Find the original:

```
gh pr view <num> -R elastic/elasticsearch-net --json body,commits
gh pr list -R elastic/elasticsearch-net --state merged --base main \
  --search "<title-keywords-without-[X.Y]-prefix>"
```

If a backport, replace the line with the **original main PR's** title (no `[X.Y]` prefix), author, and URL. The backport PR is not mentioned.

**Link related issues.** From `gh pr view <num> --json closingIssuesReferences,body`, render as `Fixes https://github.com/elastic/elasticsearch-net/issues/<n>` inline.

**Add ≤2 sub-bullets** describing what changed/fixed/improved. Sources in priority order:

1. PR body — if it explains the problem/fix in plain language, distill it.
2. Closing issue title/body — the user-visible symptom or expected behavior.
3. The diff — for small targeted PRs, the changed code paths often imply the bullet (e.g. *"now supports X in Y"*, *"no longer throws Z when W"*).

**Skip sub-bullets entirely** when the title is already fully self-describing — simple version bumps, trivial renames, dependency updates. Don't pad.

Format:

```
* <PR title> by @<author> in <PR url>
  * <bullet 1>
  * <bullet 2>
```

### 4. Analyze the `[X.Y] Regenerate client` PR(s)

This is the heart of the runbook. The PR body is always "As titled.", so all signal comes from the diff.

**Pick the branch-prefixed PR**, e.g. `[9.3] Regenerate client`. NEVER the main `Regenerate client` PR.

```
gh pr view <num> -R elastic/elasticsearch-net --json files,additions,deletions
gh pr diff <num> -R elastic/elasticsearch-net
```

**Categorize changes by file-path signal:**

| Signal | Category |
|--------|----------|
| New `*Request.g.cs` + `*Response.g.cs` triplet under `_Generated/Api/<area>/` | New endpoint (likely spec change) |
| Deleted `*Response.g.cs` under `_Generated/Api/` | Removed/consolidated response (potentially breaking; spec change) |
| New `*.g.cs` under `_Generated/Types/QueryDsl/` or `Aggregations/` | New query/aggregation type (spec change) |
| New small enum/type file under `_Generated/Types/` (e.g. `*Flag.g.cs`) | New enum value or type (spec change) |
| Added `DefaultRequestConfiguration { Accept = ... }` block | Content-type fix (spec change — often resolves a deserialization bug) |
| `*.csproj` version change (e.g. `Elastic.Transport`) | Dependency bump |
| Same code-shape change applied uniformly across many existing files (converter pattern, attribute, init block) | Generator change — likely from `elastic/client-generator-net` |
| Small +/- on existing `*Request.g.cs` (XML doc only) | Skip — not user-facing |

**Cross-reference with the closed-issue list from step 1.** Match symptoms to categorized changes (e.g. issue `"FieldType missing Wildcard"` → enum addition; issue `"SearchMvtAsync TransportException"` → content-type fix). When matched, use the issue title for the bullet text and link `(#issue)`.

**Correlate to upstream repos (best effort).** Two upstream sources can drive client diffs:

Spec changes (new endpoints, types, enums, fields) → query `elastic/elasticsearch-specification`:

```
gh pr list -R elastic/elasticsearch-specification --state merged \
  --search "merged:>=<prev-tag-date>" --json number,title,url,body --limit 50
```

Match by path/area (client `_Generated/Api/Reindex/Cancel*.g.cs` ↔ spec PR touching `specification/_global/reindex/CancelReindex*`) or by keyword (spec PR title `"Add Wildcard to FieldType"` ↔ client adds `Wildcard` enum value).

Generator changes (uniform code-shape changes across many files) → query `elastic/client-generator-net`:

```
gh pr list -R elastic/client-generator-net --state merged \
  --search "merged:>=<prev-tag-date>" --json number,title,url,body --limit 50
```

When a match is found, use the upstream PR's title/body to write a more accurate bullet — the spec/generator PR usually states the user-facing intent more clearly than the client diff. Linking the upstream PR in the release notes is **optional** (prior releases haven't); default to enriched bullet text rather than added URLs unless the upstream PR is materially significant (major area, breaking change).

If correlation isn't obvious within a few minutes, fall back to client-side analysis. **Don't fabricate matches.**

**Pick the top ≤5 bullets**, prioritized:

1. Bullets grounded in BOTH a closed issue AND an upstream PR (highest signal)
2. Issue-grounded fixes
3. New endpoints (spec-grounded if possible)
4. Removed/consolidated response types (call out as breaking when applicable)
5. New query/aggregation/enum types
6. Generator improvements with user-visible effects
7. Dependency bumps (only if user-visible behavior changes)

Cap at 5 even if more candidates qualify.

Format:

```
* Regenerate client by @<author> in https://github.com/elastic/elasticsearch-net/pull/<num>
  * Fixes <symptom> (#<issue>)
  * <other category bullet>
```

### 5. Combining multiple regenerate PRs

If two or more `[X.Y] Regenerate client` PRs fall in the release window, diff each, categorize each, combine + dedupe, then pick top ≤5 across the union. Link **both** PRs on one line:

```
* Regenerate client by @<author> in <PR1-url> and <PR2-url>
  * <combined-bullet-1>
  * ...
```

### 6. Keep `## New Contributors` verbatim

GitHub auto-generates this section from the contributor graph. Don't invent it, don't remove it. If GH omitted it, there are no new contributors this release — that's correct.

### 7. Pre-draft checklist

- [ ] `targetCommitish` = major.minor branch (NOT main)
- [ ] Tag matches the version, no `v` prefix (e.g. `9.3.6`, not `v9.3.6`)
- [ ] Every backport line references the ORIGINAL main PR only (the `[X.Y]` backport PR is NOT linked)
- [ ] Every PR with a related issue references it (`Fixes <url>` or `(#X)`)
- [ ] Every non-regen PR has ≤2 sub-bullets (or none if title is fully self-describing)
- [ ] Regenerate line has ≤5 grounded sub-bullets
- [ ] Regen bullets attempted upstream correlation (spec and/or generator) where signals suggest it
- [ ] Multiple regen PRs combined into one line, both linked
- [ ] `## New Contributors` kept exactly as GH generated it (or omitted if absent)
- [ ] `**Full Changelog**: .../compare/<prev>...<current>` line present
- [ ] No category headers added
- [ ] `gh release create` invocation includes `--draft`

### 8. Create the draft

**ALWAYS use `--draft`. Never publish a release.** Hand the resulting draft URL to the user; the user reviews and publishes manually.

```
gh release create X.Y.Z \
  --target X.Y \
  --title X.Y.Z \
  --draft \
  --notes-file release-notes.md
```

Never include AI/Claude attribution anywhere in release notes.

## Common mistakes

| Mistake | Why wrong |
|---------|-----------|
| Targeting `main` | Releases must cut from `9.3` (or matching minor) |
| Using the `Regenerate client` PR from main | Use the `[X.Y]` branch-prefixed one |
| Adding `### Bug Fixes` category headers | No prior release uses them |
| Tagging as `v9.3.6` | Tags have no `v` prefix |
| Linking the `[X.Y]` backport PR (or both) | Reference ONLY the original main PR — the backport is not linked |
| >5 regen bullets / unsubstantiated bullets | Cap at 5; ground each in a diff signal, closed issue, or upstream PR |
| >2 sub-bullets on a non-regen PR | Cap is 2 |
| Padding sub-bullets when the title already describes the PR fully | Skip them entirely in that case |
| Fabricating an upstream PR correlation | Only correlate when the match is clear; otherwise client-diff analysis |
| Publishing the release directly (omitting `--draft`) | Always create as draft; the user publishes manually |

## Canonical example — 9.3.5 (verbatim)

Use this as the structural target. Note that prior releases (including 9.3.5) do **not** yet have the new ≤2-bullet sub-bullets on non-regen PRs — that rule applies on top of this structure going forward.

```
## What's Changed

* Hand-craft IndexSettingsTimeSeriesConverter to handle out-of-range dates by @flobernd in https://github.com/elastic/elasticsearch-net/pull/8873
* Fix DefaultMappingFor not consuming `IdPropertyName` and `DisableIdInference` by @flobernd in https://github.com/elastic/elasticsearch-net/pull/8877
* Regenerate client by @flobernd in https://github.com/elastic/elasticsearch-net/pull/8883
  * Fixes `FieldType` enum doesnt have `Wildcard` value" (#8826)
  * Fixes `SearchMvtAsync` throws `TransportException` on successful `HTTP 200 _mvt` responses with valid binary tile payload (#8867)
* Align LINQ-to-ES|QL integration with `Elastic.Esql` 0.11.0 by @flobernd in https://github.com/elastic/elasticsearch-net/pull/8886

**Full Changelog**: https://github.com/elastic/elasticsearch-net/compare/9.3.4...9.3.5
```
