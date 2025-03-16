using widebot.Models;
using widebot.pagination;
using widebot.DTOs;

namespace widebot.interfaces
{
    public interface IArticlecs 
    {
        Task<List<ArticlesGetDto>> GetAll();
        Task<List<ArticlesGetDto>> GetWithFilltering(string fillterOn, string fillterQuery);
        Task<PagedList<ArticlesGetDto>> GetArticlesWithPagination(int pageNumber, int pageSize);
        Task<ArticlesCreateDto> Add(ArticlesCreateDto model);
        Task<bool> Update(ArticlesUpdateDto model, int id);
        Task<bool> Delete(int id);
        Article? GetById(int id);
        Task<ArticlesGetDto?> GetDetailsById(int id);
    }
}
