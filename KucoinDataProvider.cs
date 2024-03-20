using CryptoExchange.Net.Objects.Sockets;
using Kucoin.Net.Clients;
using Kucoin.Net.Objects.Models.Spot;
using static Polius2007Test.KucoinDataProvider;

namespace Polius2007Test
{
    public interface IKucoinDataProvider
    {
        public event EventHandler<OnDataChangedEventArgs> OnDataChanged;
        Task Start(KucoinSymbol symbol);
        Task UpdateSymbol(KucoinSymbol symbol);
        Task Stop();
    }

    public class KucoinDataProvider : IKucoinDataProvider
    {
        public event EventHandler<OnDataChangedEventArgs> OnDataChanged;
        public class OnDataChangedEventArgs : EventArgs
        {
            public required DataEvent<Kucoin.Net.Objects.Models.Spot.Socket.KucoinStreamTick> Data;
        }
        public KucoinSymbol[] Symbols;
        private readonly KucoinSocketClient _socketClient;
        private UpdateSubscription _subscription;

        public KucoinDataProvider()
        {
            _socketClient = new KucoinSocketClient();

            LoadSymbols().Wait();
            Start(Symbols.First()).Wait();
        }

        public async Task Start(KucoinSymbol symbol)
        {
            var subResult = await _socketClient.SpotApi.SubscribeToTickerUpdatesAsync(symbol.Name, data =>
            {
                OnDataChanged?.Invoke(this, new() { Data = data });
            });

            if (subResult.Success)
                _subscription = subResult.Data;
        }

        public async Task UpdateSymbol(KucoinSymbol symbol)
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
            using KucoinRestClient socketClient = new();
            var result = await socketClient.SpotApi.ExchangeData.GetSymbolsAsync();
            if (!result.Success)
                throw new Exception("Error Load symbols");
            else
                Symbols = result.Data.OrderBy(o => o.Name).ToArray();
        }
    }
}
