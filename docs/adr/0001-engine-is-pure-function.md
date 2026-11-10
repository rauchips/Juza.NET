# ADR 0001: The decision engine is a pure function

## Status

Accepted

## Context

Payment outcome interpretation must be testable, reproducible and free of hidden side effects. Callers need to be able to feed the same inputs and always receive the same decision.

## Decision

The decision engine is a pure function. It accepts a context (what is known about one payment attempt) and a rule pack, and returns a decision. It does not:

- Call providers
- Read the system clock
- Persist state
- Perform I/O

All time-related or stateful behaviour (escalation ladders, status-check counts, elapsed time) is supplied by the caller in the context.

## Consequences

- Every decision can be unit-tested with fixed inputs.
- Property tests can freely generate arbitrary contexts.
- Hosts remain responsible for scheduling status queries and storing attempt state.
- The engine is trivial to embed and has a minimal attack surface.
