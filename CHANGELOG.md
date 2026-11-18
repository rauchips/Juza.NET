# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

Each package is versioned independently. Release tags use a package prefix
(for example `core-v0.1.0`, `banks-v0.1.0`).

## [Unreleased]

### Added
- `Juza.Banks`: bank directory (44 institutions, no paybills) with lookup by code and id, search, and JSON schema. Pesalink participation is `unknown` and six entries are `unverified` until checked against CBK.

### Notes
- Pre-1.0 versions may introduce breaking changes between minor releases.
  Behaviour changes that affect payment decisions will always be called out explicitly.
