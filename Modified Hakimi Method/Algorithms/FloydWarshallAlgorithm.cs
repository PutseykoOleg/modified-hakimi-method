using Hakimi.Graphs;

namespace Hakimi.Algorithms;

// Алиас матрицы смежности
using Matrix = List<List<double>>;

// Класс, описывающий методы работы с алгоритмом Флойда-Уоршелла (T - тип значения вершины графа)
public class FloydWarshallAlgorithm
{
    // Граф
    public Graph Graph { get; private set; }

    // Матрица смежности (используется для хранения уже вычисленных значений)
    private Matrix? _matrix { get; set; } = null;

    // Конструктор
    public FloydWarshallAlgorithm(Graph graph)
    {
        this.Graph = graph;
    }

    public Matrix GetMinDistancesMatrix()
    {
        // Конвертация графа в матрицу
        this._matrix = new(this.Graph.ToMatrix());

        // Размер строки матрицы (длина списка вершин)
        int lineLength = this.Graph.Vertices.Count;

        // Для каждой вершины
        for (int k = 0; k < lineLength; k++)
        {
            // Для каждой строки матрицы
            for (int i = 0; i < lineLength; i++)
            {
                // Для каждого столбца матрицы
                for (int j = 0; j < lineLength; j++)
                {
                    // Если индексы строки и столбца не совпадают, т.е. не петля и при этом существует путь от i до j, проходящий
                    // через текущую вершину (k)
                    if (
                        i != j
                        && this._matrix[i][k] != Graph.MAX_VERTEX_VALUE
                        && this._matrix[k][j] != Graph.MAX_VERTEX_VALUE
                    )
                    {
                        // Если такой путь меньше уже проложенного между i и j
                        if (this._matrix[i][k] + this._matrix[k][j] < this._matrix[i][j])
                        {
                            // Установить новый кратчайший путь
                            this._matrix[i][j] = this._matrix[i][k] + this._matrix[k][j];
                        }
                    }
                }
            }
        }

        return this._matrix;
    }
}
