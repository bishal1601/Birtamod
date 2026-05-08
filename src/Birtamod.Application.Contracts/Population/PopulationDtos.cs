using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Birtamod.Population;

namespace Birtamod.Population;

public class PagedSortedFilteredRequestDto : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
}

public class MasterDataDto : FullAuditedEntityDto<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}

public class CreateUpdateMasterDataDto
{
    [Required]
    [MaxLength(128)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(1024)]
    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;
}

public class WardDto : FullAuditedEntityDto<Guid>
{
    public int WardNumber { get; set; }
    public string WardName { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class CreateUpdateWardDto
{
    [Range(1, 99)]
    public int WardNumber { get; set; }

    [Required]
    [MaxLength(128)]
    public string WardName { get; set; } = string.Empty;

    [MaxLength(1024)]
    public string? Description { get; set; }
}

public class HouseholdDto : FullAuditedEntityDto<Guid>
{
    public string HouseNumber { get; set; } = string.Empty;
    public string FamilyHeadName { get; set; } = string.Empty;
    public Guid WardId { get; set; }
    public int TotalMembers { get; set; }
    public string Address { get; set; } = string.Empty;
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
}

public class CreateUpdateHouseholdDto
{
    [Required]
    [MaxLength(64)]
    public string HouseNumber { get; set; } = string.Empty;

    [Required]
    [MaxLength(256)]
    public string FamilyHeadName { get; set; } = string.Empty;

    [Required]
    public Guid WardId { get; set; }

    [Range(1, 100)]
    public int TotalMembers { get; set; }

    [Required]
    [MaxLength(512)]
    public string Address { get; set; } = string.Empty;

    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
}

public class CitizenDto : FullAuditedEntityDto<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public DateTime DateOfBirthAd { get; set; }
    public string DateOfBirthBs { get; set; } = string.Empty;
    public int Age { get; set; }
    public Guid ReligionId { get; set; }
    public Guid LanguageId { get; set; }
    public Guid EthnicityId { get; set; }
    public Guid EducationQualificationId { get; set; }
    public Guid FamilyTypeId { get; set; }
    public bool DisabilityStatus { get; set; }
    public string? Occupation { get; set; }
    public string? CitizenshipNumber { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public Guid WardId { get; set; }
    public Guid HouseholdId { get; set; }
    public bool IsHouseOwner { get; set; }
    public bool HasToilet { get; set; }
    public string? Address { get; set; }
}

public class CreateUpdateCitizenDto
{
    [Required]
    [MaxLength(128)]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(128)]
    public string? MiddleName { get; set; }

    [Required]
    [MaxLength(128)]
    public string LastName { get; set; } = string.Empty;

    public Gender Gender { get; set; }
    public DateTime? DateOfBirthAd { get; set; }
    public string? DateOfBirthBs { get; set; }
    public Guid ReligionId { get; set; }
    public Guid LanguageId { get; set; }
    public Guid EthnicityId { get; set; }
    public Guid EducationQualificationId { get; set; }
    public Guid FamilyTypeId { get; set; }
    public bool DisabilityStatus { get; set; }
    public string? Occupation { get; set; }
    public string? CitizenshipNumber { get; set; }
    public string? PhoneNumber { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    public Guid WardId { get; set; }
    public Guid HouseholdId { get; set; }
    public bool IsHouseOwner { get; set; }
    public bool HasToilet { get; set; }
    public string? Address { get; set; }
}

public class DateConversionInputDto
{
    public DateTime? DateOfBirthAd { get; set; }
    public string? DateOfBirthBs { get; set; }
}

public class DateConversionResultDto
{
    public DateTime DateOfBirthAd { get; set; }
    public string DateOfBirthBs { get; set; } = string.Empty;
    public int Age { get; set; }
}

public class BulkDeleteInputDto
{
    public List<Guid> Ids { get; set; } = [];
}

public class StatisticItemDto
{
    public string Name { get; set; } = string.Empty;
    public int Count { get; set; }
}

public class DashboardSummaryDto
{
    public int TotalHouseholds { get; set; }
    public int TotalPopulation { get; set; }
    public int TotalMale { get; set; }
    public int TotalFemale { get; set; }
    public int TotalOtherGender { get; set; }
}

public class DashboardDataDto
{
    public DashboardSummaryDto Summary { get; set; } = new();
    public List<StatisticItemDto> PopulationByGender { get; set; } = [];
    public List<StatisticItemDto> PopulationByReligion { get; set; } = [];
    public List<StatisticItemDto> PopulationByLanguage { get; set; } = [];
    public List<StatisticItemDto> PopulationByEthnicity { get; set; } = [];
    public List<StatisticItemDto> PopulationByEducation { get; set; } = [];
    public List<StatisticItemDto> PopulationByFamilyType { get; set; } = [];
    public List<StatisticItemDto> PopulationByDisability { get; set; } = [];
    public List<StatisticItemDto> PopulationByToiletAvailability { get; set; } = [];
    public List<StatisticItemDto> PopulationByHouseOwnership { get; set; } = [];
    public List<StatisticItemDto> PopulationByAgeGroup { get; set; } = [];
    public List<StatisticItemDto> WardWisePopulation { get; set; } = [];
}
