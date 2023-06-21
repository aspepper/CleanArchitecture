using System.Globalization;
using AdviceCompliance.Application.Common.Interfaces;
using AdviceCompliance.Application.TodoLists.Queries.ExportTodos;
using AdviceCompliance.Infrastructure.Files.Maps;
using CsvHelper;

namespace AdviceCompliance.Infrastructure.Files;

public class CsvFileBuilder : ICsvFileBuilder
{
    public byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records)
    {
        using var memoryStream = new MemoryStream();
        using (var streamWriter = new StreamWriter(memoryStream))
        {
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

            csvWriter.Context.RegisterClassMap<TodoItemRecordMap>();
            csvWriter.WriteRecords(records);
        }

        return memoryStream.ToArray();
    }
}
