using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Birtamod.Population;

public abstract class MasterDataEntity : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
}

public class Religion : MasterDataEntity;
public class Language : MasterDataEntity;
public class Ethnicity : MasterDataEntity;
public class EducationQualification : MasterDataEntity;
public class FamilyType : MasterDataEntity;

public class Ward : FullAuditedAggregateRoot<Guid>
{
    public int WardNumber { get; set; }
    public string WardName { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class Household : FullAuditedAggregateRoot<Guid>
{
    public string HouseNumber { get; set; } = string.Empty;
    public string FamilyHeadName { get; set; } = string.Empty;
    public Guid WardId { get; set; }
    public int TotalMembers { get; set; }
    public string Address { get; set; } = string.Empty;
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }

    public Ward? Ward { get; set; }
}

public class Citizen : FullAuditedAggregateRoot<Guid>
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

    public Religion? Religion { get; set; }
    public Language? Language { get; set; }
    public Ethnicity? Ethnicity { get; set; }
    public EducationQualification? EducationQualification { get; set; }
    public FamilyType? FamilyType { get; set; }
    public Ward? Ward { get; set; }
    public Household? Household { get; set; }
}
