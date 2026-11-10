# ADR 0002: Unknown is the default, never Failed

## Status

Accepted

## Context

Provider responses are often incomplete, contradictory or missing (timeouts, lost callbacks, unmapped codes). Treating an unrecognised signal as failure is a common source of double charges and money left in limbo.

## Decision

Anything the rules do not explicitly cover becomes **Unknown**, with a status query as the recommended next action. Unknown is never final.

Only an explicit rule with Documented or Observed confidence may declare that money did not move (Failed + final).

## Consequences

- The engine fails safe.
- Callers must implement recovery paths (status queries, manual review).
- Rule authors cannot accidentally mark an ambiguous outcome as terminal failure with low-confidence reasoning.
- Host applications must treat Unknown as “do not assume the money moved or did not move”.
