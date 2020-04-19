using System;
using System.Collections.Generic;
using System.Text;
using Iklian.Core;

namespace Iklian.Data
{
    public class ShortUrlData : IShortUrlData
    {
        private readonly IklianDbContext _db;

        public ShortUrlData(IklianDbContext dbContext)
        {
            _db = dbContext;
        }

        public ShortUrl Add(ShortUrl shortUrl)
        {
            _db.ShortUrls.Add(shortUrl);
            return shortUrl;
        }

        public int Commit()
        {
            return _db.SaveChanges();
        }
    }
}
