using AutoMapper;
using Birtamod.Population;

namespace Birtamod;

public class BirtamodApplicationAutoMapperProfile : Profile
{
    public BirtamodApplicationAutoMapperProfile()
    {
        CreateMap<Religion, MasterDataDto>();
        CreateMap<Language, MasterDataDto>();
        CreateMap<Ethnicity, MasterDataDto>();
        CreateMap<EducationQualification, MasterDataDto>();
        CreateMap<FamilyType, MasterDataDto>();
        CreateMap<CreateUpdateMasterDataDto, Religion>();
        CreateMap<CreateUpdateMasterDataDto, Language>();
        CreateMap<CreateUpdateMasterDataDto, Ethnicity>();
        CreateMap<CreateUpdateMasterDataDto, EducationQualification>();
        CreateMap<CreateUpdateMasterDataDto, FamilyType>();

        CreateMap<Ward, WardDto>();
        CreateMap<CreateUpdateWardDto, Ward>();

        CreateMap<Household, HouseholdDto>();
        CreateMap<CreateUpdateHouseholdDto, Household>();

        CreateMap<Citizen, CitizenDto>();
        CreateMap<CreateUpdateCitizenDto, Citizen>()
            .ForMember(x => x.DateOfBirthAd, o => o.Ignore())
            .ForMember(x => x.DateOfBirthBs, o => o.Ignore())
            .ForMember(x => x.Age, o => o.Ignore());
    }
}
