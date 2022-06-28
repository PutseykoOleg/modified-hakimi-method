using Hakimi.Graphs;

namespace Hakimi.Algorithms;

// Класс, описывающий методы работы с алгоритмом Флойда-Уоршелла (T - тип значения вершины графа)
public class FloydWarshallAlgorithm
{
    // Граф
    public Graph Graph { get; private set; }

    // Матрица смежности (используется для хранения уже вычисленных значений)
    private double[]? _matrix { get; set; } = null;

    // Конструктор
    public FloydWarshallAlgorithm(Graph graph)
    {
        this.Graph = graph;
    }

    public double[] GetMinDistancesMatrix()
    {
        // Размер строки матрицы (длина списка вершин)
        int lineLength = this.Graph.Vertices.Count;

        // Конвертация графа в матрицу
        this._matrix = new double[lineLength * lineLength];
        double[] computedMatrix = this.Graph.ToMatrix();
        Array.Copy(computedMatrix, this._matrix, lineLength * lineLength);

        for (int k = 0; k < lineLength; ++k)
        {
            Parallel.For(
                0,
                lineLength,
                (i) =>
                {
                    if (this._matrix[i * lineLength + k] == Graph.MAX_VERTEX_VALUE)
                    {
                        return;
                    }

                    for (int j = 0; j < lineLength; ++j)
                    {
                        double distance =
                            this._matrix[i * lineLength + k] + this._matrix[k * lineLength + j];

                        if (this._matrix[i * lineLength + j] > distance)
                        {
                            this._matrix[i * lineLength + j] = distance;
                        }
                    }
                }
            );
        }

        return this._matrix;
    }
}
