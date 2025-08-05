namespace StoreApp.Shared.Dtos;

public record ErrorResponseDto(string Message, int StatusCode = 500);