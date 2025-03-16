using widebot.DTOs;

namespace widebot.interfaces
{
    public interface IShortUrl
    {
        Task<List<ShortUrlGetDto>> GetAll(); // DTO list for client/UI
        Task<ShortUrlCreateDto> Add(ShortUrlCreateDto model); // Return created DTO
        Task<bool> Delete(int id);
        Task<ShortUrlGetDto> GetById(int id); // Return DTO instead of entity
        Task<ShortUrlGetDto> GetByShortenUrl(string shortenUrl); // Return DTO
        Task<ShortUrlGetDto> GetByShortCode(string shortCode); // Return DTO
    }
}
