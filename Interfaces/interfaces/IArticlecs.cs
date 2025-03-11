using Entities.Models;
using Interfaces.pagination;
using Interfaces.DTOs;

namespace Interfaces.interfaces
{
    public interface IArticlecs 
    {
        Task<List<ArticlesDto>> GetAll();
        Task<List<ArticlesDto>> GetWithFilltering(string fillterOn, string fillterQuery);
        Task<PagedList<ArticlesDto>> GetArticlesWithPagination(int pageNumber, int pageSize);
        Task<ArticlesDto> Add(ArticlesDto model);
        Task<bool> Update(ArticlesDto model, int id);
        Task<bool> Delete(int id);
        Task<bool> CheckNameExist(string name);
        Article GetById(int id);
        Task<ArticlesDto> GetDetailsById(int id);
    }
}
