using Bybit.Net.Clients;
using Bybit.Net.Objects.Models.Spot.v3;
using CryptoExchange.Net.Objects.Sockets;
using static Polius2007Test.BybitDataProvider;

namespace Polius2007Test
{
    public interface IBybitDataProvider
    {
        event EventHandler<OnDataChangedEventArgs> OnDataChanged;
        Task Start(BybitSpotSymbolV3 symbol);
        Task UpdateSymbol(BybitSpotSymbolV3 symbol);
        Task Stop();
    }

    public class BybitDataProvider : IBybitDataProvider
    {
        public event EventHandler<OnDataChangedEventArgs> OnDataChanged;
        public class OnDataChangedEventArgs : EventArgs
        {
            public required DataEvent<Bybit.Net.Objects.Models.V5.BybitSpotTickerUpdate> Data;
        }
        public BybitSpotSymbolV3[] Symbols;
        private readonly BybitSocketClient _socketClient;
        private UpdateSubscription _subscription;

        public BybitDataProvider()
        {
            _socketClient = new BybitSocketClient();

            LoadSymbols().Wait();
            Start(Symbols.First()).Wait();
        }

        public async Task Start(BybitSpotSymbolV3 symbol)
        {
            var subResult = await _socketClient.V5SpotApi.SubscribeToTickerUpdatesAsync(symbol.Name, data =>
            {
                OnDataChanged?.Invoke(this, new OnDataChangedEventArgs() { Data = data });
            });

            if (subResult.Success)
                _subscription = subResult.Data;
        }

        public async Task UpdateSymbol(BybitSpotSymbolV3 symbol)
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
            using BybitRestClient socketClient = new();
            var result = await socketClient.SpotApiV3.ExchangeData.GetSymbolsAsync();
            if (!result.Success)
                throw new Exception("Error Load symbols");
            else
                Symbols = result.Data.OrderBy(o => o.Name).ToArray();
        }
    }
}
