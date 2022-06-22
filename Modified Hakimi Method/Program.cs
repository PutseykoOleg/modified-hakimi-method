using Hakimi.Graphs;
using Hakimi.Algorithms;
using Hakimi.Algorithms.Structs;
using Hakimi.Utils;

Graph graph = new();

//graph.AddEdge(1, 3, 8);
//graph.AddEdge(3, 2, 2);
//graph.AddEdge(2, 5, 6);
//graph.AddEdge(5, 1, 2);
//graph.AddEdge(1, 4, 6);
//graph.AddEdge(4, 5, 4);

//graph.AddEdge(1, 2, 3);
//graph.AddEdge(2, 3, 4.5);
//graph.AddEdge(3, 4, 6.5);
//graph.AddEdge(4, 5, 20);
//graph.AddEdge(4, 6, 15.5);
//graph.AddEdge(6, 8, 6);
//graph.AddEdge(8, 9, 1);
//graph.AddEdge(4, 7, 3);
//graph.AddEdge(7, 10, 3);
//graph.AddEdge(10, 11, 7);

//graph.AddEdge(1, 2, 1);
//graph.AddEdge(2, 3, 5);
//graph.AddEdge(3, 4, 2);
//graph.AddEdge(4, 1, 3);
//graph.AddEdge(2, 4, 1);
//graph.AddEdge(1, 3, 4);

graph.AddEdge(1, 2, 1);
graph.AddEdge(2, 3, 1);
graph.AddEdge(3, 5, 1);
graph.AddEdge(2, 4, 1);
graph.AddEdge(4, 5, 1);
graph.AddEdge(5, 1, 1);

Action<AbsCenter> WriteCenter = (AbsCenter center) =>
{
    Console.WriteLine(
        $"\tEdge: (v{center.Edge.Vertices[0]}, v{center.Edge.Vertices[1]})"
            + $"\n\tAbsolute radius: {center.Radius.Value} from v{center.Radius.Vertex}"
            + $"\n\tPosition: {center.Position.Offset} from v{center.Position.VertexFrom}\n"
    );
};

//ExcelHandler excel = new ExcelHandler(@"C:\Users\olego\Downloads\Graph.xls");

//int countOfCoordinates = 3368;

//List<string> firstCoordinates = excel.GetColumnValues("B", 1, countOfCoordinates, 3);
//List<string> secondCoordinates = excel.GetColumnValues("D", 1, countOfCoordinates, 3);
//List<string> weightes = excel.GetColumnValues("F", 1, countOfCoordinates, 3);

////excel = new ExcelHandler()

//for (int i = 0; i < countOfCoordinates; i++)
//{
//    double first = Double.Parse(firstCoordinates[i].Replace(" ", "").Replace('.', ','));
//    double second = Double.Parse(secondCoordinates[i].Replace(" ", "").Replace('.', ','));
//    double weight = Double.Parse(weightes[i].Replace(" ", "").Replace('.', ','));

//    graph.AddEdge(first, second, weight);
//}

HakimiMethod hm = new(graph);
ModifiedHakimiMethod mhm = new(graph);

List<AbsCenter> centers = mhm.GetAbsCenter();

Console.WriteLine("Mhm:");
for (int i = 0; i < centers.Count; i++)
{
    Console.WriteLine($"   Absolute center #{i + 1}");
    WriteCenter(centers[i]);
}

//FloydWarshallAlgorithm fwa = new(graph);

//List<List<int>> matrix = fwa.GetComputedMarix();

//for (int i = 0; i < matrix.Count; i++)
//{
//    Console.Write($"{i}: ");
//    for (int j = 0; j < matrix[i].Count; j++)
//    {
//        Console.Write($"{matrix[i][j]} ");
//    }
//    Console.WriteLine();
//}

//mhm.A();
