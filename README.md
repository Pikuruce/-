それぞれのフォルダーに各言語のファイルが入っています

これまで作った関数やクラスの使い方をまとめておきます、Collectionファイルが作業ファイルの隣にあることを前提としています

```python
#　python

import Collection as col


#　gcd関数　(ユークリッド互除法)

n = col.gcd(18, 6)

print(n) # 6 を出力


#　primeクラス　(素な数)

p = col.prime(10)

print(p.primelist) # [2, 3, 5, 7] (10以下の素数)
print(col.prime.primenumberlist(10)) # 同様

print(p.coprimelist) # [1, 3, 7, 9] (10と互いに素な数)
print(col.prime.coprimenumberlist(10)) # 同様

print(p.n_prime) # 29 を出力 (10番目の素数)
print(col.prime.theprimenumber(10)) # 同様

print(col.prime.primefactorization(10)) # [2, 5] (10の素因数分解)

print(col.prime.isprime(10)) # False (10が素数かを判定)


#　LCGクラス・ReLCGクラス　(疑似乱数・線形合同法)

rnd = col.ReLCG(0, 5)

print(rnd.random()) # 0 ~ 4 のどれかを出力

```

```cs
//　C#

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Collection;

public class exp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //　GCD関数 (ユークリッド互除法)

        Calculate cal = new Calculate();

        Debug.Log(cal.GCD(18, 6)); //　6 を出力


        //　Primeクラス

        Prime p = new Prime(10);

        Debug.Log(string.Join(", ", p.primelist)); // [2, 3, 5, 7] (10以下の素数)
        Debug.Log(string.Join(", ", Prime.PrimeNumberList(10))); // 同様

        Debug.Log(string.Join(", ", p.coprimelist)); // [1, 3, 7, 9] (10と互いに素な数)
        Debug.Log(string.Join(", ", Prime.CoPrimeNumberList(10))); // 同様

        Debug.Log(p.theprime); // 29 を出力 (10番目の素数)
        Debug.Log(Prime.ThePrimeNumber(10)); // 同様

        Debug.Log(string.Join(", ", Prime.PrimeFactorization(10))); // [2, 5] (10の素因数分解)

        Debug.Log(Prime.IsPrime(10)); // false (10が素数かを判定)


        //　LCGクラス・ReLCGクラス (疑似乱数・線形合同法)

        ReLCG rnd = new ReLCG(0, 5);

        Debug.Log(rnd.random()); // 0 ~ 4 のどれかを出力
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
```

```js
//　javascript

import * as col from "./Collection.js";


//　gcd関数 (ユークリッド互除法)

console.log(col.gcd(18, 6)); //　6 を出力


//　primeクラス

let p = new col.prime(10);

console.log(p.primelist); // [2, 3, 5, 7] (10以下の素数)
console.log(col.prime.primenumberlist(10)); // 同様

console.log(p.coprimelist); // [1, 3, 7, 9] (10以下の10と互いに素な数)
console.log(col.prime.coprimenumberlist(10)); // 同様

console.log(p.theprime); // 29 を出力 (10番目の素数)
console.log(col.prime.theprimenumber(10)); // 同様

console.log(col.prime.primefactorization(10)); // [2, 5] (10素因数分解)

console.log(col.prime.isprime(10)); // false (10が素数かを判定)


//　LCGクラス・ReLCGクラス (疑似乱数・線形合同法)

let rnd = new col.ReLCG(0, 5);

console.log(rnd.random()); //　0 ~ 4 のどれかを出力
