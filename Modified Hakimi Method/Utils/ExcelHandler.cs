using Hakimi.Graphs;
using Excel = Microsoft.Office.Interop.Excel;

namespace Hakimi.Utils;

public class ExcelHandler
{
    private string _fileName;

    public ExcelHandler(string fileName = "")
    {
        this._fileName = fileName;
    }

    public List<string> GetColumnValues(
        string columnName,
        int startCellIndex,
        int endCellIndex,
        int listIndex = 1
    )
    {
        Excel.Application excel = new Excel.Application();

        Excel.Workbook workbook = excel.Workbooks.Open(_fileName);

        Excel.Worksheet worksheet = workbook.Sheets[listIndex];

        Excel.Range cells = worksheet.Range[
            $"{columnName}{startCellIndex}",
            $"{columnName}{endCellIndex}"
        ];

        object[,] values = cells.Value2;

        int numberOfValues = values.GetUpperBound(0);

        string[] formattedValues = new string[numberOfValues];

        for (int i = 0; i < numberOfValues; i++)
        {
            formattedValues[i] = String.Format("{0}", values[i + 1, 1]);
        }

        workbook.Close(false);

        excel.Quit();

        return new(formattedValues);
    }

    public void WriteMatrixAsCSV(string fileName, Graph graph, double[] matrix)
    {
        int lineLength = (int)Math.Sqrt(matrix.Length);

        using (StreamWriter sw = new StreamWriter(fileName))
        {
            sw.Write(",");

            for (int i = 0; i < lineLength; i++)
            {
                sw.Write($"{graph.GetVertexByIndex(i)}{(i < lineLength ? ',' : "")}");
            }

            for (int i = 0; i < lineLength; i++)
            {
                for (int j = 0; j < lineLength; j++)
                {
                    string value = (matrix[i * lineLength + j]).ToString();
                    sw.Write(
                        $"{graph.GetVertexByIndex(i)},{value.Replace(',', '.')}{(j < lineLength ? ',' : "")}"
                    );
                }
                sw.WriteLine();
            }
        }
    }
}
