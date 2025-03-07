using Entities.Models;
using Interfaces.Base;
using Interfaces.Helpers;
using Interfaces.ViewModels.ArticleVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.interfaces
{
    public interface IArticlecs : ICoreBase
    {
        Task<List<GetArticlesViewModel>> GetAll();
        Task<List<GetArticlesViewModel>> GetWithFilltering(string fillterOn, string fillterQuery);
        Task<SaveArticlesViewModel> Add(SaveArticlesViewModel model);
        Task<bool> Update(SaveArticlesViewModel model, int id);
        Task<bool> Delete(int id);
        Task<bool> CheckNameExist(string name);
        Article GetById(int id);
        Task<SaveArticlesViewModel> GetDetailsById(int id);
    }
}
