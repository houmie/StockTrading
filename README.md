StockTrading
============

A local demo for C# .NET utilising WPF, WCF, PRISM, MVVM, Threading


Requirement
===========

1) Open the Solution and click on 'Manage NuGet packages for the solution'. Then search online for 'OxyPlot WPF' and install it.

2) If the client can't find the PRISM references, delete them. Right click on References and add all libs from  StockTrading\Lib into the XBAPClient project.


Service
=======

Open the solution, compile the service and go to StockTrading\Service\bin\Debug and right-click the Service.exe --> Open as Administrator. Otherwise it won't be able to bind to http on your machine and you'll get an exception.

Client
======
After he service is running, simply run the XBAPClient project from Visual studio. Your browser will open and warn you twice about a missing certificate, which you accept to proceed.


Be Patient, the service will get real prices from yahoo and the first time might take up to 10 seconds before you see something in your browser.
