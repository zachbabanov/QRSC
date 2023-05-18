# QR scanner PC-app part for AR getting the PC log
An app to generate QR which contains all PC perfomance information, such as available disk space, RAM, CPU utilization etc.  
This app is part of AR project for getting PC logs from your mobile device
# Using libraries
  * Win32 API / System for getting all log info
  * NewtonSoft JSON parser for structure log into QR
  * ZXing QR generator to get QR from JSON structure
  * Costure to include .dll's in executable
# Known issues
|  #  |                  Issue               |                                                             Reason                                                          |           Solution               |         Solved          |
|-----|--------------------------------------|-----------------------------------------------------------------------------------------------------------------------------|----------------------------------|-------------------------|
|  1  | Memory leak / generating too big QR  | New generated structure adds to previusly created                                                                           | Override with every generation   |<ul><li>- [x] </li></ul> |
|  2  | Timer reload                         | Timer starts with thread which utilize current work, and app could not be closed before timer of new generate is started    |    ?                             |<ul><li>- [ ] </li></ul> |
|  3  | Genrating QR overriding memory       | New QR continiusly hold more and more memory cause of overriding setting in JSON                                            | Singlton class/structure of logs |<ul><li>- [ ] </li></ul> |
# Using
  Build and compile on your device and use executable. You may use it even without .dll's - they are already included in .exe itself  
  Use mobile app(https://github.com/RudeGalaxy1010/AR_Diagnostic) to scan generated QR
