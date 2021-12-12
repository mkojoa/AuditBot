# Welcome to AuditBot
[![made-with-csharp](https://img.shields.io/badge/csharp-1f425f?logo=c#)](https://microsoft.com/csharp)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

This is a small auditing package

![](https://vistr.dev/badge?repo=mkojoa.AuditBot&color=0058AD)

The need for auditing already exists in projects, 
the requirement to log information to prove something happened, 
but also to capture `who`, `what` and `when` a business action happened.

`AuditBot` helps in generating tracking information about operations being executed. 
It gathers environmental information such as the caller
 `user id`, `machine name`, `method name`, `exceptions`, 
including `execution time`.

#### Fuel my efforts with a cup of Coffee.
<a href="https://www.buymeacoffee.com/mkojoa" target="_blank"><img src="https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png" alt="Buy Me A Coffee" style="height:30px !important;width: 174px !important;box-shadow: 0px 3px 2px 0px rgba(190, 190, 190, 0.5) !important;-webkit-box-shadow: 0px 3px 2px 0px rgba(190, 190, 190, 0.5) !important;" ></a>

## Features

- capture who with the `IPrincipal` or `IClaimsPrincipal`
- Audit log channel 
    - [X] [File](#eazy-logging)
    - [X] [Console](#eazy-logging)
    - [X] [SqlServer Database](#eazy-logging)

## Get Started

#### Installation (Not Yet)
    - Install-Package AuditBot

> - Once you have configured the `AddAuditBot()` service in the Startup.cs file, 
> - Once you have configured the `UseAuditBot()` in the SetUp.cs file, 
> you're ready to define the `AuditBotOptions` in the `app.settings.json`.

###### appsettings
```yaml
"AuditBot": {
    "ApplicationName": "auditbot-demo-api",
    "Source": "AuditBot.Demo.Api",
    "SubjectId": "sub",
    "SubjectClaim": "name",
    "IncludeFormVariables": false,
    "ClientIdClaim": "client_id",
    "Mail": {
      "Driver": "SMTP",
      "Host": "smtp.gmail.com",
      "Port": 587,
      "Username": "persol.audit@gmail.com",
      "Password": "PersolAudit@2021!",
      "From": {
        "Address": "persol.audit@gmail.com",
        "Name": "no-reply"
      }
    },
    "DataSource": {
      "Enabled": true,
      "SqlServer": {
        "ConnectionId" :  ""
      }
    }
  }
```