using Hakimi.Algorithms;
using Hakimi.Algorithms.Structs;
using Hakimi.Graphs;
using Hakimi.Utils;

namespace Hakimi;

public static class App
{
    public enum TestAlgorithm
    {
        HakimiMethod,
        ModifiedHakimiMethod,
        BothMethods
    }

    public static void Test(
        int dataSetNumber,
        TestAlgorithm algorithm = TestAlgorithm.ModifiedHakimiMethod
    )
    {
        Graph graph = new();

        switch (dataSetNumber)
        {
            // From open source work
            case 0:
                graph.AddEdge(1, 3, 8);
                graph.AddEdge(3, 2, 2);
                graph.AddEdge(2, 5, 6);
                graph.AddEdge(5, 1, 2);
                graph.AddEdge(1, 4, 6);
                graph.AddEdge(4, 5, 4);

                // (v2, v5) | R = 6,02 from v2 | P = 3,98 from v2
                break;

            // First example (with blue vertices)
            case 1:
                graph.AddEdge(1, 2, 3);
                graph.AddEdge(2, 3, 4.5);
                graph.AddEdge(3, 4, 6.5);
                graph.AddEdge(4, 5, 20);
                graph.AddEdge(4, 6, 15.5);
                graph.AddEdge(6, 8, 6);
                graph.AddEdge(8, 9, 1);
                graph.AddEdge(4, 7, 3);
                graph.AddEdge(7, 10, 3);
                graph.AddEdge(10, 11, 7);

                // (v4, v6) | R = 21,26 from v4 | P = 1,24 from v4
                break;

            // Second example (from notebook sheet)
            case 2:
                graph.AddEdge(1, 2, 1);
                graph.AddEdge(2, 3, 5);
                graph.AddEdge(3, 4, 2);
                graph.AddEdge(4, 1, 3);
                graph.AddEdge(2, 4, 1);
                graph.AddEdge(1, 3, 4);

                // (v3, v4) | R = 2 from v3 | P = 2 from v3
                // (v2, v4) | R = 2 from v2 | P = 1 from v2
                break;

            // From open sourse work
            case 3:
                graph.AddEdge(1, 2, 1);
                graph.AddEdge(2, 3, 1);
                graph.AddEdge(3, 5, 1);
                graph.AddEdge(2, 4, 1);
                graph.AddEdge(4, 5, 1);
                graph.AddEdge(5, 1, 1);

                // (v1, v2) | R = 1,5 from v1 | P = 0,5 from v1
                // (v2, v3) | R = 1,5 from v2 | P = 0,5 from v2
                // (v3, v5) | R = 1,5 from v3 | P = 0,5 from v4
                // (v2, v4) | R = 1,5 from v2 | P = 0,5 from v2
                // (v4, v5) | R = 1,5 from v4 | P = 0,5 from v4
                // (v5, v1) | R = 1,5 from v5 | P = 0,5 from v5
                break;

            default:
                break;
        }

        if (algorithm == TestAlgorithm.HakimiMethod || algorithm == TestAlgorithm.BothMethods)
        {
            HakimiMethod hm = new(graph);

            _WriteResult(hm, "Hakimi method");
        }

        if (
            algorithm == TestAlgorithm.ModifiedHakimiMethod
            || algorithm == TestAlgorithm.BothMethods
        )
        {
            ModifiedHakimiMethod mhm = new(graph);

            _WriteResult(mhm, "Modified Hakimi method");
        }
    }

    public static void Run(string inputPath, string outputPath)
    {
        Graph graph = new();

        ExcelHandler excel = new ExcelHandler(inputPath);

        int countOfCoordinates = 3368;

        List<string> firstCoordinates = excel.GetColumnValues("B", 1, countOfCoordinates, 3);
        List<string> secondCoordinates = excel.GetColumnValues("D", 1, countOfCoordinates, 3);
        List<string> weightes = excel.GetColumnValues("F", 1, countOfCoordinates, 3);

        for (int i = 0; i < countOfCoordinates; i++)
        {
            double first = Double.Parse(firstCoordinates[i].Replace(" ", "").Replace('.', ','));
            double second = Double.Parse(secondCoordinates[i].Replace(" ", "").Replace('.', ','));
            double weight = Double.Parse(weightes[i].Replace(" ", "").Replace('.', ','));

            graph.AddEdge(first, second, weight);
        }

        ModifiedHakimiMethod mhm = new(graph);

        _WriteResult(mhm, "Modified Hakimi method");

        excel.WriteMatrixAsCSV(outputPath, graph, mhm.MinDistances);
    }

    private static void _WriteResult(HakimiMethod method, string title)
    {
        List<AbsCenter> centers = method.GetAbsCenter();

        Console.WriteLine($"{title}:");
        for (int i = 0; i < centers.Count; i++)
        {
            _WriteCenterToConsole(centers, i);
        }
    }

    private static void _WriteCenterToConsole(List<AbsCenter> centers, int index)
    {
        AbsCenter center = centers[index];

        Console.WriteLine($"   Absolute center #{index + 1}:");
        Console.WriteLine(
            $"\tEdge: (v{center.Edge.Vertices[0]}, v{center.Edge.Vertices[1]})"
                + $"\n\tAbsolute radius: {center.Radius.Value} from v{center.Radius.Vertex}"
                + $"\n\tPosition: {center.Position.Offset} from v{center.Position.VertexFrom}\n"
        );
    }
}
