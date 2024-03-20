
namespace Polius2007Test
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            binanceTitleLabl = new Label();
            bybitTitleLabl = new Label();
            kucoinTiteLabl = new Label();
            binanceTickerLabl = new Label();
            bybitTickerLabl = new Label();
            kucoinTickerLabl = new Label();
            tickersComboBox = new ComboBox();
            SuspendLayout();
            // 
            // binanceTitleLabl
            // 
            binanceTitleLabl.AutoSize = true;
            binanceTitleLabl.Location = new Point(10, 80);
            binanceTitleLabl.Name = "binanceTitleLabl";
            binanceTitleLabl.Size = new Size(49, 15);
            binanceTitleLabl.TabIndex = 0;
            binanceTitleLabl.Text = "Binance";
            // 
            // bybitTitleLabl
            // 
            bybitTitleLabl.AutoSize = true;
            bybitTitleLabl.Location = new Point(10, 120);
            bybitTitleLabl.Name = "bybitTitleLabl";
            bybitTitleLabl.Size = new Size(34, 15);
            bybitTitleLabl.TabIndex = 1;
            bybitTitleLabl.Text = "Bybit";
            // 
            // kucoinTiteLabl
            // 
            kucoinTiteLabl.AutoSize = true;
            kucoinTiteLabl.Location = new Point(10, 160);
            kucoinTiteLabl.Name = "kucoinTiteLabl";
            kucoinTiteLabl.Size = new Size(44, 15);
            kucoinTiteLabl.TabIndex = 2;
            kucoinTiteLabl.Text = "Kucoin";
            // 
            // binanceTickerLabl
            // 
            binanceTickerLabl.AutoSize = true;
            binanceTickerLabl.Location = new Point(80, 80);
            binanceTickerLabl.Name = "binanceTickerLabl";
            binanceTickerLabl.Size = new Size(0, 15);
            binanceTickerLabl.TabIndex = 4;
            // 
            // bybitTickerLabl
            // 
            bybitTickerLabl.AutoSize = true;
            bybitTickerLabl.Location = new Point(80, 120);
            bybitTickerLabl.Name = "bybitTickerLabl";
            bybitTickerLabl.Size = new Size(0, 15);
            bybitTickerLabl.TabIndex = 5;
            // 
            // kucoinTickerLabl
            // 
            kucoinTickerLabl.AutoSize = true;
            kucoinTickerLabl.Location = new Point(80, 160);
            kucoinTickerLabl.Name = "kucoinTickerLabl";
            kucoinTickerLabl.Size = new Size(0, 15);
            kucoinTickerLabl.TabIndex = 6;
            // 
            // tickersComboBox
            // 
            tickersComboBox.DisplayMember = "0";
            tickersComboBox.FormattingEnabled = true;
            tickersComboBox.Location = new Point(10, 20);
            tickersComboBox.Name = "tickersComboBox";
            tickersComboBox.Size = new Size(99, 23);
            tickersComboBox.TabIndex = 8;
            tickersComboBox.SelectedIndexChanged += TickersComboBox_SelectedIndexChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(259, 198);
            Controls.Add(tickersComboBox);
            Controls.Add(kucoinTickerLabl);
            Controls.Add(bybitTickerLabl);
            Controls.Add(binanceTickerLabl);
            Controls.Add(kucoinTiteLabl);
            Controls.Add(bybitTitleLabl);
            Controls.Add(binanceTitleLabl);
            Name = "MainForm";
            Text = "Tickers";
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion

        private Label binanceTitleLabl;
        private Label bybitTitleLabl;
        private Label kucoinTiteLabl;
        private Label binanceTickerLabl;
        private Label bybitTickerLabl;
        private Label kucoinTickerLabl;
        private ComboBox tickersComboBox;
    }
}
