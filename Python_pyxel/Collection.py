import time

def gcd(n1, n2):
    '''
    最大公約数を見つけます

    条件

    n1 >= n2
    '''

    if n1 % n2 == 0:
        return n2
    else:
        return gcd(n2, n1 % n2)

class prime():
    '''
    primelist にn以下の素数を格納します

    coprimelist にn以下のnと互いに素な数を格納します
    '''

    primelist = []
    coprimelist = []
    n_prime = 0

    def __init__(self, n = 0):
        self.primelist = prime.primenumberlist(n)
        self.n_prime = prime.theprimenumber(n)
        self.coprimelist = prime.coprimenumberlist(n)

    def primenumberlist(n):
        '''
        n 以下の素数をリストで返します
        '''

        n_list = [x for x in range(5, n + 1, 2) if x % 3 != 0]
        prime_list = [2, 3]
        for _ in range(n):
            if prime_list[-1] ** 2 >= n:
                prime_list += n_list
                break
            else:
                prime_list.append(n_list[0])
                n_list = [x for x in n_list if x % prime_list[-1] != 0]
            if len(n_list) == 0:
                break
        return prime_list
    
    def theprimenumber(n):
        '''
        n 番目の素数を返します
        '''

        prime_list = [2, 3]
        i = 1
        while len(prime_list) < n:
            for j in range(2):
                check = True
                for s in prime_list:
                    if (6 * i + (-1) ** (j + 1)) % s == 0:
                        check = False
                        break
                    if s ** 2 >= 6 * i + (-1) ** (j + 1):
                        break
                if check:
                    prime_list.append(6 * i + (-1) ** (j + 1))
            i += 1
        return prime_list[n - 1]

    def coprimenumberlist(n):
        '''
        n と互いに素な n 未満の数をリストで返します
        '''

        cpri = [1]
        for i in range(2, n):
            if gcd(n, i) == 1:
                cpri.append(i)
        return cpri
    
    def primefactorization(n):
        '''
        n の素因数をリストで返します
        '''

        primefac = []
        m = n
        for i in range(2, n + 1):
            while m % i == 0:
                primefac.append(i)
                m /= i
        return primefac
    
    def isprime(n):
        '''
        n が素数かどうかを調べ bool で返します
        '''

        i = 1
        for j in range(2, 4):
            if n % j == 0:
                return False
        
        while True:
            for j in range(1, 3):
                nearprime = 6 * i + (-1) ** j
                if nearprime ** 2 > n:
                    return True
                elif n % nearprime == 0:
                    return False

class LCG():
    '''
    線形合同法 (Linear congruential generators) による乱数を生成します
    '''

    __seed = 0
    __a = 0
    __c = 0
    __m = 1
    __x = 0
    __start = 0

    def __init__(self, start, end):
        if start >= end:
            #　start が end 以上の場合、計算で起こる無限ループ回避のため初期設定をしない
            print("randomliner : start < end")
        else:
            #　初期設定をする
            #　a, c, m, seed の値を取得
            self.__start = start
            self.__m = end
            self.__a, self.__c = self.__find_a_c()
            self.__seed = pow(int(time.time() * 1000), 1, self.__m)
    
    def  __find_a_c(self):
        '''
        a と c の値を計算して return します
        '''

        #　a-1 の値を求める
        a = 4
        j = 0
        for i in prime.primefactorization(self.__m):
            if (j != i):
                a *= i
                j = i
        
        #　c の値を求める
        n_coprime = prime(self.__m).coprimelist
        c = n_coprime[len(n_coprime) // 2]
        
        return a + 1, c

    def random(self):
        '''
        生成した乱数を返します
        '''

        #　seed値が 0 以上か、生成した数字が start 未満のとき繰り返し計算する
        self.__x = (self.__a * self.__x + self.__c) % self.__m
        while self.__seed > 0 or self.__x < self.__start:
            self.__x = (self.__a * self.__x + self.__c) % self.__m
            self.__seed -= 1
        return self.__x

class ReLCG():
    '''
    LCGを改良した乱数を生成します
    '''

    __seed = 0
    __a = 0
    __c = 0
    __m = 10000
    __x = 0
    __start = 0
    __end = 0 #　追加

    def __init__(self, start, end):
        if start >= end:
            #　start が end 以上の場合、計算で起こる無限ループ回避のため初期設定をしない
            print("randomliner : start < end")
        else:
            #　初期設定をする
            #　a, c, m, seed の値を取得
            self.__start = start
            self.__end = end #　追加
            if self.__m < end: #　追加
                self.__m = end
            
            self.__a, self.__c = self.__find_a_c()
            self.__seed = pow(int(time.time() * 1000), 1, self.__m)
    
    def  __find_a_c(self):
        '''
        a と c の値を計算して return します
        '''

        #　a-1 の値を求める
        a = 4
        j = 0
        for i in prime.primefactorization(self.__m):
            if (j != i):
                a *= i
                j = i
        
        #　c の値を求める
        n_coprime = prime(self.__m).coprimelist
        c = n_coprime[len(n_coprime) // 2]
        
        return a + 1, c

    def random(self):
        '''
        生成した乱数を返します
        '''

        #　seed値が 0 以上か、生成した数字が start 未満のとき繰り返し計算する
        self.__x = (self.__a * self.__x + self.__c) % self.__m
        generated = self.__x * (self.__end - self.__start) // self.__m + self.__start #　追加
        while self.__seed > 0: #　一部変更
            self.__x = (self.__a * self.__x + self.__c) % self.__m
            self.__seed -= 1
        return generated #　変更
