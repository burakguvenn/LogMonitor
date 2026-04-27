using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using LogMonitor.Entities.Models.Enums;

namespace LogMonitor.Business.DTOs;

public class CreateLogDto
{
    public string Message { get; set;} = string.Empty;

    [EnumDataType(typeof(AppLogLevel), ErrorMessage = "Invalid log level!")]
    public AppLogLevel Level { get; set;}
    
    public JsonDocument? Metadata { get; set; }
}