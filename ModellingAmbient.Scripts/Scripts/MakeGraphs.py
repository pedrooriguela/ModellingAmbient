from multiprocessing import Value

from matplotlib.lines import lineStyles
import matplotlib.pyplot as plt
import matplotlib.dates as mdates
import pandas as pd
import numpy as np

def plot_ticker_adjusted_price(tickers_df: pd.DataFrame, ticker_name):
    dates = tickers_df["Date"]
    values = tickers_df[ticker_name]

    cor_fundo = '#131314'
    cor_linha = '#00E5FF'
    cor_eixos = '#555555'
    cor_texto = '#AAAAAA'

    fig, ax = plt.subplots()
    fig.patch.set_facecolor(cor_fundo)
    ax.set_facecolor(cor_fundo)

    ax.plot(dates, values)
    ax.xaxis.set_major_formatter(mdates.DateFormatter('%Y-%m-%d'))
    ax.xaxis.set_major_locator(mdates.DayLocator(interval=30))

    y_min = values.min()*0.95
    n_camadas = 50

    for i in np.linspace(0, 1, n_camadas):
        y_bottom = y_min + (values-y_min)*i
        ax.fill_between(dates, y_bottom, values, color=cor_linha, alpha=0.03, lw=0)

        ax.spines['top'].set_visible(False)
        ax.spines['right'].set_visible(False)
        ax.spines['left'].set_color(cor_eixos)
        ax.spines['bottom'].set_color(cor_eixos)

        ax.tick_params(color=cor_texto, which='both')
        ax.xaxis.label.set_color(cor_texto)
        ax.yaxis.label.set_color(cor_texto)

        ax.grid(color='#2A2A2B', linestyle='--', linewidth=0.5, alpha=0.5)

        plt.tight_layout()
        plt.plot()

        plt.savefig('C:\\Users\\origu\\RiderProjects\\ModellingAmbient\\ModellingAmbient.Core\\Csvs')
