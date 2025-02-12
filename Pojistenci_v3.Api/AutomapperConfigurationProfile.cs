using AutoMapper;
using Pojistenci_v3.Common.ModelsDTO;
using Pojistenci_v3.Common.ModelsDTO.CarInsuranceAccidentRecordDTOs;
using Pojistenci_v3.Common.ModelsDTO.CArInsuranceDTOs;
using Pojistenci_v3.Common.ModelsDTO.HomeInsuranceDamageRecordDTOs;
using Pojistenci_v3.Common.ModelsDTO.HomeInsuranceDTOs;
using Pojistenci_v3.Data.Models;

namespace Pojistenci_v3.Api
{
	/// <summary>
	/// Třída pro konfiguraci mapování pomocí AutoMapperu.
	/// Obsahuje definice mapování mezi entitami a jejich DTO.
	/// </summary>
	public class AutomapperConfigurationProfile : Profile
	{
		/// <summary>
		/// Inicializuje novou instanci třídy <see cref="AutomapperConfigurationProfile"/>.
		/// Definuje mapování mezi různými modely a jejich DTO.
		/// </summary>
		public AutomapperConfigurationProfile()
		{
			// Mapování mezi RegisterInsuredDTO a Insured
			CreateMap<RegisterInsuredDTO, Insured>()
				.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)); // Mapování UserName na Email

			// Mapování mezi RegisterInsurerDTO a Insurer
			CreateMap<RegisterInsurerDTO, Insurer>()
				.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)); // Mapování UserName na Email

			// Obousměrné mapování mezi Insurer a InsurerDTO
			CreateMap<InsurerDTO, Insurer>().ReverseMap();

			// Obousměrné mapování mezi Insured a InsuredDTO
			CreateMap<InsuredDTO, Insured>().ReverseMap();

			// Mapování Insurance a InsuranceDTO
			CreateMap<Insurance, InsuranceDTO>()
				.ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
				.ReverseMap()
				.ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<InsuranceType>(src.Type)));

			// Mapování mezi HomeInsurance a HomeInsuranceDTO
			CreateMap<HomeInsurance, HomeInsuranceDTO>()
				.IncludeBase<Insurance, InsuranceDTO>() // Zahrnutí mapování z Insurance na InsuranceDTO
				.ForMember(dest => dest.HomeInsuranceType, opt => opt.MapFrom(src => src.HomeInsuranceType.ToString()))
				.ReverseMap()
				.IncludeBase<InsuranceDTO, Insurance>() // Zahrnutí mapování zpět z InsuranceDTO na Insurance
				.ForMember(dest => dest.HomeInsuranceType, opt => opt.MapFrom(src => Enum.Parse<HomeInsuranceType>(src.HomeInsuranceType)));

			// Mapování pro aktualizaci HomeInsurance
			CreateMap<UpdateHomeInsuranceDTO, HomeInsurance>();

			// Mapování pro vytvoření HomeInsurance
			CreateMap<CreateHomeInsuranceDTO, HomeInsurance>()
				.ForMember(dest => dest.PropertyType, opt => opt.MapFrom(src => Enum.Parse<PropertyType>(src.PropertyType)))
				.ForMember(dest => dest.HomeInsuranceType, opt => opt.MapFrom(src => Enum.Parse<HomeInsuranceType>(src.HomeInsuranceType)));

			// Mapování mezi CarInsurance a CarInsuranceDTO
			CreateMap<CarInsurance, CarInsuranceDTO>()
				.IncludeBase<Insurance, InsuranceDTO>()
				.ForMember(dest => dest.CarInsuranceType, opt => opt.MapFrom(src => src.CarInsuranceType.ToString()))
				.ReverseMap()
				.IncludeBase<InsuranceDTO, Insurance>()
				.ForMember(dest => dest.CarInsuranceType, opt => opt.MapFrom(src => Enum.Parse<CarInsuranceType>(src.CarInsuranceType)));

			// Mapování pro aktualizaci CarInsurance
			CreateMap<UpdateCarInsuranceDTO, CarInsurance>();

			// Mapování pro vytvoření CarInsurance
			CreateMap<CreateCarInsuranceDTO, CarInsurance>()
				.ForMember(dest => dest.FuelType, opt => opt.MapFrom(src => Enum.Parse<FuelType>(src.FuelType)))
				.ForMember(dest => dest.UsageType, opt => opt.MapFrom(src => Enum.Parse<UsageType>(src.UsageType)))
				.ForMember(dest => dest.CarInsuranceType, opt => opt.MapFrom(src => Enum.Parse<CarInsuranceType>(src.CarInsuranceType)));

			// Mapování pro záznamy o nehodách vozidel
			CreateMap<CarInsuranceAccidentRecord, CarInsuranceAccidentRecordDTO>();
			CreateMap<CreateCarInsuranceAccidentRecordDTO, CarInsuranceAccidentRecord>();
			CreateMap<UpdateCarInsuranceAccidentRecordDTO, CarInsuranceAccidentRecord>();

			// Mapování pro záznamy o škodách na pojištění domácnosti
			CreateMap<HomeInsuranceDamageRecord, HomeInsuranceDamageRecordDTO>();
			CreateMap<CreateHomeInsuranceDamageRecordDTO, HomeInsuranceDamageRecord>();
			CreateMap<UpdateHomeInsuranceDamageRecordDTO, HomeInsuranceDamageRecord>();
		}
	}
}
