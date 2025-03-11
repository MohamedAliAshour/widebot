using Interfaces.DTOs;

namespace Interfaces.interfaces
{
    public interface IShortUrl
    {
        Task<List<ShortUrlDto>> GetAll(); // DTO list for client/UI
        Task<ShortUrlDto> Add(ShortUrlDto model); // Return created DTO
        Task<bool> Delete(int id);
        Task<ShortUrlDto> GetById(int id); // Return DTO instead of entity
        Task<ShortUrlDto> GetByShortenUrl(string shortenUrl); // Return DTO
        Task<ShortUrlDto> GetByShortCode(string shortCode); // Return DTO
    }
}
