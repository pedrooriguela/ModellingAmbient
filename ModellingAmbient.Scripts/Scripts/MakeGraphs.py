import pandas as pd
import plotly.graph_objects as go

def plot_ticker_adjusted_price(tickers_df: pd.DataFrame, ticker_name):
    dates = tickers_df["Date"]
    values = tickers_df[ticker_name]

    cor_fundo = '#131314'
    cor_linha = '#00E5FF'
    cor_eixos = '#555555'
    cor_texto = '#AAAAAA'

    fig = go.Figure()

    # Adiciona a linha ao gráfico e um preenchimento gradiente abaixo dela
    fig.add_trace(go.Scatter(
        x=dates,
        y=values,
        mode='lines',
        line=dict(color=cor_linha, width=2),
        fill='tozeroy',
        fillcolor='rgba(0, 229, 255, 0.1)',
        name=ticker_name
    ))

    # Configurações de layout semelhantes ao do matplotlib original
    fig.update_layout(
        plot_bgcolor=cor_fundo,
        paper_bgcolor=cor_fundo,
        font=dict(color=cor_texto),
        xaxis=dict(
            showgrid=True,
            gridcolor='#2A2A2B',
            gridwidth=0.5,
            griddash='dash',
            linecolor=cor_eixos,
            tickcolor=cor_texto,
            tickformat='%Y-%m-%d'
        ),
        yaxis=dict(
            showgrid=True,
            gridcolor='#2A2A2B',
            gridwidth=0.5,
            griddash='dash',
            linecolor=cor_eixos,
            tickcolor=cor_texto
        ),
        margin=dict(l=40, r=40, t=40, b=40),
        showlegend=False
    )

    # Salva o gráfico como PNG
    fig.write_image(f'C:\\Users\\origu\\RiderProjects\\ModellingAmbient\\ModellingAmbient.Core\\Csvs\\{ticker_name}.png')

    # Retorna o gráfico como uma string HTML
    return fig.to_html(full_html=False, include_plotlyjs='cdn')

