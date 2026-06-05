 using ModellingAmbient.Infra.Services;
using ModellingAmbient.Core.Interfaces;
using ModellingAmbient.Infra.Interfaces;

namespace ModellingAmbient.Core.Repositories;

public class B3Repository : IB3Repository
{
    private readonly IPythonEngineService _pythonEngineService;
    public B3Repository(IPythonEngineService pythonEngineService)
    {
        _pythonEngineService = pythonEngineService;
    }

    public void GetB3Data()
    {
        _pythonEngineService.Execute(ctx =>
        {
            dynamic dataScript = ((PythonEngineService)ctx).GetModule("GetData");
            dataScript.get_b3_data();
        });
    }

    public void GetTickerAdjCloseData(string ticker_name)
    {
        _pythonEngineService.Execute(ctx =>
        {
            dynamic dataScript = ((PythonEngineService)ctx).GetModule("GetData");
            dataScript.get_adj_close_data_by_name(ticker_name);
        });
    }

    public void CreatePlot(string ticker_name) 
    {
        _pythonEngineService.Execute(ctx =>
        {
            dynamic dataScript = ((PythonEngineService)ctx).GetModule("GetData");
            dynamic graphScript = ((PythonEngineService)ctx).GetModule("MakeGraphs");
            dynamic ticker_column = dataScript.get_adj_close_data_by_name(ticker_name);
            dynamic df = dataScript.get_df();
            graphScript.plot_ticker_adjusted_price(df, ticker_name);

        });
    }
}