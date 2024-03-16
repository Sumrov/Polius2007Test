using Binance.Net.Clients;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects.Sockets;
using static Polius2007Test.BinanceDataProvider;

namespace Polius2007Test
{
    public interface IBinanceDataProvider
    {
        public event EventHandler<OnDataChangedEventArgs> OnDataChanged;
        Task Start(string symbol);
        Task UpdateSymbol(string symbol);
        Task Stop();
    }

    public class BinanceDataProvider : IBinanceDataProvider
    {
        public event EventHandler<OnDataChangedEventArgs> OnDataChanged;
        public class OnDataChangedEventArgs
        {
            public DataEvent<IBinanceTick> Data;
        }
        private IBinanceSocketClient _socketClient;
        private UpdateSubscription _subscription;
        public string[] Symbols =
        [
            "BTCUSDT",
            "ETHUSDT"
        ];

        public BinanceDataProvider()
        {
            _socketClient = new BinanceSocketClient();

            Start(Symbols[0]).Wait();
        }

        public async Task Start(string symbol)
        {
            var subResult = await _socketClient.SpotApi.ExchangeData.SubscribeToTickerUpdatesAsync(symbol, data =>
            {
                OnDataChanged?.Invoke(this, new OnDataChangedEventArgs() { Data = data });
            });

            if (subResult.Success)
                _subscription = subResult.Data;
        }

        public async Task UpdateSymbol(string symbol)
        {
            await Stop();
            await Start(symbol);
        }

        public async Task Stop()
        {
            await _socketClient.UnsubscribeAsync(_subscription);
        }
    }
}
