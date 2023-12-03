
using Microsoft.Extensions.Configuration;
using System.Globalization;
using BankingEvaluation.DbContext.Models;
using iTextSharp.text.pdf.parser;
using iTextSharp.text.pdf;
using System.Text.RegularExpressions;
using BankingEvaluation.DbContext;
using BankingEvaluation.Extensions;
using BankingEvaluation.Models;

namespace BankingEvaluation.Services
{
    internal class ReportImporter : IReportImporter
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;


        public ReportImporter(IConfiguration conf, IUnitOfWork unitOfWork)
        {
            _configuration = conf;
            _unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<ImportInfoViewModel>> Import()
        {
            var directoryPath = _configuration.ReportFiles();

            DirectoryInfo directory = new DirectoryInfo(directoryPath);

            var files = directory.EnumerateFiles()
                .Where(p => string.Equals(p.Extension, ".pdf"))
                .ToList();

            var importer = new PdfImporter();
            var importInfo = new List<ImportInfoViewModel>();

            foreach (var file in files)
            {
                var accounts = importer.Read(file);

                await _unitOfWork.AddAsync(accounts);
                await _unitOfWork.CommitAsync();

                importInfo.Add(accounts.ToImportInfo());

                file.Delete();
            }

            return importInfo;
        }


        private class PdfImporter
        {
            public ReportFileInfo Read(FileInfo file)
            {
                var content = new List<Account>();
                int numberOfPages;
                using (PdfReader reader = new PdfReader(file.FullName))
                {
                    numberOfPages = reader.NumberOfPages;
                    for (int page = 1; page <= reader.NumberOfPages; page++)
                    {
                        content.AddRange(ProcessText(PdfTextExtractor.GetTextFromPage(reader, page, new SimpleTextExtractionStrategy())));
                    }
                }

                return new ReportFileInfo()
                {
                    Name = file.Name,
                    Accounts = content,
                    NumberOfPages = numberOfPages,
                    NumberOfTransactions = content.Count
                };
            }

            private IEnumerable<Account> ProcessText(string pageText)
            {
                var interator = new Interator();

                using (TextReader reader = new StringReader(pageText))
                {
                    interator.Parse(reader);
                }

                return MappToAccount(interator.Packages);
            }
            
            private IEnumerable<Account> MappToAccount(IList<IEnumerable<string>> packages)
            {
                return packages.Select(p =>
                {
                    return new Account()
                    {
                        Date = DateTime.Parse(p.First(), CultureInfo.GetCultureInfo("de-DE"), DateTimeStyles.None),
                        Value = double.Parse(p.Last(), NumberStyles.Any, CultureInfo.GetCultureInfo("de-DE")),
                        Created = DateTime.Now,
                        Text = p.Skip(1).Take(p.Count() - 2).Select(q => new AccountText()
                        {
                            Item = q
                        }).ToList()
                    };
                });
            }

            private class Interator
            {
                public IList<IEnumerable<string>> Packages = new List<IEnumerable<string>>();
                string formatPattern = @"^\d{2}\.\d{2}\.\d{4}$";

                public void Parse(TextReader reader)
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (Regex.IsMatch(line, formatPattern))
                        {
                            Package(reader);
                        }
                    }
                }

                private void Package(TextReader reader)
                {
                    string line;
                    var package = new List<string>();

                    package.Add(reader.ReadLine());

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (Regex.IsMatch(line, formatPattern) || line.Contains("FinanzreportNr") || line.Contains("Neuer Saldo"))
                        {
                            Packages.Add(package);

                            if (line.Contains("Neuer Saldo"))
                                return;

                            Package(reader);

                            return;
                        }

                        package.Add(line);
                    }
                }
            }


        }
        
    }
}
