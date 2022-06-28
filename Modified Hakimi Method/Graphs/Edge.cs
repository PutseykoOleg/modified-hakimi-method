namespace Hakimi.Graphs;

// Класс ребра взвешенного неориентированного графа (T - тип значения вершин)
public struct Edge
{
    // Пара вершин
    public List<double> Vertices { get; private set; }

    // Вес ребра
    public double Weight { get; private set; }

    // Конструктор
    public Edge(double firstVertex, double secondVertex, double weight)
    {
        this.Vertices = new() { firstVertex, secondVertex };
        this.Weight = weight;
    }

    // Получение обратного ребра
    public Edge GetReverseEdge()
    {
        return new(this.Vertices[1], this.Vertices[0], this.Weight);
    }

    // Проверка двух ребер на неравенство
    public static bool operator !=(Edge firstEdge, Edge secondEdge) => !(firstEdge == secondEdge);

    // Проверка двух ребер на равенство
    public static bool operator ==(Edge firstEdge, Edge secondEdge)
    {
        // Значения обоих ребер в удобном виде
        List<List<double>> values =
            new()
            {
                new() { firstEdge.Vertices[0], firstEdge.Vertices[1] },
                new() { secondEdge.Vertices[0], secondEdge.Vertices[1] }
            };

        // Т.к. рассматривается неориентированный граф, то ребра вида [a, b] и [a, b] будут равны
        bool areValuesEqual =
            values[0][0].Equals(values[1][0]) && values[0][1].Equals(values[1][1]);
        // и ребра вида [a, b] и [b, a] тоже будут равны
        bool areValuesReverseEqual =
            values[0][0].Equals(values[1][1]) && values[0][1].Equals(values[1][0]);

        // Если оба равенства справедливы, и вес ребер совпадает, то они равны
        return (areValuesEqual || areValuesReverseEqual) && firstEdge.Weight == secondEdge.Weight;
    }
}
