using widebot.Models;
using widebot.pagination;
using widebot.DTOs;

namespace widebot.interfaces
{
    public interface IArticlecs 
    {
        Task<List<ArticlesDto>> GetAll();
        Task<List<ArticlesDto>> GetWithFilltering(string fillterOn, string fillterQuery);
        Task<PagedList<ArticlesDto>> GetArticlesWithPagination(int pageNumber, int pageSize);
        Task<ArticlesDto> Add(ArticlesDto model);
        Task<bool> Update(ArticlesDto model, int id);
        Task<bool> Delete(int id);
        Article? GetById(int id);
        Task<ArticlesDto?> GetDetailsById(int id);
    }
}
