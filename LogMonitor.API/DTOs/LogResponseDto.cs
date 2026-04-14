namespace LogMonitor.API.DTOs;

public class LogResponseDto
{
    public long Id { get; set; }
    public string Message { get; set; } = null!;
    public string Level { get; set; } = null!; //enum'dan sayı değil string okur
    public DateTime CreatedAt { get; set;}
    public int UserId { get; set; }
}