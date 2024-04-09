using UnityEngine;

public class HashFunction
{
    public static double[,] GenerateNoiseMap(int width, int height, double scale, int seed)
    {
        double[,] noiseMap = new double[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                double noise = Hash2D(seed, x, y)/ scale;
                noiseMap[x, y] = noise;
            }
        }

        return noiseMap;
    }

    private const int X_PRIME = 1619;
    private const int Y_PRIME = 31337;
    private const int Z_PRIME = 6971;
    private const int W_PRIME = 1013;

    private static float Hash2D(int seed, int x, int y)
    {
        int hash = seed;
        hash ^= X_PRIME * x;
        hash ^= Y_PRIME * y;

        hash = hash * hash * hash * 60493;
        hash = (hash >> 13) ^ hash;

        float result = (hash & int.MaxValue) / (float)int.MaxValue; // Преобразование в диапазон [0, 1]
        result = SmoothStep(0, 1, result);
        return result;
    }
   
    public static float SmoothStep(float edge0, float edge1, float x)
    {
        // Клипирование x к диапазону [0, 1]
        float t = Mathf.Clamp01((x - edge0) / (edge1 - edge0)); 
    
        // Вычисление кубической интерполяции
        return t * t * (3f - 2f * t);
    }

}