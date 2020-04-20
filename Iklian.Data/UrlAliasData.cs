using System;
using System.Collections.Generic;
using System.Text;
using Iklian.Core;

namespace Iklian.Data
{
    public class UrlAliasData : IUrlAliasData
    {
        private readonly IklianDbContext _db;

        public UrlAliasData(IklianDbContext dbContext)
        {
            _db = dbContext;
        }

        public UrlAlias Add(UrlAlias urlAlias)
        {
            _db.ShortUrls.Add(urlAlias);
            return urlAlias;
        }

        public int Commit()
        {
            return _db.SaveChanges();
        }
    }
}
