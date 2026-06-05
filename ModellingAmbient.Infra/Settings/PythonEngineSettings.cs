using System;
using System.Collections.Generic;
using System.Text;

namespace ModellingAmbient.Infra.Settings;

public class PythonEngineSettings
{
    public const string SectionName = "PythonEngine";
    public string PythonDllPath { get; set; } = "C:\\Users\\origu\\AppData\\Local\\Python\\pythoncore-3.14-64\\python314.dll";
    public string ScriptsRelativePath { get; set; } = "..\\..\\..\\..\\ModellingAmbient.Scripts\\Scripts";
    public string VenvRelativePath { get; set; } = "..\\venv\\Lib\\site-packages";
}
