# Module 14: Security & Compliance

> **Goal:** Build secure .NET applications by understanding common vulnerabilities, implementing robust authentication/authorization, and complying with industry standards.

---

## Learning Outcomes

| # | Outcome | Evidence |
|---|---------|----------|
| 1 | Identify and mitigate OWASP Top 10 vulnerabilities in .NET apps | Pen-test report with remediation notes |
| 2 | Implement secure authentication/authorization (Identity, OAuth2/OIDC) | Auth flow diagrams + working demo |
| 3 | Protect data at rest and in transit using .NET cryptography APIs | Encryption/decryption utilities with tests |
| 4 | Apply secure coding practices (input validation, logging, error handling) | Secure coding checklist |
| 5 | Align with compliance requirements (GDPR, PCI DSS, SOC2) | Compliance mapping document |

---

## Threat Landscape

- **OWASP Top 10**: Injection, Broken Auth, Sensitive Data Exposure, XML External Entities, Broken Access Control, Security Misconfig, XSS, Deserialization, Components with Vulnerabilities, Logging/Monitoring.
- **Secure SDLC**: integrate threat modeling, code scanning, security testing throughout development.
- **Zero Trust Principles**: verify explicitly, least privilege, assume breach.

---

## Detailed Notes

### 1. OWASP Top 10 & Threat Modeling

- **Injection:** Use parameterized queries, ORM safeguards, and stored procedures. Validate and sanitize all inputs.
- **Broken Authentication:** Enforce MFA, secure password policies, lockout on suspicious attempts, store passwords with strong hashing (PBKDF2/bcrypt).
- **Sensitive Data Exposure:** Encrypt data in transit with TLS 1.2+, at rest with transparent data encryption, limit data exposure in logs.
- **Threat Modeling:** Apply STRIDE (Spoofing, Tampering, Repudiation, Information disclosure, Denial of service, Elevation). Use Microsoft Threat Modeling Tool or draw.io diagrams.
- **Secure SDLC:** Integrate static analysis (Sonar, Snyk), dependency scanning, and manual security reviews at each stage.

#### Quick Check (5 Questions)
1. How does parameterized SQL mitigate injection attacks?
2. Which hashing algorithms are recommended for password storage?
3. What is the STRIDE acronym used for?
4. How can sensitive data leak through logs and how do you prevent it?
5. Why should security reviews be part of every SDLC iteration?

---

### 2. Authentication & Authorization Architecture

- **Identity Providers:** ASP.NET Core Identity for local accounts; Azure AD B2C, Auth0 for enterprise single sign-on.
- **OAuth2/OIDC:** Authorization Code flow for web apps, PKCE for SPA, Client Credentials for service-to-service. Validate tokens using middleware.
- **Claims-Based Security:** Use claims to describe user attributes; implement policy-based authorization for complex rules.
- **Session Management:** Store tokens securely (server-side caches, HttpOnly cookies). Implement refresh token rotation.
- **Multi-Factor Authentication:** Support OTP, authenticator apps, FIDO2 keys for defense against credential theft.

#### Quick Check (5 Questions)
1. When should you use the Authorization Code flow with PKCE?
2. How do claims differ from roles in authorization decisions?
3. Why is refresh token rotation important?
4. How does ASP.NET Core Identity enforce password policies?
5. What transports should you use for tokens to avoid CSRF and XSS?

---

### 3. Secure Coding Practices & Defensive Techniques

- **Input Validation:** Validate server-side; use allow-lists, regular expressions, and strongly typed DTOs.
- **Output Encoding:** HTML, URL, JavaScript encoding prevents XSS. Razor automatically encodes by default.
- **Exception Handling:** Return sanitized error responses; log details internally. Use middleware to convert exceptions to `ProblemDetails`.
- **Dependency Management:** Monitor vulnerabilities with `dotnet list package --vulnerable`, Dependabot, or Snyk.
- **Least Privilege:** Apply principle to database accounts, filesystem access, and API permissions.

#### Quick Check (5 Questions)
1. Why must validation occur on the server even if client-side validation exists?
2. How does output encoding differ from input validation?
3. What risks arise from exposing stack traces to clients?
4. How do you continuously monitor dependencies for vulnerabilities?
5. Give an example of enforcing least privilege in database access.

---

### 4. Cryptography & Secrets Management

- **ASP.NET Core Data Protection:** Safely store keys in Azure Key Vault or Redis for multi-instance apps.
- **Symmetric Encryption:** Use `AesGcm` or `AesCng` for encrypting data; avoid obsolete algorithms (`DES`, `RC2`).
- **Hashing:** Use `KeyDerivation.Pbkdf2` for passwords; `HMACSHA256` for data integrity.
- **Certificates & TLS:** Use managed certificates, enforce HTTPS via middleware (`UseHttpsRedirection`), configure HSTS.
- **Secrets Storage:** Never commit secrets to source control. Use user secrets locally, environment variables, managed identities, or Key Vault in production.

#### Quick Check (5 Questions)
1. What algorithm should you use for symmetric encryption in .NET?
2. How does the Data Protection API handle key rotation?
3. Why are managed identities preferable to storing credentials?
4. When should you enable HSTS in an ASP.NET Core application?
5. How do you protect secrets in local development?

---

### 5. Compliance, Monitoring & Incident Response

- **Compliance Mapping:** Identify applicable regulations (GDPR, PCI DSS, HIPAA). Document data flows, retention policies, and consent management.
- **Auditing:** Use EF Core interceptors or middleware to log access to sensitive resources. Retain immutable audit logs.
- **Monitoring:** Implement SIEM integration (Azure Sentinel, Splunk). Set alerts for unusual patterns (failed logins, privilege escalation).
- **Incident Response:** Create runbooks outlining detection, containment, eradication, recovery, post-mortem steps. Conduct tabletop exercises.
- **Data Subject Rights:** Implement processes to handle data deletion, export, or correction requests as per GDPR.

#### Quick Check (5 Questions)
1. How do audit logs support compliance audits?
2. What steps constitute an effective incident response lifecycle?
3. How can you detect privilege escalation attempts?
4. Why is documenting data retention policies crucial for GDPR?
5. What is a tabletop exercise and why should you run one regularly?

---

## Authentication & Authorization

### ASP.NET Core Identity

- Identity scaffolding, custom user fields.
- Password policies, 2FA, account lockout.
- External login providers (Google, Microsoft).

### OAuth2 / OpenID Connect

- Authorization code flow, client credentials.
- IdentityServer, Duende, Azure AD B2C.
- Token validation, refresh tokens, lifetime management.

### Role & Policy-Based Authorization

- `[Authorize(Roles="Admin")]` vs policy-based.
- Custom authorization handlers (claims, requirements).
- Resource-based authorization.

---

## Secure Coding Practices

- Input validation (FluentValidation, regex sanitization).
- Output encoding (Razor: HTML, JavaScript, URL encoding).
- Parameterized SQL, stored procedures.
- Secure exception handling (no stack traces in responses).
- Logging PII handling, GDPR compliance.
- Feature flags to disable functionality quickly.

---

## Data Protection & Cryptography

- ASP.NET Core Data Protection API.
- Symmetric encryption (`AesManaged`), hashing (`SHA256`, `HMAC`).
- Password hashing (PBKDF2, bcrypt, Argon2).
- Protecting configuration secrets (Azure Key Vault, user secrets).
- TLS configuration, certificate pinning for clients.

---

## Monitoring & Incident Response

- Centralize logs with correlation IDs.
- Intrusion detection, anomaly detection.
- Audit trails: who did what and when.
- Alerts on unusual access patterns.
- Incident response playbooks, runbooks.

---

## Compliance Overview

- **GDPR:** data subject rights, data minimization, consent tracking.
- **PCI DSS:** secure storage of card data, network segmentation.
- **HIPAA (if applicable):** safeguarding PHI, logging requirements.
- **SOC2:** controls for security, availability, confidentiality.
- Documentation & evidence collection.

---

## Hands-On Labs

1. **Threat Modeling**
   - Use STRIDE to analyze a sample API.
   - Document risks and mitigations.

2. **Secure Authentication Demo**
   - Implement Identity with MFA and external login.
   - Integrate JWT bearer auth for APIs.

3. **Data Encryption Utility**
   - Build library for encrypting sensitive fields.
   - Store keys securely (Key Vault).

4. **Security Testing Pipeline**
   - Integrate `dotnet list package --vulnerable`.
   - Add Snyk or GitHub Dependabot scanning.
   - Run OWASP ZAP against local API.

5. **Compliance Checklist**
   - Map application features to compliance requirements.
   - Create data retention and deletion policies.

---

## Interview Check-In

- “How do you prevent SQL injection in .NET?”
- “Explain OAuth2 flows and when to use each.”
- “How would you secure secrets in an ASP.NET Core app?”
- “What’s the difference between symmetric and asymmetric encryption?”
- Scenario: respond to data breach detection.

---

## Resources

- [Microsoft Security Documentation](https://learn.microsoft.com/security/)
- [ASP.NET Core Security](https://learn.microsoft.com/aspnet/core/security/)
- [OWASP Cheat Sheets](https://cheatsheetseries.owasp.org/)
- [Azure Key Vault](https://learn.microsoft.com/azure/key-vault/general/)
- [Duende IdentityServer](https://duendesoftware.com/products/identityserver)

---

## Hindi Video Tutorials

- [OWASP Top 10 Explained in Hindi – WsCube Tech](https://www.youtube.com/watch?v=fxp79DbDeIs)
- [ASP.NET Core Identity & JWT Authentication in Hindi – CodeDecode](https://www.youtube.com/watch?v=3wIWESyqCC4)
- [Azure Key Vault & Secrets Management in Hindi – Geeky Shows](https://www.youtube.com/watch?v=V8y3F4iAfDg)

---

> ✅ **Completion Criteria:** Your .NET applications follow secure-by-design principles, pass security audits, and you can articulate compliance requirements and mitigation strategies in interviews.

