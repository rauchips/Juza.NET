# ADR 0005: Money-moving requests are never retried automatically

## Status

Accepted

## Context

An automatic retry of a transfer or STK Push can result in the customer being charged twice. Timeouts after dispatch are especially dangerous because the request may have been accepted by the provider.

## Decision

Adapters never retry a money-moving request on their own. A timeout or ambiguous response produces an **Unknown** decision with a status query as the next action. Retry is only advised by the engine when the status is Failed and final and an idempotency key is present.

## Consequences

- Hosts remain in control of when (and whether) a payment is re-attempted.
- Status-query and recovery flows become first-class.
- Idempotency keys are required for safe retries.
