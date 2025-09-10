// Models/ElectionForm.cs
using Electionapp.UI.Models;

namespace Electionapp.UI.Models;

public class ElectionForm
{
    public string Name { get; set; } = "";
    public string CategoryCode { get; set; } = "L";
    public DateTime StartLocal { get; set; } = DateTime.Today.AddHours(9);
    public DateTime EndLocal { get; set; } = DateTime.Today.AddHours(17);
    public string TimeZoneId { get; set; } = "America/Chicago";
    public string Status { get; set; } = "Draft";

    public static ElectionForm New() => new();

    public static ElectionForm FromDto(ElectionDto e)
    {
        return new ElectionForm
        {
            Name = e.Name,
            CategoryCode = e.CategoryCode,
            StartLocal = DateTime.SpecifyKind(e.StartUtc, DateTimeKind.Utc).ToLocalTime(),
            EndLocal = DateTime.SpecifyKind(e.EndUtc, DateTimeKind.Utc).ToLocalTime(),
            TimeZoneId = e.TimeZoneId,
            Status = e.Status
        };
    }
}
