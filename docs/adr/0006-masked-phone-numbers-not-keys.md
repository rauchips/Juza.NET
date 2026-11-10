# ADR 0006: Masked phone numbers are never used as keys

## Status

Accepted

## Context

Since March 2026, M-Pesa callbacks carry masked phone numbers. Using a phone number as the primary matching key for a payment is no longer reliable and will break matching for many flows.

## Decision

Adapters and sample code never use a phone number (or MSISDN) as the primary key for matching a callback or status result to an attempt. Matching is performed on:

- Checkout request ID / merchant request ID
- M-Pesa receipt or transaction ID
- The caller’s own reference / idempotency key

A separate type represents masked numbers so they cannot be accidentally treated as real MSISDNs.

## Consequences

- Matching logic is robust to masking roll-outs.
- Callers must store the identifiers returned at dispatch time.
- Documentation and quick-starts emphasise reference-based matching.
