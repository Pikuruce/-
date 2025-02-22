using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Collection
{
    //-----------------------#1-------------------------------
    public class Calculate
    {
        /// <summary>
        /// 二つの数字の最大公約数を求めます
        /// </summary>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        /// <returns></returns>
        public int GCD(int n1, int n2)
        {
            if (n1 % n2 == 0)
            {
                return n2;
            }
            else
            {
                return GCD(n2, n1 % n2);
            }
        }
    }

    //-----------------------#2-------------------------------

    /// <summary>
    /// 素な数に関するクラス
    /// </summary>
    public class Prime
    {
        public int[] primelist = {};
        public int theprime = 0;
        public int[] coprimelist = {};
        public Prime(int n)
        {
            primelist = PrimeNumberList(n);
            theprime = ThePrimeNumber(n);
            coprimelist = CoPrimeNumberList(n);
        }

        /// <summary>
        /// n以下の素数を返します
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int[] PrimeNumberList(int n)
        {
            List<int> prime_list = new List<int> {2, 3};
            int[] n_list = Enumerable.Range(0, (n - 5) / 2 + 1).Select(x => 5 + x * 2).Where(x => x % 3 != 0).ToArray();

            for (int i = 0; i < n; i++)
            {
                if (Math.Pow(prime_list[prime_list.Count - 1], 2) >= n)
                {
                    prime_list.AddRange(n_list.ToList());
                    break;
                }
                else
                {
                    prime_list.Add(n_list[0]);
                    n_list = n_list.Where(x => x % n_list[0] != 0).ToArray();
                }
                if (n_list.Length == 0)
                {
                    break;
                }
            }

            return prime_list.ToArray();
        }

        /// <summary>
        /// n番目の素数を返します
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int ThePrimeNumber(int n)
        {
            List<int> primelist = new List<int> {2, 3};
            int i = 1;
            while (primelist.Count < n)
            {
                for (int j = 0; j < 2; j++)
                {
                    bool check = true;
                    foreach (int s in primelist)
                    {
                        if ((6 * i + (int)Math.Pow(-1, j + 1)) % s == 0)
                        {
                            check = false;
                            break;
                        }
                        if (Math.Pow(s, 2) >= 6 * i + (int)Math.Pow(-1, j + 1))
                        {
                            break;
                        }
                    }
                    if (check)
                    {
                        primelist.Add(6 * i + (int)Math.Pow(-1, j + 1));
                    }
                }
                i++;
            }
            return primelist[n - 1];
        }

        /// <summary>
        /// n以下のnと互いに素な数を返します
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int[] CoPrimeNumberList(int n)
        {
            List<int> coprimelist = new List<int> {1};
            Calculate cal = new Calculate();
            for (int i = 2; i < n; i++)
            {
                if (cal.GCD(n, i) == 1)
                {
                    coprimelist.Add(i);
                }
            }
            return coprimelist.ToArray();
        }

        /// <summary>
        /// nを素因数分解します
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int[] PrimeFactorization(int n)
        {
            List<int> primefac = new List<int> {};
            int m = n;
            for (int i = 2; i < n + 1; i++)
            {
                while (m % i == 0)
                {
                    primefac.Add(i);
                    m /= i;
                }
                if (m == 1)
                {
                    break;
                }
            }
            return primefac.ToArray();
        }

        /// <summary>
        /// n が素数かどうかを判定します
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool IsPrime(int n)
        {
            int i = 1;
            for (int j = 2; j < 4; j++)
            {
                if (n % j == 0)
                {
                    return false;
                }
            }

            while (true)
            {
                for (int j = 1; j < 3; j++)
                {
                    int nearprime = 6 * i + (int)Math.Pow(-1, j);
                    if (Math.Pow(nearprime, 2) > n)
                    {
                        return true;
                    }
                    else if (n % nearprime == 0)
                    {
                        return false;
                    }
                }
            }
        }
    }

    //-----------------------#3-------------------------------

    /// <summary>
    ///　線形合同法 (Linear congruential generators) による乱数を生成します
    /// </summary>
    class LCG
    {
        private int seed;
        private int a;
        private int c;
        private int m;
        private int x = 0;
        private int start;

        /// <summary>
        /// start 以上 end 未満の乱数を生成します
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public LCG(int start, int end)
        {
            if (start >= end)
            {
                //　start が end 以上の場合、計算で起こる無限ループ回避のため初期設定をしない
                Debug.Log("randomliner : start < end");
            }
            else
            {
                //　初期設定をする
                //　a, c, m, seed の値を取得
                this.start = start;
                this.seed = DateTime.Now.Millisecond % end;
                this.m = end;
                int[] a_c = find_a_c(end);
                this.a = a_c[0];
                this.c = a_c[1];
            }
        }

        /// <summary>
        /// a と c の値を求めます
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        private int[] find_a_c(int m)
        {
            //　a_c を定義、0番目が a、 1番目が c
            int[] a_c = {4, 0};

            //　a の値を求める
            int j = 0;
            foreach (int i in Prime.PrimeFactorization(m))
            {
                if (i != j)
                {
                    a_c[0] *= i;
                    j = i;
                }
            }
            a_c[0] += 1;

            //　c の値を求める
            int[] coli = Prime.CoPrimeNumberList(m);
            a_c[1] = coli[coli.Length / 2];
            return a_c;
        }

        /// <summary>
        ///　生成した乱数を返します
        /// </summary>
        /// <returns></returns>
        public int random()
        {
            //　乱数を生成、生成した値が start 未満または seed が 1以上なら再生成
            this.x = (this.a * this.x + this.c) % this.m;
            while (this.seed > 0 || this.start > this.x)
            {
                this.x = (this.a * this.x + this.c) % this.m;
                this.seed -= 1;
            }
            return this.x;
        }
    }

    /// <summary>
    ///　LCGを改良した乱数を生成します
    /// </summary>
    class ReLCG
    {
        private int seed;
        private int a;
        private int c;
        private int m = 10000;
        private int x = 0;
        private int start;
        private int end; //　追加

        /// <summary>
        /// start 以上 end 未満の乱数を生成します
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public ReLCG(int start, int end)
        {
            if (start >= end)
            {
                //　start が end 以上の場合、計算で起こる無限ループ回避のため初期設定をしない
                Debug.Log("randomliner : start < end");
            }
            else
            {
                //　初期設定をする
                //　a, c, m, seed の値を取得
                this.start = start;
                this.seed = DateTime.Now.Millisecond % this.m;
                this.end = end; //　追加
                if (this.m < end) //　追加
                {
                    this.m = end;
                }
                int[] a_c = find_a_c(end);
                this.a = a_c[0];
                this.c = a_c[1];
            }
        }

        /// <summary>
        /// a と c の値を求めます
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        private int[] find_a_c(int m)
        {
            //　a_c を定義、0番目が a、 1番目が c
            int[] a_c = {4, 0};

            //　a の値を求める
            int j = 0;
            foreach (int i in Prime.PrimeFactorization(m))
            {
                if (i != j)
                {
                    a_c[0] *= i;
                    j = i;
                }
            }
            a_c[0] += 1;

            //　c の値を求める
            int[] coli = Prime.CoPrimeNumberList(m);
            a_c[1] = coli[coli.Length / 2];
            return a_c;
        }

        /// <summary>
        ///　生成した乱数を返します
        /// </summary>
        /// <returns></returns>
        public int random()
        {
            //　乱数を生成、生成した値が start 未満または seed が 1以上なら再生成
            this.x = (this.a * this.x + this.c) % this.m;
            int generated = this.x * (this.end - this.start) / this.m + this.start; //　追加
            while (this.seed > 0 || generated < this.start) //　一部変更
            {
                this.x = (this.a * this.x + this.c) % this.m;
                this.seed -= 1;
                generated = this.x * (this.end - this.start) / this.m + this.start; //　追加
            }
            return generated; //　変更
        }
    }
}
