using Faqidy.Domain.Entities.IdentityModule;
using Faqidy.Domain.Entities.sotialMediaModule;
using Faqidy.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faqidy.Application.SocialMedia.MissingProfile.DTOs
{
    public class MissingChildDto
    {
        public Guid Id { get; set; }
        public string ChildName { get; set; }
        public int AgeAtDisappearance { get; set; }
        public int? CurrentEstimatedAge { get; set; }
        public Gender Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime DisappearanceDate { get; set; }
        public string? DisappearanceLocation { get; set; }
        public string DisappearanceCity { get; set; }
        public string DisappearanceGovernorate { get; set; }
        public string? PhysicalDescription { get; set; }
        public int HeightCM { get; set; }
        public int WeightKM { get; set; }
        public string? EyeColor { get; set; }
        public string? HairColor { get; set; }
        public string? SkinTone { get; set; }
        public CaseStatus Status { get; set; }
        public string ContactInfo { get; set; } // json string 
        public DateTime CreateOn { get; set; }
        public string ReporterId { get; set; }

        public List<ChildPhotoDto> ChildPhotos { get; set; }
    }
}
