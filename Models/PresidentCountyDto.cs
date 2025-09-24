//namespace PresidentCountyAPI.Dtos;

// Response DTO
public record PresidentCountyDto(
    string Id,
    string State,
    string County,
    string CandidateName,
    string Party,
    string TotalVotes,
    string Won
);

// Create DTO
public record CreatePresidentCountyDto(
    string State,
    string County,
    string CandidateName,
    string Party,
    string TotalVotes,
    string Won
);

// Update DTO
public record UpdatePresidentCountyDto(
    string State,
    string County,
    string CandidateName,
    string Party,
    string TotalVotes,
    string Won
);
