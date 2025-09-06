using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nia.Application.IServices
{
    public interface IImageService
    {
        (byte[] FileBytes, string ContentType)? GetImage(string fileName);
    }
}
