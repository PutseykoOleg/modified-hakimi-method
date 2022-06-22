using Hakimi.Graphs;

namespace Hakimi.Algorithms;

public class ModifiedHakimiMethod : HakimiMethod
{
    public ModifiedHakimiMethod(Graph graph) : base(graph)
    {
        ExcludeEdges();
    }

    private int P(int i, int j)
    {
        int result = 0;

        for (int s = 0; s < _minDistances.Count; s++)
        {
            if (s != i && s != j)
            {
                result = (int)Math.Max(result, Math.Min(d(s, i), d(s, j)));
            }
        }

        return result;
    }

    private double H()
    {
        double result = Graph.MAX_VERTEX_VALUE;

        for (int i = 0; i < _graph.Edges.Count; i++)
        {
            Edge edge = _graph.Edges[i];
            List<int> indexes =
                new()
                {
                    _graph.GetVertexIndex(edge.Vertices[0]),
                    _graph.GetVertexIndex(edge.Vertices[1])
                };

            int vs = 1;

            result = Math.Min(
                result,
                P(indexes[0], indexes[1]) + (vs * _minDistances[indexes[0]][indexes[1]]) / 2
            );
        }

        return result;
    }

    private void ExcludeEdges()
    {
        double h = H();

        for (int i = 0; i < _graph.Edges.Count; i++)
        {
            Edge edge = _graph.Edges[i];
            List<int> indexes =
                new()
                {
                    _graph.GetVertexIndex(edge.Vertices[0]),
                    _graph.GetVertexIndex(edge.Vertices[1])
                };

            int p = P(indexes[0], indexes[1]);

            if (p >= h)
            {
                _edges = _edges.Where(x => x != edge).ToList();
            }
        }
    }
}
