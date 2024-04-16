using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class MatrixTools
{ 
    public static (Vector2Int, Vector2Int)[] NearestNeighbor(bool[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        List<(Vector2Int,Vector2Int)> nearestNeighbors = new();

        // Проходим по каждой точке в матрице
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                if (!matrix[y, x]) // Пропускаем точки, которые являются "ложными" (False)
                {
                    continue;
                }

                var currentPoint = new Vector2Int(x,y); // Текущая точка
                double minDistance = double.PositiveInfinity;
                Vector2Int nearestIndex = new Vector2Int(0,0);

                // Проходим по каждой точке в матрице
                for (int y1 = 0; y1 < rows; y1++)
                {
                    for (int x1 = 0; x1 < cols; x1++)
                    {
                        if (!matrix[y1, x1] 
                            || (y == y1 && x == x1) 
                            || nearestNeighbors.Any(item=> item.Item1 == new Vector2Int(x1, y1))) // Пропускаем "ложные" точки и текущую точку
                        {
                            continue;
                        }

                        var point = new Vector2Int(x1, y1); // Текущая проверяемая точка
                        double distance = Distance(currentPoint, point);

                        // Если расстояние меньше минимального расстояния, обновляем минимальное расстояние и индекс ближайшей точки
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            nearestIndex = new Vector2Int(x1, y1); // Индекс точки в одномерном массиве
                        }
                    }
                }

                // Записываем индекс ближайшей точки в массив ближайших соседей
                nearestNeighbors.Add((new Vector2Int(x,y), nearestIndex));
            }
        }

        return nearestNeighbors.ToArray();
    }

    /// <summary>
    /// Проходит все точки вокруг (y,x) по спирали
    /// </summary>
    /// <typeparam name="type"></typeparam>
    /// <param name="arrayContents"></param>
    /// <param name="y"></param>
    /// <param name="x"></param>
    /// <param name="LayersCount"></param>
    /// <param name="action"></param>
    /// <param name=""></param>
    internal static void SpiralArrayTraversal<type>(type[,] arrayContents, Vector2Int position, Func<Vector2Int, Result> action, int LayerCount = -1)
    {
        // Создаем очередь для хранения координат элементов, которые нужно посетить
        Queue<(int, int)> queue = new Queue<(int, int)>();
        // Создаем массив для отметки элементов, которые уже были посещены
        bool[,] visited = new bool[arrayContents.GetLength(0), arrayContents.GetLength(1)];
        // Добавляем точку p в очередь и отмечаем ее как посещенную
        queue.Enqueue((position.y, position.x));
        visited[position.y, position.x] = true;
        // Пока очередь не пуста
        while (queue.Count > 0)
        {

            // Извлекаем первый элемент из очереди
            (position.y, position.x) = queue.Dequeue();
            // Делаем что-то с этим элементом
            var result = action(position);
            if(result == Result.Break) break;
            // Проходим по всем четырем направлениям от этого элемента
            int[] dx = { 0, 0, -1, 1, -1, 1, -1, 1}; // Смещение по x
            int[] dy = { -1, 1, 0, 0, -1, 1, 1, -1 }; // Смещение по y

            if (LayerCount != 0)
            {
                for (int k = 0; k < 8; k++)
                {
                    // Вычисляем координаты соседнего элемента
                    int nx = position.x + dx[k];
                    int ny = position.y + dy[k];
                    // Проверяем, что они в пределах массива и не были посещены
                    if (nx >= 0 && nx < arrayContents.GetLength(1) && ny >= 0 && ny < arrayContents.GetLength(0) && !visited[ny, nx])
                    {
                        // Добавляем соседний элемент в очередь и отмечаем его как посещенный
                        queue.Enqueue((ny, nx));
                        visited[ny, nx] = true;
                    }
                }
                LayerCount--;
            }
        }
    }

    internal enum  Result
    {
        None,
        Break,

    }
    // Функция для вычисления расстояния между двумя точками
    static double Distance(Vector2Int point1, Vector2Int point2)
    {
        int dx = point1.x - point2.x;
        int dy = point1.y - point2.y;
        return Math.Sqrt(dx * dx + dy * dy);
    }
}