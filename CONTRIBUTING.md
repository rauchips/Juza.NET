# Contributing to Juza.NET

Thank you for your interest in contributing.

## Before you start

- For anything beyond a small fix or documentation typo, **open an issue first** so the work is not wasted.
- Rule and data changes have higher bars than ordinary code changes.

## Development setup

1. Install the .NET SDK matching `global.json`.
2. Clone the repository.
3. Run `dotnet restore` and `dotnet test`.
4. Prefer short-lived branches off `main` and Conventional Commits.

## Pull request checklist

- [ ] Tests added or updated
- [ ] Documentation updated where needed
- [ ] Changelog entry (if user-facing)
- [ ] Behaviour change flagged clearly
- [ ] No secrets or personal data in the change
- [ ] For rule changes: source, confidence level, and at least one test case

## Rule and data changes

These are treated with extra care because a wrong decision or a wrong paybill number can cause real harm.

- Every rule must have a source and a confidence level (Documented / Observed / Inferred).
- Changes that make a decision *less* cautious require a second look (even from the same author, after a day).
- CI validates schemas, rejects ambiguous rules, and fails if a rule has no test case.

## Code of conduct

This project follows the Contributor Covenant. See [CODE_OF_CONDUCT.md](CODE_OF_CONDUCT.md).

## License

By contributing you agree that your contributions are licensed under the MIT license. We use a Developer Certificate of Origin (DCO) sign-off rather than a CLA.
