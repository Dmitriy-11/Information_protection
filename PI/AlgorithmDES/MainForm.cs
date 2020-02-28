using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgorithmDES
{
    public partial class MainForm : Form
    {

        // IP(ОТi) - для начальной перестановки
        int[] IP = {58, 50, 42, 34, 26, 18, 10, 2,
                    60, 52, 44, 36, 28, 20, 12, 4,
                    62, 54, 46, 38, 30, 22, 14, 6,
                    64, 56, 48, 40, 32, 24, 16, 8,
                    57, 49, 41, 33, 25, 17,  9, 1,
                    59, 51, 43, 35, 27, 19, 11, 3,
                    61, 53, 45, 37, 29, 21, 13, 5,
                    63, 55, 47, 39, 31, 23, 15, 7 };

        //IP-1(Bi) - для завершающей обратной перестановки
        int[] IPB = {40, 8, 48, 16, 56, 24, 64, 32,
                     39, 7, 47, 15, 55, 23, 63, 31,
                     38, 6, 46, 14, 54, 22, 62, 30,
                     37, 5, 45, 13, 53, 21, 61, 29,
                     36, 4, 44, 12, 52, 20, 60, 28,
                     35, 3, 43, 11, 51, 19, 59, 27,
                     34, 2, 42, 10, 50, 18, 58, 26,
                     33, 1, 41,  9, 49, 17, 57, 25 };

        // E(i) - для расширения E
        int[] E = {32,  1,  2,  3,  4,  5,
                    4,  5,  6,  7,  8,  9,
                    8,  9, 10, 11, 12, 13,
                   12, 13, 14, 15, 16, 17,
                   16, 17, 18, 19, 20, 21,
                   20, 21, 22, 23, 24, 25,
                   24, 25, 26, 27, 28, 29,
                   28, 29, 30, 31, 32,  1};


        int[,] Si = { {14,4,13,1,2,15,11,8,3,10,6,12,5,9,0,7}, // S1
                      {0,15,7,4,14,2,13,1,10,6,12,11,9,5,3,8},
                      {4,1,14,8,13,6,2,11,15,12,9,7,3,10,5,0},
                      {15,12,8,2,4,9,1,7,5,11,3,14,10,0,6,13},

                      {15,1,8,14,6,11,3,4,9,7,2,13,12,0,5,10}, // S2
                      {3,13,4,7,15,2,8,14,12,0,1,10,6,9,11,5},
                      {0,14,7,11,10,4,13,1,5,8,12,6,9,3,2,15},
                      {13,8,10,1,3,15,4,2,11,6,7,12,0,5,14,9},
                     
                      {10,0,9,14,6,3,15,5,1,13,12,7,11,4,2,8}, // S3
                      {13,7,0,9,3,4,6,10,2,8,5,14,12,11,15,1},
                      {13,6,4,9,8,15,3,0,11,1,2,12,5,10,14,7},
                      {1,10,13,0,6,9,8,7,4,15,14,3,11,5,2,12},
                     
                      {7,13,14,3,0,6,9,10,1,2,8,5,11,12,4,15}, // S4
                      {13,8,11,5,6,15,0,3,4,7,2,12,1,10,14,9},
                      {10,6,9,0,12,11,7,13,15,1,3,14,5,2,8,4},
                      {3,15,0,6,10,1,13,8,9,4,5,11,12,7,2,14},
                     
                      {2,12,4,1,7,10,11,6,8,5,3,15,13,0,14,9}, // S5
                      {14,11,2,12,4,7,13,1,5,0,15,10,3,9,8,6},
                      {4,2,1,11,10,13,7,8,15,9,12,5,6,3,0,14},
                      {11,8,12,7,1,14,2,13,6,15,0,9,10,4,5,3},
                     
                      {12,1,10,15,9,2,6,8,0,13,3,4,14,7,5,11}, // S6
                      {10,15,4,2,7,12,9,5,6,1,13,14,0,11,3,8},
                      {9,14,15,5,2,8,12,3,7,0,4,10,1,13,11,6},
                      {4,3,2,12,9,5,15,10,11,14,1,7,6,0,8,13},
                     
                      {4,11,2,14,15,0,8,13,3,12,9,7,5,10,6,1}, // S7
                      {13,0,11,7,4,9,1,10,14,3,5,12,2,15,8,6},
                      {1,4,11,13,12,3,7,14,10,15,6,8,0,5,9,2},
                      {6,11,13,8,1,4,10,7,9,5,0,15,14,2,3,12},
                     
                      {13,2,8,4,6,15,11,1,10,9,3,14,5,0,12,7}, // S8
                      {1,15,13,8,10,3,7,4,12,5,6,11,0,14,9,2},
                      {7,11,4,1,9,12,14,2,0,6,10,13,15,3,5,8}, // ?
                      {2,1,14,7,4,10,8,13,15,12,9,0,3,5,6,11}  // ?
                     };

        // P(i)- для перестановка P
        int[] Pi = { 16,  7, 20, 21, 29, 12, 28, 17,
                      1, 15, 23, 26,  5, 18, 31, 10,
                      2,  8, 24, 14, 32, 27,  3,  9,
                     19, 13, 30,  6, 22, 11,  4, 25};

        string Key = "2354267354237543";

        // PC1(j) - для перестановки выбора битов исходного ключа K
        int[] PC1 = {57, 49, 41, 33, 25, 17,  9,  1, 58, 50, 42, 34, 26, 18,
                     10,  2, 59, 51, 43, 35, 27, 19, 11,  3, 60, 52, 44, 36,
                     63, 55, 47, 39, 31, 23, 15,  7, 62, 54, 46, 38, 30, 22,
                     14,  6, 61, 53, 45, 37, 29, 21, 13,  5, 28, 20, 12,  4};

        // LSi - для операция циклического сдвига на LSi позиций влево
        int[] LS = { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1};

        // PC2(j) - для перестановке-выбору PC2
        int[] PC2 = {14, 17, 11, 24,  1,  5,  3, 28, 15,  6, 21, 10, 23, 19, 12,  4,
                     26,  8, 16,  7, 27, 20, 13,  2, 41, 52, 31, 37, 47, 55, 30, 40,
                     51, 45, 33, 48, 44, 49, 39, 56, 34, 53, 46, 42, 50, 36, 29, 32};

        // Начальный вектор
        static string C0;


        string mod;       // режим перевода

        public MainForm()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// Переключатель "Шифровать"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            // Убираем элементы управления
            radioButton3.Checked = false;
            radioButton4.Checked = false;

            mod = "Шифровать";
            label1.Text = "Bвод произвольного открытого текста ";
            label2.Text = "Шифрограмма";
            
            // Очищаем поле для ввода
            textBox1.Clear();
            textBox2.Clear();

            // Устанавливаем начальный вектор
            C0 = "0110010110011100011001101001111100010110010011010100110011000010";
        }

        /// <summary>
        /// Переключатель "Дешифровать"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            // Убираем элементы управления
            radioButton3.Checked = false;
            radioButton4.Checked = false;

            mod = "Дешифровать";
            label1.Text = "Шифрограмма ";
            label2.Text = "Открытый текст ";

            // Очищаем поле для ввода
            textBox1.Clear();
            textBox2.Clear();

            // Устанавливаем начальный вектор
            C0 = "0110010110011100011001101001111100010110010011010100110011000010";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Очищаем поле вывода
            textBox2.Clear();

            switch (mod)
            {
                case "Шифровать в CBC":
                    EncryptionCBC();
                    break;
                case "Шифровать в CBF":
                    EncryptionCBF();
                    break;
                case "Дешифровать в CBC":
                    DecryptionCBC();
                    break;
                case "Дешифровать в CBF":
                    DecryptionCBF();
                    break;
                default:
                    break;
            }     
        }

        /// <summary>
        ///  Функция для первеода строки в двочисную СС
        /// </summary>
        /// <param name="str"></param>
        /// <returns>  Список строк (строка = 64-бит информации) </returns>
        private string TransliteIn2CC(string str)
        {
            // открытый текст в двоичном сс
            string In2CC = "";
            
            for (int i = 0; i < str.Length; i++)
            {
                // перевод каждого когда символа в двоичную СС
                In2CC += Convert.ToString(Convert.ToInt32(str[i]), 2).PadLeft(8, '0');         
            }


            return In2CC;
        }

        /// <summary>
        /// Функция для сдвига на определенное кол-во бит слова влево
        /// </summary>
        /// <param name="count"> кол-во бит для сдвига </param>
        /// <param name="str"> сама строка, которая нуждается в сдвиге </param>
        /// <returns></returns>
        private string ShiftBit(int count, string str)
        {
            string tempStr = "";
            
            for (int i = 0; i < str.Length; i++)
            {
                tempStr += str[(i + count) % str.Length];
            }

            return tempStr;
        }

        /// <summary>
        /// Функция для формирования ключей
        /// </summary>
        /// <returns> список ключей </returns>
        private List<string> generationKey()
        {
            // Ключ К
            string tempKey = "";
            for (int i = 0; i < 16; i++)
            {
                tempKey += Convert.ToString(Convert.ToInt32(Key.Substring(i,1), 16), 2).PadLeft(4, '0');
            }
            // Перестановка-выбор PC1
            string perestanovkaPC1 = "";
            for (int i = 0; i < 56; i++)
            {
                perestanovkaPC1 += tempKey[PC1[i] - 1];
            }

            // Разделение на левый и правый блок
            string LeftBlock = perestanovkaPC1.Substring(0, 28);
            string RightBlock = perestanovkaPC1.Substring(28);
            string tempLeftAndRight = "";

            // Получение 16-ти ключей
            List<string> key = new List<string>();
            string perestanovkaPC2 = "";
            for (int i = 0; i < 16; i++)
            {
                LeftBlock = ShiftBit(LS[i], LeftBlock);
                RightBlock = ShiftBit(LS[i], RightBlock);
                tempLeftAndRight = LeftBlock + RightBlock;
                // перестановка-выбор PC2
                for (int j = 0; j < 48; j++)
                {
                    perestanovkaPC2 += tempLeftAndRight[PC2[j] - 1];
                }
                // добавление ключа в список
                key.Add(perestanovkaPC2);
                // чистка строку
                perestanovkaPC2 = "";
            }
            return key;
        }

        /// <summary>
        /// Функция для битовой операциии XOR (исключающее «или»)
        /// </summary>
        /// <param name="str1"> строка, находящаяся слева от оператора </param>
        /// <param name="str2"> строка, находящаяся справа от оператора </param>
        /// <returns> Результат сложения по модулю 2 </returns>
        private string XOR(string str1, string str2)
        {
            string rezultXOR = "";
            for (int i = 0; i < str1.Length; i++)
            {
                rezultXOR += (Convert.ToInt32(str1[i]) ^ Convert.ToInt32(str2[i])).ToString();
            }
            return rezultXOR;
        }

        /// <summary>
        /// Функция для вычисления значения f(Ri-1, Ki)
        /// </summary>
        /// <param name="Ri"> Левый блок </param>
        /// <returns> f(Ri-1, Ki) </returns>
        private string calcF(string Ri, string key)
        {
            // Расширение Е
            string tempString = Ri.PadLeft(48, ' ');
            StringBuilder temp = new StringBuilder(tempString);
            for (int i = 0; i < 48; i++)
            {
                temp[i] = Ri[E[i] - 1];
            }
            tempString = temp.ToString();

            // Расширение Е XOR Kлючi
            string rezultXOR = XOR(tempString, key);

            #region Прохождение по Si
            List<string> S = new List<string>();
            List<string> k = new List<string>();
            List<string> l = new List<string>();
            for (int i = 0; i < 8; i++)
            {
                S.Add(rezultXOR.Substring(i + 5 * i, 6));
                k.Add(S[i].Substring(0, 1) + S[i].Substring(5, 1)); // b1b6
                l.Add(S[i].Substring(1, 4));                        // b2b3b4b5
            }
            S.Clear();
            int kk=0, 
                ll=0;
            tempString = "";
            for (int i = 0; i < k.Count; i++)
            {
                switch (k[i])
                {
                    case "00": kk = 0; break;
                    case "01": kk = 1; break;
                    case "10": kk = 2; break;
                    case "11": kk = 3; break;
                }

                switch (l[i])
                {
                    case "0000": ll = 0; break;
                    case "0001": ll = 1; break;
                    case "0010": ll = 2; break;
                    case "0011": ll = 3; break;
                    case "0100": ll = 4; break;
                    case "0101": ll = 5; break;
                    case "0110": ll = 6; break;
                    case "0111": ll = 7; break;
                    case "1000": ll = 8; break;
                    case "1001": ll = 9; break;
                    case "1010": ll = 10; break;
                    case "1011": ll = 11; break;
                    case "1100": ll = 12; break;
                    case "1101": ll = 13; break;
                    case "1110": ll = 14; break;
                    case "1111": ll = 15; break;
                }

                tempString += (Convert.ToString(Si[(kk + i * 4), ll], 2).PadLeft(4, '0'));
            }
            #endregion

            // Перестановка Р
            temp = new StringBuilder(tempString);
            for (int i = 0; i < 32; i++)
            {
                temp[i] = tempString[Pi[i] - 1];
            }
            tempString = temp.ToString();

            // Очищаем временню переменную
            temp.Clear();

            return tempString;
        }

        /// <summary>
        /// Шифрация в режиме СВС
        /// </summary>
        private void EncryptionCBC()
        {
            // Если не хватает кол-во символом, то в конце добавляем пробелы
            while (textBox1.Text.Length % 8 != 0)
            {
                textBox1.Text += " ";
            }

            // Открытый текст в двоичном сс
            string OTIn2CC = TransliteIn2CC(textBox1.Text);

            List<string> rezultEncoder = new List<string>();

            while (OTIn2CC.Length != 0)
            {
                // Берем первый 64-ех битовый блок
                string oneBlock = OTIn2CC.Substring(0, 64);
                // Делаем сложение по модулю 2
                oneBlock = XOR(C0, oneBlock);

                // Начальная перестановка
                string temp = "";
                for (int i = 0; i < 64; i++)
                {
                    temp += oneBlock[IP[i] - 1];
                }

                #region Деление на левую и правую части

                string str32bitLeft = string.Empty;
                str32bitLeft = temp.Substring(0, 32);
                string str32bitRight = string.Empty;
                str32bitRight = temp.Substring(32, 32);

                #endregion

                // Формирование ключей
                List<string> listKey = new List<string>();
                listKey = generationKey();

                // 16 циклов шифрующих преобразований
                string tepmLeftBlock = "";
                for (int j = 0; j < 16; j++)
                {
                    tepmLeftBlock = str32bitLeft;
                    // Li = Ri-1
                    str32bitLeft = str32bitRight;
                    // Ri = Li-1 XOR F
                    str32bitRight = XOR(tepmLeftBlock, calcF(str32bitRight, listKey[j]));
                }

                // Итоговый двоичный код (64-бит)
                string rezultEncoderBin = string.Empty;
                rezultEncoderBin = str32bitRight + str32bitLeft;
				
				 // Завершающая обратная перестановка
                string sTemp = "";
                for (int i = 0; i < rezultEncoderBin.Length; i++)
                {
                    sTemp += rezultEncoderBin[IPB[i] - 1];
                }
                rezultEncoderBin = sTemp;
				
                // Заносим результат ШТ в буфер
                C0 = rezultEncoderBin;

                // Перевод двоичных чисел в соответсвующие им символы
                string templ = "";
                for (int j = 0; j < rezultEncoderBin.Length / 8; j++)
                {
                    templ += Convert.ToChar(Convert.ToInt32(rezultEncoderBin.Substring(j + 7 * j, 8), 2));
                }
                rezultEncoder.Add(templ);

                // Удаляем зашифрованый блок 
                OTIn2CC = OTIn2CC.Remove(0, 64);
            }

            // Вывод
            string textBox2Text = "";
            for (int i = 0; i < rezultEncoder.Count; i++)
            {
                textBox2Text += rezultEncoder[i];
            }
            textBox2.Text = textBox2Text;

        }

        /// <summary>
        /// Шифрация в режиме CBF
        /// </summary>
        private void EncryptionCBF()
        {
            // Если не хватает кол-во символом, то в конце добавляем пробелы
            while (textBox1.Text.Length % 2 != 0)
            {
                textBox1.Text += " ";
            }

            // k бит
            int k = 16;

            // Открытый текст в двоичном сс
            string OTIn2CC = TransliteIn2CC(textBox1.Text);
            List<string> rezultEncoder = new List<string>();
            string inputBlock = C0;

            while (OTIn2CC.Length != 0)
            {
                // Начальная перестановка
                string temp = "";
                for (int i = 0; i < 64; i++)
                {
                    temp += inputBlock[IP[i] - 1];
                }

                #region Деление на левую и правую части

                string str32bitLeft = string.Empty;
                str32bitLeft = temp.Substring(0, 32);
                string str32bitRight = string.Empty;
                str32bitRight = temp.Substring(32, 32);

                #endregion

                // Формирование ключей
                List<string> listKey = new List<string>();
                listKey = generationKey();

                // 16 циклов шифрующих преобразований
                string tepmLeftBlock = "";
                for (int j = 0; j < 16; j++)
                {
                    tepmLeftBlock = str32bitLeft;
                    // Li = Ri-1
                    str32bitLeft = str32bitRight;
                    // Ri = Li-1 XOR F
                    str32bitRight = XOR(tepmLeftBlock, calcF(str32bitRight, listKey[j]));
                }

                // Итоговый двоичный код (64-бит)
                string rezultEncoderBin = string.Empty;
                rezultEncoderBin = str32bitRight + str32bitLeft;

                // Завершающая обратная перестановка
                string sTemp = "";
                for (int i = 0; i < rezultEncoderBin.Length; i++)
                {
                    sTemp += rezultEncoderBin[IPB[i] - 1];
                }
                rezultEncoderBin = sTemp;
                sTemp = "";

                // Складываем по модулю 2 первые k бит ОТ и выходного блоко
                sTemp = XOR(rezultEncoderBin.Substring(0, k), OTIn2CC.Substring(0, k));

                // Изменяем входной блок
                inputBlock = inputBlock.Remove(0, k);
                inputBlock = inputBlock + sTemp;

                // Перевод двоичных чисел в соответсвующие им символы
                string templ = "";
                for (int j = 0; j < sTemp.Length / 8; j++)
                {
                    templ += Convert.ToChar(Convert.ToInt32(sTemp.Substring(j + 7 * j, 8), 2));
                }
                rezultEncoder.Add(templ);

                // Удаляем зашифрованый блок 
                OTIn2CC = OTIn2CC.Remove(0, k);
            }

            // Вывод
            string textBox2Text = "";
            for (int i = 0; i < rezultEncoder.Count; i++)
            {
                textBox2Text += rezultEncoder[i];
            }
            textBox2.Text = textBox2Text;

        }

        /// <summary>
        /// Дешифрация в режиме CBC
        /// </summary>
        private void DecryptionCBC()
        {
            // открытый текст в двоичном сс
            string OTIn2CC = TransliteIn2CC(textBox1.Text);

            List<string> rezultDecoder = new List<string>();

            while (OTIn2CC.Length !=0)
            {
                // Берем первый 64-ех битовый блок
                string oneBlock = OTIn2CC.Substring(0, 64);
                
                // Начальная перестановка
                string temp = "";
                for (int i = 0; i < 64; i++)
                {
                    temp += oneBlock[IP[i] - 1];
                }

                #region Деление на левую и правую части

                string str32bitLeft = string.Empty;
                str32bitLeft = temp.Substring(0, 32);
                string str32bitRight = string.Empty;
                str32bitRight = temp.Substring(32, 32);

                #endregion

                // Формирование ключей
                List<string> listKey = new List<string>();
                listKey = generationKey();

                // 16 циклов дешифрующих преобразований
                string tepmLeftBlock = "";
                for (int j = 15; j > -1; j--)
                {
                    tepmLeftBlock = str32bitLeft;
                    // Li = Ri-1
                    str32bitLeft = str32bitRight;
                    // Ri = Li-1 XOR F
                    str32bitRight = XOR(tepmLeftBlock, calcF(str32bitRight, listKey[j]));
                }

                // Итоговый двоичный код (64-бит)
                string rezultEncoderBin = string.Empty;
                rezultEncoderBin = str32bitRight + str32bitLeft;

                // Завершающая обратная перестановка
                string sTemp = "";
                for (int j = 0; j < rezultEncoderBin.Length; j++)
                {
                    sTemp += rezultEncoderBin[IPB[j] - 1];
                }
                rezultEncoderBin = sTemp;

                // Результат складываем по модулю 2 с буфером
                rezultEncoderBin = XOR(rezultEncoderBin, C0);


                // Перевод двоичных чисел в соответсвующие им символы
                string templ = "";
                for (int i = 0; i < rezultEncoderBin.Length / 8; i++)
                {
                    templ += Convert.ToChar(Convert.ToInt32(rezultEncoderBin.Substring(i + 7 * i, 8), 2));
                }
                rezultDecoder.Add(templ);

                // Заносим блок ШТ в буфер
                C0 = OTIn2CC.Substring(0, 64);

                // Удаляем дешифрованый блок 
                OTIn2CC = OTIn2CC.Remove(0, 64);

            }

            // Вывод
            string textBox2Text = "";
            for (int i = 0; i < rezultDecoder.Count; i++)
            {
                textBox2Text += rezultDecoder[i];
            }
            textBox2.Text = textBox2Text;

        }

        /// <summary>
        /// Дешифрация в режиме CBF
        /// </summary>
        private void DecryptionCBF()
        {
            // k бит
            int k = 16;

            // Шифртекст в двоичном сс
            string ShTIn2CC = TransliteIn2CC(textBox1.Text);
            List<string> rezultDecoder = new List<string>();
            string inputBlock = C0;

            while (ShTIn2CC.Length != 0)
            {
                // Начальная перестановка
                string temp = "";
                for (int i = 0; i < 64; i++)
                {
                    temp += inputBlock[IP[i] - 1];
                }

                #region Деление на левую и правую части

                string str32bitLeft = string.Empty;
                str32bitLeft = temp.Substring(0, 32);
                string str32bitRight = string.Empty;
                str32bitRight = temp.Substring(32, 32);

                #endregion

                // Формирование ключей
                List<string> listKey = new List<string>();
                listKey = generationKey();

                // 16 циклов шифрующих преобразований
                string tepmLeftBlock = "";
                for (int j = 0; j < 16; j++)
                {
                    tepmLeftBlock = str32bitLeft;
                    // Li = Ri-1
                    str32bitLeft = str32bitRight;
                    // Ri = Li-1 XOR F
                    str32bitRight = XOR(tepmLeftBlock, calcF(str32bitRight, listKey[j]));
                }

                // Итоговый двоичный код (64-бит)
                string rezultEncoderBin = string.Empty;
                rezultEncoderBin = str32bitRight + str32bitLeft;

                // Завершающая обратная перестановка
                string sTemp = "";
                for (int i = 0; i < rezultEncoderBin.Length; i++)
                {
                    sTemp += rezultEncoderBin[IPB[i] - 1];
                }
                rezultEncoderBin = sTemp;
                sTemp = "";

                // Складываем по модулю 2 первые k бит ОТ и выходного блока
                sTemp = XOR(rezultEncoderBin.Substring(0, k), ShTIn2CC.Substring(0, k));

                // Изменяем входной блок
                inputBlock = inputBlock.Remove(0, k);
                inputBlock = inputBlock + ShTIn2CC.Substring(0,k);

                // Перевод двоичных чисел в соответсвующие им символы
                string templ = "";
                for (int j = 0; j < sTemp.Length / 8; j++)
                {
                    templ += Convert.ToChar(Convert.ToInt32(sTemp.Substring(j + 7 * j, 8), 2));
                }
                rezultDecoder.Add(templ);

                // Удаляем зашифрованый блок 
                ShTIn2CC = ShTIn2CC.Remove(0, k);
            }

            // Вывод
            string textBox2Text = "";
            for (int i = 0; i < rezultDecoder.Count; i++)
            {
                textBox2Text += rezultDecoder[i];
            }
            textBox2.Text = textBox2Text;

        }

        /// <summary>
        /// Сообщение о том, что не выбран режим перевода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            //if (mod != "Шифровать" && mod != "Дешифровать")
            //{
            //    MessageBox.Show("Выберите режим перевода!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            int index = mod.IndexOf(" в CB");

            if (index > -1)
            {
                mod = mod.Remove(index);
            }
            mod += " в CBC";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            int index = mod.IndexOf(" в CB");

            if (index > -1)
            {
                mod = mod.Remove(index);
            }

            mod += " в CBF";
        }
    }
}
