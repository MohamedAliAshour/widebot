using widebot.DTOs;

namespace widebot.interfaces
{
    public interface IRedirect
    {
        Task<ShortUrlGetDto> GetByShortCode(string shortCode); // Return DTO
    }
}
