# Juza.NET

**A .NET toolkit that turns Kenyan payment provider responses into safe decisions.**

Most libraries wrap an API. Juza.NET answers what happened to a payment and what to do next, including when the honest answer is “unknown”.

> **Status:** Experimental until 1.0.  
> **License:** MIT  
> **Not affiliated** with or endorsed by any payment provider or bank. Trademarks belong to their owners. Decisions are advisory.

---

## Why this exists

Kenyan payment integrations break in the gap between what a provider says and what actually happened to the money:

- A timeout can still complete on the customer’s phone.
- Callbacks can be lost or duplicated.
- Provider codes are hardcoded in switch statements.
- The ground keeps shifting (masked phone numbers, new rails, changing behaviour).

Juza.NET gives you a decision engine and carefully sourced data so you can act safely.

---

## Packages

| Package | What it does                                   |
| --- |------------------------------------------------|
| **Juza.Core** | Decision engine, status model, rule loader     |
| **Juza.Banks** | Kenyan bank directory + official bank paybills |
| **Juza.Paybills** | Curated paybill directory                      |
| **Juza.Pesalink** | Pesalink helpers / client + rule pack          |
| **Juza.Mpesa** | Daraja helpers / client + rule pack            |

Install only what you need.

```bash
dotnet add package Juza.Core
dotnet add package Juza.Banks
# etc.
```

---

## 60-second example

```csharp
// You already received a provider response.
// Ask the engine what it means and what to do next.

var context = new PaymentContext
{
    Provider = "mpesa-daraja",
    Operation = "stk-push",
    Stage = "dispatch",
    HttpStatus = 504,
    ElapsedSeconds = 30,
    Attempt = 1,
    IdempotencyKey = "order-1042"
};

var decision = engine.Decide(context, rulePack);

// decision.Status        → Unknown
// decision.Final         → false
// decision.Retry         → AfterStatusCheck
// decision.NextAction    → QueryStatus
// decision.Reason        → timeout_after_dispatch
// decision.Confidence    → Inferred
```

---

## How decisions work

The engine returns one of four statuses:

| Status | Meaning                                       |
| --- |-----------------------------------------------|
| **Succeeded** | The provider gave an explicit success signal  |
| **Failed** | The provider confirmed the money did not move |
| **Pending** | Accepted and in progress - wait               |
| **Unknown** | The outcome cannot be determined yet          |

**Unknown is never final.**  
**Failed + final** only comes from a documented or observed rule.  
**Retry is Safe** only when the payment is Failed and final *and* an idempotency key is present.

Provider knowledge lives in versioned JSON rule packs. Every rule carries a source and a confidence level (Documented / Observed / Inferred / Default).

---

## Data packages

### Banks

Typed list of Kenyan banks with codes, SWIFT, Pesalink participation, and official bank paybills.

### Paybills

A carefully curated paybill directory. It starts small, and grows only through reviewed contributions. Prefer incomplete + correct over complete + risky.

---

## Contributing

- Open an issue before larger changes.
- Rule and data changes must include evidence and provenance.
- CI must pass.
- See [CONTRIBUTING.md](CONTRIBUTING.md).

---

## Security

See [SECURITY.md](SECURITY.md) for how to report vulnerabilities privately.

---

## License

MIT. See [LICENSE](LICENSE).

---

## Disclaimer

Juza.NET is not affiliated with, endorsed by, or connected to Safaricom, M-Pesa, Pesalink, IPSL, any bank, or any other payment provider. All trademarks belong to their respective owners.

The decision engine gives advice. The host application remains responsible for how money is handled.
