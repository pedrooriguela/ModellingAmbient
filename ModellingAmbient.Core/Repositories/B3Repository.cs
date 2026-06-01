using Python.Runtime;
using ModellingAmbient.Core.Interfaces;

namespace ModellingAmbient.Core.Repositories;

public class B3Repository : IB3Repository
{
    public B3Repository() {}

    public void GetB3Data()
    {
        Runtime.PythonDLL = @"C:\Users\origu\AppData\Local\Python\pythoncore-3.14-64\python314.dll";
        PythonEngine.Initialize();

        using (Py.GIL())
        {
            string pastaExecucao = AppDomain.CurrentDomain.BaseDirectory;
            string pastaScripts = Path.GetFullPath(Path.Combine(pastaExecucao, @"..\..\..\..\ModellingAmbient.Scripts/Scripts"));
            string pastaVenvLibs = Path.GetFullPath(Path.Combine(pastaScripts, @"..\venv\Lib\site-packages"));
            dynamic sys = Py.Import("sys");
            sys.path.append(pastaScripts);
            sys.path.append(pastaVenvLibs);

            dynamic dataScript = Py.Import("GetData");
            dataScript.get_b3_data();

        }


    }
}