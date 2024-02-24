using AutoMapper;
using Records.BLL.Interfaces;
using Records.DAL.Interfaces;
using Records.Data.DataTransferObjects;
using Records.Data.Enums;
using Records.Data.Interfaces;
using Records.Data.Models;
using Records.Data.Responses;

namespace Records.BLL.Services;

public class BestRecordService : IBestRecordService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BestRecordService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public Task<IBaseResponse<BestRecordDto>> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IBaseResponse<IEnumerable<BestRecordDto>>> Get()
    {
        try
        {
            var models = await _unitOfWork.BestRecordRepository.GetAsync();

            if (models.Count is 0)
            {
                return CreateBaseResponse<IEnumerable<BestRecordDto>>("0 objects found", StatusCode.NotFound);
            }

            var dtoList = new List<BestRecordDto>();
                
            foreach (var model in models)
                dtoList.Add(_mapper.Map<BestRecordDto>(model));
                
            return CreateBaseResponse<IEnumerable<BestRecordDto>>("Success!", StatusCode.Ok, dtoList, dtoList.Count);
        }
        catch(Exception e) 
        {
            return CreateBaseResponse<IEnumerable<BestRecordDto>>(e.Message, StatusCode.InternalServerError);
        }
    }

    public async Task<IBaseResponse<string>> UpdateById(Guid id, BestRecordDto modelDto)
    {
        try
        {
            var model = _mapper.Map<BestRecord>(modelDto);

            await _unitOfWork.BestRecordRepository.UpdateAsync(model);
            await _unitOfWork.SaveChangesAsync();
            
            return CreateBaseResponse<string>("Object updated!", StatusCode.Ok, resultsCount: 1);
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