using System.ComponentModel.DataAnnotations;
using LogMonitor.API.Models.Enums;

namespace LogMonitor.API.DTOs;

public class CreateLogDto
{
    public string Message { get; set;} = string.Empty;
    [EnumDataType(typeof(AppLogLevel), ErrorMessage = "Invalid log level!")]
    public AppLogLevel Level { get; set;}
}