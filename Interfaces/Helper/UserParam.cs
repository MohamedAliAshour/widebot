using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Helpers
{
    public class UserParam
    {
        public string Language { get; set; } = "ar";       
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 0;

        private int _pageSize = 10;

        public string? Key { get; set; }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}
