using Entities.Models;
using Interfaces.Base;
using Interfaces.Helpers;
using Interfaces.ViewModels.ArticleVM;
using Interfaces.ViewModels.ShortUrlVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.interfaces
{
    public interface IShortUrl : ICoreBase
    {
        Task<List<GetShortUrlViewModel>> GetAll();
        Task<SaveShortUrlViewModel> Add(SaveShortUrlViewModel model);
        Task<bool> Delete(int id);
        Task<ShortUrl> GetById(int id);
        Task<ShortUrl> GetByShortenUrl(string id);
        Task<ShortUrl> GetByShortCode(string id);
    }
}
