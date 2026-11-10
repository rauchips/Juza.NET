# Quick-start: Interpret a response you already have

Use `Juza.Core` only. No adapter required.

## 1. Install

```bash
dotnet add package Juza.Core
```

## 2. Load a rule pack

Rule packs ship as JSON (embedded in adapter packages and also available in the repository `data/` folder). Load the pack for the provider and operation you are handling.

## 3. Build a context

Supply what you know about the attempt:

- Provider and operation
- Stage (dispatch, callback, or status-check)
- HTTP status and/or provider result code
- Selected response fields
- Elapsed time, attempt number, whether a callback has already arrived
- Your idempotency key

## 4. Ask for a decision

```csharp
var decision = engine.Decide(context, rulePack);
```

Act on `decision.Status`, `decision.NextAction`, `decision.Retry` and `decision.Reason`.

## 5. Safety defaults

- If the status is Unknown, schedule a status query; do not mark the payment failed or paid.
- Only retry when the engine says Retry = Safe and you have an idempotency key.
- Log the rule id, pack version and confidence for audit.

See the main [README](../../README.md) and the project plan for the full model.
