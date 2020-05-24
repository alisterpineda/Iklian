using System.Linq;
using Iklian.Core;
using Microsoft.EntityFrameworkCore;

namespace Iklian.Data
{
    public class UrlAliasData : IUrlAliasData
    {
        private readonly IklianDbContext _db;

        public UrlAliasData(IklianDbContext dbContext)
        {
            _db = dbContext;
        }

        public UrlAlias Create(UrlAlias urlAlias)
        {
            _db.UrlAliases.Add(urlAlias);
            return urlAlias;
        }

        public UrlAlias GetUrlAliasFromAlias(string alias)
        {
            return _db.UrlAliases.FirstOrDefault(x => x.Alias == alias);
        }

        public UrlAlias Update(UrlAlias urlAlias)
        {
            var entity = _db.UrlAliases.Attach(urlAlias);
            entity.State = EntityState.Modified;
            return urlAlias;
        }

        public int Commit()
        {
            return _db.SaveChanges();
        }
    }
}
