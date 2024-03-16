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
            _binanceDataProvider.OnDataChanged += BinanceDataProvider_OnDataChanged;
            _bybitDataProvider.OnDataChanged += BybitDataProvider_OnDataChanged;
            _kucoinDataProvider.OnDataChanged += KucoinDataProvider_OnDataChanged;
            tickersComboBox.SelectedIndex = 0;

        }

        private void KucoinDataProvider_OnDataChanged(object? sender, KucoinDataProvider.OnDataChangedEventArgs e)
        {
            if (kucoinTickerLabl.InvokeRequired)
            {
                BybitTickerLabl.BeginInvoke(new Action(() =>
                {
                    kucoinTickerLabl.Text = e.Data.Data.LastPrice.ToString();
                }));
            }
            else
            {
                kucoinTickerLabl.Text = e.Data.Data.LastPrice.ToString();
            }
        }

        private void BybitDataProvider_OnDataChanged(object? sender, BybitDataProvider.OnDataChangedEventArgs e)
        {
            if (BybitTickerLabl.InvokeRequired)
            {
                BybitTickerLabl.BeginInvoke(new Action(() =>
                {
                    BybitTickerLabl.Text = e.Data.Data.LastPrice.ToString();
                }));
            }
            else
            {
                BybitTickerLabl.Text = e.Data.Data.LastPrice.ToString();
            }
        }

        private void BinanceDataProvider_OnDataChanged(object? sender, BinanceDataProvider.OnDataChangedEventArgs e)
        {
            if (binanceTickerLabl.InvokeRequired)
            {
                binanceTickerLabl.BeginInvoke(new Action(() =>
                {
                    binanceTickerLabl.Text = e.Data.Data.LastPrice.ToString();
                }));
            }
            else
            {
                binanceTickerLabl.Text = e.Data.Data.LastPrice.ToString();
            }
        }

        private async void TickersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (sender as ComboBox)!;

            await _binanceDataProvider.UpdateSymbol(_binanceDataProvider.Symbols[comboBox.SelectedIndex]);
            await _bybitDataProvider.UpdateSymbol(_bybitDataProvider.Symbols[comboBox.SelectedIndex]);
            await _kucoinDataProvider.UpdateSymbol(_kucoinDataProvider.Symbols[comboBox.SelectedIndex]);
        }
    }
}
