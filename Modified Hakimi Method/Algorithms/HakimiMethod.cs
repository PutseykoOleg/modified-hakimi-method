using Hakimi.Graphs;
using Hakimi.Algorithms.Structs;

namespace Hakimi.Algorithms;

using Matrix = List<List<double>>;

public class HakimiMethod
{
    protected Graph _graph = new();
    protected Matrix _minDistances = new();
    protected List<Edge> _edges = new();

    public HakimiMethod(Graph graph)
    {
        this._graph = graph;
        this._edges = _graph.Edges;

        FloydWarshallAlgorithm fwa = new(graph);

        this._minDistances = fwa.GetMinDistancesMatrix()
            .Select(row => row.Select(x => x == Graph.MAX_VERTEX_VALUE ? 0 : x).ToList())
            .ToList();
    }

    protected double d(int i, int j) => i == j ? 0 : _minDistances[i][j];

    protected double maxPath(int vertexIndex) => _minDistances[vertexIndex].Max();

    public List<AbsCenter> GetAbsCenter()
    {
        List<AbsCenter> centres = new();

        double step;
        double T1;
        double T2;

        double radius = -1;
        int vertexFrom = -1;

        foreach (Edge edge in _edges)
        {
            step = edge.Weight * 0.01;

            for (double Ksi = 0; Ksi <= edge.Weight; Ksi += step)
            {
                int xa = _graph.GetVertexIndex(edge.Vertices[0]);
                int xb = _graph.GetVertexIndex(edge.Vertices[1]);

                double s = 0;

                for (int xi = 0; xi < _graph.Vertices.Count; xi++)
                {
                    T1 = Ksi + d(xb, xi);
                    T2 = edge.Weight + d(xa, xi) - Ksi;

                    if (d(xa, xi) != 0)
                    {
                        if (d(xb, xi) != 0)
                        {
                            if (s < Math.Min(T1, T2))
                            {
                                s = Math.Min(T1, T2);
                                vertexFrom = xa;
                            }
                        }
                        else if (s < T1)
                        {
                            s = T1;
                            vertexFrom = xa;
                        }
                    }
                    else if (d(xb, xi) != 0 && s < T2)
                    {
                        s = T2;
                        vertexFrom = xa;
                    }
                }

                if (s != 0)
                {
                    if (radius == -1 || radius >= s)
                    {
                        if (
                            centres.Count > 0
                            && Math.Round(s, 2) != Math.Round(centres.Last().Radius.Value, 2)
                        )
                        {
                            centres.Clear();
                        }

                        radius = s;
                        centres.Add(
                            new AbsCenter
                            {
                                Edge = edge,
                                Radius = new()
                                {
                                    Value = radius,
                                    Vertex = _graph.GetVertexByIndex(vertexFrom)
                                },
                                Position = new()
                                {
                                    Offset = Math.Round(maxPath(xa) - radius, 2),
                                    VertexFrom = _graph.GetVertexByIndex(vertexFrom)
                                }
                            }
                        );
                    }
                }
            }
        }

        return centres;
    }
}
