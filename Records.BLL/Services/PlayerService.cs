using AutoMapper;
using Records.BLL.Interfaces;
using Records.DAL.Interfaces;
using Records.Data.DataTransferObjects;
using Records.Data.Enums;
using Records.Data.Interfaces;
using Records.Data.Models;
using Records.Data.Responses;

namespace Records.BLL.Services;

public class PlayerService : IPlayerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PlayerService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public Task<IBaseResponse<PlayerDto>> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IBaseResponse<IEnumerable<PlayerDto>>> Get()
    {
        try
        {
            var models = await _unitOfWork.PlayerRepository.GetAsync();

            if (models.Count is 0)
            {
                return CreateBaseResponse<IEnumerable<PlayerDto>>("0 objects found", StatusCode.NotFound);
            }

            var dtoList = new List<PlayerDto>();
                
            foreach (var model in models)
                dtoList.Add(_mapper.Map<PlayerDto>(model));
                
            return CreateBaseResponse<IEnumerable<PlayerDto>>("Success!", StatusCode.Ok, dtoList, dtoList.Count);
        }
        catch(Exception e) 
        {
            return CreateBaseResponse<IEnumerable<PlayerDto>>(e.Message, StatusCode.InternalServerError);
        }
    }

    public async Task<IBaseResponse<string>> Insert(PlayerDto? modelDto)
    {
        try
        {
            if (modelDto is not null)
            {
                modelDto.Id = Guid.NewGuid();
                
                var bestRecordDto = new BestRecordDto()
                {
                    Id = Guid.NewGuid(),
                    PlayerId = modelDto.Id,
                    TotalScore = 0,
                    PinkScore = 0,
                    GreenScore = 0
                };
                
                modelDto.BestRecordId = bestRecordDto.Id;
                
                await _unitOfWork.BestRecordRepository.InsertAsync(_mapper.Map<BestRecord>(bestRecordDto));
                await _unitOfWork.PlayerRepository.InsertAsync(_mapper.Map<Player>(modelDto));
                await _unitOfWork.SaveChangesAsync();

                return CreateBaseResponse<string>("Object inserted!", StatusCode.Ok, resultsCount: 1);
            }

            return CreateBaseResponse<string>("Objet can`t be empty...", StatusCode.BadRequest);
        }
        catch (Exception e)
        {
            return CreateBaseResponse<string>(e.Message, StatusCode.InternalServerError);
        }
    }

    public async Task<IBaseResponse<string>> DeleteById(Guid id)
    {
        try
        {
            var model = await _unitOfWork.PlayerRepository.GetByIdAsync(id);
            await _unitOfWork.BestRecordRepository.DeleteAsync(model.BestRecordId);
            
            await _unitOfWork.PlayerRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return CreateBaseResponse<string>("Object deleted!", StatusCode.Ok, resultsCount: 1);
        }
        catch (Exception e)
        {
            return CreateBaseResponse<string>($"{e.Message} or object not found", StatusCode.InternalServerError);
        }
    }
    
    private BaseResponse<T> CreateBaseResponse<T>(string description, StatusCode statusCode, T? data = default, int resultsCount = 0)
    {
        return new BaseResponse<T>()
        {
            ResultsCount = resultsCount,
            Data = data!,
            Description = description,
            StatusCode = statusCode
        };
    }
}