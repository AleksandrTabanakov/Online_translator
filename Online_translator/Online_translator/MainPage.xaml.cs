﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace Translate_program
{
    public class CodePart
    {
        List<operation> operations;
        List<string> properties;
    }
    public class OperatorYsloviya:operation
    {
        public List<operation> operationsWithTrue;
        public List<operation> operationsWithFalse;
        public List<string> properties;
    } 
    public class OperatorCycle:operation
    {
        public operation yslovie;
        public List<operation> operations;
        public List<string> properties;
    }
    public class operation
    {
        public string formulation;
    }
    public partial class MainPage : ContentPage
    {
        string name1 = "";
        string name2 = "";
    public static string name3 = "";
        public static List<string> translator = new List<string>();
        public static List<string> translator2 = new List<string>();
        public MainPage()
        { 
            InitializeComponent();
            this.Title = "Welcome to translator!";
            this.BackgroundColor = Color.Azure;
            //Получаем размер дисплея нашего андроида
            var display = DeviceDisplay.MainDisplayInfo;
            double height = display.Height;
            double width = display.Width;
            this.Editor2.HeightRequest = height / 100 * 13;
        }



        private void Button_Clicked(object sender, EventArgs e)
        {
            if (name1 != name2 && name1 != "" && name2 != "" && translator.Count != 0)
            {
                List<string> vtoroy_yazuk = new List<string>();
                if (name1 == "Pascal")
                {
                    //List<string>[] peremen = new List<string>[14];
                    int until = 0;
                    string vvod = "";
                    int random = 0;
                    List<string> razdeltel2 = new List<string>();
                    for (int i = 0; i < translator.Count; i++)
                    {
                        string stroka = translator[i];
                        stroka = stroka.Trim();
                        StringBuilder str = new StringBuilder();
                        str.Append(stroka[0]);
                        for (int st = 1; st < stroka.Length; st++)
                        {
                            if (stroka[st] == ' ' && stroka[st - 1] == ' ')
                                continue;
                            str.Append(stroka[st]);
                        }
                        stroka = str.ToString();
                        for (int j = 0; j < stroka.Length; j++)
                        {
                            if (stroka[j] == '{' || stroka[j] == '}')
                            {
                                if (vvod != "")
                                {
                                    while (vvod[vvod.Length - 1] == ' ')
                                        vvod = vvod.Remove(vvod.Length - 1);
                                    razdeltel2.Add(vvod);
                                    vvod = "";
                                }
                                else
                                {
                                    vvod += stroka[j];
                                    while (vvod[vvod.Length - 1] == ' ')
                                        vvod = vvod.Remove(vvod.Length - 1);
                                    razdeltel2.Add(vvod);
                                }
                                continue;
                            }
                            if (stroka[j] == ';')
                            {
                                vvod += stroka[j];
                                while (vvod[vvod.Length - 1] == ' ')
                                    vvod = vvod.Remove(vvod.Length - 1);
                                razdeltel2.Add(vvod);
                                vvod = "";
                            }
                            else
                            {
                                vvod += stroka[j];
                            }
                        }
                        if (vvod != "")
                        {
                            while (vvod[vvod.Length - 1] == ' ')
                                vvod = vvod.Remove(vvod.Length - 1);
                            razdeltel2.Add(vvod);
                            vvod = "";
                        }
                    }

                    //_____________________________________________________________________________________________________________________________________________________________________________
                    if (name1 == "Pascal")
                    {
                        List<string> razdeltel = new List<string>();
                        //Разделение на до бегин зен енд;
                        string stroka3 = "";
                        for (int i = 0; i < razdeltel2.Count; i++)
                        {
                            razdeltel2[i] = razdeltel2[i].ToLower();
                            razdeltel2[i] = razdeltel2[i].Trim();
                            string stroka = razdeltel2[i];
                            if (stroka3.Length > 0 && stroka3 != " ")
                                razdeltel.Add(stroka3);
                            stroka3 = "";
                            if (stroka == "do" || stroka == "then" || stroka == "begin" || stroka == "end" || stroka == "end;" || stroka == "repeat")
                            {
                                if (stroka == "do") { stroka3 += "@konec"; razdeltel.Add(stroka3); stroka3 = ""; }
                                if (stroka == "then") { stroka3 += "@konec"; razdeltel.Add(stroka3); stroka3 = ""; }
                                if (stroka == "begin") { stroka3 += "@openblock"; razdeltel.Add(stroka3); stroka3 = ""; }
                                if (stroka == "end") { stroka3 += "@closeblock"; razdeltel.Add(stroka3); stroka3 = ""; }
                                if (stroka == "end;") { stroka3 += "@closeblock"; razdeltel.Add(stroka3); stroka3 = ""; }
                                if (stroka == "repeat") { stroka3 += "@do"; razdeltel.Add(stroka3); stroka3 = ""; }
                                continue;
                            }
                            stroka += " ";

                            stroka3 += stroka[0];
                            for (int j = 1; j < stroka.Length; j++)
                            {
                                if (j + 2 < stroka.Length)
                                {
                                    if ((stroka[j - 1] == ' ' || stroka[j - 1] == ')') && (stroka[j + 2] == '(' || stroka[j + 2] == ' '))
                                    {
                                        if (stroka[j] == 'd' && stroka[j + 1] == 'o')
                                        {
                                            j++;
                                            stroka3 += "@konec";
                                            razdeltel.Add(stroka3);
                                            stroka3 = "";
                                            continue;
                                        }
                                    }
                                    else if (stroka[j - 1] == 'd' && stroka[j] == 'o' && (stroka[j + 1] == ' ' || stroka[j + 1] == '('))
                                    {

                                        stroka3 = "@closeblock";
                                        razdeltel.Add(stroka3);
                                        stroka3 = "";
                                        continue;
                                    }
                                }
                                if (j + 3 < stroka.Length)
                                {
                                    if ((stroka[j - 1] == ' ' || stroka[j - 1] == ')') && (stroka[j + 3] == '(' || stroka[j + 3] == ' '))
                                    {
                                        if (stroka[j] == 'e' && stroka[j + 1] == 'n' && stroka[j + 2] == 'd')
                                        {
                                            j += 2;
                                            razdeltel.Add(stroka3);
                                            stroka3 = "@closeblock";
                                            razdeltel.Add(stroka3);
                                            stroka3 = "";
                                            continue;
                                        }
                                    }
                                    else if (stroka[j - 1] == 'e' && stroka[j] == 'n' && stroka[j + 1] == 'd' && (stroka[j + 2] == ' ' || stroka[j + 2] == '('))
                                    {
                                        j++;
                                        stroka3 = "@closeblock";
                                        razdeltel.Add(stroka3);
                                        stroka3 = "";
                                        continue;
                                    }
                                }

                                if (j + 4 < stroka.Length)
                                {
                                    if ((stroka[j - 1] == ' ' || stroka[j - 1] == ')') && (stroka[j + 4] == '(' || stroka[j + 4] == ' '))
                                    {
                                        if (stroka[j] == 't' && stroka[j + 1] == 'h' && stroka[j + 2] == 'e' && stroka[j + 3] == 'n')
                                        {
                                            j += 3;
                                            stroka3 += "@konec";
                                            razdeltel.Add(stroka3);
                                            stroka3 = "";
                                            continue;
                                        }
                                        if (stroka[j] == 'e' && stroka[j + 1] == 'n' && stroka[j + 2] == 'd' && stroka[j + 3] == ';')
                                        {
                                            j += 3;
                                            stroka3 += "@closeblock";
                                            razdeltel.Add(stroka3);
                                            stroka3 = "";
                                            continue;
                                        }
                                    }
                                    else if (stroka[j - 1] == 'e' && stroka[j] == 'n' && stroka[j + 1] == 'd' && stroka[j + 2] == ';' && (stroka[j + 3] == '(' || stroka[j + 3] == ' '))
                                    {
                                        j += 2;
                                        stroka3 = "@closeblock";
                                        razdeltel.Add(stroka3);
                                        stroka3 = "";
                                        continue;
                                    }
                                    else if (stroka[j - 1] == 't' && stroka[j] == 'h' && stroka[j + 1] == 'e' && stroka[j + 2] == 'n' && (stroka[j + 3] == '(' || stroka[j + 3] == ' '))
                                    {
                                        j += 2;
                                        stroka3 = "@konec";
                                        razdeltel.Add(stroka3);
                                        stroka3 = "";
                                        continue;
                                    }
                                }
                                if (j + 5 < stroka.Length)
                                {
                                    if ((stroka[j - 1] == ' ' || stroka[j - 1] == ')') && (stroka[j + 5] == '(' || stroka[j + 5] == ' '))
                                    {
                                        if (stroka[j] == 'b' && stroka[j + 1] == 'e' && stroka[j + 2] == 'g' && stroka[j + 3] == 'i' && stroka[j + 4] == 'n')
                                        {
                                            j += 4;
                                            if (stroka3 != " " && stroka3 != "")
                                                razdeltel.Add(stroka3);
                                            stroka3 = "@openblock";
                                            razdeltel.Add(stroka3);
                                            stroka3 = "";
                                            continue;
                                        }
                                    }
                                    else if (stroka[j - 1] == 'b' && stroka[j] == 'e' && stroka[j + 1] == 'g' && stroka[j + 2] == 'i' && stroka[j + 3] == 'n' && (stroka[j + 4] == '(' || stroka[j + 4] == ' '))
                                    {
                                        j += 3;
                                        if (stroka3 != " " && stroka3 != "")
                                            stroka3 = "@openblock";
                                        razdeltel.Add(stroka3);
                                        stroka3 = "";
                                        continue;
                                    }
                                }
                                if (j + 6 < stroka.Length)
                                {
                                    if ((stroka[j - 1] == ' ' || stroka[j - 1] == ')') && (stroka[j + 6] == '(' || stroka[j + 6] == ' '))
                                    {
                                        if (stroka[j] == 'r' && stroka[j + 1] == 'e' && stroka[j + 2] == 'p' && stroka[j + 3] == 'e' && stroka[j + 4] == 'a' && stroka[j + 5] == 't')
                                        {
                                            j += 5;
                                            if (stroka3 != " " && stroka3 != "")
                                                razdeltel.Add(stroka3);
                                            stroka3 = "@do";
                                            razdeltel.Add(stroka3);
                                            stroka3 = "";
                                            continue;
                                        }
                                    }
                                    else if (stroka[j - 1] == 'r' && stroka[j] == 'e' && stroka[j + 1] == 'p' && stroka[j + 2] == 'e' && stroka[j + 3] == 'a' && stroka[j + 4] == 't' && (stroka[j + 5] == '(' || stroka[j + 5] == ' '))
                                    {
                                        j += 4;
                                        if (stroka3 != " " && stroka3 != "")
                                            stroka3 = "@do";
                                        razdeltel.Add(stroka3);
                                        stroka3 = "";
                                        continue;
                                    }
                                }
                                if (stroka3 == "" && stroka[j] == ' ')
                                    continue;
                                else
                                    stroka3 += stroka[j];
                            }

                        }

                        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                        //               Обработка вара

                        for (int i = 0; i < razdeltel.Count; i++)
                        {

                            var poisk_var = razdeltel[i].ToLower().Split(new char[] { ' ', ',', ':', '=', ';', '[', ']' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            if (poisk_var[0] == "var") //внутри ифа обработка вара
                            {
                                if (poisk_var.Count == 1)
                                {
                                    razdeltel.RemoveAt(0);
                                    poisk_var = razdeltel[i].ToLower().Split(new char[] { ' ', ',', ':', '=', ';', '[', ']' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                }
                                else
                                {
                                    poisk_var.RemoveAt(0);
                                }
                                while (poisk_var[0] != "@openblock")
                                {
                                    if (razdeltel[i].Contains("[") || razdeltel[i].Contains("string") || razdeltel[i].Contains("'"))
                                    {
                                        if (razdeltel[i].Contains(":") && razdeltel[i].IndexOf(":") < razdeltel[i].IndexOf("string"))
                                        {
                                            if (razdeltel[i].IndexOf("array") == -1)
                                            {
                                                vtoroy_yazuk.Add("@obyavl #string ");
                                                vtoroy_yazuk.Add("");
                                                for (int j = 0; j < poisk_var.Count - 1; j++)
                                                    vtoroy_yazuk[vtoroy_yazuk.Count - 1] += poisk_var[j] + " ";
                                            }
                                            else
                                            {
                                                if (razdeltel[i].IndexOf(":") < razdeltel[i].LastIndexOf(","))
                                                {
                                                    vtoroy_yazuk.Add("@obyavl #arr_2D ");
                                                    vtoroy_yazuk.Add("");
                                                    string nach = razdeltel[i].Substring(razdeltel[i].IndexOf("[") + 1, razdeltel[i].IndexOf(".") - razdeltel[i].IndexOf("[") - 1);
                                                    string kon = razdeltel[i].Substring(razdeltel[i].IndexOf(".") + 2, razdeltel[i].LastIndexOf(",") - razdeltel[i].IndexOf(".") - 2) + "-";
                                                    vtoroy_yazuk[vtoroy_yazuk.Count - 1] += nach + " " + kon + "(" + nach + ")" + "+2";
                                                    nach = razdeltel[i].Substring(razdeltel[i].LastIndexOf(",") + 1, razdeltel[i].LastIndexOf(".") - razdeltel[i].LastIndexOf(",") - 2);
                                                    kon = razdeltel[i].Substring(razdeltel[i].LastIndexOf(".") + 1, razdeltel[i].LastIndexOf("]") - razdeltel[i].LastIndexOf(".") - 1) + "-";
                                                    vtoroy_yazuk[vtoroy_yazuk.Count - 2] += nach + " " + kon + "(" + nach + ")" + "+2" + " @obyavl #string ";
                                                    for (int j = 0; j < poisk_var.Count - 5; j++)
                                                        vtoroy_yazuk[vtoroy_yazuk.Count - 1] += poisk_var[j] + " ";
                                                }
                                                else
                                                {
                                                    vtoroy_yazuk.Add("@obyavl #arr ");
                                                    vtoroy_yazuk.Add("");
                                                    string nach = razdeltel[i].Substring(razdeltel[i].IndexOf("[") + 1, razdeltel[i].IndexOf(".") - razdeltel[i].IndexOf("[") - 1);
                                                    string kon = razdeltel[i].Substring(razdeltel[i].LastIndexOf(".") + 1, razdeltel[i].LastIndexOf("]") - razdeltel[i].LastIndexOf(".") - 1) + "-";
                                                    vtoroy_yazuk[vtoroy_yazuk.Count - 2] += nach + " " + kon + "(" + nach + ")" + "+2" + " @obyavl #string ";
                                                    for (int j = 0; j < poisk_var.Count - 4; j++)
                                                        vtoroy_yazuk[vtoroy_yazuk.Count - 1] += poisk_var[j] + " ";
                                                }
                                            }
                                        }
                                        else if (razdeltel[i].IndexOf("'") != -1)
                                        {
                                            vtoroy_yazuk.Add($"@obyavl #neyavn {poisk_var[0]} ");
                                            for (int j = razdeltel[i].IndexOf("'") + 1, j1 = razdeltel[i].LastIndexOf("'"); j < j1; j++)
                                            {
                                                vtoroy_yazuk[vtoroy_yazuk.Count - 1] += razdeltel[i][j];
                                            }
                                        }
                                        else if (razdeltel[i].IndexOf("array") != -1)
                                        {
                                            if (razdeltel[i].IndexOf(":") < razdeltel[i].LastIndexOf(","))
                                            {
                                                Typee(poisk_var, ref vtoroy_yazuk, 5);
                                                string nach = razdeltel[i].Substring(razdeltel[i].IndexOf("[") + 1, razdeltel[i].IndexOf(".") - razdeltel[i].IndexOf("[") - 1);
                                                string kon = razdeltel[i].Substring(razdeltel[i].IndexOf(".") + 2, razdeltel[i].LastIndexOf(",") - razdeltel[i].IndexOf(".") - 2) + "-";
                                                string nach1 = razdeltel[i].Substring(razdeltel[i].LastIndexOf(",") + 1, razdeltel[i].LastIndexOf(".") - razdeltel[i].LastIndexOf(",") - 2);
                                                string kon1 = razdeltel[i].Substring(razdeltel[i].LastIndexOf(".") + 1, razdeltel[i].LastIndexOf("]") - razdeltel[i].LastIndexOf(".") - 1) + "-";
                                                vtoroy_yazuk[vtoroy_yazuk.Count - 2] = "@obyavl #arr_2D " + nach + " " + kon + "(" + nach + ")" + "+2 " + nach1 + " " + kon1 + "(" + nach1 + ")" + "+2 " + vtoroy_yazuk[vtoroy_yazuk.Count - 2];
                                            }
                                            else
                                            {
                                                Typee(poisk_var, ref vtoroy_yazuk, 4);
                                                string nach = razdeltel[i].Substring(razdeltel[i].IndexOf("[") + 1, razdeltel[i].IndexOf(".") - razdeltel[i].IndexOf("[") - 1);
                                                string kon = razdeltel[i].Substring(razdeltel[i].LastIndexOf(".") + 1, razdeltel[i].LastIndexOf("]") - razdeltel[i].LastIndexOf(".") - 1) + "-";
                                                vtoroy_yazuk[vtoroy_yazuk.Count - 2] = "@obyavl #arr " + nach + " " + kon + "(" + nach + ")" + "+2 " + vtoroy_yazuk[vtoroy_yazuk.Count - 2];
                                            }
                                        }
                                    }
                                    else
                                        Typee(poisk_var, ref vtoroy_yazuk, 1);
                                    i++;
                                    poisk_var = razdeltel[i].ToLower().Split(new char[] { ' ', ',', ':', ';', '=', '[', ']' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                }
                                i--;
                            }
                            else if (poisk_var[0] == "const")
                            {
                                if (poisk_var.Count == 1)
                                {
                                    i++;
                                    poisk_var = razdeltel[i].ToLower().Split(new char[] { ' ', ',', ':', '=', ';', '[', ']' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                }
                                else
                                    poisk_var.RemoveAt(0);
                                while (poisk_var[0] != "begin" && poisk_var[0] != "var")
                                {
                                    string znach = razdeltel[i].Substring(razdeltel[i].IndexOf("=") + 1); znach = znach.Remove(znach.Length - 1);
                                    znach = znach.Trim(' '); znach = znach.TrimEnd(' ');
                                    if (znach[0] == '\'')
                                    {
                                        znach.Remove(0, 1);
                                        znach.Remove(znach.Length - 1);
                                    }
                                    vtoroy_yazuk.Add($"@obyavl @const {znach}");
                                    vtoroy_yazuk.Add(poisk_var[0]);
                                    i++;
                                    poisk_var = razdeltel[i].ToLower().Split(new char[] { ' ', ',', ':', ';', '=' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                }
                                i--;
                            }
                            else if (poisk_var[0] == "@openblock")
                                break;
                        }
                        //for (int i = 0; i < vtoroy_yazuk.Count; i += 2)
                        //{
                        //    //    Console.WriteLine(vtoroy_yazuk[i]+ vtoroy_yazuk[i+1]);
                        //    if (name2 == "C#" || name2 == "C/C++")
                        //    {






                                
                                
                        //    }
                        //}
                        razdeltel.RemoveAt(0);

                        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                        for (int i = 0; i < razdeltel.Count; i++)
                        {
                            StringBuilder sb = new StringBuilder();
                            for (int je = 0; je < razdeltel[i].Length; je++)
                            {
                                if (razdeltel[i][je] == '[')
                                {
                                    if (razdeltel[i][je] == '\'')
                                    {
                                        break;
                                    }
                                    if (razdeltel[i][je] == '[')
                                    {

                                        while (je != razdeltel[i].Length - 1 && razdeltel[i][je] != '\'' && razdeltel[i][je] != ';')
                                        {
                                            if (razdeltel[i][je] == ',')
                                            {
                                                if (razdeltel[i][je] == ',' && name2 == "C/C++")
                                                {
                                                    sb.Append("-1][");
                                                }
                                                else sb.Append("-1,");


                                            }
                                            else if (razdeltel[i][je] != ']')
                                            {
                                                sb.Append(razdeltel[i][je]);
                                            }
                                            else
                                            {
                                                sb.Append("-1]");

                                                break;
                                            }
                                            je++;
                                        }
                                    }
                                    continue;
                                }
                                else sb.Append(razdeltel[i][je]);
                            }
                            razdeltel[i] = (sb.ToString());
                            sb.Clear();
                        }
                        for (int i = 0; i < razdeltel.Count; i++)
                        {
                            if (razdeltel[i][0] == 'f' && razdeltel[i][1] == 'o' && razdeltel[i][2] == 'r' && razdeltel[i][3] == ' ')
                            {


                                string[] rof = razdeltel[i].ToLower().Split(new string[] { " ", ":=" }, StringSplitOptions.RemoveEmptyEntries);
                                string na_obchem1 = "@cycle #pred ";
                                string na_obchem2 = "@local_per ";
                                string na_obchem3 = "@cyc_if ";
                                string na_obchem4 = "@telo ";
                                if (rof[1] == "var")
                                {
                                    na_obchem2 += $"@obyavl #neyavn {rof[2]} "; int ind = 3;
                                    for (; rof[ind] != "to" && rof[ind] != "downto"; ind++)
                                        na_obchem2 += rof[ind] + " ";
                                    ind += 1;
                                    To_downto(rof, ref na_obchem3, ref na_obchem4, 2, ind);
                                }
                                else
                                {
                                    na_obchem4 += $"@prisvoenie {rof[1]} ";
                                    int ind = 2;
                                    for (; rof[ind] != "to" && rof[ind] != "downto"; ind++)
                                        na_obchem4 += rof[ind] + " ";
                                    ind += 1;
                                    na_obchem4 += "\n";
                                    To_downto(rof, ref na_obchem3, ref na_obchem4, 1, ind);
                                }
                                //  Console.WriteLine(na_obchem1);
                                translator2.Add("for (");
                                //   Console.WriteLine(na_obchem2);
                                if (na_obchem2 != "@local_per ")
                                {
                                    string[] translator22 = na_obchem2.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                                    translator2[translator2.Count - 1] += "var " + translator22[3] + "=";
                                    for (int u = 4; u < translator22.Length; u++)
                                    {
                                        if (translator22[u] == "mod")
                                            translator2[translator2.Count - 1] += "%";
                                        else if (translator22[u] == "div")
                                            translator2[translator2.Count - 1] += "/";
                                        else translator2[translator2.Count - 1] += translator22[u];
                                    }
                                    translator2[translator2.Count - 1] += ";";
                                    string perepis = "";
                                    string[] translator23 = na_obchem3.Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                                    for (int k = 0; k < translator23[1].Length; k++)
                                    {
                                        if (translator23[1][k] != '=')
                                            perepis += translator23[1][k];
                                    }
                                    for (int k = 2; k < translator23.Length; k++)
                                    {
                                        if (translator23[k] == "mod")
                                            perepis += "%";
                                        else if (translator23[k] == "div")
                                            perepis += "/";
                                        else perepis += translator23[k];
                                    }
                                    translator2[translator2.Count - 1] += perepis + ";";
                                    string[] translator24 = na_obchem4.Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                                    if (translator24[2] == "+")
                                        translator2[translator2.Count - 1] += translator24[1] + "++)";
                                    else translator2[translator2.Count - 1] += translator24[1] + "--)";
                                }
                                else
                                {
                                    string[] translator22 = na_obchem4.Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                                    translator2[translator2.Count - 1] += translator22[2] + "=";
                                    for (int u = 3; u < translator22.Length - 3; u++)
                                    {
                                        if (translator22[u] == "mod")
                                            translator2[translator2.Count - 1] += "%";
                                        else if (translator22[u] == "div")
                                            translator2[translator2.Count - 1] += "/";
                                        else translator2[translator2.Count - 1] += translator22[u];
                                    }
                                    translator2[translator2.Count - 1] += ";";
                                    string perepis = "";
                                    string[] translator23 = na_obchem3.Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                                    for (int k = 0; k < translator23[1].Length; k++)
                                    {
                                        if (translator23[1][k] != '=')
                                            perepis += translator23[1][k];
                                    }
                                    for (int k = 2; k < translator23.Length; k++)
                                    {
                                        if (translator23[k] == "mod")
                                            perepis += "%";
                                        else if (translator23[k] == "div")
                                            perepis += "/";
                                        else perepis += translator23[k];
                                    }
                                    translator2[translator2.Count - 1] += perepis + ";";
                                    if (translator22[translator22.Length - 2] == "+")
                                        translator2[translator2.Count - 1] += translator22[translator22.Length - 3] + "++)";
                                    else translator2[translator2.Count - 1] += translator22[translator22.Length - 3] + "--)";
                                }
                                //  Console.WriteLine(na_obchem3); 
                                //  Console.WriteLine(na_obchem4);
                                continue;

                            }
                            List<string> vtoroyyazyk = new List<string>();
                            if (razdeltel[i].Length > 4 && razdeltel[i].Substring(0, 5) == "print" && (razdeltel[i].Length == 5 || (razdeltel[i].Length > 5 && (razdeltel[i][5] == ' ' || razdeltel[i][5] == '('))))
                            {
                                vtoroyyazyk.Add("@vstr_funk pechat ");
                                int index = razdeltel[i].IndexOf('(');
                                if (index != -1)
                                {
                                    vtoroyyazyk[vtoroyyazyk.Count - 1] += Vuvod(index, razdeltel, ref i, false);
                                    string[] translator22 = vtoroyyazyk[vtoroyyazyk.Count - 1].Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
                                    //   Console.WriteLine(vtoroyyazyk[vtoroyyazyk.Count - 1]);
                                    if (name2 == "C#")
                                    {

                                        translator2.Add("Console.Write(" + translator22[1] + ");");
                                        //   Console.WriteLine(translator2[4]);
                                    }
                                    if (name2 == "C/C++")
                                        translator2.Add(si2(translator22));
                                    continue;
                                }
                                else
                                {
                                    i += 1;

                                    vtoroyyazyk[vtoroyyazyk.Count - 1] += Vuvod(0, razdeltel, ref i, false);
                                    string[] translator22 = vtoroyyazyk[vtoroyyazyk.Count - 1].Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
                                    //    Console.WriteLine(vtoroyyazyk[vtoroyyazyk.Count - 1]);
                                    if (name2 == "C#")
                                    {

                                        translator2.Add("Console.Write(" + translator22[1] + ");");
                                        //   Console.WriteLine(translator2[4]);
                                    }
                                    if (name2 == "C/C++")
                                        translator2.Add(si2(translator22));
                                    continue;
                                }
                            }
                            else if (razdeltel[i].Length > 6 && razdeltel[i].Substring(0, 7) == "println" && (razdeltel[i].Length == 7 || (razdeltel[i].Length > 7 && (razdeltel[i][7] == ' ' || razdeltel[i][7] == '('))))//Если функция Println
                            {
                                vtoroyyazyk.Add("@vstr_funk pechat_line ");
                                int index = razdeltel[i].IndexOf('(');
                                if (index != -1)
                                {
                                    vtoroyyazyk[vtoroyyazyk.Count - 1] += Vuvod(index, razdeltel, ref i, false);
                                    //    Console.WriteLine(vtoroyyazyk[vtoroyyazyk.Count - 1]);
                                    string[] translator22 = vtoroyyazyk[vtoroyyazyk.Count - 1].Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
                                    if (name2 == "C#")
                                    {

                                        translator2.Add("Console.WriteLine(" + translator22[1] + ");");
                                        //   Console.WriteLine(translator2[4]);
                                    }
                                    if (name2 == "C/C++")
                                    {
                                        translator2.Add(si(translator22));
                                    }
                                    continue;
                                }
                                else
                                {
                                    i += 1;

                                    vtoroyyazyk[vtoroyyazyk.Count - 1] += Vuvod(0, razdeltel, ref i, false);
                                    //   Console.WriteLine(vtoroyyazyk[vtoroyyazyk.Count - 1]);
                                    string[] translator22 = vtoroyyazyk[vtoroyyazyk.Count - 1].Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
                                    if (name2 == "C#")
                                    {

                                        translator2.Add("Console.WriteLine(" + translator22[1] + ");");
                                        //    Console.WriteLine(translator2[4]);
                                    }
                                    if (name2 == "C/C++")
                                    {
                                        translator2.Add(si(translator22));
                                    }
                                    continue;
                                }
                            }
                            else if (razdeltel[i].Length > 4 && razdeltel[i].Substring(0, 5) == "write" && (razdeltel[i].Length == 5 || (razdeltel[i].Length > 5 && (razdeltel[i][5] == ' ' || razdeltel[i][5] == '('))))
                            {
                                vtoroyyazyk.Add("@vstr_funk pechat ");
                                int index = razdeltel[i].IndexOf('(');
                                if (index != -1)
                                {
                                    vtoroyyazyk[vtoroyyazyk.Count - 1] += Vuvod(index, razdeltel, ref i, true);
                                    // Console.WriteLine(vtoroyyazyk[vtoroyyazyk.Count - 1]);
                                    string[] translator22 = vtoroyyazyk[vtoroyyazyk.Count - 1].Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
                                    if (name2 == "C#")
                                    {
                                        translator2.Add("Console.Write(" + translator22[1] + ");");
                                        // Console.WriteLine(translator2[4]);
                                    }
                                    if (name2 == "C/C++")
                                        translator2.Add(si2(translator22));
                                    continue;
                                }
                                else
                                {
                                    i += 1;
                                    vtoroyyazyk[vtoroyyazyk.Count - 1] += Vuvod(0, razdeltel, ref i, true);
                                    //  Console.WriteLine(vtoroyyazyk[vtoroyyazyk.Count - 1]);
                                    string[] translator22 = vtoroyyazyk[vtoroyyazyk.Count - 1].Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
                                    if (name2 == "C#")
                                        translator2.Add("Console.Write(" + translator22[1] + ");");
                                    if (name2 == "C/C++")
                                        translator2.Add(si2(translator22));
                                    //  Console.WriteLine(translator2[4]);
                                    continue;
                                }
                            }
                            else if (razdeltel[i].Length > 6 && razdeltel[i].Substring(0, 7) == "writeln" && (razdeltel[i].Length == 7 || (razdeltel[i].Length > 7 && (razdeltel[i][7] == ' ' || razdeltel[i][7] == '('))))//Если функция Println
                            {
                                vtoroyyazyk.Add("@vstr_funk pechat_line ");
                                int index = razdeltel[i].IndexOf('(');
                                if (index != -1)
                                {
                                    vtoroyyazyk[vtoroyyazyk.Count - 1] += Vuvod(index, razdeltel, ref i, true);
                                    string[] translator22 = vtoroyyazyk[vtoroyyazyk.Count - 1].Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
                                    //  Console.WriteLine(vtoroyyazyk[vtoroyyazyk.Count - 1]);
                                    if (name2 == "C#")
                                    {

                                        translator2.Add("Console.WriteLine(" + translator22[1] + ");");
                                        //    Console.WriteLine(translator2[4]);
                                    }
                                    if (name2 == "C/C++")
                                        translator2.Add(si(translator22));
                                    continue;
                                }
                                else
                                {
                                    i += 1;

                                    vtoroyyazyk[vtoroyyazyk.Count - 1] += Vuvod(0, razdeltel, ref i, true);
                                    string[] translator22 = vtoroyyazyk[vtoroyyazyk.Count - 1].Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
                                    // Console.WriteLine(vtoroyyazyk[vtoroyyazyk.Count - 1]);
                                    if (name2 == "C#")
                                    {

                                        translator2.Add("Console.WriteLine(" + translator22[1] + ");");
                                        //  Console.WriteLine(translator2[4]);
                                    }
                                    if (name2 == "C/C++")
                                    {
                                        translator2.Add(si(translator22));
                                    }
                                    continue;
                                }
                            }
                            else if (razdeltel[i].Length > 3 && razdeltel[i].Substring(0, 4) == "read" && (razdeltel[i].Length > 4 && (razdeltel[i][4] == ' ' || razdeltel[i][4] == '(')))
                            {
                                vtoroyyazyk.Add(Vvod(razdeltel, vtoroyyazyk, vtoroy_yazuk, i, false));
                                if (name2 == "C/C++")
                                {
                                    string[] vvodsi = vtoroyyazyk[0].Split(' ');
                                    vtoroyyazyk.RemoveAt(0);
                                    translator2.Add("cin");
                                    for (int j = 0; j < vvodsi.Length; j++)
                                    {
                                        if (vvodsi[j].Length > 0 && vvodsi[j][0] != ' ' && vvodsi[j][0] == '#')
                                        {
                                            translator2[translator2.Count - 1] += ">>" + vvodsi[j + 1];
                                            j++;
                                        }
                                    }
                                    translator2[translator2.Count - 1] += ";";
                                    continue;
                                }
                                else if (name2 == "C#")
                                {
                                    string splita = "newsplitnumber_" + random.ToString();
                                    translator2.Add("string[] " + splita + " = Console.ReadLine().Split(new char[] { ' ' },StringSplitOptions.RemoveEmptyEntries);");
                                    string[] vvodsharp = vtoroyyazyk[0].Split(' ');

                                    vtoroyyazyk.RemoveAt(0);
                                    int counter = (vvodsharp.Length - 3) / 2;
                                    translateTypeData(ref vvodsharp, counter);
                                    int counter2 = 0;
                                    int indexer = 1;
                                    while (counter != counter2)
                                    {
                                        indexer += 2;
                                        if (vvodsharp[indexer - 1] != "string")
                                            translator2.Add(vvodsharp[indexer] + "=" + vvodsharp[indexer - 1] + ".Parse(" + splita + "[" + counter2.ToString() + "]);");
                                        else translator2.Add(vvodsharp[indexer] + "=" + splita + "[" + counter2.ToString() + "];");
                                        counter2++;
                                    }
                                    continue;
                                }
                            }

                            else if (razdeltel[i].Length > 6 && razdeltel[i].Substring(0, 6) == "readln" && (razdeltel[i].Length > 6 && (razdeltel[i][6] == ' ' || razdeltel[i][6] == '(' || razdeltel[i][6] == ';')))
                            {
                                if (razdeltel[i][6] == ';')
                                {
                                    vtoroyyazyk.Add("@vstr_funk vvod_null");
                                    continue;
                                }
                                else
                                {
                                    vtoroyyazyk.Add(Vvod(razdeltel, vtoroyyazyk, vtoroy_yazuk, i, true));
                                    if (name2 == "C/C++")
                                    {
                                        string[] vvodsi = vtoroyyazyk[0].Split(' ');
                                        vtoroyyazyk.RemoveAt(0);
                                        translator2.Add("cin");
                                        for (int j = 0; j < vvodsi.Length; j++)
                                        {
                                            if (vvodsi[j].Length > 0 && vvodsi[j][0] != ' ' && vvodsi[j][0] == '#')
                                            {
                                                translator2[translator2.Count - 1] += ">>" + vvodsi[j + 1];
                                                j++;
                                            }
                                        }
                                        translator2[translator2.Count - 1] += ";";
                                        continue;
                                    }
                                    else if (name2 == "C#")
                                    {
                                        string[] vvodsharp = vtoroyyazyk[0].Split(' ');

                                        vtoroyyazyk.RemoveAt(0);
                                        int counter = (vvodsharp.Length - 3) / 2;
                                        translateTypeData(ref vvodsharp, counter);
                                        int counter2 = 0;
                                        int indexer = 1;
                                        while (counter != counter2)
                                        {
                                            indexer += 2;
                                            if (vvodsharp[indexer - 1] != "string")
                                                translator2.Add(vvodsharp[indexer] + "=" + vvodsharp[indexer - 1] + ".Parse(Console.ReadLine());");
                                            else translator2.Add(vvodsharp[indexer] + "= Console.ReadLine());");
                                            counter2++;
                                        }
                                        continue;
                                    }
                                }
                            }

                            if (razdeltel[i][0] == 'i' && razdeltel[i][1] == 'f' && (razdeltel[i][2] == ' ' || razdeltel[i][2] == '('))
                            {
                                vtoroyyazyk.Add("if");
                                string simvol = "";
                                string shifr = "";
                                for (int j = 2; j < razdeltel[i].Length; j++)
                                {
                                    if (razdeltel[i][j] == ' ')
                                    {
                                        continue;
                                    }
                                    if (j + 3 < razdeltel[i].Length)
                                    {
                                        if ((razdeltel[i][j - 1] == ' ' || razdeltel[i][j - 1] == ')') && (razdeltel[i][j + 3] == '(' || razdeltel[i][j + 3] == ' '))
                                        {
                                            if (razdeltel[i][j] == 'x' && razdeltel[i][j + 1] == 'o' && razdeltel[i][j + 2] == 'r')
                                            {
                                                shifr = "@iskluchauchiyili";
                                                if (simvol != "")
                                                    vtoroyyazyk.Add(simvol);
                                                vtoroyyazyk.Add(shifr);
                                                simvol = "";
                                                shifr = "";
                                                j += 2;
                                                continue;
                                            }
                                            if (razdeltel[i][j] == 'o' && razdeltel[i][j + 1] == 'r')
                                            {
                                                shifr = "@ili";
                                                if (simvol != "")
                                                    vtoroyyazyk.Add(simvol);
                                                vtoroyyazyk.Add(shifr);
                                                simvol = "";
                                                shifr = "";
                                                j += 1;
                                                continue;
                                            }
                                            if (razdeltel[i][j] == 'n' && razdeltel[i][j + 1] == 'o' && razdeltel[i][j + 2] == 't')
                                            {
                                                shifr = "@ne";
                                                if (simvol != "")
                                                    vtoroyyazyk.Add(simvol);
                                                vtoroyyazyk.Add(shifr);
                                                simvol = "";
                                                shifr = "";
                                                j += 2;
                                                continue;
                                            }
                                            if (razdeltel[i][j] == 'd' && razdeltel[i][j + 1] == 'i' && razdeltel[i][j + 2] == 'v')
                                            {
                                                shifr = "@delenie";
                                                if (simvol != "")
                                                    vtoroyyazyk.Add(simvol);
                                                vtoroyyazyk.Add(shifr);
                                                simvol = "";
                                                shifr = "";
                                                j += 2;
                                                continue;
                                            }
                                            if (razdeltel[i][j] == 'm' && razdeltel[i][j + 1] == 'o' && razdeltel[i][j + 2] == 'd')
                                            {
                                                shifr = "@deleniesostatkom";
                                                if (simvol != "")
                                                    vtoroyyazyk.Add(simvol);
                                                vtoroyyazyk.Add(shifr);
                                                simvol = "";
                                                shifr = "";
                                                j += 2;
                                                continue;
                                            }
                                            if (razdeltel[i][j] == 'a' && razdeltel[i][j + 1] == 'n' && razdeltel[i][j + 2] == 'd')
                                            {
                                                shifr = "@i";
                                                if (simvol != "")
                                                    vtoroyyazyk.Add(simvol);
                                                vtoroyyazyk.Add(shifr);
                                                simvol = "";
                                                shifr = "";
                                                j += 2;
                                                continue;
                                            }
                                        }
                                    }
                                    if (razdeltel[i][j] == '=')
                                    {
                                        if (razdeltel[i][j - 1] == '<' || razdeltel[i][j - 1] == '>')
                                        {
                                            simvol += "=";
                                            continue;
                                        }
                                        shifr = "@sravnenie";
                                        if (simvol != "")
                                            vtoroyyazyk.Add(simvol);
                                        vtoroyyazyk.Add(shifr);
                                        simvol = "";
                                        shifr = "";
                                        continue;
                                    }

                                    //________________________________________________________________________________________________________
                                    //if (j < razdeltel[i].Length - 4 && !char.IsLetterOrDigit(razdeltel[i][j-1]) && razdeltel[i].Substring(j, 3) == "abs" && (razdeltel[i][j + 4] == ' ' || razdeltel[i][j + 4] == '('))
                                    //{
                                    //    j += 3;
                                    //    if (simvol != "" && simvol != " ")
                                    //    {
                                    //        vtoroyyazyk.Add(simvol);
                                    //        simvol = "";
                                    //    }
                                    //    vtoroyyazyk.Add("@vstr`funk`modul`");
                                    //    continue;
                                    //}
                                    mathematicalFunctions(ref vtoroyyazyk, ref j, i, razdeltel, ref simvol);

                                    //__________________________________________________________________________________________________
                                    if (razdeltel[i][j] == ':' && razdeltel[i][j + 1] == '=')
                                    {
                                        j++;
                                        shifr = "@prisvaivanie";
                                        if (simvol != "")
                                            vtoroyyazyk.Add(simvol);
                                        vtoroyyazyk.Add(shifr);
                                        simvol = "";
                                        shifr = "";
                                        continue;
                                    }

                                    if (razdeltel[i][j] == '<' && razdeltel[i][j + 1] == '>')
                                    {
                                        j++;
                                        shifr = "@neravno";
                                        if (simvol != "")
                                            vtoroyyazyk.Add(simvol);
                                        vtoroyyazyk.Add(shifr);
                                        simvol = "";
                                        shifr = "";
                                        continue;
                                    }
                                    if (razdeltel[i][j] == '@')
                                    {
                                        vtoroyyazyk.Add(simvol);
                                        simvol = "@";
                                    }
                                    else
                                        simvol += razdeltel[i][j];
                                }
                                if (simvol != "")
                                    vtoroyyazyk.Add(simvol);
                            }
                            else
                            {
                                string simvol = "";
                                string shifr = "";
                                for (int j = 0; j < razdeltel[i].Length; j++)
                                {
                                    mathematicalFunctions(ref vtoroyyazyk, ref j, i, razdeltel, ref simvol);
                                    if (razdeltel[i][j] == ' ')
                                    {
                                        continue;
                                    }
                                    if (razdeltel[i] == "until")
                                    {
                                        translator2.Add("}");
                                        razdeltel[i] = "while(!(";
                                        razdeltel[i + 1] = razdeltel[i] + razdeltel[i + 1];
                                        razdeltel.RemoveAt(i);
                                        until = 1;
                                        i--;
                                        break;
                                    }
                                    if (j + 5 < razdeltel[i].Length)
                                    {
                                        if (razdeltel[i][j] == 'u' && razdeltel[i][j + 1] == 'n' && razdeltel[i][j + 2] == 't' && razdeltel[i][j + 3] == 'i' && razdeltel[i][j + 4] == 'l' && (razdeltel[i][j + 5] == ' ' || razdeltel[i][j + 5] == '('))
                                        {
                                            if (simvol != "")
                                                vtoroyyazyk.Add(simvol);
                                            translator2.Add("}");
                                            vtoroyyazyk.Add("while(!(");
                                            until = 1;
                                            simvol = "";
                                            j += 4;
                                            continue;
                                        }
                                    }
                                    if (j > 1)
                                    {
                                        if (j + 3 < razdeltel[i].Length)
                                        {
                                            if ((razdeltel[i][j - 1] == ' ' || razdeltel[i][j - 1] == ')') && (razdeltel[i][j + 3] == '(' || razdeltel[i][j + 3] == ' '))
                                            {
                                                if (razdeltel[i][j] == 'x' && razdeltel[i][j + 1] == 'o' && razdeltel[i][j + 2] == 'r')
                                                {
                                                    shifr = "@iskluchauchiyili";
                                                    if (simvol != "")
                                                        vtoroyyazyk.Add(simvol);
                                                    vtoroyyazyk.Add(shifr);
                                                    simvol = "";
                                                    shifr = "";
                                                    j += 2;
                                                    continue;
                                                }
                                                if (razdeltel[i][j] == 'n' && razdeltel[i][j + 1] == 'o' && razdeltel[i][j + 2] == 't')
                                                {
                                                    shifr = "ne";
                                                    if (simvol != "")
                                                        vtoroyyazyk.Add(simvol);
                                                    vtoroyyazyk.Add(shifr);
                                                    simvol = "";
                                                    shifr = "";
                                                    j += 2;
                                                    continue;
                                                }
                                                if (razdeltel[i][j] == 'd' && razdeltel[i][j + 1] == 'i' && razdeltel[i][j + 2] == 'v')
                                                {
                                                    shifr = "@delenie";
                                                    if (simvol != "")
                                                        vtoroyyazyk.Add(simvol);
                                                    vtoroyyazyk.Add(shifr);
                                                    simvol = "";
                                                    shifr = "";
                                                    j += 2;
                                                    continue;
                                                }

                                                if (razdeltel[i][j] == 'm' && razdeltel[i][j + 1] == 'o' && razdeltel[i][j + 2] == 'd')
                                                {
                                                    shifr = "@deleniesostatkom";
                                                    if (simvol != "")
                                                        vtoroyyazyk.Add(simvol);
                                                    vtoroyyazyk.Add(shifr);
                                                    simvol = "";
                                                    shifr = "";
                                                    j += 2;
                                                    continue;
                                                }
                                                if (razdeltel[i][j] == 'a' && razdeltel[i][j + 1] == 'n' && razdeltel[i][j + 2] == 'd')
                                                {
                                                    shifr = "@i";
                                                    if (simvol != "")
                                                        vtoroyyazyk.Add(simvol);
                                                    vtoroyyazyk.Add(shifr);
                                                    simvol = "";
                                                    shifr = "";
                                                    j += 2;
                                                    continue;
                                                }
                                            }
                                        }
                                    }
                                    if (razdeltel[i][j] == '=')
                                    {
                                        if (simvol != "")
                                            vtoroyyazyk.Add(simvol);
                                        vtoroyyazyk.Add("@sravnenie");
                                        simvol = "";
                                        continue;
                                    }
                                    if (razdeltel[i][j] == ':' && razdeltel[i][j + 1] == '=')
                                    {
                                        j++;
                                        if (simvol != "")
                                            vtoroyyazyk.Add(simvol);
                                        vtoroyyazyk.Add("@prisvaivanie");
                                        simvol = "";
                                        continue;
                                    }

                                    if (razdeltel[i][j] == '<' && razdeltel[i][j + 1] == '>')
                                    {
                                        j++;
                                        if (simvol != "")
                                            vtoroyyazyk.Add(simvol);
                                        vtoroyyazyk.Add("@neravno");
                                        simvol = "";
                                        continue;
                                    }
                                    if (j + 8 < razdeltel[i].Length)
                                    {
                                        if (razdeltel[i][j] == 'e' && razdeltel[i][j + 1] == 'l' && razdeltel[i][j + 2] == 's' && razdeltel[i][j + 3] == 'e' && razdeltel[i][j + 4] == ' ' && razdeltel[i][j + 5] == 'i' && razdeltel[i][j + 6] == 'f' && razdeltel[i][j + 7] == ' ')
                                        {
                                            j += 7;
                                            string elseif = "else if ";
                                            vtoroyyazyk.Add(elseif);
                                            continue;
                                        }
                                    }
                                    if (j + 4 < razdeltel[i].Length)
                                    {
                                        if (razdeltel[i][j] == 'e' && razdeltel[i][j + 1] == 'l' && razdeltel[i][j + 2] == 's' && razdeltel[i][j + 3] == 'e' && razdeltel[i][j + 4] == ' ')
                                        {
                                            string else2 = "else";
                                            j += 4;
                                            vtoroyyazyk.Add(else2);
                                            continue;
                                        }
                                    }
                                    if (razdeltel[i][j] == '@')
                                    {
                                        vtoroyyazyk.Add(simvol);
                                        simvol = "@";
                                    }
                                    else if (razdeltel[i][j] == ';' && until == 1)
                                    {
                                        simvol += "));";
                                    }
                                    else simvol += razdeltel[i][j];
                                }
                                if (simvol != "")
                                    vtoroyyazyk.Add(simvol);
                            }
                            if (vtoroyyazyk.Count != 0)
                                translator2.Add("");
                            for (int o = 0; o < vtoroyyazyk.Count; o++)
                            {
                                if (vtoroyyazyk[o] == "" || vtoroyyazyk[o] == " ")
                                    continue;
                                if (name2 == "C#" || name2 == "C/C++")
                                {
                                    if (vtoroyyazyk[o] == "if")
                                        translator2[translator2.Count - 1] += (vtoroyyazyk[o] + "(");

                                    else if (vtoroyyazyk[o] == "@iskluchauchiyili")
                                        translator2[translator2.Count - 1] += '^';
                                    else if (vtoroyyazyk[o] == "@ne")
                                        translator2[translator2.Count - 1] += '!';
                                    else if (vtoroyyazyk[o] == "@delenie")
                                        translator2[translator2.Count - 1] += '/';
                                    else if (vtoroyyazyk[o] == "@deleniesostatkom")
                                        translator2[translator2.Count - 1] += '%';
                                    else if (vtoroyyazyk[o] == "@i")
                                        translator2[translator2.Count - 1] += "&&";
                                    else if (vtoroyyazyk[o] == "@sravnenie")
                                        translator2[translator2.Count - 1] += "==";
                                    else if (vtoroyyazyk[o] == "@prisvaivanie")
                                        translator2[translator2.Count - 1] += '=';
                                    else if (vtoroyyazyk[o] == "@neravno")
                                        translator2[translator2.Count - 1] += "!=";
                                    else if (vtoroyyazyk[o] == "@konec")
                                        translator2[translator2.Count - 1] += ')';
                                    else if (vtoroyyazyk[o] == "@ili")
                                        translator2[translator2.Count - 1] += "||";
                                    else if (vtoroyyazyk[o] == "@openblock")
                                        translator2[translator2.Count - 1] += '{';
                                    else if (vtoroyyazyk[o] == "@closeblock")
                                        translator2[translator2.Count - 1] += '}';
                                    else if (vtoroyyazyk[o] == "@do")
                                        translator2[translator2.Count - 1] += "do{";
                                    else if (vtoroyyazyk[o] == "else if ")
                                        translator2[translator2.Count - 1] += "else if(";
                                    else if (vtoroyyazyk[o][0] == '@')
                                    {
                                        if (name2 == "C#")
                                        {
                                            if (vtoroyyazyk[o] == "@vstrfunk_ln")
                                            {
                                                translator2[translator2.Count - 1] += "Math.Log";
                                            }
                                            else if (vtoroyyazyk[o] == "@vstrfunk_modul")
                                            {
                                                translator2[translator2.Count - 1] += "Math.Abs";
                                            }
                                            else if (vtoroyyazyk[o] == "@vstrfunk_sin")
                                            {
                                                translator2[translator2.Count - 1] += "Math.Sin";
                                            }
                                            else if (vtoroyyazyk[o] == "@vstrfunk_cos")
                                            {
                                                translator2[translator2.Count - 1] += "Math.Cos";
                                            }
                                            else if (vtoroyyazyk[o] == "@vstrfunk_celchast")
                                            {
                                                translator2[translator2.Count - 1] += "Math.Truncate";
                                            }
                                            else if (vtoroyyazyk[o] == "@vstrfunk_drobchast")
                                            {
                                                translator2[translator2.Count - 1] += vtoroyyazyk[o + 1] + "- Math.Truncate";
                                            }
                                            else if (vtoroyyazyk[o] == "@vstrfunk_kor")
                                            {
                                                translator2[translator2.Count - 1] += "Math.Sqrt";
                                            }
                                            else if (vtoroyyazyk[o] == "@vstrfunk_stepen")
                                            {
                                                translator2[translator2.Count - 1] += "Math.Pow";
                                            }
                                            else if (vtoroyyazyk[o] == "@vstrfunk_plusodin")
                                            {
                                                translator2[translator2.Count - 1] += "1+" + vtoroyyazyk[o + 1];
                                            }
                                            else if (vtoroyyazyk[o] == "@vstrfunk_kvadrat")
                                            {
                                                string s1 = vtoroyyazyk[o + 1];
                                                string left = "Math.Pow(";
                                                string right = "";
                                                int balanser = 1;
                                                int balanser2 = 0;
                                                for (int j = 1; j < s1.Length; j++)
                                                {
                                                    if (balanser2 == 1)
                                                    {
                                                        right += s1[j];
                                                        continue;
                                                    }

                                                    if (s1[j] == '(')
                                                    {
                                                        balanser++;
                                                        left += s1[j];
                                                    }
                                                    else if (s1[j] == ')')
                                                    {
                                                        balanser--;
                                                        if (balanser == 0)
                                                        {
                                                            balanser2 = 1;
                                                            left += ",2)";
                                                            continue;
                                                        }
                                                        left += s1[j];
                                                    }
                                                    else left += s1[j];
                                                }
                                                translator2[translator2.Count - 1] += left + right;
                                                o++;
                                            }
                                        }
                                        else if (name2 == "C/C++")
                                        {
                                            if (vtoroyyazyk[o] == "@vstrfunk_ln")
                                            {
                                                translator2[translator2.Count - 1] += "log";
                                            }
                                            else if (vtoroyyazyk[o] == "@vstrfunk_modul")
                                            {
                                                translator2[translator2.Count - 1] += "abs";
                                            }
                                            else if (vtoroyyazyk[o] == "@vstrfunk_sin")
                                            {
                                                translator2[translator2.Count - 1] += "sin";
                                            }
                                            else if (vtoroyyazyk[o] == "@vstrfunk_cos")
                                            {
                                                translator2[translator2.Count - 1] += "cos";
                                            }
                                            else if (vtoroyyazyk[o] == "@vstrfunk_celchast")
                                            {
                                                translator2[translator2.Count - 1] += "(int)";
                                            }
                                            else if (vtoroyyazyk[o] == "@vstrfunk_drobchast")
                                            {
                                                translator2[translator2.Count - 1] += vtoroyyazyk[o + 1] + "(int)";
                                            }
                                            else if (vtoroyyazyk[o] == "@vstrfunk_kor")
                                            {
                                                translator2[translator2.Count - 1] += "sqrt";
                                            }
                                            else if (vtoroyyazyk[o] == "@vstrfunk_stepen")
                                            {
                                                translator2[translator2.Count - 1] += "pow";
                                            }
                                            else if (vtoroyyazyk[o] == "@vstrfunk_plusodin")
                                            {
                                                translator2[translator2.Count - 1] += "1+" + vtoroyyazyk[o + 1];
                                            }
                                            else if (vtoroyyazyk[o] == "@vstrfunk_kvadrat")
                                            {
                                                string s1 = vtoroyyazyk[o + 1];
                                                string left = "pow(";
                                                string right = "";
                                                int balanser = 1;
                                                int balanser2 = 0;
                                                for (int j = 1; j < s1.Length; j++)
                                                {
                                                    if (balanser2 == 1)
                                                    {
                                                        right += s1[j];
                                                        continue;
                                                    }

                                                    if (s1[j] == '(')
                                                    {
                                                        balanser++;
                                                        left += s1[j];
                                                    }
                                                    else if (s1[j] == ')')
                                                    {
                                                        balanser--;
                                                        if (balanser == 0)
                                                        {
                                                            balanser2 = 1;
                                                            left += ",2)";
                                                            continue;
                                                        }
                                                        left += s1[j];
                                                    }
                                                    else left += s1[j];
                                                }
                                                translator2[translator2.Count - 1] += left + right;
                                                o++;
                                            }

                                        }
                                    }
                                    else translator2[translator2.Count - 1] += vtoroyyazyk[o];
                                }
                                //       Console.WriteLine(vtoroyyazyk[o]);
                            }

                        }

                    }
                    if (name2 == "C#")
                        for (int i = 0; i < 3; i++)
                            translator2.Add("}");
                    else if (name2 == "C/C++")
                        translator2.Add("}");
                }



                if (name2 == "C#")
                {
                    string[] c = { "using System;", "using System.Collections.Generic;", "using System.Linq;", "using System.Text;", "using System.Threading.Tasks;",  "using System.IO;"
                                   ,"namespace ConsoleApp1","{","class Program","{","static void Main(string[] args)","{" };
                    for (int i = 0; i < c.Length; i++)
                        translator2.Add(c[i]);
                    for (int i = 0; i < vtoroy_yazuk.Count; i += 2)
                    {
                        if (vtoroy_yazuk[i].Substring(0, 13) == "@obyavl #arr ")
                        {
                            int[] a = new int[10], b = new int[10];
                            // int[] a, b, c;  a = new int[3]; b = new int[4]; c = new int[5];
                            translator2.Add(massiv(vtoroy_yazuk, i, name2)[0] + "[]");
                            string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            if (translator22.Length > 1)
                            {
                                translator2[translator2.Count - 1] += string.Join(",", translator22) + "; ";
                                for (int j = 0; j < translator22.Length; j++)
                                {
                                    translator2[translator2.Count - 1] += translator22[j] + " = new " + massiv(vtoroy_yazuk, i, name2)[0] + "[" + massiv(vtoroy_yazuk, i, name2)[1] + "]" + ";";
                                }
                            }
                            else
                            {
                                translator2[translator2.Count - 1] += translator22[0] + " = new " + massiv(vtoroy_yazuk, i, name2)[0] + "[" + massiv(vtoroy_yazuk, i, name2)[1] + "]";
                            }
                        }
                        else if (vtoroy_yazuk[i].Substring(0, 16) == "@obyavl #arr_2D ")
                        {
                            //int[,] a = new int[10, 12];
                            // int[] a, b, c;  a = new int[3]; b = new int[4]; c = new int[5];
                            translator2.Add(massiv(vtoroy_yazuk, i, name2)[0] + "[,]");
                            string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            if (translator22.Length > 1)
                            {
                                translator2[translator2.Count - 1] += string.Join(",", translator22) + "; ";
                                for (int j = 0; j < translator22.Length; j++)
                                {
                                    translator2[translator2.Count - 1] += translator22[j] + " = new " + massiv(vtoroy_yazuk, i, name2)[0] + "[" + massiv(vtoroy_yazuk, i, name2)[1] + "," + massiv(vtoroy_yazuk, i, name2)[2] + "]" + ";";
                                }
                            }
                            else
                            {
                                translator2[translator2.Count - 1] += translator22[0] + " = new " + massiv(vtoroy_yazuk, i, name2)[0] + "[" + massiv(vtoroy_yazuk, i, name2)[1] + "," + massiv(vtoroy_yazuk, i, name2)[2] + "]";
                            }
                        }
                        else if (vtoroy_yazuk[i] == "@obyavl #cel_32 ")
                        {
                            translator2.Add("int "); string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            translator2[translator2.Count - 1] += string.Join(",", translator22) + ";";
                        }
                        else if (vtoroy_yazuk[i] == "@obyavl #drob_48 ")
                        {
                            translator2.Add("double "); string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            translator2[translator2.Count - 1] += string.Join(",", translator22) + ";";
                        }
                        else if (vtoroy_yazuk[i] == "@obyavl #u_cel_8 ")
                        {
                            translator2.Add("byte "); string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            translator2[translator2.Count - 1] += string.Join(",", translator22) + ";";
                        }
                        else if (vtoroy_yazuk[i] == "@obyavl #cel_8 ")
                        {
                            translator2.Add("sbyte "); string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            translator2[translator2.Count - 1] += string.Join(",", translator22) + ";";
                        }
                        else if (vtoroy_yazuk[i] == "@obyavl #cel_16 ")
                        {
                            translator2.Add("short "); string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            translator2[translator2.Count - 1] += string.Join(",", translator22) + ";";
                        }
                        else if (vtoroy_yazuk[i] == "@obyavl #u_cel_16 ")
                        {
                            translator2.Add("ushort "); string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            translator2[translator2.Count - 1] += string.Join(",", translator22) + ";";
                        }
                        else if (vtoroy_yazuk[i] == "@obyavl #cel_64 ")
                        {
                            translator2.Add("long ");
                            string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            translator2[translator2.Count - 1] += string.Join(",", translator22) + ";";
                        }
                        else if (vtoroy_yazuk[i] == "@obyavl #u_cel_32 ")
                        {
                            translator2.Add("uint ");
                            string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            translator2[translator2.Count - 1] += string.Join(",", translator22) + ";";
                        }
                        else if (vtoroy_yazuk[i] == "@obyavl #u_cel_32 ")
                        {
                            translator2.Add("uint ");
                            string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            translator2[translator2.Count - 1] += string.Join(",", translator22) + ";";
                        }
                        else if (vtoroy_yazuk[i] == "@obyavl #simv_16 ")
                        {
                            translator2.Add("char "); string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            translator2[translator2.Count - 1] += string.Join(",", translator22) + ";";
                        }
                        else if (vtoroy_yazuk[i] == "@obyavl #drob_64 ")
                        {
                            translator2.Add("double "); string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            translator2[translator2.Count - 1] += string.Join(",", translator22) + ";";
                        }
                        else if (vtoroy_yazuk[i] == "@obyavl #drob_32 ")
                        {
                            translator2.Add("float "); string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            translator2[translator2.Count - 1] += string.Join(",", translator22) + ";";
                        }
                        else if (vtoroy_yazuk[i] == "@obyavl #logic ")
                        {
                            translator2.Add("bool "); string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            translator2[translator2.Count - 1] += string.Join(",", translator22) + ";";
                        }
                        else if (vtoroy_yazuk[i] == "@obyavl #string ")
                        {
                            translator2.Add("string "); string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            translator2[translator2.Count - 1] += string.Join(",", translator22) + ";";
                        }
                        else if (vtoroy_yazuk[i] == "@obyavl #u_cel_64 ")
                        {
                                translator2.Add("ulong ");
                            string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            translator2[translator2.Count - 1] += string.Join(",", translator22) + ";";
                        }
                        else if (vtoroy_yazuk[i] == "@obyavl #neyavn ")
                        {
                            translator2.Add("var "); string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            if ((int)translator22[1][0] == 39)
                            {
                                if (translator22[1].Length > 3) //translator2[translator2.Count - 1] += string.Join("", translator22) + ";";
                                {
                                    string zamec = "";
                                    for (int j = 0; j < translator22[1].Length; j++)
                                    {
                                        if (translator22[1][j] == '\'')
                                            zamec += '\"';
                                        else zamec += translator22[1][j];
                                    }
                                    translator22[1] = zamec;
                                }
                            }
                            translator2[translator2.Count - 1] += string.Join("=", translator22) + ";";
                        }
                    }
                }





                else if (name2 == "C/C++")
                {
                    string[] c = { "#include <iostream>", "#include<fstream>", "#include <Windows.h>", "#include<string>", "#include<cmath>", "#include<conio.h>", "using namespace std;", "int main()", "{", "setlocale(LC_ALL, \"RUSSIAN\");" };
                    for (int i = 0; i < c.Length; i++)
                        translator2.Add(c[i]);
                    for (int i = 0; i < vtoroy_yazuk.Count; i += 2)
                    {
                        if (vtoroy_yazuk[i].Substring(0, 13) == "@obyavl #arr ")
                        {
                            {
                                translator2.Add(massiv(vtoroy_yazuk, i, name2)[0]);
                                //long A[10], b[10],c[10];
                                string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                if (translator22.Length > 1)
                                {
                                    for (int j = 0; j < translator22.Length; j++)
                                    {
                                        if (j < translator22.Length - 1)
                                            translator2[translator2.Count - 1] += translator22[j] + "[" + massiv(vtoroy_yazuk, i, name2)[1] + "]" + ", ";
                                        else
                                            translator2[translator2.Count - 1] += translator22[j] + "[" + massiv(vtoroy_yazuk, i, name2)[1] + "]" + ";";
                                    }
                                }
                                else
                                    translator2[translator2.Count - 1] += translator22[0] + "[" + massiv(vtoroy_yazuk, i, name2)[1] + "]" + ";";
                            }
                            // translator2.Add(massiv(vtoroy_yazuk, i)[0]); string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            // translator2[translator2.Count - 1] += string.Join(",", translator22) + ";";
                        }
                        else if (vtoroy_yazuk[i].Substring(0, 16) == "@obyavl #arr_2D ")
                        {
                            translator2.Add(massiv(vtoroy_yazuk, i, name2)[0]);
                            //int a[10][12],b[15][13];
                            string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            if (translator22.Length > 1)
                            {
                                for (int j = 0; j < translator22.Length; j++)
                                {
                                    if (j < translator22.Length - 1)
                                        translator2[translator2.Count - 1] += translator22[j] + "[" + massiv(vtoroy_yazuk, i, name2)[1] + "]" + "[" + massiv(vtoroy_yazuk, i, name2)[2] + "]" + ", ";
                                    else
                                        translator2[translator2.Count - 1] += translator22[j] + "[" + massiv(vtoroy_yazuk, i, name2)[1] + "]" + "[" + massiv(vtoroy_yazuk, i, name2)[2] + "]" + ";";
                                }
                            }
                            else
                                translator2[translator2.Count - 1] += translator22[0] + "[" + massiv(vtoroy_yazuk, i, name2)[1] + "]" + "[" + massiv(vtoroy_yazuk, i, name2)[2] + "]" + ";";
                        }
                        else if (vtoroy_yazuk[i] == "@obyavl #cel_32 ")
                        {
                            translator2.Add("int "); string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            translator2[translator2.Count - 1] += string.Join(",", translator22) + ";";
                        }
                        else if (vtoroy_yazuk[i] == "@obyavl #drob_48 ")
                        {
                            translator2.Add("double "); string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            translator2[translator2.Count - 1] += string.Join(",", translator22) + ";";
                        }
                        else if (vtoroy_yazuk[i] == "@obyavl #u_cel_8 ")
                        {
                            translator2.Add("byte "); string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            translator2[translator2.Count - 1] += string.Join(",", translator22) + ";";
                        }
                        else if (vtoroy_yazuk[i] == "@obyavl #cel_8 ")
                        {
                            translator2.Add("sbyte "); string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            translator2[translator2.Count - 1] += string.Join(",", translator22) + ";";
                        }
                        else if (vtoroy_yazuk[i] == "@obyavl #cel_16 ")
                        {
                            translator2.Add("short "); string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            translator2[translator2.Count - 1] += string.Join(",", translator22) + ";";
                        }
                        else if (vtoroy_yazuk[i] == "@obyavl #u_cel_16 ")
                        {
                            translator2.Add("ushort "); string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            translator2[translator2.Count - 1] += string.Join(",", translator22) + ";";
                        }
                        else if (vtoroy_yazuk[i] == "@obyavl #cel_64 ")
                        {
                            translator2.Add("long long ");
                            string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            translator2[translator2.Count - 1] += string.Join(",", translator22) + ";";
                        }
                        else if (vtoroy_yazuk[i] == "@obyavl #u_cel_32 ")
                        {
                            translator2.Add("unsigned long ");
                            string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            translator2[translator2.Count - 1] += string.Join(",", translator22) + ";";
                        }
                        else if (vtoroy_yazuk[i] == "@obyavl #u_cel_32 ")
                        {
                            translator2.Add("unsigned long ");
                            string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            translator2[translator2.Count - 1] += string.Join(",", translator22) + ";";
                        }
                        else if (vtoroy_yazuk[i] == "@obyavl #simv_16 ")
                        {
                            translator2.Add("char "); string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            translator2[translator2.Count - 1] += string.Join(",", translator22) + ";";
                        }
                        else if (vtoroy_yazuk[i] == "@obyavl #drob_64 ")
                        {
                            translator2.Add("double "); string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            translator2[translator2.Count - 1] += string.Join(",", translator22) + ";";
                        }
                        else if (vtoroy_yazuk[i] == "@obyavl #drob_32 ")
                        {
                            translator2.Add("float "); string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            translator2[translator2.Count - 1] += string.Join(",", translator22) + ";";
                        }
                        else if (vtoroy_yazuk[i] == "@obyavl #logic ")
                        {
                            translator2.Add("bool "); string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            translator2[translator2.Count - 1] += string.Join(",", translator22) + ";";
                        }
                        else if (vtoroy_yazuk[i] == "@obyavl #string ")
                        {
                            translator2.Add("string "); string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            translator2[translator2.Count - 1] += string.Join(",", translator22) + ";";
                        }
                        else if (vtoroy_yazuk[i] == "@obyavl #u_cel_64 ")
                        {
                            translator2.Add("unsigned long long ");
                            string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            translator2[translator2.Count - 1] += string.Join(",", translator22) + ";";
                        }
                        else if (vtoroy_yazuk[i] == "@obyavl #neyavn ")
                        {
                            translator2.Add("var "); string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            if ((int)translator22[1][0] == 39)
                            {
                                if (translator22[1].Length > 3) //translator2[translator2.Count - 1] += string.Join("", translator22) + ";";
                                {
                                    string zamec = "";
                                    for (int j = 0; j < translator22[1].Length; j++)
                                    {
                                        if (translator22[1][j] == '\'')
                                            zamec += '\"';
                                        else zamec += translator22[1][j];
                                    }
                                    translator22[1] = zamec;
                                }
                            }
                            translator2[translator2.Count - 1] += string.Join("=", translator22) + ";";
                        }
                    }
                }
                else translator2.Add("Данные языки находятся в разработке, просим прощения");
                Navigation.PushAsync(new PageTwo());
            }
            else if (name1 == name2 && name1 != "" && name2 != "")
            {
                DisplayAlert("                 Ошибка!", "Выбран одинаковый язык.", "ОK");
            }
            else if (translator.Count != 0)
            {
                DisplayAlert("                 Ошибка!", "Не введен код", "ОK");
            }
            else if (translator.Count == 0)
            {
                DisplayAlert("                 Ошибка!", "Вы не ввели текст", "ОK");
            }
            else
            {
                DisplayAlert("                 Ошибка!", "Вы не выбрали нужные языки.", "ОK");
            }
        }
        private void Baraban(object sender, EventArgs e)
        {
            name1 = Picker1.Items[Picker1.SelectedIndex];
        }
        private void Baraban2(object sender, EventArgs e)
        {
            name2 = Picker2.Items[Picker2.SelectedIndex];
        }
        private void Baraban3(object sender, EventArgs e)
        {
           
            translator = Editor2.Text.Split('\n','\r').ToList();
        }
        static void To_downto(string[] rof, ref string na_obchem3, ref string na_obchem4, int i, int ind)
        {
            if (rof.Contains("to"))
            {
                na_obchem4 += $"{rof[i]} + 1";//в конец тела цикла
                na_obchem3 += $"{rof[i]}<=";
                for (; rof[ind] != "@konec"; ind++)
                    na_obchem3 += rof[ind] + " ";
            }
            else
            {
                na_obchem4 += $"{rof[i]} - 1";//в конец тела цикла
                na_obchem3 += $"{rof[i]}<=";
                for (; rof[ind] != "@konec"; ind++)
                    na_obchem3 += rof[ind] + " ";
            }
        }
        static string si(string[] translator22)
        {
            bool flag = false;
            string aba = "cout<<";
            string Si = translator22[1];
            for (int j = 2; j < Si.Length; j++)
            {
                if (Si[j] == '{')
                {

                    j++;
                    while (Si[j] != '}')
                    {
                        aba += Si[j];
                        j++;
                    }
                    aba += "<<";
                    continue;
                }
                if (Si[j] == '\"')
                { aba += "endl;"; continue; }
                if (aba[aba.Length - 1] == '<')
                    aba += "\"";
                if (Si[j + 1] != '{' && Si[j + 1] != '\"')
                {
                    aba += Si[j];
                }
                else
                {
                    aba += Si[j] + "\"<<";
                }
            }
            return aba;
        }
        static string si2(string[] translator22)
        {

            string aba = "cout<<";
            string Si = translator22[1];
            for (int j = 2; j < Si.Length; j++)
            {
                if (Si[j] == '{')
                {

                    j++;
                    while (Si[j] != '}')
                    {
                        aba += Si[j];
                        j++;
                    }
                    aba += "<<";
                    continue;
                }
                if (Si[j] == '\"')
                { aba += "endl;"; continue; }
                if (aba[aba.Length - 1] == '<')
                    aba += "\"";
                if (Si[j + 1] != '{' && Si[j + 1] != '\"')
                {
                    aba += Si[j];
                }
                else
                {
                    aba += Si[j] + "\"<<";
                }
            }
            aba = aba.Remove(aba.Length - 7);
            aba += ";";
            return aba;
        }
        static string Vuvod(int index, List<string> razdeltel, ref int i, bool w)
        {
            string pechat = "@$\"";
            for (int j = index; razdeltel[i][j] != ')'; j++)
            {
                if (razdeltel[i][j] == ' ')
                {
                    if (j != razdeltel[i].Length - 1)
                        continue;
                    else
                    {
                        j = -1;
                        i++;
                    }
                }
                else if (razdeltel[i][j] == '(')
                {
                    if (j != razdeltel[i].Length - 1)
                        continue;
                    else
                    {
                        j = -1;
                        i++;
                    }
                }
                else if (razdeltel[i][j] == '\'')
                {
                    if (j != razdeltel[i].Length - 1)
                        j++;
                    else
                    {
                        j = -1;
                        i++;
                    }
                    while (true)
                    {
                        pechat += razdeltel[i][j];
                        if (razdeltel[i][j + 1] != '\'')
                            j++;
                        else
                        {
                            if (j != razdeltel[i].Length - 1)
                                j++;
                            else
                            {
                                j = -1;
                                i++;
                            }
                            break;
                        }
                    }
                }
                else if (razdeltel[i][j] == ',')
                {
                    if (w == false)
                        pechat += ' ';
                }
                else
                {
                    pechat += '{';
                    while (j != razdeltel[i].Length && razdeltel[i][j] != ')' && razdeltel[i][j] != ' ' && razdeltel[i][j] != ',')
                    {
                        pechat += razdeltel[i][j];
                        j++;
                    }
                    pechat += '}';
                    if (j == razdeltel[i].Length)
                    {
                        j = -1;
                        i++;

                    }
                    else if (razdeltel[i][j] == ')')
                        break;
                }
                if (j == razdeltel[i].Length - 1)
                {
                    j = -1;
                    i++;
                }
            }
            return pechat += "\"";
        }
        static void Typee(List<string> poisk_var, ref List<string> vtoroy_yazuk, int par)
        {
            switch (poisk_var[poisk_var.Count - 1])
            {
                case "integer":
                    {
                        vtoroy_yazuk.Add("@obyavl #cel_32 ");
                        vtoroy_yazuk.Add("");
                        for (int j = 0; j < poisk_var.Count - par; j++)
                            vtoroy_yazuk[vtoroy_yazuk.Count - 1] += poisk_var[j] + " ";
                        break;
                    }
                case "real":
                    {
                        vtoroy_yazuk.Add("@obyavl #drob_48 ");
                        vtoroy_yazuk.Add("");
                        for (int j = 0; j < poisk_var.Count - par; j++)
                            vtoroy_yazuk[vtoroy_yazuk.Count - 1] += poisk_var[j] + " ";
                        break;
                    }
                case "byte":
                    {
                        vtoroy_yazuk.Add("@obyavl #u_cel_8 ");
                        vtoroy_yazuk.Add("");
                        for (int j = 0; j < poisk_var.Count - par; j++)
                            vtoroy_yazuk[vtoroy_yazuk.Count - 1] += poisk_var[j] + " ";
                        break;
                    }
                case "shortint":
                    {
                        vtoroy_yazuk.Add("@obyavl #cel_8 ");
                        vtoroy_yazuk.Add("");
                        for (int j = 0; j < poisk_var.Count - par; j++)
                            vtoroy_yazuk[vtoroy_yazuk.Count - 1] += poisk_var[j] + " ";
                        break;
                    }
                case "smallint":
                    {
                        vtoroy_yazuk.Add("@obyavl #cel_16 ");
                        vtoroy_yazuk.Add("");
                        for (int j = 0; j < poisk_var.Count - par; j++)
                            vtoroy_yazuk[vtoroy_yazuk.Count - 1] += poisk_var[j] + " ";
                        break;
                    }
                case "word":
                    {
                        vtoroy_yazuk.Add("@obyavl #u_cel_16 ");
                        vtoroy_yazuk.Add("");
                        for (int j = 0; j < poisk_var.Count - par; j++)
                            vtoroy_yazuk[vtoroy_yazuk.Count - 1] += poisk_var[j] + " ";
                        break;
                    }
                case "int64":
                    {
                        vtoroy_yazuk.Add("@obyavl #cel_64 ");
                        vtoroy_yazuk.Add("");
                        for (int j = 0; j < poisk_var.Count - par; j++)
                            vtoroy_yazuk[vtoroy_yazuk.Count - 1] += poisk_var[j] + " ";
                        break;
                    }
                case "longword":
                    {
                        vtoroy_yazuk.Add("@obyavl #u_cel_32 ");
                        vtoroy_yazuk.Add("");
                        for (int j = 0; j < poisk_var.Count - par; j++)
                            vtoroy_yazuk[vtoroy_yazuk.Count - 1] += poisk_var[j] + " ";
                        break;
                    }
                case "uin64":
                    {
                        vtoroy_yazuk.Add("@obyavl #u_cel_64 ");
                        vtoroy_yazuk.Add("");
                        for (int j = 0; j < poisk_var.Count - par; j++)
                            vtoroy_yazuk[vtoroy_yazuk.Count - 1] += poisk_var[j] + " ";
                        break;
                    }
                case "char":
                    {
                        vtoroy_yazuk.Add("@obyavl #simv_16 ");
                        vtoroy_yazuk.Add("");
                        for (int j = 0; j < poisk_var.Count - par; j++)
                            vtoroy_yazuk[vtoroy_yazuk.Count - 1] += poisk_var[j] + " ";
                        break;
                    }
                case "double":
                    {
                        vtoroy_yazuk.Add("@obyavl #drob_64 ");
                        vtoroy_yazuk.Add("");
                        for (int j = 0; j < poisk_var.Count - par; j++)
                            vtoroy_yazuk[vtoroy_yazuk.Count - 1] += poisk_var[j] + " ";
                        break;
                    }
                case "single":
                    {
                        vtoroy_yazuk.Add("@obyavl #drob_32 ");
                        vtoroy_yazuk.Add("");
                        for (int j = 0; j < poisk_var.Count - par; j++)
                            vtoroy_yazuk[vtoroy_yazuk.Count - 1] += poisk_var[j] + " ";
                        break;
                    }
                case "boolean":
                    {
                        vtoroy_yazuk.Add("@obyavl #logic ");
                        vtoroy_yazuk.Add("");
                        for (int j = 0; j < poisk_var.Count - par; j++)
                            vtoroy_yazuk[vtoroy_yazuk.Count - 1] += poisk_var[j] + " ";
                        break;
                    }
                case "longint":
                    {
                        vtoroy_yazuk.Add("@obyavl #cel_32 ");
                        vtoroy_yazuk.Add("");
                        for (int j = 0; j < poisk_var.Count - par; j++)
                            vtoroy_yazuk[vtoroy_yazuk.Count - 1] += poisk_var[j] + " ";
                        break;
                    }
                default:
                    {
                        vtoroy_yazuk.Add("@obyavl #neyavn ");
                        vtoroy_yazuk.Add("");
                        vtoroy_yazuk[vtoroy_yazuk.Count - 1] += String.Join(" ", poisk_var.ToArray());
                        break;
                    }
            }
        }
        static string[] massiv(List<string> vtoroy_yazuk, int i, string name2)
        {
            int znachenie = 0;
            string[] tipdannih = new string[3];
            string[] razdel = vtoroy_yazuk[i].Split(new char[] { '@', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            razdel[razdel.Length - 2] += " " + razdel[razdel.Length - 1] + " ";
            if (razdel[1] == "#arr")
                znachenie = 4;
            else znachenie = 6;
            if (razdel[znachenie] == "obyavl #cel_32 ")
            {
                tipdannih[0] = ("int ");
            }
            if (razdel[znachenie] == "obyavl #drob_48 ")
            {
                tipdannih[0] = ("double ");
            }
            if (razdel[znachenie] == "obyavl #u_cel_8 ")
            {
                tipdannih[0] = ("byte ");
            }
            if (razdel[znachenie] == "obyavl #cel_8 ")
            {
                tipdannih[0] = ("sbyte ");
            }
            if (razdel[znachenie] == "obyavl #cel_16 ")
            {
                tipdannih[0] = ("short ");
            }
            if (razdel[znachenie] == "obyavl #u_cel_16 ")
            {
                tipdannih[0] = ("ushort ");
            }
            if (razdel[znachenie] == "obyavl #cel_64 ")
            {
                if (name2 == "C#")
                    tipdannih[0] = ("long ");
                else if (name2 == "C/C++")
                    tipdannih[0] = ("long long ");
            }
            if (razdel[znachenie] == "obyavl #u_cel_32 ")
            {
                tipdannih[0] = ("uint ");
            }
            if (razdel[znachenie] == "obyavl #u_cel_64 ")
            {
                if (name2 == "C#")
                    tipdannih[0] = ("ulong ");
                else if (name2 == "C/C++")
                    tipdannih[0] = "unsigned long ";
            }

            if (razdel[znachenie] == "obyavl #simv_16 ")
            {
                tipdannih[0] = ("char ");
            }
            if (razdel[znachenie] == "obyavl #drob_64 ")
            {
                tipdannih[0] = ("double ");
            }
            if (razdel[znachenie] == "obyavl #drob_32 ")
            {
                tipdannih[0] = ("float ");
            }
            if (razdel[znachenie] == "obyavl #string ")
            {
                tipdannih[0] = ("string ");
            }
            if (razdel[znachenie] == "obyavl #logic ")
            {
                tipdannih[0] = ("bool ");
            }
            if (razdel[1] == "#arr")
                tipdannih[1] = razdel[3];
            else
            {
                tipdannih[1] = razdel[3];
                tipdannih[2] = razdel[5];
            }
            return tipdannih;
        }
        static string Vvod(List<string> razdeltel, List<string> vtoroyyazyk, List<string> vtoroy_yazuk, int i, bool ln)
        {
            string line = "@vstr_funk vvod";
            if (ln)
                line += "_line";
            line += " ";
            int index1 = razdeltel[i].IndexOf('(');
            string name = "";
            string name2 = "";
            int count = 0;
            for (int j = index1 + 1, prov = 0; razdeltel[i][j - 1] != ')' && prov == 0; j++)
            {
                if (razdeltel[i][j] == ' ' && string.IsNullOrEmpty(name)) continue;
                else if (razdeltel[i][j] == '[')
                {
                    while (razdeltel[i][j] != ']')
                    {

                        name2 += razdeltel[i][j];
                        j++;
                    }
                    name2 += "]";
                }
                else if (razdeltel[i][j] != ',' && razdeltel[i][j] != ')')
                    name += razdeltel[i][j];
                else
                {
                    for (int v = 0; v < vtoroy_yazuk.Count && name != ""; v += 2)
                    {
                        string[] arr = vtoroy_yazuk[v + 1].Split(' ');
                        for (int v1 = 0; v1 < arr.Length; v1++)
                        {
                            if (name == arr[v1])
                            {
                                if (vtoroy_yazuk[v].Substring(vtoroy_yazuk[v].IndexOf(' ') + 1) == "#string")
                                {
                                    count++;
                                    name += name2;
                                    line += $"#string {name}";
                                    prov = 1;
                                }
                                else
                                {
                                    count += 1;
                                    name += name2;
                                    line += $"{vtoroy_yazuk[v].Substring(vtoroy_yazuk[v].IndexOf(' ') + 1)}{name} ";
                                }
                                name = "";
                                break;
                            }
                        }
                    }

                }

            }
            //if (count > 1)
            //            line += $"@razd ' '";
            return line;
        }
        static void translateTypeData(ref string[] vvodsharp, int counter)
        {
            int ind = 1;
            int counter2 = 0;
            while (counter2 != counter)
            {
                ind += 2;
                if (vvodsharp[ind - 1] == "#cel_32")
                {
                    vvodsharp[ind - 1] = "int";
                }
                else if (vvodsharp[ind - 1] == "#drob_48")
                {
                    vvodsharp[ind - 1] = ("double");
                }
                else if (vvodsharp[ind - 1] == "@obyavl #u_cel_8")
                {
                    vvodsharp[ind - 1] = ("byte");
                }
                else if (vvodsharp[ind - 1] == "#cel_8")
                {
                    vvodsharp[ind - 1] = ("sbyte");
                }
                else if (vvodsharp[ind - 1] == "#cel_16")
                {
                    vvodsharp[ind - 1] = ("short");
                }
                else if (vvodsharp[ind - 1] == "@#u_cel_16")
                {
                    vvodsharp[ind - 1] = ("ushort");
                }
                else if (vvodsharp[ind - 1] == "@obyavl #cel_64")
                {
                    vvodsharp[ind - 1] = ("long");
                }
                else if (vvodsharp[ind - 1] == "#u_cel_32")
                {
                    vvodsharp[ind - 1] = ("uint");
                }
                else if (vvodsharp[ind - 1] == "#u_cel_64")
                {
                    vvodsharp[ind - 1] = ("ulong");
                }

                else if (vvodsharp[ind - 1] == "#simv_16")
                {
                    vvodsharp[ind - 1] = ("char");
                }
                else if (vvodsharp[ind - 1] == "#drob_64")
                {
                    vvodsharp[ind - 1] = ("double");
                }
                else if (vvodsharp[ind - 1] == "#drob_32")
                {
                    vvodsharp[ind - 1] = ("float");
                }
                else if (vvodsharp[ind - 1] == "#logic")
                {
                    vvodsharp[ind - 1] = ("bool");
                }
                else if (vvodsharp[ind - 1] == "#string")
                {
                    vvodsharp[ind - 1] = ("string");
                }
                else if (vvodsharp[ind - 1] == "#neyavn")
                {
                    vvodsharp[ind - 1] = ("var");
                }
                counter2++;
            }
        }
        static void mathematicalFunctions(ref List<string> vtoroyyazyk, ref int j, int i, List<string> razdeltel, ref string simvol)
        {
            if (j < razdeltel[i].Length - 3 && (j == 0 || !char.IsLetterOrDigit(razdeltel[i][j - 1])) && razdeltel[i].Substring(j, 2) == "ln" && (razdeltel[i][j + 2] == ' ' || razdeltel[i][j + 2] == '('))
            {
                j += 2;
                if (simvol != "")
                    vtoroyyazyk.Add(simvol);
                simvol = "";
                vtoroyyazyk.Add("@vstrfunk_ln");
            }
            else if (j < razdeltel[i].Length - 4 && (j == 0 || !char.IsLetterOrDigit(razdeltel[i][j - 1])) && razdeltel[i].Substring(j, 3) == "abs" && (razdeltel[i][j + 3] == ' ' || razdeltel[i][j + 3] == '('))
            {
                j += 3;
                if (simvol != "")
                    vtoroyyazyk.Add(simvol);
                simvol = "";
                vtoroyyazyk.Add("@vstrfunk_modul");
            }
            else if (j < razdeltel[i].Length - 4 && (j == 0 || !char.IsLetterOrDigit(razdeltel[i][j - 1])) && razdeltel[i].Substring(j, 3) == "sin" && (razdeltel[i][j + 3] == ' ' || razdeltel[i][j + 3] == '('))
            {
                j += 3;
                if (simvol != "")
                    vtoroyyazyk.Add(simvol);
                simvol = "";
                vtoroyyazyk.Add("@vstrfunk_sin");
            }
            else if (j < razdeltel[i].Length - 4 && (j == 0 || !char.IsLetterOrDigit(razdeltel[i][j - 1])) && razdeltel[i].Substring(j, 3) == "cos" && (razdeltel[i][j + 3] == ' ' || razdeltel[i][j + 3] == '('))
            {
                j += 3;
                if (simvol != "")
                    vtoroyyazyk.Add(simvol);
                simvol = "";
                vtoroyyazyk.Add("@vstrfunk_cos");
            }
            else if (j < razdeltel[i].Length - 4 && (j == 0 || !char.IsLetterOrDigit(razdeltel[i][j - 1])) && razdeltel[i].Substring(j, 3) == "int" && (razdeltel[i][j + 3] == ' ' || razdeltel[i][j + 3] == '('))
            {
                j += 3;
                if (simvol != "")
                    vtoroyyazyk.Add(simvol);
                simvol = "";
                vtoroyyazyk.Add("@vstrfunk_celchast");
            }
            else if (j < razdeltel[i].Length - 4 && (j == 0 || !char.IsLetterOrDigit(razdeltel[i][j - 1])) && razdeltel[i].Substring(j, 3) == "inc" && (razdeltel[i][j + 3] == ' ' || razdeltel[i][j + 3] == '('))
            {
                j += 3;
                if (simvol != "")
                    vtoroyyazyk.Add(simvol);
                simvol = "";
                vtoroyyazyk.Add("@vstrfunk_plusodin");
            }
            else if (j < razdeltel[i].Length - 4 && (j == 0 || !char.IsLetterOrDigit(razdeltel[i][j - 1])) && razdeltel[i].Substring(j, 3) == "sqr" && (razdeltel[i][j + 3] == ' ' || razdeltel[i][j + 3] == '('))
            {
                j += 3;
                if (simvol != "")
                    vtoroyyazyk.Add(simvol);
                simvol = "";
                vtoroyyazyk.Add("@vstrfunk_kvadrat");
            }
            else if (j < razdeltel[i].Length - 5 && (j == 0 || !char.IsLetterOrDigit(razdeltel[i][j - 1])) && razdeltel[i].Substring(j, 4) == "frac" && (razdeltel[i][j + 4] == ' ' || razdeltel[i][j + 4] == '('))
            {
                j += 4;
                if (simvol != "")
                    vtoroyyazyk.Add(simvol);
                simvol = "";
                vtoroyyazyk.Add("@vstrfunk_drobchast");
            }
            else if (j < razdeltel[i].Length - 5 && (j == 0 || !char.IsLetterOrDigit(razdeltel[i][j - 1])) && razdeltel[i].Substring(j, 4) == "sqrt" && (razdeltel[i][j + 4] == ' ' || razdeltel[i][j + 4] == '('))
            {
                j += 4;
                if (simvol != "")
                    vtoroyyazyk.Add(simvol);
                vtoroyyazyk.Add("@vstrfunk_kor");
                simvol = "";
            }
            else if (j < razdeltel[i].Length - 6 && (j == 0 || !char.IsLetterOrDigit(razdeltel[i][j - 1])) && razdeltel[i].Substring(j, 5) == "power" && (razdeltel[i][j + 5] == ' ' || razdeltel[i][j + 5] == '('))
            {
                j += 5;
                if (simvol != "")
                    vtoroyyazyk.Add(simvol);
                simvol = "";
                vtoroyyazyk.Add("@vstrfunk_stepen");
            }
        }
    }
}
