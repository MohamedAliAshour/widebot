using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.ViewModels.ShortUrlVM
{
    public class GetShortUrlWithPaginationViewModel
    {
        public int Id { get; set; }

        public string LongUrl { get; set; }

        public string ShortUrl1 { get; set; }

        public string ShortCode { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
