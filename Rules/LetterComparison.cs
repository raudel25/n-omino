namespace Rules;

public class LetterComparisonVocalsComodin:IComparison<char>
{
    public bool Compare(char a, char b)
    {
        char[] vocals = new[] {'a', 'e', 'i', 'o', 'u'};
        for (int i = 0; i < 5; i++)
        {
            if (a == vocals[i] || b == vocals[i]) return true;
        }

        return false;
    }
}

public class LetterComparisonAlphabet:IComparison<char>
{
    public bool Compare(char a, char b)
    {
        return a <= b;
    }
}