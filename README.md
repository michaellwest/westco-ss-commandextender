# Westco SS Command Extender

Westco SS Command Extender provides the ability to extend commands such as `ItemNew` which traditionally only allow for a single implementation.

Current **Westco SS Command Extender** project is compatible with:

| Product   |      Version      |  Revision |
|----------|:-------------:|:------:|
| Sitecore |  **8.2** | rev. 170407 |

[![License](https://img.shields.io/badge/license-MIT%20License-brightgreen.svg)](https://opensource.org/licenses/MIT)

## Setup

Getting started is fairly straightforward.

### Deploy the following files:

* \bin\Westco.SS.Foundation.CommandExtender.dll
* \App_Config\Include\Foundation\Westco\Westco.SS.Foundation.CommandExtender.config

### Patch the commands:

Here we want to allow for both SPE and Code Editor to override the `ItemNew` insert options. 

```xml
<configuration xmlns:patch="http://www.sitecore.net/xmlconfig/">
  <sitecore>
    <commands>
      <command name="item:new">
        <menucommands hint="list">
          <menucommand type="Cognifide.PowerShell.Client.Commands.MenuItems.ItemNew, Cognifide.PowerShell" />
          <menucommand type="Sitecore.SharedSource.Shell.Framework.Commands.MenuItems.ItemNew, Sitecore.SharedSource.CodeEditor" />
        </menucommands>
      </command>
    </commands>
  </sitecore>
</configuration>
```
