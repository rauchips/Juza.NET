# ADR 0003: Provider knowledge lives in JSON data, not code

## Status

Accepted

## Context

Hard-coded switch statements over provider result codes require a code change and release whenever a new or undocumented code appears. Knowledge about provider behaviour also needs an audit trail of where it came from.

## Decision

Provider behaviour is expressed as versioned JSON rule packs. Each rule carries:

- Match conditions
- A decision
- A confidence level (Documented / Observed / Inferred / Default)
- A source (with retrieval date where applicable)

The engine loads and evaluates these packs. New codes or corrected interpretations are data changes, not code releases.

## Consequences

- Rule packs can be read by any language.
- Behaviour changes are explicit and reviewable.
- Confidence and provenance become first-class.
- The M-Pesa rule pack can be used with third-party Daraja clients without depending on Juza.Mpesa.
