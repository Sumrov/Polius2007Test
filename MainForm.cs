namespace Polius2007Test
{
    public partial class MainForm : Form
    {
        private BinanceDataProvider _binanceDataProvider = new();
        private BybitDataProvider _bybitDataProvider = new();
        private KucoinDataProvider _kucoinDataProvider = new();

        public MainForm()
        {
            InitializeComponent();
            Subscribe();
            FillTickersComboBox();
            tickersComboBox.SelectedIndex = 0;
        }

        private void KucoinDataProvider_OnDataChanged(object? sender, KucoinDataProvider.OnDataChangedEventArgs e)
        {
            UpdateTickerLabel(kucoinTickerLabl, e.Data.Data.LastPrice.ToString());
        }

        private void BybitDataProvider_OnDataChanged(object? sender, BybitDataProvider.OnDataChangedEventArgs e)
        {
            UpdateTickerLabel(bybitTickerLabl, e.Data.Data.LastPrice.ToString());
        }

        private void BinanceDataProvider_OnDataChanged(object? sender, BinanceDataProvider.OnDataChangedEventArgs e)
        {
            UpdateTickerLabel(binanceTickerLabl, e.Data.Data.LastPrice.ToString());
        }

        private async void TickersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is not ComboBox comboBox)
                return;
            string? symbolName = comboBox.SelectedItem?.ToString();
            if (symbolName == null)
                return;

            var binanceSymbol = _binanceDataProvider.Symbols.Single(o => o.BaseAsset + o.QuoteAsset == symbolName);
            await _binanceDataProvider.UpdateSymbol(binanceSymbol);

            var bybitSymbol = _bybitDataProvider.Symbols.Single(o => o.BaseAsset + o.QuoteAsset == symbolName);
            await _bybitDataProvider.UpdateSymbol(bybitSymbol);

            var kucoinSymbol = _kucoinDataProvider.Symbols.Single(o => o.BaseAsset + o.QuoteAsset == symbolName);
            await _kucoinDataProvider.UpdateSymbol(kucoinSymbol);
        }

        private void FillTickersComboBox()
        {
            string[] binanceSymbols = _binanceDataProvider.Symbols
                .Select(o => o.BaseAsset + o.QuoteAsset)
                .ToArray();

            string[] bybitSymbols = _bybitDataProvider.Symbols
                .Select(o => o.BaseAsset + o.QuoteAsset)
                .ToArray();

            string[] kucoinSymbols = _kucoinDataProvider.Symbols
                .Select(o => o.BaseAsset + o.QuoteAsset)
                .ToArray();

            string[] commonSymbols = binanceSymbols
                .Intersect(bybitSymbols)
                .Intersect(kucoinSymbols)
                .ToArray();

            tickersComboBox.Items.AddRange(commonSymbols);
        }

        private void Subscribe()
        {
            _binanceDataProvider.OnDataChanged += BinanceDataProvider_OnDataChanged;
            _bybitDataProvider.OnDataChanged += BybitDataProvider_OnDataChanged;
            _kucoinDataProvider.OnDataChanged += KucoinDataProvider_OnDataChanged;
        }

        private void UpdateTickerLabel(Label label, string value)
        {
            if (label.InvokeRequired)
            {
                label.BeginInvoke(new Action(() =>
                {
                    label.Text = value;
                }));
            }
            else
            {
                label.Text = value;
            }
        }
    }
}
