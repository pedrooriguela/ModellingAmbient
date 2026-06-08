using System;
using System.Collections.Generic;
using System.Text;

namespace ModellingAmbient.Infra.Interfaces;

public interface IPythonEngineService
{
    void Execute(Action<dynamic> action);
    T Execute<T>(Func<dynamic, T> func);
    string GetDllPath();
}
