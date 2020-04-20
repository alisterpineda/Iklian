using System;
using System.Collections.Generic;
using System.Text;
using Iklian.Core;

namespace Iklian.Data
{
    public interface IUrlAliasData
    {
        UrlAlias Add(UrlAlias urlAlias);
        UrlAlias Update(UrlAlias urlAlias);
        int Commit();
    }
}
