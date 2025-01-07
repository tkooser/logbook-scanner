using Azure.AI.FormRecognizer.DocumentAnalysis;

namespace test;

public class CsvFormatTests
{
    [Fact]
    public void FormatsWithMultipleRows()
    {
        var table1 = DocumentAnalysisModelFactory.DocumentTable(2, 2, new List<DocumentTableCell>
        {
            DocumentAnalysisModelFactory.DocumentTableCell(content: "Header1"),
            DocumentAnalysisModelFactory.DocumentTableCell(content: "Header2"),
            DocumentAnalysisModelFactory.DocumentTableCell(columnIndex: 0, rowIndex: 1, content: "Content1"),
            DocumentAnalysisModelFactory.DocumentTableCell(columnIndex: 1, rowIndex: 1, content: "Content2"),
            DocumentAnalysisModelFactory.DocumentTableCell(columnIndex: 0, rowIndex: 2, content: "Content21"),
            DocumentAnalysisModelFactory.DocumentTableCell(columnIndex: 1, rowIndex: 2, content: "Content22"),
        });

        var table2 = DocumentAnalysisModelFactory.DocumentTable(2, 2, new List<DocumentTableCell>
        {
            DocumentAnalysisModelFactory.DocumentTableCell(content: "Header3"),
            DocumentAnalysisModelFactory.DocumentTableCell(content: "Header4"),
            DocumentAnalysisModelFactory.DocumentTableCell(columnIndex: 0, rowIndex: 1, content: "Content3"),
            DocumentAnalysisModelFactory.DocumentTableCell(columnIndex: 1, rowIndex: 1, content: "Content4"),
            DocumentAnalysisModelFactory.DocumentTableCell(columnIndex: 0, rowIndex: 2, content: "Content23"),
            DocumentAnalysisModelFactory.DocumentTableCell(columnIndex: 1, rowIndex: 2, content: "Content24"),
        });

        var expected = "Header1,Header2,Header3,Header4\r\nContent1,Content2,Content3,Content4\r\nContent21,Content22,Content23,Content24\r\n";

        var formatter = new CsvFormatter();
        var actual = formatter.GenerateCsv(table1, table2);
        Assert.Equal(expected, actual);
    }
}