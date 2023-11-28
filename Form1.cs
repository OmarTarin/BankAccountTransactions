
using Microsoft.Extensions.Logging;

namespace BankingEvaluation
{
    public partial class Form1 : Form
    {
        private readonly IService _service;
        private readonly ILogger _logger;


        public Form1(IService service)
        {
            _service = service;
            _logger = _service.LoggerFactory<Form1>();

            _logger.LogInformation("start programm");

            InitializeComponent();
        }

        
    }


}