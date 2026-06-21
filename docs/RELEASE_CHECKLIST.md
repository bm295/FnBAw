# Release Checklist

Status: Done

Use this checklist before launching FnB Management to restaurant owners and food-service operators.

## Security

- [ ] Authentication is required for dashboard, menu, inventory, orders, and reports.
- [ ] Role-based authorization matches `docs/USER_ROLES.md`.
- [ ] HTTPS is enforced in production.
- [ ] Secure cookie settings are enabled for production.
- [ ] Anti-forgery protection is enabled on all state-changing forms.
- [ ] Secrets are stored outside the repository and deployment templates.
- [ ] Audit logging covers login, role changes, inventory adjustments, menu price changes, and order cancellations.

## Data

- [ ] Production database connection is configured through environment variables or a secret manager.
- [ ] Database migrations have been applied successfully.
- [ ] Seed/demo data is separated from customer production data.
- [ ] Backup and restore procedures have been tested.
- [ ] Data retention, privacy, and deletion requirements have been reviewed.
- [ ] Timezone and currency settings match the target restaurant market.

## Performance

- [ ] Dashboard loads within the agreed service target under expected traffic.
- [ ] Order creation remains responsive during peak shift volume.
- [ ] Report exports complete within the agreed time window.
- [ ] Low-stock and revenue calculations are tested with realistic data volume.
- [ ] Application logs, metrics, and health checks are available in production.

## Deployment

- [ ] Production configuration file and environment variables are complete.
- [ ] Container image builds successfully from a clean checkout.
- [ ] Infrastructure settings include CPU, memory, health check, logging, and restart policy.
- [ ] Deployment runbook includes rollout and rollback steps.
- [ ] Smoke tests pass after deployment.
- [ ] Public DNS, TLS certificate, and monitoring alerts are configured.

## Support

- [ ] Support contact information is visible to operators.
- [ ] Incident escalation steps are documented.
- [ ] Known limitations are documented for sales and onboarding teams.
- [ ] Demo account and walkthrough data are ready.
- [ ] Training notes exist for owner, manager, cashier, kitchen, and inventory workflows.
- [ ] Final launch approval is recorded by the product owner.

## Completion checklist

- [x] Security pre-launch checks documented.
- [x] Data pre-launch checks documented.
- [x] Performance pre-launch checks documented.
- [x] Deployment pre-launch checks documented.
- [x] Support pre-launch checks documented.
