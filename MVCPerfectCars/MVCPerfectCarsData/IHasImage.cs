using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPerfectCarsData
{
    public interface IHasImage
    {
        string Image { get; set; }

        string SafeImage { get; }

        IFormFile ImageFile { get; set; }

    }
}



