using System;
using System.Collections.Generic;
using System.Text;

namespace oderme.Services
{
    public interface IGpsDependencyService
    {
        void OpenSettings();
        bool IsGpsEnable();
        void turnOnGps();
    }
}
