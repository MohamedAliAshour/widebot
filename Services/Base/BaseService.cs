using Entities.Models;
using Interfaces.Base;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Base
{
    public class BaseService : ICoreBase
    {
        private readonly DataContext _context;

        public BaseService(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public string GenerateRandomCode()
        {
            throw new NotImplementedException();
        }

        public string GenerateRandomCodeAsNumber()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAll()
        {
            throw new NotImplementedException();
        }

        public bool SaveMultiImage(string root, List<string> imgs, out List<string> fileName)
        {
            throw new NotImplementedException();
        }

        //public bool SaveMultiImageFormFileAsync(string root, List<IFormFile> imgs, out List<string> fileName)
        //{
        //    throw new NotImplementedException();
        //}

        public bool SaveSingleImage(string root, string img, out string fileName)
        {
            throw new NotImplementedException();
        }

        //public bool SaveSingleImageFormFile(string root, IFormFile img, out string fileName)
        //{
        //    throw new NotImplementedException();
        //}

        //public virtual async Task<T> Add<T>(T model, int userId) where T : class
        //{
        //    throw new NotImplementedException();
        //}

        //public virtual async Task<bool> CheckNameExist(string name)
        //{
        //    throw new NotImplementedException();
        //}

        //public virtual async Task<bool> Delete(int id, int userId)
        //{
        //    throw new NotImplementedException();
        //}

        //public virtual T GetById<T>(int id) where T : class
        //{
        //    throw new NotImplementedException();
        //}

        //public virtual async Task<T> GetDetailsById<T>(int id) where T : class
        //{
        //    throw new NotImplementedException();
        //}

        ////public virtual async Task<PagedList<T>> GetWithPaginations<T>(UserParam param) where T : class
        ////{
        ////    throw new NotImplementedException();
        ////}

        //public virtual async Task<bool> Update<T>(T model, int id) where T : class
        //{
        //    throw new NotImplementedException();
        //}
    }
}
