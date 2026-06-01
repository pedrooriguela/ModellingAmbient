import pandas as pd
import yfinance as yf
import numpy as np

def __get_raw_data() -> pd.DataFrame:
    path = 'C:\\Users\\origu\\RiderProjects\\ModellingAmbient\\ModellingAmbient.Core\\Csvs\\tickers.csv'
    tickers_list = pd.read_csv(path)['ticker'].dropna().unique().tolist()
    tickers_list = [t for t in tickers_list if t not in {'^BVSP', 'IBOV', 'IBOV11.SA'}]
    raw = yf.download(tickers_list, start='2010-01-01', end='2025-12-31', auto_adjust=False)
    if raw is None:
        raise Exception('Não foi possivel ler dados de yfinance')
    else:
        return raw


def get_b3_data():
    adj_close = __get_raw_data()['Adj Close'].copy()
    if isinstance(adj_close, pd.Series):
        adj_close_data: pd.DataFrame = adj_close.to_frame()
    else:
        adj_close_data: pd.DataFrame = adj_close
    for col in adj_close_data.columns:
        s = adj_close_data[col].copy()
        adj_close_data[col] = __clean_adj_close_data(s)
    adj_close_data.reset_index().to_csv('C:\\Users\\origu\\RiderProjects\\ModellingAmbient\\ModellingAmbient.Core\\Csvs\\b3-2010-2025.csv', index=False)



def __clean_adj_close_data(s, window=60, thresh=5):
    logarithmic_return = pd.Series(np.log(s / s.shift(1)))
    rolling_median_return = logarithmic_return.rolling(window, min_periods=20).median()
    rolling_mad_return = ((logarithmic_return - rolling_median_return)
                        .abs()
                        .rolling(window, min_periods=20)
                        .median()
                        )
    rolling_mad_return = rolling_mad_return.replace(0, np.nan).bfill()
    mask = (logarithmic_return - rolling_median_return).abs() / (1.4826 * rolling_mad_return) > thresh
    return s.mask(mask).ffill()
