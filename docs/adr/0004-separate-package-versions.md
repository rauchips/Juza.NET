# ADR 0004: Each package is versioned separately

## Status

Accepted

## Context

A bank-list correction or a paybill update should not force consumers of the decision engine or an adapter to take a new major version. Different packages evolve at different rates.

## Decision

Each NuGet package has its own version and release notes. Release tags use a package prefix (`core-v0.1.0`, `banks-v0.1.0`, `paybills-v0.1.0`, etc.).

## Consequences

- Consumers can upgrade data packages independently of the engine.
- Breaking changes are isolated.
- CI and release workflows must select the correct project from the tag.
