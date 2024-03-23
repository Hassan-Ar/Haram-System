using Haram.RemittanceSystem.Currencies;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.VirtualFileSystem;
using SearchOption = System.IO.SearchOption;

namespace Haram.RemittanceSystem
{
    public class RemittanceDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Currency, Guid> _currencyrepository;
        private readonly IVirtualFileProvider _virtualFileProvider;
        public RemittanceDataSeederContributor(IRepository<Currency, Guid> currencyrepository, IVirtualFileProvider virtualFileProvider)
        {
            _currencyrepository = currencyrepository;
            _virtualFileProvider = virtualFileProvider;
        }

        public async Task SeedAsync(DataSeedContext context)
        {

            if (!await _currencyrepository.AnyAsync())
            {
                var file = _virtualFileProvider.GetFileInfo("codes-all-edited.csv");
                var currencies = ReadFromCSVFile(file.CreateReadStream());
                await _currencyrepository.InsertManyAsync(currencies, autoSave: true);
            }
        }

        private List<Currency> ReadFromCSVFile(Stream stream)
        {
            var list = new List<Currency>();
            using (TextFieldParser parser = new TextFieldParser(stream))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                // Read header
                string[] headers = parser.ReadFields();

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();

                    list.Add(Currency.Create(fields[0], fields[1]));
                }
            }
            return list;
        }
    }
}

