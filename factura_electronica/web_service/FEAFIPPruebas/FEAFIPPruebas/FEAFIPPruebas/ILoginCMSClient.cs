﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEAFIPPruebas
{
    public interface ILoginCMSClient : IDisposable
    {
        string loginCms (string param);
        void Close(); 
        
    }
}
