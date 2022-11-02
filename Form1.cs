using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otomata_Proje_deneme
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void alfabe_yazdir()
        {
            String alfabe = Convert.ToString(textBox1.Text);
            string[] harfler = alfabe.Split(',');
            for (int i = 0; i < harfler.Length; i++)
            {
                listBox2.Items.Add(harfler[i]);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Random rastgele = new Random();
            int secilen = rastgele.Next(0, 2); //parantez içi dinamik değer seçme
            int uretilen = rastgele.Next(0, 10); //* bulunan ifade için dinamik adet
            int adet = Convert.ToInt32(textBox3.Text);  //yazdırılacak kelime adedi
            String kural = Convert.ToString(textBox2.Text);
            label8.Text = kural;
            char[] harf = kural.ToCharArray();  //Düzenli ifadenin karakter boyu
            alfabe_yazdir(); //Listbox2'ye alfabede bulunan harfleri yazar
            string parantez_onu = null; //parantezin önünde bulunan değerleri tutar [ab(a+b) => ab]
            string parantez_ici = null; //parantez içinde bulunan değerleri tutar [ab(a+b) => a+b]
            string secilen_ifade = null; //parantezde seçilen değer [ab(a+b) => a , b]
            string parantez_sonu = null; //parantezin sonunda bulunan değerleri tutar [(a+b)ab => ab]
            for (int i = 0; i < harf.Length; i++)
            {
                if (harf[i] != '(') //ifade parantez ile başlamıyorsa
                {
                    for (int j = 0; j < harf.Length; j++)
                    {
                        if (harf[j] == '*') //ab*(a+b)* , ab*(a+b)
                        {
                            for (int k = j + 1; k < harf.Length; k++)
                            {
                                if (harf[k] == '(')
                                {
                                    for (int l = k + 1; l < harf.Length; l++)
                                    {
                                        if (harf[l] == ')')
                                        {
                                            string[] ifade = parantez_ici.Split('+');
                                            if (harf[harf.Length - 1] == ')') //ab*(a+b)
                                            {
                                                if (secilen == 0)
                                                {
                                                    secilen_ifade = String.Concat(secilen_ifade, ifade[secilen]);
                                                }
                                                if (secilen == 1)
                                                {
                                                    secilen_ifade = String.Concat(secilen_ifade, ifade[secilen]);
                                                }
                                            }
                                            else
                                            {//parantezden sonra * geldiği zamanlarda kullanılan ifade
                                                if (harf[l + 1] == '*') //ab*(a+b)*
                                                {
                                                    for (int z = 0; z < uretilen; z++)
                                                    {
                                                        if (secilen == 0)
                                                        {
                                                            secilen_ifade = String.Concat(secilen_ifade, ifade[secilen]);
                                                        }
                                                        if (secilen == 1)
                                                        {
                                                            secilen_ifade = String.Concat(secilen_ifade, ifade[secilen]);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            parantez_ici = String.Concat(parantez_ici, harf[l]);
                                        }
                                    }
                                    for (int u = 0; u < adet; u++) //ab*(a+b)* , ab*(a+b) yazdırma kısmı
                                    {
                                        parantez_onu = String.Concat(parantez_onu, parantez_onu);
                                        parantez_onu = String.Concat(parantez_onu, secilen_ifade);
                                        listBox1.Items.Add(parantez_onu);
                                    }
                                }
                                break;
                            }
                            break;
                        }
                        else //ab(a+b)* , ab(a+b)
                        {
                            if (harf[j] != '(')  //ab
                            {
                                parantez_onu = String.Concat(parantez_onu, harf[j]);
                            }
                            else if (harf[j] == '(') 
                            {
                                for (int l = j + 1; l < harf.Length; l++)
                                {
                                    if (harf[l] == ')')
                                    {
                                        string[] ifade = parantez_ici.Split('+');
                                        if (harf[harf.Length - 1] == ')') //ab(a+b)
                                        {
                                            if (secilen == 0)
                                            {
                                                secilen_ifade = String.Concat(secilen_ifade, ifade[secilen]);
                                            }
                                            if (secilen == 1)
                                            {
                                                secilen_ifade = String.Concat(secilen_ifade, ifade[secilen]);
                                            }
                                        }
                                        else
                                        {
                                            if (harf[l + 1] == '*') //ab(a+b)*
                                            {
                                                for (int m = 0; m < uretilen; m++)
                                                {
                                                    if (secilen == 0)
                                                    {
                                                        secilen_ifade = String.Concat(secilen_ifade, ifade[secilen]);
                                                    }
                                                    if (secilen == 1)
                                                    {
                                                        secilen_ifade = String.Concat(secilen_ifade, ifade[secilen]);
                                                    }
                                                }
                                            }
                                        }
                                        for (int h = 0; h < adet; h++) //ab(a+b)* , ab(a+b) yazdırma kısmı
                                        {
                                            parantez_onu = String.Concat(parantez_onu, secilen_ifade);
                                            listBox1.Items.Add(parantez_onu);
                                        }
                                    }
                                    else
                                    {
                                        parantez_ici = String.Concat(parantez_ici, harf[l]);
                                    }
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
                else if (harf[i] == '(') //ifade parantez ile başlıyorsa
                {
                    for (int l = i + 1; l < harf.Length; l++)
                    {
                        if (harf[l] == ')')
                        {
                            string[] ifade = parantez_ici.Split('+');
                            if (harf[harf.Length - 1] == ')')
                            {
                                if (secilen == 0)
                                {
                                    secilen_ifade = String.Concat(secilen_ifade, ifade[secilen]);
                                }
                                if (secilen == 1)
                                {
                                    secilen_ifade = String.Concat(secilen_ifade, ifade[secilen]);
                                }
                            }
                            else
                            {
                                for (int m = l + 1; m < harf.Length; m++)
                                {
                                    if (harf[m] == '*') //(a+b)*ab , (a+b)*ab*
                                    {
                                        for (int f = m + 1; f < harf.Length; f++)
                                        {
                                            if (harf[f] == '*') //(a+b)*ab*
                                            {
                                                for (int o = f; o < uretilen; o++)
                                                {
                                                    parantez_sonu = String.Concat(parantez_sonu, parantez_sonu);
                                                }
                                            }
                                            else //(a+b)*ab
                                            {
                                                parantez_sonu = String.Concat(parantez_sonu, harf[f]);
                                            }
                                        }
                                        for (int o = 0; o < uretilen; o++)
                                        {
                                            if (secilen == 0)
                                            {
                                                secilen_ifade = String.Concat(secilen_ifade, ifade[secilen]);
                                            }
                                            if (secilen == 1)
                                            {
                                                secilen_ifade = String.Concat(secilen_ifade, ifade[secilen]);
                                            }
                                        }
                                        break;
                                    }
                                    else if (harf[m] != '*') //(a+b)ab* ,  (a+b)ab
                                    {
                                        for (int f = m; f < harf.Length; f++)
                                        {
                                            if (harf[f] == '*') //(a+b)aa*
                                            {
                                                for (int o = 0; o < uretilen; o++)
                                                {
                                                    parantez_sonu = String.Concat(parantez_sonu, parantez_sonu);
                                                }
                                            }
                                            else //(a+b)ab
                                            {
                                                parantez_sonu = String.Concat(parantez_sonu, harf[f]);
                                            }
                                        }
                                        if (secilen == 0)
                                        {
                                            secilen_ifade = String.Concat(secilen_ifade, ifade[secilen]);
                                        }
                                        if (secilen == 1)
                                        {
                                            secilen_ifade = String.Concat(secilen_ifade, ifade[secilen]);
                                        }
                                        break; //sondaki tekrarlamayı engeller                                                          
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            for (int h = 0; h < adet; h++)
                            {
                                parantez_sonu = String.Concat(secilen_ifade, parantez_sonu);
                                listBox1.Items.Add(parantez_sonu);
                            }
                        }
                        else
                        {
                            parantez_ici = String.Concat(parantez_ici, harf[l]);
                        }
                    }
                }
                break;
            }
        }
    }
}
