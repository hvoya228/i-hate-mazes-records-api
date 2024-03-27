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


    public async Task<IBaseResponse<BestRecordDto>> GetById(Guid id)
    {
        try
        {
            var model = await _unitOfWork.BestRecordRepository.GetByIdAsync(id);
            var modelDto = _mapper.Map<BestRecordDto>(model);

            return CreateBaseResponse("Success!", StatusCode.Ok, modelDto, 1);
        }
        catch (Exception e)
        {
            return CreateBaseResponse<BestRecordDto>($"{e.Message} or object not found", StatusCode.InternalServerError);
        }
    }

    public async Task<IBaseResponse<string>> Update(BestRecordDto modelDto)
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