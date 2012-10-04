octo-nuget
==========

NuGet multi-feed manager in .NET.

Use it if you want to have a few NuGet feeds per package readiness (QA, UAT, Production) etc.

Getting started
---------------
Setup your per-machine environment variables:

- CRS\_JIRA\_HOST=https://youjirawebserver.com 

- CRS\_JIRA\_TOKEN=base64(login:password)

- CRS\_QA\_PRIVATE=C:\NuGet\private\qa

- CRS\_QA\_PUBLIC=C:\NuGet\public\qa

- CRS\_QA\_PUBLIC=C:\NuGet\public\qa

If you want to set up more environments, use the following pattern:
*CRS\_{ENVIRONMENT}_(PRIVATE|PUBLIC)*

How to use
---------------
Use packages.aspx - to list all visible packages

Use configuration.aspx - to check your configuration

Use jira.ashx - to integrate with JIRA:

- ?action=publish&issue=TST-44&to=QA:

copies project.major.minor.hotfix.build.nupkg from \private\ to \public\

- ?action=reject&issue=TST-44

deletes project.major.minor.hotfix.build.nupkg from all public\* folders (per environment)

- ?action=purge&issue=TST-44

the same - but also deletes from private\* folders also

- ?action=create-release&delivery=TST-45&version=1.2.3.1

create Release Package sub-ticket with specified version for specified Delivery parent ticket


