//11111111111111
Console.WriteLine("Введіть розмір масиву N:");
int N = int.Parse(Console.ReadLine());

int[] array = new int[N];

// Заповнюємо масив елементами
for (int i = 0; i < N; i++)
{
    Console.Write($"Введіть {i + 1}-й елемент масиву: ");
    array[i] = int.Parse(Console.ReadLine());
}

// Знаходимо добуток елементів з парними номерами
int productOfEvenIndexedElements = 1;
for (int i = 0; i < N; i += 2)
{
    productOfEvenIndexedElements *= array[i];
}

Console.WriteLine($"Добуток елементів з парними номерами: {productOfEvenIndexedElements}");

// Знаходимо суму елементів між першим і останнім нульовими елементами
int firstZeroIndex = -1;
int lastZeroIndex = -1;

for (int i = 0; i < N; i++)
{
    if (array[i] == 0)
    {
        if (firstZeroIndex == -1)
        {
            firstZeroIndex = i;
        }
        else
        {
            lastZeroIndex = i;
        }
    }
}

int sumBetweenZeros = 0;
if (firstZeroIndex != -1 && lastZeroIndex != -1)
{
    for (int i = firstZeroIndex + 1; i < lastZeroIndex; i++)
    {
        sumBetweenZeros += array[i];
    }
}

Console.WriteLine($"Сума елементів між першим і останнім нульовими елементами: {sumBetweenZeros}");

// Сортуємо масив так, щоб спочатку були всі додатні елементи, а потім від'ємні
Array.Sort(array, (a, b) =>
{
    if (a >= 0 && b >= 0)
    {
        return a.CompareTo(b);
    }
    else if (a < 0 && b < 0)
    {
        return b.CompareTo(a);
    }
    else
    {
        return a >= 0 ? -1 : 1;
    }
});

Console.WriteLine("Відсортований масив:");
foreach (int element in array)
{
    Console.Write(element + " ");
}
    

//22222222222222222222222
int[,] matrix = new int[,]
        {
            {1, 0, 3, 4},
            {0, 2, 3, 1},
            {5, 6, 7, 8},
            {0, 0, 0, 0},
        };

// Знаходимо кількість стовпців з нульовим елементом
int columnsWithZero = CountColumnsWithZero(matrix);

Console.WriteLine($"Кількість стовпців з нульовим елементом: {columnsWithZero}");

// Знаходимо номер рядка з найдовшою серією однакових елементів
int longestSeriesRow = FindLongestSeriesRow(matrix);

Console.WriteLine($"Номер рядка з найдовшою серією однакових елементів: {longestSeriesRow}");

//33333333333333333

Console.WriteLine("Введіть текстовий рядок:");
string inputText = Console.ReadLine();

// Розділити текст на слова та підрахувати кількість різних слів
string[] words = inputText.Split(new char[] { ' ', ',', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
int uniqueWordsCount = words.Distinct().Count();

Console.WriteLine($"Кількість різних слів: {uniqueWordsCount}");

// Підрахувати кількість використаних символів
int usedCharactersCount = inputText.Replace(" ", "").Length;
Console.WriteLine($"Кількість використаних символів: {usedCharactersCount}");

// Видалити слова з подвоєними літерами
string[] wordsWithoutDuplicates = RemoveWordsWithDuplicates(words);
string cleanedText = string.Join(" ", wordsWithoutDuplicates);

Console.WriteLine("Текст після видалення слів з подвоєними літерами:");
Console.WriteLine(cleanedText);



//фунції
static int FindLongestSeriesRow(int[,] matrix)
{
    int longestSeriesRow = -1;
    int maxLength = 0;
    int currentLength = 1;

    for (int row = 0; row < matrix.GetLength(0); row++)
    {
        for (int col = 1; col < matrix.GetLength(1); col++)
        {
            if (matrix[row, col] == matrix[row, col - 1])
            {
                currentLength++;
            }
            else
            {
                currentLength = 1;
            }

            if (currentLength > maxLength)
            {
                maxLength = currentLength;
                longestSeriesRow = row;
            }
        }

        currentLength = 1;
    }

    return longestSeriesRow + 1; // +1 because row numbers usually start from 1, not 0
}
static int CountColumnsWithZero(int[,] matrix)
{
    int columnsWithZero = 0;

    for (int col = 0; col < matrix.GetLength(1); col++)
    {
        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            if (matrix[row, col] == 0)
            {
                columnsWithZero++;
                break;
            }
        }
    }

    return columnsWithZero;
}
static bool HasDuplicateLetters(string word)
{
    return word.GroupBy(c => c).Any(group => group.Count() > 1);
}
static string[] RemoveWordsWithDuplicates(string[] words)
{
    List<string> filteredWords = new List<string>();

    foreach (string word in words)
    {
        if (!HasDuplicateLetters(word))
        {
            filteredWords.Add(word);
        }
    }

    return filteredWords.ToArray();
}