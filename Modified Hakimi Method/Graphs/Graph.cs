namespace Hakimi.Graphs;

// Класс взвешенного неориентированного графа (T - тип значения вершины)
public class Graph
{
    // Список вершин
    public List<double> Vertices { get; private set; } = new();

    public List<Edge> Edges { get; private set; } = new();

    // Пары [<вершина> - <прилежащие к ней ребра>]
    public Dictionary<double, List<Edge>> LinkedEdges { get; private set; } = new();

    public const double MAX_VERTEX_VALUE = Double.MaxValue;

    public Graph() { }

    // Добавить новое ребро в граф
    public void AddEdge(double firstVertexValue, double secondVertexValue, double weight)
    {
        // Новое ребро
        Edge edge =
            new(
                this.GetVertex(firstVertexValue, true),
                this.GetVertex(secondVertexValue, true),
                weight
            );

        this.Edges.Add(edge);

        // Пройтись по вершинам ребра
        for (int i = 0; i < 2; i++)
        {
            // Взять текущую
            double vertex = edge.Vertices[i];

            // Если текущей вершины нет в списке пар [<вершина> - <прилежащие к ней ребра>], то добавить
            if (!this.LinkedEdges.Keys.Contains(vertex))
            {
                this.LinkedEdges.Add(vertex, new());
            }

            // Если нового ребра еще нет в списке прилежащих к текущей вершине, то добавить
            if (
                !this._IsEdgeContained(vertex, edge)
                && !this._IsEdgeContained(vertex, edge.GetReverseEdge())
            )
            {
                this.LinkedEdges[vertex].Add(edge);
            }
        }
    }

    // Получение вершины, лежащей в списке вершин
    public double GetVertex(double value, bool addIfNotExist = false)
    {
        try
        {
            // Пройтись по вершинам
            foreach (double vertex in this.Vertices)
            {
                // Если значения искомой и текущей совпадают, то вернуть текущую
                if (vertex.Equals(value))
                {
                    return vertex;
                }
            }

            // Если необходимо добавить новую вершину при отсутствии в списке вершины с заданным значением
            if (addIfNotExist)
            {
                // Создать вершину
                double newVertex = value;

                // Положить в список вершин и в список пар [<вершина> - <прилежащие к ней ребра>]
                this.Vertices.Add(newVertex);
                this.LinkedEdges.Add(newVertex, new());

                // Вернуть только что созданную вершину
                return newVertex;
            }
            // Иначе выбросить ошибку
            else
            {
                throw new ArgumentException(
                    $"Вершина со значением \"{value.ToString()}\" не найдена"
                );
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");

            // При наличии ошибки вернуть пустую вершину
            return -1;
        }
    }

    // Получить вершину из списка по индексу
    public double GetVertexByIndex(int index)
    {
        return this.Vertices[index];
    }

    // Получить индекс вершины из списка
    public int GetVertexIndex(double vertex)
    {
        return this.Vertices.IndexOf(vertex);
    }

    // Преобразование графа в матрицу смежности, значениями которой являются веса ребер,
    // а строки и столбцы - индексы вершин из списка
    public double[] ToMatrix()
    {
        int lineLength = this.Vertices.Count;

        double[] matrix = new double[lineLength * lineLength];

        for (int i = 0; i < lineLength; i++)
        {
            for (int j = 0; j < lineLength; j++)
            {
                matrix[i * lineLength + j] = this.GetStraightWay(
                    this.Vertices[i],
                    this.Vertices[j]
                );
            }
        }

        return matrix;
    }

    // Получение прямого пути от одной вершины до другой, т.е. расстояние между вершинами,
    // лежащими на одном ребре
    public double GetStraightWay(double firstVertex, double secondVertex)
    {
        // Если вершины совпадают, то вернуть максимально возможное целое число ("бесконечность")
        if (firstVertex == secondVertex)
        {
            return MAX_VERTEX_VALUE;
        }

        // Получить список прилежащих ребер
        List<Edge> linkedEdges = this.LinkedEdges[firstVertex];

        // Пройтись по каждому
        foreach (Edge edge in linkedEdges)
        {
            // Если расстояние между заданными вершинами и вершинами, лежащими на текущем ребре, равно
            if (
                (edge.Vertices[0] == firstVertex && edge.Vertices[1] == secondVertex)
                || (edge.Vertices[1] == firstVertex && edge.Vertices[0] == secondVertex)
            )
            {
                // Вернуть это расстояние
                return edge.Weight;
            }
        }

        // Если не найдено таких ребер, расстояние между вершинами которого, равно расстоянию между заданными
        // вершинами, то вернуть "бесконечность"
        return MAX_VERTEX_VALUE;
    }

    // Проверка на содержание ребра в спике прилежащих к заданной вершине
    private bool _IsEdgeContained(double vertex, Edge edge)
    {
        foreach (Edge otherEdge in this.LinkedEdges[vertex])
        {
            if (otherEdge == edge)
                return true;
        }
        return false;
    }
}
