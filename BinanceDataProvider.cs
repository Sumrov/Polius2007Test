using Binance.Net.Clients;
using Binance.Net.Interfaces;
using Binance.Net.Objects.Models.Spot;
using CryptoExchange.Net.Objects.Sockets;
using static Polius2007Test.BinanceDataProvider;

namespace Polius2007Test
{
    public interface IBinanceDataProvider
    {
        public event EventHandler<OnDataChangedEventArgs> OnDataChanged;
        Task Start(BinanceSymbol symbol);
        Task UpdateSymbol(BinanceSymbol symbol);
        Task Stop();
    }

    public class BinanceDataProvider : IBinanceDataProvider
    {
        public event EventHandler<OnDataChangedEventArgs> OnDataChanged;
        public class OnDataChangedEventArgs : EventArgs
        {
            public required DataEvent<IBinanceTick> Data;
        }
        public BinanceSymbol[] Symbols;
        private readonly BinanceSocketClient _socketClient;
        private UpdateSubscription _subscription;

        public BinanceDataProvider()
        {
            _socketClient = new BinanceSocketClient();

            LoadSymbols().Wait();
            Start(Symbols.First()).Wait();
        }

        public async Task Start(BinanceSymbol symbol)
        {
            var subResult = await _socketClient.SpotApi.ExchangeData.SubscribeToTickerUpdatesAsync(symbol.Name, data =>
            {
                OnDataChanged?.Invoke(this, new OnDataChangedEventArgs() { Data = data });
            });

            if (subResult.Success)
                _subscription = subResult.Data;
        }

        public async Task UpdateSymbol(BinanceSymbol symbol)
        {
            await Stop();
            await Start(symbol);
        }

        public async Task Stop()
        {
            await _socketClient.UnsubscribeAsync(_subscription);
        }

        private async Task LoadSymbols()
        {
            using BinanceSocketClient socketClient = new();
            var result = await socketClient.SpotApi.ExchangeData.GetExchangeInfoAsync();
            if (!result.Success)
                throw new Exception("Error Load symbols");
            else
                Symbols = result.Data.Result.Symbols.OrderBy(o => o.Name).ToArray();
        }
    }
}
