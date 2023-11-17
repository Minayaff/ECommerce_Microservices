 using MangoWeb.Models;

namespace MangoWeb.Services.IService
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsyncs(RequestDto requestDto);
    }
}
