using System;
using UnityEngine;

public static class MatrixWalker
{
    public static void TakeStep<T>(T[,] matrix, T value, Vector2Int start, Vector2Int end, int size = 0)
    {
        var currentPoint = start;
        while (TakeStep(start, end, ref currentPoint))
        {
            MatrixTools.SpiralArrayTraversal(matrix, currentPoint, point =>
            {
                matrix[point.y, point.x] = value;
                return MatrixTools.Result.None;
            }, size);
        }
        
    }
    private static bool TakeStep(Vector2Int start, Vector2Int end, ref Vector2Int current)
    {
        if (current.x == end.x && current.y == end.y)
        {
            return false; // Reached destination
        }

        var delta = end - current;


        if (Math.Abs(end.x) != Math.Abs(current.x))
        {
            current.x += Math.Sign(delta.x);
        }
        else
        {
            current.y += Math.Sign(delta.y);
        }

        return true;
    }

}