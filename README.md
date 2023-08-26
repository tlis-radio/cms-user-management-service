# CMS User Management

This project is meant to be a web api for managing users in a TLIS CMS.

<p align="center">
  <img height=200 src="https://github.com/tlis-radio/cms-user-managemet/assets/27611887/c06e5c54-ec60-4fea-8023-34a18d631b23"></img>
</p>

## Architecture

```mermaid
flowchart TD
    A(CMS Api Gateway) <-->|Http| U(CMS User Management Service)
    U <-->|Read/Write| D[(Postgres)]
    U <-->|Https| Auth[Auth0 Management API]
```

## Setup project

### Required developer secrets

```bash
dotnet user-secrets set "Auth0:Domain" "<value>"
```

```bash
dotnet user-secrets set "Auth0:ClientId" "<value>"
```

```bash
dotnet user-secrets set "Auth0:ClientSecret" "<value>"
```

```bash
dotnet user-secrets set "Jwt:Authority" "<value>"
```
