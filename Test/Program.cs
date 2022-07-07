using Rules;
using Table;

var a=new AssignScoreTokenGcd();

Token<int> t=new Token<int>(new []{15,15});

Console.WriteLine(a.ScoreToken(t));