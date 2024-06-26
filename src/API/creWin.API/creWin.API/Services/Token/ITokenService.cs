using CreWin.Common.ViewModels;

namespace creWin.API.Services.Token
{
    public interface ITokenService
    {
        Task<TokenResponseDto> GenerateJwtToken(LoginUserViewModel dto);
    }
}