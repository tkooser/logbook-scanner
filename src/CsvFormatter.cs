using System.Dynamic;
using System.Globalization;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using CsvHelper;
public class CsvFormatter
{
    public string GenerateCsv(DocumentTable table1, DocumentTable table2)
    {
        using var writer = new StringWriter();
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        var records = new List<dynamic>();
        var table1headers = table1.Cells.Where(c => c.RowIndex == 0).Select(c => c.Content).ToList();
        var table2headers = table2.Cells.Where(c => c.RowIndex == 0).Select(c => c.Content).ToList();
        for (int i = 1; i <= table1.RowCount; i++)
        {
            dynamic record = new ExpandoObject();
            var table1cells = table1.Cells.Where(c => c.RowIndex == i).Select(c => new KeyValuePair<string, object>( table1headers[c.ColumnIndex], c.Content));
            var table2cells = table2.Cells.Where(c => c.RowIndex == i).Select(c => new KeyValuePair<string, object>(table2headers[c.ColumnIndex], c.Content));
            foreach (var cell in table1cells.Concat(table2cells))
            {
                ((IDictionary<string, object>)record)[cell.Key] = cell.Value;
            }
            records.Add(record);
        }
        csv.WriteRecords(records);
        return writer.ToString();
    }
}