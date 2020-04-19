using System;
using System.Collections.Generic;
using System.Text;
using Iklian.Core;

namespace Iklian.Data
{
    public interface IShortUrlData
    {
        ShortUrl Add(ShortUrl shortUrl);
        int Commit();
    }
}
