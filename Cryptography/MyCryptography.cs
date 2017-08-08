using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Numerics;
using System.Security.Cryptography;
using System.IO;


namespace Cryptography
{
    class MyCryptography
    {
        private int euclid(int a, int b)
        {
            int r1, r2, t1, t2, q, r, t, inverse, temp;
            if (a < b)
            {
                temp = b;
                b = a;
                a = temp;
            }
            r1 = a;
            r2 = b;
            t1 = 0;
            t2 = 1;
            while (r2 != 0)
            {
                q = r1 / r2;
                r = r1 % r2;
                t = t1 - (q * t2);
                t1 = t2;
                t2 = t;
                r1 = r2;
                r2 = r;
            }
            inverse = t1 % t2;
            if (inverse < 0)
            {
                inverse = inverse + t2;
            }
            return inverse;
        }

        static int[,] Multiplication(int[,] a, int[,] b)
        {
            if (a.GetLength(1) != b.GetLength(0)) throw new Exception("Матриці неможна перемножити!");
            int[,] r = new int[a.GetLength(0), b.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    for (int k = 0; k < b.GetLength(0); k++)
                    {
                        r[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            return r;
        }


        private bool checkOneMatrix(int[,] inputMatrix,int alphabetLength) 
        {
            int[,] oneMatrix = new int[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };
            int [,]resultMatrix = inverseMatrix(inputMatrix, alphabetLength);
                resultMatrix = Multiplication(resultMatrix, inputMatrix);
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    resultMatrix[i, j] = resultMatrix[i, j] % alphabetLength;
                    if (resultMatrix[i,j]!=oneMatrix[i,j])
                    return false;
                }
            return true;
        }



        public int[,] inverseMatrix(int[,] inputMatrix,int alphabetLength) 
        {
            int[,] resultMatrix = new int[3, 3];

            int det = (inputMatrix[0, 0] * (inputMatrix[1, 1] * inputMatrix[2, 2] - inputMatrix[1, 2] * inputMatrix[2, 1]) -
                inputMatrix[0, 1] * (inputMatrix[1, 0] * inputMatrix[2, 2] - inputMatrix[1, 2] * inputMatrix[2, 0]) +
                inputMatrix[0, 2] * (inputMatrix[1, 0] * inputMatrix[2, 1] - inputMatrix[1, 1] * inputMatrix[2, 0]))/*%alphabetLength*/;
            
            int abc = det;
            if (abc < 0) det = alphabetLength - ((Math.Abs(abc)) % alphabetLength);
            else det = (abc % alphabetLength);
            det = euclid(det, alphabetLength);

            resultMatrix[0, 0] =(int)Math.Pow(-1,1+1)*   ((inputMatrix[1, 1] * inputMatrix[2, 2]) - (inputMatrix[2, 1] * inputMatrix[1, 2]));
            resultMatrix[0, 1] =(int)Math.Pow(-1, 1 + 2)*((inputMatrix[1, 0] * inputMatrix[2, 2]) - (inputMatrix[2, 0] * inputMatrix[1, 2]));
            resultMatrix[0, 2] =(int)Math.Pow(-1, 1 + 3)*((inputMatrix[1, 0] * inputMatrix[2, 1]) - (inputMatrix[2, 0] * inputMatrix[1, 1]));

            resultMatrix[1, 0] = (int)Math.Pow(-1, 2 + 1) * ((inputMatrix[0, 1] * inputMatrix[2, 2]) - (inputMatrix[2, 1] * inputMatrix[0, 2]));
            resultMatrix[1, 1] = (int)Math.Pow(-1, 2 + 2) * ((inputMatrix[0, 0] * inputMatrix[2, 2]) - (inputMatrix[2, 0] * inputMatrix[0, 2]));
            resultMatrix[1, 2] = (int)Math.Pow(-1, 2 + 3) * ((inputMatrix[0, 0] * inputMatrix[2, 1]) - (inputMatrix[2, 0] * inputMatrix[0, 1]));

            resultMatrix[2, 0] = (int)Math.Pow(-1, 3 + 1) * ((inputMatrix[0, 1] * inputMatrix[1, 2]) - (inputMatrix[0, 2] * inputMatrix[1, 1]));
            resultMatrix[2, 1] = (int)Math.Pow(-1, 3 + 2) * ((inputMatrix[0, 0] * inputMatrix[1, 2]) - (inputMatrix[0, 2] * inputMatrix[1, 0]));
            resultMatrix[2, 2] = (int)Math.Pow(-1, 3 + 3) * ((inputMatrix[0, 0] * inputMatrix[1, 1]) - (inputMatrix[0, 1] * inputMatrix[1, 0]));
           
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                    {
                        if (resultMatrix[i, j] < 0) resultMatrix[i, j] = alphabetLength - 
                            ((Math.Abs(resultMatrix[i, j])) % alphabetLength);
                        else resultMatrix[i, j] = (resultMatrix[i, j] % alphabetLength);
                        resultMatrix[i, j] = ((resultMatrix[i, j] * det) % alphabetLength);
                    }

            int[,] transponMatrix = new int[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    transponMatrix[i, j] = resultMatrix[j, i];
                }

            resultMatrix = transponMatrix;
            return resultMatrix;
        }
       

        public string  shifrHilla(string alphabet,string inputedText, string keyText, bool encrypt) 
        {
            //12 14 18
            string result="";
            string inputText=inputedText;
            while (inputText.Length % 3 != 0)
            {
                inputText += " ";
            }

            if (inputText.Length > 0)
            {
                int[,] keyArray = new int[3, 3];
                List<int> resultList = new List<int>();
                int countKey=0;

                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++) 
                    {
                        keyArray[i, j] = alphabet.ToString().IndexOf(keyText[countKey]);
                        countKey++;
                    }
                    if (!checkOneMatrix(keyArray, alphabet.Length)) throw new Exception("Матриця не є оборотною!"+'\n'+"Змініть ключ або довжину алфавіту!");
                if (encrypt == false) keyArray = inverseMatrix(keyArray, alphabet.Length);

                        for (int i = 0; i < inputText.Length;)
                        {
                            int letterOne = alphabet.ToString().IndexOf(inputText[i]); i++;
                            int letterTwo = alphabet.ToString().IndexOf(inputText[i]); i++;
                            int letterThree = alphabet.ToString().IndexOf(inputText[i]); i++;
                              for (int j = 0; j < 3; j++)
                              {
                                        int localResultAdd = 0;
                                        localResultAdd += keyArray[j, 0] * letterOne;
                                        localResultAdd += keyArray[j, 1] *letterTwo;
                                        localResultAdd += keyArray[j, 2] * letterThree;
                                        localResultAdd = localResultAdd % alphabet.Length;
                                        resultList.Add(localResultAdd);
                              }
                        }
                        foreach (var letter in resultList)
                        if (letter>=0)
                        result += alphabet[letter];
            }
            return result;
        }

        public string feistelNetwork(string inputText,string alphabet,int roundCount,int K0, bool decrypt) 
        {
            string result="";
            
            if (inputText.Length % 2 != 0) inputText = inputText + " ";
            int[] letterArray = new int[inputText.Length];

            for (int i = 0; i < inputText.Length; i++)
                letterArray[i] = alphabet.ToString().IndexOf(inputText[i]);

            
            for (int j = 0; j < inputText.Length; j+=2) 
            {

                int K = decrypt ? K0 + roundCount-1 : K0;
                int l = letterArray[j];
                int r = letterArray[j+1];
                for (int i = 0; i < roundCount; i++)
                {
                    if (i < roundCount-1)
                    {
                        int t = l;
                        l = r ^ feistelFunction(K, l, alphabet.Length);
                        r = t;
                    }
                    else 
                    {
                        r = r ^ feistelFunction(K, l, alphabet.Length);
                    }
                    K += decrypt ? -1 : 1;
                    
                }
                letterArray[j] = l;
                letterArray[j+1] = r;
            }
            try
            {
                foreach (var letter in letterArray)
                {
                    result += alphabet[letter];
                }
            }
            catch 
            {
                throw new Exception("Не відповідність алфавіту!" + '\n' + "В алфавіті відсутні деякі літери з тексту!");
            }
                return (result);
        }
        int feistelFunction(int K, int i, int alphabetLength)
        {
            return ((K + i) % alphabetLength);
        }

        public static void DESEncryptor(string text, string key, string iv, string filepath)
        {
            byte[] pText = UnicodeEncoding.UTF8.GetBytes(text);

            using (DESCryptoServiceProvider desCryptoService = new DESCryptoServiceProvider())
            {
                try
                {
                    desCryptoService.Key = Encoding.ASCII.GetBytes(key);
                    desCryptoService.IV = Encoding.ASCII.GetBytes(iv);

                    FileStream fsIn = new FileStream(filepath, FileMode.Create, FileAccess.Write);
                    CryptoStream crsIn = new CryptoStream(fsIn, desCryptoService.CreateEncryptor(), CryptoStreamMode.Write);
                    crsIn.Write(pText, 0, pText.Length);
                    crsIn.Close();
                    fsIn.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        public static string DESDecryptor(string key, string iv, string filepath)
        {
            string result = "";
            using (DESCryptoServiceProvider desCryptoService = new DESCryptoServiceProvider())
            {
                try
                {
                    desCryptoService.Key = Encoding.ASCII.GetBytes(key);
                    desCryptoService.IV = Encoding.ASCII.GetBytes(iv);
                    FileStream fsOut = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                    CryptoStream crsOut = new CryptoStream(fsOut, desCryptoService.CreateDecryptor(), CryptoStreamMode.Read);
                    StreamReader strRead = new StreamReader(crsOut);
                    result = strRead.ReadToEnd();
                    strRead.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            return result;
        }

        public BigInteger[] RSAgetLetterNumber(string inputText, string alphabet) 
        {
            BigInteger[] result = new BigInteger[inputText.Length];

            for (int i = 0; i < inputText.Length; i++)
                result[i] = alphabet.ToString().IndexOf(inputText[i]);

            return result;
        }

        public string RSAgetLetterFromNumber(BigInteger []arrayLetter,string alphabet)
        {
            string result = "";
            try
            {
                foreach (var letter in arrayLetter)
                    result += alphabet[(int)letter % alphabet.Length].ToString();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            return result; 
        }

        public BigInteger[] RSAgetKeys(BigInteger p, BigInteger q) 
        {
            BigInteger[] rsaEND = new BigInteger[3];
            rsaEND[1] = BigInteger.Multiply(p, q);
            BigInteger rsa_fn = BigInteger.Multiply((p - 1), (q - 1));
            rsaEND[0] = 19;            
            //rsaEND[2] = BigInteger.ModPow(rsaEND[0], (rsa_fn - 1), rsa_fn);
            BigInteger kk = 0;
            for (int i = 0; kk != 1; i++)
            {
                //rsaEND[2] = ((rsa_fn-1) * i) / rsaEND[0];
                //kk = (rsaEND[2] * rsaEND[0]) % rsa_fn;
                //if (kk < 0) kk += rsa_fn;
                rsaEND[2] = ((rsa_fn * i + 1)) / rsaEND[0];
                kk = (rsaEND[2] * rsaEND[0]) % rsa_fn;
                if (kk < 0) kk += rsa_fn;
            }
            return rsaEND;
        }

        public BigInteger[] RSAEncode(BigInteger e, BigInteger n, BigInteger[] text) 
        {
            text = To1_5(text);
            BigInteger[] result = text;
            for (int i = 0; i < text.Length; i++)
            {
                text[i] = BigInteger.ModPow(text[i], e, n);
            }
            return result;
        }

        public BigInteger[] RSADecode(BigInteger e, BigInteger n, BigInteger[] text)
        {
            BigInteger[] result = text;
            for (int i = 0; i < text.Length; i++)
            {
                text[i] = BigInteger.ModPow(text[i], e, n);
            }
            return From1_5(result);
        }

        public BigInteger[] To1_5(BigInteger[] tex)
        {
            List<BigInteger> text = new List<BigInteger>(tex);
            for(int i=0;i< text.Count % 3; i++)
            {
                text.Add(26);
            }
            
            List<BigInteger> result = new List<BigInteger>();
            for (int i = 0; i < text.Count; i+=3)
            {
                result.Add(100 * (int)(text[i]/10) + 10 * (int)(text[i] % 10) + (int)(text[i+1] / 10));
                result.Add(100 * (int)(text[i+1] % 10) + 10 * (int)(text[i+2] / 10) + (int)(text[i+2] % 10));
            }
            return result.ToArray();
        }

        public BigInteger[] From1_5(BigInteger[] tex)
        {
            List<BigInteger> text = new List<BigInteger>(tex);

            List<BigInteger> result = new List<BigInteger>();
            for (int i = 0; i < text.Count; i += 2)
            {
                result.Add(10 * (int)(text[i] / 100) +  (int)(text[i] / 10)%10);

                result.Add(10 * (int)(text[i] % 10) + (int)(text[i + 1] / 100));

                result.Add(10 * ((int)(text[i+1] / 10) % 10) + (int)(text[i+1] % 10));

            }
            return result.ToArray();
        }


    }
}
