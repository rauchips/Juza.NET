# The four statuses

Every decision returns one of four statuses.

| Status | Meaning | Typical next step |
| --- | --- | --- |
| **Succeeded** | The provider gave an explicit success signal | None (or business post-processing) |
| **Failed** | The provider confirmed the money did not move | May be safe to retry if final and an idempotency key is present |
| **Pending** | Accepted and in progress | Wait for callback or poll status |
| **Unknown** | The outcome cannot be determined yet | Query status; never assume success or failure |

## Rules of thumb

- **Unknown is never final.** Keep investigating until you have a terminal signal or escalate to manual review.
- **Failed + final** only comes from a rule with Documented or Observed confidence. An inferred or default rule cannot declare that money did not move.
- **Succeeded** requires an explicit success rule. The absence of an error is not success.
- A signal that contradicts a previously terminal state produces a conflict for manual review; it never silently overwrites.
