﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameON.Common.Services
{
    public interface IGeolocatorService
    {
        double Latitude { get; set; }

        double Longitude { get; set; }

        Task GetLocationAsync();
    }

}
