
using BankingEvaluation.DbContext;
using BankingEvaluation.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace BankingEvaluation
{
    internal partial class Form1 : Form
    {
        private readonly IService _service;
        private readonly ILogger _logger;


        public Form1(IService service)
        {
            _service = service;
            _logger = _service.LoggerFactory<Form1>();

            _logger.LogInformation("Start programm");

            InitializeComponent();
        }

        private void LoadButtonClick(object sender, EventArgs e)
        {
            var provider = _service.Provider.GetService<IReportProvider>();

            var from = dateTimePicker1.Value.Date;
            var to = dateTimePicker2.Value.Date;

            if (from > to)
            {
                MessageBox.Show("Start date must be bigger than to date", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            IEnumerable<string>? searchText = null;
            if (!string.IsNullOrEmpty(searchBox.Text))
                searchText = searchBox.Text.Split(";");

            var accounts = provider.LoadAccounts(from, to, searchText);

            accountViewModelBindingSource.DataSource = accounts;

            sumBox.Text = accounts.Sum(p => p.Value).ToString();

        }

        private async void loadReportButton_Click(object sender, EventArgs e)
        {
            var provider = _service.Provider.GetService<IReportImporter>();
            var importInfo = await provider.Import();

            string tooltipText = "Import Information:\n";
            foreach (var info in importInfo)
            {
                tooltipText += $"{info.Name}: Pages - {info.NumberOfPages}, Transactions - {info.NumberOfTransactions}\n";
            }
            
            MessageBox.Show(tooltipText, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }


}