using System;
using System.Collections;
using System.Collections.Generic;

public static class twoStack {

   public static double calculate(string s)
        {
     
          Console.WriteLine(s);
           List<string> tokens = new List<string>();
          
           int i = 0;
           while(i < s.Length)
           {
             char c = s[i];
             if(c >= '0' && c <= '9')
             {
                int start = i;
                i++;
                int end = start + 1;
                while( i < s.Length && ( (s[i] >= '0' && s[i] <= '9') || (s[i] == '.') )  )
                {
                  i++;
                  end++;
                }
                tokens.Add(s.Substring(start, end-start));
                i--;
             }
             else if(c != ' ')
              {
                tokens.Add(c.ToString());
              }
             i++;
           }
     
        Stack<string> stack = new Stack<string>();
        Queue<string> queue = new Queue<string>();
     
        foreach(string tok in tokens)
        {
          if(tok == "(")
          {
            stack.Push(tok);
          }
          else if(tok == "+" || tok == "-" || tok == "*" || tok == "/" || tok == "^")
          {
            while(stack.Count > 0 && precedence(stack.Peek()) >= precedence(tok))
            {
              queue.Enqueue(stack.Pop());
            }
            stack.Push(tok);
          }
          else if(tok == ")")
          {
            while(stack.Count > 0 && stack.Peek() != "(")
            {
              queue.Enqueue(stack.Pop());
            }
            stack.Pop();
          }
          else
          {
            queue.Enqueue(tok);
          }
        }
        while(stack.Count > 0)
          queue.Enqueue(stack.Pop());  
        Stack<double> result = new Stack<double>();
        
        while(queue.Count > 0)
        {
          string tok = queue.Dequeue();
          if(tok == "+" || tok == "-" || tok == "/" || tok == "*" || tok == "^")
          {
            double a = result.Pop();
            double b = result.Pop();
            result.Push(calc(a, b, tok));
          }
          else
            result.Push(Double.Parse(tok));
        }
     
          double final = result.Pop();
          return final;
        }
  
    public static int precedence(string op)
    {
      if(op == "+" || op == "-")
        return 1;
      if(op == "/" || op == "*")
        return 2;
      if(op == "^")
        return 3;
      return -1;
    }
  
    public static double calc(double a, double b, string op)
    {
      if(op == "+")
        return a + b;
      if(op == "-")
        return b - a;
      if(op == "*")
        return a * b;
      if(op == "/")
        return b / a;
      if(op == "^")
        return Math.Pow(b, a);
      return 0;
    }

  
}
