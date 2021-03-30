# LottoDriver SDK Usage Examples

This repository contains examples for using LottoDriver SDK.

If you are targeting .NET Framework, start with LottoDriver.Examples.NetFramework.sln solution file. If you are using Visual Studio IDE, VS2017 or later is required.
  
If you are targeting .NET Core, start with LottoDriver.Examples.NetCore.sln solution file. If you are using Visual Studion IDE, VS2019 or later is required. 
  
The examples include:
- WindowsService - Demonstrates using LottoDriver SDK in a WindowsService. If you are targeting .NET Framework in your applications, start here.
- WorkerService - Demonstrates using LottoDriver SDK in a WorkerService. If you are targeting .NET Core in your applications, start here.
- DatabaseViewer - Helper WinForms application for viewing the SQLite database that both WindowsService and WorkerService use.

## Quick Start

### 1. Obtain ClientId/Secret

To run the applications you need to obtain client id/secret combination from LottoDriver. The easiest way to obtain it is by contacting: <info@lottodriver.com>.
When you receive the credentials enter them in one of the configuration files (depending on which example you are using):

- `LottoDriver.Examples.CustomersApi.WinService\App.config`
- `LottoDriver.Examples.CustomersApi.WorkerService\appsettings.json`

### 2. Run WinService or WorkerService

After the configuration of client id/secret, simply start the application of your choice (WinService or WorkerService) from Visual Studio. 

### 3. Observe the Data

If everything is correctly configured in previous steps, you should see json data being received and printed in the console window.

You can view the latest draw rounds and results using DatabaseViewer application. If you did not change the default location of of the SQLite database file,
you should be able to run the application from Visual Studio without any additional configuration.

## SQLite Database

The examples use SQLite for locally persisting the data received from LottoDriver. By default, all three applications search for
`lotto_driver_examples.db` file in the Database folder. If the file is not found, it will be created automatically.

The location of the SQLite database can be changed in configuration files for each application.
