namespace LogMonitor.Entities.Models.Enums;

public enum AppLogLevel
{
    // 1xx : Informational
    Debug = 1,
    
    // 2xx : Success / Standard Operations
    Info = 2, 

    // 3xx : Client Errors / Warnings
    Warning = 3,

    // 4xx : Server Errors / Exceptions
    Error = 4, 
    
    // 5xx : Fatal / System Crash
    Critical = 5
}