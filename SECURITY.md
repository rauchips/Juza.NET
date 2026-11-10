# Security Policy

## Supported versions

Security fixes are applied to the latest minor release and, for critical issues, the previous minor release.

## Reporting a vulnerability

Please **do not** open a public GitHub issue for security problems.

Use GitHub’s private vulnerability reporting feature on this repository, or contact the maintainer directly.

You should receive an acknowledgement within three working days. We will keep you informed of progress and publish a GitHub security advisory when a fix is available.

## What to include

- A clear description of the issue
- Steps to reproduce (if applicable)
- Affected package and version
- Any potential impact you have identified

## Scope

In scope:

- The decision engine and rule evaluation
- Callback parsing and validation logic
- Secrets handling and logging redaction
- Package publishing and supply-chain aspects under our control

Out of scope:

- Issues in upstream providers (Safaricom, Pesalink, banks, etc.)
- Misconfiguration by host applications
- Denial-of-service against public endpoints you yourself expose

Thank you for helping keep Juza.NET safe.
