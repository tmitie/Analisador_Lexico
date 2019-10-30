using System;

public class Semantico : Constants
{
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void executeAction(int action, Token token) throws SemanticError
	public virtual void executeAction(int action, Token token)
	{
		Console.WriteLine("Acao #" + action + ", Token: " + token);
	}
}