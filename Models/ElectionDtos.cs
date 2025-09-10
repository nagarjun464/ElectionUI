namespace Electionapp.UI.Models;

public class CreateElectionDto
{
    public string Name { get; set; } = "";
    public string CategoryCode { get; set; } = "L";
    public DateTime StartUtc { get; set; }
    public DateTime EndUtc { get; set; }
    public string TimeZoneId { get; set; } = "America/Chicago";
}

public class UpdateElectionDto
{
    public string Name { get; set; } = "";
    public string CategoryCode { get; set; } = "L";
    public DateTime StartUtc { get; set; }
    public DateTime EndUtc { get; set; }
    public string TimeZoneId { get; set; } = "America/Chicago";
    public string Status { get; set; } = "Draft"; // Draft|Scheduled|Open|Closed
}

public class ElectionDto
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public string CategoryCode { get; set; } = "L";
    public int CategoryId { get; set; }
    public DateTime StartUtc { get; set; }
    public DateTime EndUtc { get; set; }
    public string TimeZoneId { get; set; } = "America/Chicago";
    public string Status { get; set; } = "Draft";
    public DateTime CreatedUtc { get; set; }
    public DateTime UpdatedUtc { get; set; }
}
