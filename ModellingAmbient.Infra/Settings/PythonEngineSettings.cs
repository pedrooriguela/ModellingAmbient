using System;
using System.Collections.Generic;
using System.Text;

namespace ModellingAmbient.Infra.Settings;

public class PythonEngineSettings
{
    public const string SectionName = "PythonEngineSettings";
    public string PythonDllPath { get; set; }
    public string ScriptsRelativePath { get; set; }
    public string VenvRelativePath { get; set; }
}
