using AutoMapper;
using Records.Data.DataTransferObjects;
using Records.Data.Models;

namespace Records.BLL;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BestRecord, BestRecordDto>();
        CreateMap<BestRecordDto, BestRecord>();
        
        CreateMap<Player, PlayerDto>();
        CreateMap<PlayerDto, Player>();
    }
}