using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ModellingAmbient.Infra.Interfaces;
using ModellingAmbient.Infra.Settings;
using Python.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModellingAmbient.Infra.Services;

public class PythonEngineService : IPythonEngineService
{
    private readonly PythonEngineSettings _settings;
    private readonly ILogger<PythonEngineService> _logger;
    private readonly string _scriptsPath;
    private readonly string _venvPath;
    private bool _disposed;
    private readonly Dictionary<string, dynamic> _moduleCache = new();

    public PythonEngineService(
        IOptions<PythonEngineSettings> settings,
        ILogger<PythonEngineService> logger)
    {
        _settings = settings.Value;
        _logger = logger;

        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        _scriptsPath = Path.GetFullPath(Path.Combine(baseDir, _settings.ScriptsRelativePath));
        _venvPath = Path.GetFullPath(Path.Combine(_scriptsPath, _settings.VenvRelativePath));

        InitializeEngine();
    }

    private void InitializeEngine()
    {
        if (PythonEngine.IsInitialized)
        {
            _logger.LogDebug("PythonEngine is already initialized. Skipping...");
            return;
        }

        _logger.LogDebug("Initializing PythonEngine...");
        Runtime.PythonDLL = _settings.PythonDllPath;
        PythonEngine.Initialize();

        using (Py.GIL())
        {
            dynamic sys = Py.Import("sys");
            var currentPaths = new PyList(sys.path).Select(p => p.ToString()).ToList();
            if (!currentPaths.Contains(_scriptsPath))
                sys.path.append(_scriptsPath);

            if (!currentPaths.Contains(_venvPath))
                sys.path.append(_venvPath);
        }
    }

    public dynamic GetModule(string moduleName)
    {
        if (_moduleCache.TryGetValue(moduleName, out var cached))
            return cached;

        using (Py.GIL())
        {
            _logger.LogDebug("Importing python module: {Module}", moduleName);
            var module = Py.Import(moduleName);
            _moduleCache[moduleName] = module;
            return module;
        }
    }

    public void Execute(Action<dynamic> action)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        try
        {
            using (Py.GIL())
            {
                action(this);
            }
        }
        catch (PythonException ex)
        {
            _logger.LogError(ex, "Python error: {Type} - {Message}", ex.Type, ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error when executing python module");
            throw;
        }
    }

    public T Execute<T>(Func<dynamic, T> func)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        try
        {
            using (Py.GIL())
            {
                return func(this);
            }
        }
        catch (PythonException ex)
        {
            _logger.LogError(ex, "Python error: {Type} - {Message}", ex.Type, ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error when executing python module");
            throw;
        }
    }

    public void Dispose()
    {
        if(_disposed) return;
        _logger.LogInformation("Shutting down PythonEngine");
        foreach (var module in _moduleCache.Values)
        {
            module?.Dispose();
        }
        _moduleCache.Clear();
        if(PythonEngine.IsInitialized)
            PythonEngine.Shutdown();
        _disposed = true;
    }
}
