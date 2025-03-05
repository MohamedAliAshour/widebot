using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Base
{
    public interface ICoreBase
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        bool SaveSingleImage(string root, string img, out string fileName);
        //bool SaveSingleImageFormFile(string root, IFormFile img, out string fileName);
        bool SaveMultiImage(string root, List<string> imgs, out List<string> fileName);
        //bool SaveMultiImageFormFileAsync(string root, List<IFormFile> imgs, out List<string> fileName);
        string GenerateRandomCode();
        string GenerateRandomCodeAsNumber();
    }

}
