using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.VisualBasic;

namespace IaTema1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Read Key");

            RandomStrategy(GetInitialState(6, 15, 0, 8, 0, 1));
            Console.WriteLine("Read Ceva");
        }

        public static State GetInitialState(int b, int m1, int m2, int c1, int c2, int pb)
            => new State(b, m1, c1, pb, m2, c2);

        public static bool CheckIfFinalState(State state)
        {
            if (state.c1 == 0 &&
                state.m1 == 0 &&
                state.pb == 2)
                return true;
            else 
                return false;
        }

        public static State RandomStrategy(State initialState)
        {
            var currentState = initialState;
            Console.WriteLine(currentState);
            while (CheckIfFinalState(initialState) == false)
            {
                initialState = currentState;
                int contor = 0;
                var visitedStates = new List<State> {initialState};
                while (visitedStates.Count < 100)
                {
                    contor++;
                    if (contor == 100)
                        break;
                    if (CheckIfFinalState(initialState))
                    {
                        return initialState;
                    }
                    var v1 = new Random().Next(initialState.b);
                    var v2 = new Random().Next(initialState.b);
                    var decideIfNegative = new Random().Next(2);
                    if (initialState.pb == 2)
                    {
                        v1 = -v1;
                        v2 = -v2;
                    }

                    var nextState = TransitionToState(initialState, v1, v2);
                    if (!visitedStates.Contains(nextState))
                    {
                        initialState = nextState;
                        visitedStates.Add(initialState);
                        Console.WriteLine(initialState);
                    }
                }
            }

            return initialState;
        }

        public static State TransitionToState(State s, int v1, int v2)
        {
            if (CheckIfTransitionParametersAreValid(s, v1, v2) == false)
                return s;
            State nextState = new State(s.b, s.m1 - v1, s.c1 - v2, 3 - s.pb, s.m2 + v1,  s.c2 + v2);
            if (CheckIfStateIsValid(s, s.c1 + s.c2, s.m1 + s.m2))
            {
                Console.WriteLine("v1 : " + v1 + " v2 : "+ v2);
                return nextState;
            }
            else
                return s;
        }

        private static bool CheckIfTransitionParametersAreValid(State s, int v1, int v2)
        {
            int m = s.m1 + s.m2;
            int c = s.c1 + s.c2;
            //if (!(v1 <= m && v1 >= -m)) // nu stiu de ce strica programul..
            //    return false;
            //if (!(v2 <= c && v1 >= -c))
            //    return false;
            if (! ( (s.pb == 1 && v1 >= 0 && v2 >= 0) || (s.pb == 2 && v1 <= 0 && v2 <= 0) ))
                return false;
            if (v1 + v2 > s.b || v1 + v2 < -s.b)
                return false;
            if (v1 == 0 && v2 == 0)
                return false;
            if (s.m1 - v1 < 0 || s.c1 - v2 < 0 || s.m2 + v1 < 0 || s.c2 + v2 < 0)
                return false;

            return true;
        }
        private static bool CheckIfStateIsValid(State s, int c, int m)
        { 
            if (s.c1 + s.c2 != c)
                return false;
            if (s.m1 + s.m2 != m)
                return false;
            if (s.m1 < s.c1 || s.m2 < s.c2)
                return false;

            return true;
        }
    }
}
