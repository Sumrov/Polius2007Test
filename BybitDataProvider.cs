using Bybit.Net.Clients;
using Bybit.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects.Sockets;
using static Polius2007Test.BybitDataProvider;

namespace Polius2007Test
{
    public interface IBybitDataProvider
    {
        event EventHandler<OnDataChangedEventArgs> OnDataChanged;
        Task Start(string symbol);
        Task UpdateSymbol(string symbol);
        Task Stop();
    }

    public class BybitDataProvider : IBybitDataProvider
    {
        public event EventHandler<OnDataChangedEventArgs> OnDataChanged;
        public class OnDataChangedEventArgs
        {
            public DataEvent<Bybit.Net.Objects.Models.V5.BybitSpotTickerUpdate> Data;
        }
        private IBybitSocketClient _socketClient;
        private UpdateSubscription _subscription;
        public string[] Symbols =
        [
            "BTCUSDT",
            "ETHUSDT"
        ];

        public BybitDataProvider()
        {
            _socketClient = new BybitSocketClient();

            Start(Symbols[0]).Wait();
        }

        public async Task Start(string symbol)
        {
            var subResult = await _socketClient.V5SpotApi.SubscribeToTickerUpdatesAsync(symbol, data =>
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
