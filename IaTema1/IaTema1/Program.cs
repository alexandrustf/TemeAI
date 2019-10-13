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

            RandomStrategy(GetInitialState(6, 8, 0, 5, 0, 1));
            //BacktrackingStrategy(GetInitialState(6, 5, 0, 3, 0, 1));
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
        
        //public static State BacktrackingStrategy(State initialState) // NOT working!
        //{
        //    if (CheckIfFinalState(initialState))
        //    {
        //        return initialState;
        //    }
        //    var s = new State(initialState);
        //    while (!CheckIfFinalState(s))
        //    {
        //        s = BKT(s);
        //    }

        //    return s;
        //}

        //private static State BKT(State s)
        //{
        //    for (int v1 = 0; v1 <= s.m1; v1++)
        //    {
        //        for (int v2 = 0; v2 <= s.c1; v2++)
        //        {
        //            int v11 = v1, v22 = v2;
        //            if (s.pb == 2)
        //            {
        //                v11 = -v1;
        //                v22 = -v2;
        //            }
        //            var nextState = TransitionToState(s, v11, v22);
        //            if (!nextState.Equals(s))
        //            {
        //                Console.WriteLine("AM AJUNS AICI!");
        //                s = nextState;
        //                Console.WriteLine(s);
        //                if (CheckIfFinalState(s))
        //                    return s;
        //            }
        //        }
        //    }
        //    return s;
        //}
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

                    var nextState = FindValidRandomState(initialState);
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

        private static State FindValidRandomState(State initialState)
        {
            var v1 = new Random().Next(initialState.b);
            var v2 = new Random().Next(initialState.b);
            if (initialState.pb == 2)
            {
                v1 = -v1;
                v2 = -v2;
            }

            State nextState = new State(initialState);
            if (CheckValidState(initialState, v1, v2))
                nextState = new State(initialState.b, initialState.m1 - v1, initialState.c1 - v2, 3 - initialState.pb,
                    initialState.m2 + v1, initialState.c2 + v2);
            return nextState;
        }

        private static bool CheckValidState(State s, int v1, int v2)
        {
            if (CheckIfTransitionParametersAreValid(s, v1, v2) && CheckIfStateIsValid(s, s.c1 + s.c2, s.m1 + s.m2))
                return true;
            return false;

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
