using CryptoExchange.Net.Objects.Sockets;
using Kucoin.Net.Clients;
using Kucoin.Net.Interfaces.Clients;
using static Polius2007Test.KucoinDataProvider;

namespace Polius2007Test
{
    public interface IKucoinDataProvider
    {
        public event EventHandler<OnDataChangedEventArgs> OnDataChanged;
        Task Start(string symbol);
        Task UpdateSymbol(string symbol);
        Task Stop();
    }

    public class KucoinDataProvider : IKucoinDataProvider
    {
        public event EventHandler<OnDataChangedEventArgs> OnDataChanged;
        public class OnDataChangedEventArgs
        {
            public DataEvent<Kucoin.Net.Objects.Models.Spot.Socket.KucoinStreamTick> Data;
        }
        private IKucoinSocketClient _socketClient;
        private UpdateSubscription _subscription;
        public string[] Symbols =
        [
            "BTC-USDT",
            "ETH-USDT"
        ];

        public KucoinDataProvider()
        {
            _socketClient = new KucoinSocketClient();

            Start(Symbols[0]).Wait();
        }

        public async Task Start(string symbol)
        {
            var subResult = await _socketClient.SpotApi.SubscribeToTickerUpdatesAsync(symbol, data =>
            {
                OnDataChanged?.Invoke(this, new() { Data = data });
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
