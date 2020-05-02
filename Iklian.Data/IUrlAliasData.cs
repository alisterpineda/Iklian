using System;
using System.Collections.Generic;
using System.Text;
using Iklian.Core;

namespace Iklian.Data
{
    public interface IUrlAliasData
    {
        UrlAlias Create(UrlAlias urlAlias);
        UrlAlias GetUrlAliasFromAlias(string alias);
        UrlAlias Update(UrlAlias urlAlias);
        int Commit();
    }
}
