using Faqidy.Application.SocialMedia.MissingProfile.DTOs;
using Faqidy.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Faqidy.Application.SocialMedia.MissingProfile.Commands.AddMissingProfile
{
    public record AddMissingProfileCommands(
        string ChildName,
        int AgeAtDisappearance,
        int CurrentEstimatedAge,
        Gender Gender,
        DateTime? BirthDate,
        DateTime DisappearanceDate,
        string? DisappearanceLocation,
        string DisappearanceCity,
        string DisappearanceGovernorate,
        string PhysicalDescription,
        int HeightCM ,
        int WeightKM,
        string? EyeColor,
        string? HairColor,
        string? SkinTone,
        CaseStatus Status,
        bool IsVerified,
        string ContactInfo,
        List<IFormFile> Photos
        ) : IRequest<MissingChildDto>; // out TResponse
}
