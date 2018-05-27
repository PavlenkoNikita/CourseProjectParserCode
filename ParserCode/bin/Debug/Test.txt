// передача параметра с params
static void Addition(params int[] integers)
{
    int result = 0;
    for (int i = 0; i < integers.Length; i++)
    {
        result += integers[i];
    }
    Console.WriteLine(result);
}
// передача массива
static void AdditionMas(int[] integers, int k)
{
    int result = 0;
    for (int i = 0; i < integers.Length; i++)
    {
        result += (integers[i]*k);
    }
    Console.WriteLine(result);
}

vserg3d@gmail.com