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

    public async Task<IBaseResponse<BestPlayerDto>> GetBestPlayer()
    {
        try
        {
            var bestRecords = await _unitOfWork.BestRecordRepository.GetAsync();
            var bestPlayerRecord = bestRecords.MaxBy(r => r.TotalScore);

            if (bestPlayerRecord == null)
                return CreateBaseResponse("No best player found", StatusCode.NotFound, new BestPlayerDto());
            
            var player = await _unitOfWork.PlayerRepository.GetByIdAsync(bestPlayerRecord.PlayerId);
            var bestPlayerDto = new BestPlayerDto()
            {
                TotalScore = bestPlayerRecord.TotalScore,
                Name = player.Name
            };
                
            return CreateBaseResponse("Success!", StatusCode.Ok, bestPlayerDto, 1);

        }
        catch (Exception e)
        {
            return CreateBaseResponse<BestPlayerDto>(e.Message, StatusCode.InternalServerError);
        }
    }

    public async Task<IBaseResponse<List<BestPlayerDto>>> GetBestPlayers()
    {
        try
        {
            var bestRecords = await _unitOfWork.BestRecordRepository.GetAsync();
            var bestPlayersRecords = bestRecords.OrderByDescending(r => r.TotalScore).Take(5);
            
            var bestPlayersDtos = new List<BestPlayerDto>();
            
            foreach (var bestPlayerRecord in bestPlayersRecords)
            {
                var bestPlayerDto = new BestPlayerDto()
                {
                    TotalScore = bestPlayerRecord.TotalScore,
                    Name = (await _unitOfWork.PlayerRepository.GetByIdAsync(bestPlayerRecord.PlayerId)).Name
                };
                
                bestPlayersDtos.Add(bestPlayerDto);
            }
            
            return CreateBaseResponse("Success!", StatusCode.Ok, bestPlayersDtos, bestPlayersDtos.Count);
        }
        catch (Exception e)
        {
            return CreateBaseResponse<List<BestPlayerDto>>(e.Message, StatusCode.InternalServerError);
        }
    }


    public async Task<IBaseResponse<PlayerDto>> GetById(Guid id)
    {
        try
        {
            var player = await _unitOfWork.PlayerRepository.GetByIdAsync(id);
            var playerDto = _mapper.Map<PlayerDto>(player);
            
            return CreateBaseResponse("Success!", StatusCode.Ok, playerDto, 1);
        }
        catch (Exception e)
        {
            return CreateBaseResponse<PlayerDto>(e.Message, StatusCode.InternalServerError);
        }
    }

    public async Task<IBaseResponse<List<PlayerDto>>> Get()
    {
        try
        {
            var players = await _unitOfWork.PlayerRepository.GetAsync();
            var playerDtos = _mapper.Map<List<PlayerDto>>(players);
            
            return CreateBaseResponse("Success!", StatusCode.Ok, playerDtos, playerDtos.Count);
        }
        catch (Exception e)
        {
            return CreateBaseResponse<List<PlayerDto>>(e.Message, StatusCode.InternalServerError);
        }
    }

    public async Task<IBaseResponse<PlayerDto>> Login(string username, string password)
    {
        try
        {
            var players = await _unitOfWork.PlayerRepository.GetAsync();
            
            var player = players.FirstOrDefault(p => p.Name == username && p.Password == password);
            
            return player == null ? 
                CreateBaseResponse<PlayerDto>("Wrong password or username...", StatusCode.BadRequest) : 
                CreateBaseResponse("Player login!", StatusCode.Ok, _mapper.Map<PlayerDto>(player), resultsCount: 1);
        }
        catch (Exception e)
        {
            return CreateBaseResponse<PlayerDto>(e.Message, StatusCode.InternalServerError);
        }
    }

    public async Task<IBaseResponse<PlayerDto>> Insert(PlayerDto? modelDto)
    {
        try
        {
            if (modelDto is null)
                return CreateBaseResponse<PlayerDto>("Objet can`t be empty...", StatusCode.BadRequest);
            
            var players = await _unitOfWork.PlayerRepository.GetAsync();

            if (players.Any(player => player.Name == modelDto.Name))
            {
                return CreateBaseResponse<PlayerDto>("Name already exists...", StatusCode.BadRequest);
            }
            
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

            return CreateBaseResponse("Object inserted!", StatusCode.Ok, modelDto, resultsCount: 1);

        }
        catch (Exception e)
        {
            return CreateBaseResponse<PlayerDto>(e.Message, StatusCode.InternalServerError);
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