using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.IO;

namespace Translate_program
{
    public partial class MainPage : ContentPage
    {

        string name1 = "";
        string name2 = "";
        public static string name3 = "";
        public static List<string> translator = new List<string>();
        public static List<string> translator2 = new List<string>();
        public MainPage()
        {
            Application.Current.UserAppTheme = OSAppTheme.Light;
            InitializeComponent();


            //Получаем размер дисплея нашего андроида
            var display = DeviceDisplay.MainDisplayInfo;
            double height = display.Height;
            double width = display.Width;
            this.Editor2.HeightRequest = height / 100 * 13;


        }

        private void ClearBox(object sender, EventArgs e)
        {
            translator.Clear();
            Editor2.Text = null;
        }
        static bool PascalObrabotkaDo(ref int j, string stroka,ref string stroka3, ref List<string> razdeltel)
        {
            bool isHere = false;
            if (InputProverkaLeft(j, stroka) && InputProverkaRight(j, 2, stroka))
            {
                if (stroka[j] == 'd' && stroka[j + 1] == 'o')
                {
                    j++;
                    stroka3 += "@konec";
                    razdeltel.Add(stroka3);
                    stroka3 = "";
                    isHere = true;
                }
            }
            else if (j!=0&&(stroka[j - 1] == 'd' && stroka[j] == 'o' && InputProverkaRight(j, 1, stroka)))
            {
                stroka3 += "@closeblock";
                razdeltel.Add(stroka3);
                stroka3 = "";
                isHere = true;
            }
            return isHere;
        }
        static bool PascalObrabotkaEnd(ref int j, string stroka, ref string stroka3, ref List<string> razdeltel)
        {
            bool isHere = false;
            if (InputProverkaLeft(j, stroka) && InputProverkaRight(j, 3, stroka))
            {
                if (stroka[j] == 'e' && stroka[j + 1] == 'n' && stroka[j + 2] == 'd')
                {
                    j += 2;
                    razdeltel.Add(stroka3);
                    stroka3 = "@closeblock";
                    razdeltel.Add(stroka3);
                    stroka3 = "";
                    isHere = true;
                }
            }
            else if (j!=0&&(stroka[j - 1] == 'e' && stroka[j] == 'n' && stroka[j + 1] == 'd' && InputProverkaRight(j, 2, stroka)))
            {
                j++;
                stroka3 = "@closeblock";
                razdeltel.Add(stroka3);
                stroka3 = "";
                isHere = true;
            }
            return isHere;
        }
        static bool PascalObrabotkaThen(ref int j, string stroka, ref string stroka3, ref List<string> razdeltel)
        {
            bool isHere = false;
            if (InputProverkaLeft(j, stroka) && InputProverkaRight(j, 4, stroka))
            {
                if (stroka[j] == 't' && stroka[j + 1] == 'h' && stroka[j + 2] == 'e' && stroka[j + 3] == 'n')
                {
                    j += 3;
                    stroka3 += "@konec";
                    razdeltel.Add(stroka3);
                    stroka3 = "";
                    isHere = true;
                }
                if (stroka[j] == 'e' && stroka[j + 1] == 'n' && stroka[j + 2] == 'd' && stroka[j + 3] == ';')
                {
                    j += 3;
                    stroka3 += "@closeblock";
                    razdeltel.Add(stroka3);
                    stroka3 = "";
                    isHere = true;
                }
            }
            return isHere;
        }
        static bool PascalObrabotkaBegin(ref int j, string stroka, ref string stroka3, ref List<string> razdeltel)
        {
            if (InputProverkaLeft(j, stroka) && InputProverkaRight(j, 5, stroka))
            {
                if (stroka[j] == 'b' && stroka[j + 1] == 'e' && stroka[j + 2] == 'g' && stroka[j + 3] == 'i' && stroka[j + 4] == 'n')
                {
                    j += 4;
                    if (stroka3 != " " && stroka3 != "")
                        razdeltel.Add(stroka3);
                    stroka3 = "@openblock";
                    razdeltel.Add(stroka3);
                    stroka3 = "";
                    return true;
                }
            }
            else if (j!=0&&(stroka[j - 1] == 'b' && stroka[j] == 'e' && stroka[j + 1] == 'g' && stroka[j + 2] == 'i' && stroka[j + 3] == 'n' && InputProverkaRight(j, 4, stroka)))
            {
                j += 3;
                if (stroka3 != " " && stroka3 != "")
                    stroka3 = "@openblock";
                razdeltel.Add(stroka3);
                stroka3 = "";
                return true;
            }
            return false;
        }
        static bool PascalObrabotkaRepeat(ref int j, string stroka, ref string stroka3, ref List<string> razdeltel)
        {
            if (InputProverkaLeft(j, stroka) && InputProverkaRight(j, 6, stroka))
            {
                if (stroka[j] == 'r' && stroka[j + 1] == 'e' && stroka[j + 2] == 'p' && stroka[j + 3] == 'e' && stroka[j + 4] == 'a' && stroka[j + 5] == 't')
                {
                    j += 5;
                    if (stroka3 != " " && stroka3 != "")
                        razdeltel.Add(stroka3);
                    stroka3 = "@do";
                    razdeltel.Add(stroka3);
                    stroka3 = "";
                    return true;
                }
            }
            else if (j!=0&&(stroka[j - 1] == 'r' && stroka[j] == 'e' && stroka[j + 1] == 'p' && stroka[j + 2] == 'e' && stroka[j + 3] == 'a' && stroka[j + 4] == 't' && InputProverkaRight(j, 5, stroka)))
            {
                j += 4;
                if (stroka3 != " " && stroka3 != "")
                    stroka3 = "@do";
                razdeltel.Add(stroka3);
                stroka3 = "";
                return true;
            }
            return false;
        }

        private void Translate(object sender, EventArgs e)
        {

            string checkError = Check(name1, name2, translator);
            if (checkError == "Language=")
            {
                DisplayAlert("                 Ошибка!", "Выбран одинаковый язык.", "ОK");
                return;
            }
            else if (checkError == "missing text")
            {
                DisplayAlert("                 Ошибка!", "Не введен код", "ОK");
                return;
            }
            else if (checkError == "choiceLanguage")
            {
                DisplayAlert("                 Ошибка!", "Вы не выбрали нужные языки.", "ОK");
                return;
            }
            List<string> vtoroy_yazuk = new List<string>();
            if (name1 == "Pascal")
            {
                List<string>[] peremen = new List<string>[14];
                int until = 0;
                List<string> razdeltel2 = new List<string>();
                PascalCodeToNice(razdeltel2);
                List<string> razdeltel = new List<string>();
                PascalObrabotkaConstructionElements(razdeltel2, ref razdeltel);
                PascalObrabotkaVar(razdeltel, ref vtoroy_yazuk);
                PascalObrabotkaSkobki(ref razdeltel);
                PascalObrabotkaOther(razdeltel,ref vtoroy_yazuk, until);
            }
            if (name2 == "C#")
            {
                CSharpTranslate(vtoroy_yazuk);
            }
            else translator2.Add("Данные языки находятся в разработке, просим прощения");
            Navigation.PushAsync(new PageTwo());
        }
        static void IncludeLibrary(ref List<string> translator2)
        {
                string[] c = { "using System;", "using System.Collections.Generic;", "using System.Linq;", "using System.Text;", "using System.Threading.Tasks;",  "using System.IO;"
                        ,"namespace ConsoleApp1","{","class Program","{","static void Main(string[] args)","{" };
                for (int i = 0; i < c.Length; i++)
                    translator2.Add(c[i]);
        }
        string Check(string name1, string name2, List<string> translator)
        {
            if (name1 != name2 && name1 != "" && name2 != "" && translator.Count != 0)
                return "true";
            else
            {
                if (name1 == name2 && name1 != "" && name2 != "")
                {
                    return "Language=";
                }
                else if (translator.Count == 0)
                {
                    return "missing text";
                }
                else
                {
                    return "choiceLanguage";
                }
            }
        }
        private void FAQ(object sender, EventArgs e)
        {

            translator2.Add(@"Вас приветствует переводчик языка в бета - версии.
Расскажу как пользоваться. На данном этапе можно переводить
только с Pascal на С++ и C#. Остальные языки находятся в разработке.
Не следует использовать комментарии - это может привести к ошибке.
Данная программа пока лишь помогает облегчить вам перевод.В каких - то
случаях может произойти так, что не все сможет перевестить.
Очень сложно учесть абсолютно все особенности языка: (
При вставлении кода, где просят ввести текст также можно скролить.
Удержите палец на данном блоке и прокручивайте куда вам угодно.
   ");
            translator2.Add("Rules");
            Navigation.PushAsync(new PageTwo());
        }
        private void SelectorUp(object sender, EventArgs e)
        {
            name1 = Picker1.Items[Picker1.SelectedIndex];
        }
        private void SelectorDown(object sender, EventArgs e)
        {
            name2 = Picker2.Items[Picker2.SelectedIndex];
        }
        private void EditorCode(object sender, EventArgs e)
        {
            if (Editor2.Text != null)
                translator = Editor2.Text.Split('\n', '\r').ToList();
        }
        static bool InputProverkaLeft(int index, string stroka)
        {
            return index == 0 || stroka[index - 1] == ' ' || stroka[index - 1] == ')';
        }
        static bool InputProverkaRight(int index,int smechenie,string stroka)
        {
            return stroka[index + smechenie] == '(' || stroka[index + smechenie] == ' ';
        }
        static void PascalObrabotkaConstructionElements(List<string> razdeltel2, ref List<string> razdeltel)
        {
            string stroka3 = "";
            for (int i = 0; i < razdeltel2.Count; i++)
            {
                razdeltel2[i] = razdeltel2[i].ToLower().Trim();
                string stroka = razdeltel2[i];
                if (stroka3.Length > 0 && stroka3 != " ")
                    razdeltel.Add(stroka3);
                stroka3 = "";
                stroka += " ";
                stroka3 += stroka[0];
                for (int j = 0; j < stroka.Length; j++)
                {
                    if (j + 2 < stroka.Length)
                    {
                        if (PascalObrabotkaDo(ref j, stroka, ref stroka3, ref razdeltel))
                            continue;
                    }
                    if (j + 3 < stroka.Length)
                    {
                        if (PascalObrabotkaEnd(ref j, stroka, ref stroka3, ref razdeltel))
                            continue;
                    }
                    if (j + 4 < stroka.Length)
                    {
                        if (PascalObrabotkaThen(ref j, stroka, ref stroka3, ref razdeltel))
                            continue;
                    }
                    if (j + 5 < stroka.Length)
                    {
                        if (PascalObrabotkaBegin(ref j, stroka, ref stroka3, ref razdeltel))
                            continue;
                    }
                    if (j + 6 < stroka.Length)
                    {
                        if (PascalObrabotkaRepeat(ref j, stroka, ref stroka3, ref razdeltel))
                            continue;
                    }
                    if (stroka3 == "" && stroka[j] == ' ')
                        continue;
                    else
                        stroka3 += stroka[j];
                }
            }
        }
        static void PascalObrabotkaVar(List<string> razdeltel,ref List<string> vtoroy_yazuk)
        {
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
        }
        static void PascalObrabotkaSkobki(ref List<string> razdeltel)
        {
            for (int i = 0; i<razdeltel.Count; i++)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int je = 0; je<razdeltel[i].Length; je++)
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
                                        sb.Append("-1,");
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
                    razdeltel[i] = sb.ToString();
sb.Clear();
                }
        }
        static void PascalObrabotkaOther(List<string> razdeltel, ref List<string> vtoroy_yazuk, int until)
        {
            for (int i = 0; i < razdeltel.Count; i++)
            {
                if (razdeltel[i][0] == 'i' && razdeltel[i][1] == 'f' && (razdeltel[i][2] == ' ' || razdeltel[i][2] == '('))
                {
                    vtoroy_yazuk.Add("if");
                    string simvol = "";
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
                                    if (simvol != "")
                                        vtoroy_yazuk.Add(simvol);
                                    vtoroy_yazuk.Add("@iskluchauchiyili");
                                    simvol = "";
                                    j += 2;
                                    continue;
                                }
                                if (razdeltel[i][j] == 'o' && razdeltel[i][j + 1] == 'r')
                                {
                                    if (simvol != "")
                                        vtoroy_yazuk.Add(simvol);
                                    vtoroy_yazuk.Add("@ili");
                                    simvol = "";
                                    j += 1;
                                    continue;
                                }
                                if (razdeltel[i][j] == 'n' && razdeltel[i][j + 1] == 'o' && razdeltel[i][j + 2] == 't')
                                {
                                    if (simvol != "")
                                        vtoroy_yazuk.Add(simvol);
                                    vtoroy_yazuk.Add("@ne");
                                    simvol = "";
                                    j += 2;
                                    continue;
                                }
                                if (razdeltel[i][j] == 'd' && razdeltel[i][j + 1] == 'i' && razdeltel[i][j + 2] == 'v')
                                {
                                    if (simvol != "")
                                        vtoroy_yazuk.Add(simvol);
                                    vtoroy_yazuk.Add("@delenie");
                                    simvol = "";
                                    j += 2;
                                    continue;
                                }
                                if (razdeltel[i][j] == 'm' && razdeltel[i][j + 1] == 'o' && razdeltel[i][j + 2] == 'd')
                                {
                                    if (simvol != "")
                                        vtoroy_yazuk.Add(simvol);
                                    vtoroy_yazuk.Add("@deleniesostatkom");
                                    simvol = "";
                                    j += 2;
                                    continue;
                                }
                                if (razdeltel[i][j] == 'a' && razdeltel[i][j + 1] == 'n' && razdeltel[i][j + 2] == 'd')
                                {
                                    if (simvol != "")
                                        vtoroy_yazuk.Add(simvol);
                                    vtoroy_yazuk.Add("@i");
                                    simvol = "";
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
                            if (simvol != "")
                                vtoroy_yazuk.Add(simvol);
                            vtoroy_yazuk.Add("@sravnenie");
                            simvol = "";
                            continue;
                        }
                        InputMathematicalFunctions(ref vtoroy_yazuk, ref j, i, razdeltel, ref simvol);

                        //__________________________________________________________________________________________________
                        if (razdeltel[i][j] == ':' && razdeltel[i][j + 1] == '=')
                        {
                            j++;
                            if (simvol != "")
                                vtoroy_yazuk.Add(simvol);
                            vtoroy_yazuk.Add("@prisvaivanie");
                            simvol = "";
                            continue;
                        }
                        if (razdeltel[i][j] == '<' && razdeltel[i][j + 1] == '>')
                        {
                            j++;
                            if (simvol != "")
                                vtoroy_yazuk.Add(simvol);
                            vtoroy_yazuk.Add("@neravno");
                            simvol = "";
                            continue;
                        }
                        if (razdeltel[i][j] == '@')
                        {
                            vtoroy_yazuk.Add(simvol);
                            simvol = "@";
                        }
                        else
                            simvol += razdeltel[i][j];
                    }
                    if (simvol != "")
                        vtoroy_yazuk.Add(simvol);
                }

                else
                {
                    string simvol = "";
                    for (int j = 0; j < razdeltel[i].Length; j++)
                    {
                        InputMathematicalFunctions(ref vtoroy_yazuk, ref j, i, razdeltel, ref simvol);
                        if (razdeltel[i][j] == ' ')
                        {
                            continue;
                        }
                        if (razdeltel[i] == "until")
                        {
                            vtoroy_yazuk.Add("@openblock");
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
                                    vtoroy_yazuk.Add(simvol);
                                vtoroy_yazuk.Add("@closeblock");
                                //vtoroy_yazuk.Add("while(!(");
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
                                        if (simvol != "")
                                            vtoroy_yazuk.Add(simvol);
                                        vtoroy_yazuk.Add("@iskluchauchiyili");
                                        simvol = "";
                                        j += 2;
                                        continue;
                                    }
                                    if (razdeltel[i][j] == 'n' && razdeltel[i][j + 1] == 'o' && razdeltel[i][j + 2] == 't')
                                    {
                                        if (simvol != "")
                                            vtoroy_yazuk.Add(simvol);
                                        vtoroy_yazuk.Add("@ne");
                                        simvol = "";
                                        j += 2;
                                        continue;
                                    }
                                    if (razdeltel[i][j] == 'd' && razdeltel[i][j + 1] == 'i' && razdeltel[i][j + 2] == 'v')
                                    {
                                        if (simvol != "")
                                            vtoroy_yazuk.Add(simvol);
                                        vtoroy_yazuk.Add("@delenie");
                                        simvol = "";
                                        j += 2;
                                        continue;
                                    }
                                    if (razdeltel[i][j] == '<' && razdeltel[i][j + 1] == '>')
                                    {
                                        j++;
                                        if (simvol != "")
                                            vtoroy_yazuk.Add(simvol);
                                        vtoroy_yazuk.Add("@neravno");
                                        simvol = "";
                                        continue;
                                    }
                                    if (razdeltel[i][j] == '@')
                                    {
                                        vtoroy_yazuk.Add(simvol);
                                        simvol = "@";
                                    }
                                    else
                                        simvol += razdeltel[i][j];
                                }
                                if (simvol != "")
                                    vtoroy_yazuk.Add(simvol);
                            }
                            else
                            {
                                simvol = "";
                                for (int je = 0; je < razdeltel[i].Length; je++)
                                {
                                    InputMathematicalFunctions(ref vtoroy_yazuk, ref je, i, razdeltel, ref simvol);
                                    if (razdeltel[i][je] == ' ')
                                    {
                                        continue;
                                    }
                                    if (razdeltel[i] == "until")
                                    {
                                        vtoroy_yazuk.Add("@openblock");
                                        razdeltel[i] = "while(!(";
                                        razdeltel[i + 1] = razdeltel[i] + razdeltel[i + 1];
                                        razdeltel.RemoveAt(i);
                                        until = 1;
                                        i--;
                                        break;
                                    }
                                    if (je + 5 < razdeltel[i].Length)
                                    {
                                        if (razdeltel[i][je] == 'u' && razdeltel[i][je + 1] == 'n' && razdeltel[i][je + 2] == 't' && razdeltel[i][je + 3] == 'i' && razdeltel[i][je + 4] == 'l' && (razdeltel[i][je + 5] == ' ' || razdeltel[i][je + 5] == '('))
                                        {
                                            if (simvol != "")
                                                vtoroy_yazuk.Add(simvol);
                                            vtoroy_yazuk.Add("@closeblock");
                                            vtoroy_yazuk.Add("while(!(");
                                            until = 1;
                                            simvol = "";
                                            je += 4;
                                            continue;
                                        }
                                    }
                                    if (je > 1)
                                    {
                                        if (je + 3 < razdeltel[i].Length)
                                        {
                                            if ((razdeltel[i][je - 1] == ' ' || razdeltel[i][je - 1] == ')') && (razdeltel[i][je + 3] == '(' || razdeltel[i][je + 3] == ' '))
                                            {
                                                if (razdeltel[i][je] == 'x' && razdeltel[i][je + 1] == 'o' && razdeltel[i][je + 2] == 'r')
                                                {
                                                    if (simvol != "")
                                                        vtoroy_yazuk.Add(simvol);
                                                    vtoroy_yazuk.Add("@iskluchauchiyili");
                                                    simvol = "";
                                                    je += 2;
                                                    continue;
                                                }
                                                if (razdeltel[i][je] == 'n' && razdeltel[i][je + 1] == 'o' && razdeltel[i][je + 2] == 't')
                                                {
                                                    if (simvol != "")
                                                        vtoroy_yazuk.Add(simvol);
                                                    vtoroy_yazuk.Add("ne");
                                                    simvol = "";
                                                    je += 2;
                                                    continue;
                                                }
                                                if (razdeltel[i][je] == 'd' && razdeltel[i][je + 1] == 'i' && razdeltel[i][je + 2] == 'v')
                                                {
                                                    if (simvol != "")
                                                        vtoroy_yazuk.Add(simvol);
                                                    vtoroy_yazuk.Add("@delenie");
                                                    simvol = "";
                                                    je += 2;
                                                    continue;
                                                }
                                                if (razdeltel[i][je] == 'm' && razdeltel[i][je + 1] == 'o' && razdeltel[i][je + 2] == 'd')
                                                {
                                                    if (simvol != "")
                                                        vtoroy_yazuk.Add(simvol);
                                                    vtoroy_yazuk.Add("@deleniesostatkom");
                                                    simvol = "";
                                                    je += 2;
                                                    continue;
                                                }
                                                if (razdeltel[i][je] == 'a' && razdeltel[i][je + 1] == 'n' && razdeltel[i][je + 2] == 'd')
                                                {
                                                    if (simvol != "")
                                                        vtoroy_yazuk.Add(simvol);
                                                    vtoroy_yazuk.Add("@i");
                                                    simvol = "";
                                                    je += 2;
                                                    continue;
                                                }
                                            }
                                        }
                                    }
                                    if (razdeltel[i][je] == '=')
                                    {
                                        if (simvol != "")
                                            vtoroy_yazuk.Add(simvol);
                                        vtoroy_yazuk.Add("@sravnenie");
                                        simvol = "";
                                        continue;
                                    }
                                    if (razdeltel[i][je] == ':' && razdeltel[i][je + 1] == '=')
                                    {
                                        je++;
                                        if (simvol != "")
                                            vtoroy_yazuk.Add(simvol);
                                        vtoroy_yazuk.Add("@prisvaivanie");
                                        simvol = "";
                                        continue;
                                    }
                                    if (razdeltel[i][je] == 'm' && razdeltel[i][je + 1] == 'o' && razdeltel[i][je + 2] == 'd')
                                    {
                                        if (simvol != "")
                                            vtoroy_yazuk.Add(simvol);
                                        vtoroy_yazuk.Add("@deleniesostatkom");
                                        simvol = "";
                                        je += 2;
                                        continue;
                                    }
                                    if (razdeltel[i][je] == 'a' && razdeltel[i][je + 1] == 'n' && razdeltel[i][je + 2] == 'd')
                                    {
                                        if (simvol != "")
                                            vtoroy_yazuk.Add(simvol);
                                        vtoroy_yazuk.Add("@i");
                                        simvol = "";
                                        je += 2;
                                        continue;
                                    }
                                }
                            }
                        }
                        if (razdeltel[i][j] == '=')
                        {
                            if (simvol != "")
                                vtoroy_yazuk.Add(simvol);
                            vtoroy_yazuk.Add("@sravnenie");
                            simvol = "";
                            continue;
                        }
                        if (razdeltel[i][j] == ':' && razdeltel[i][j + 1] == '=')
                        {
                            j++;
                            if (simvol != "")
                                vtoroy_yazuk.Add(simvol);
                            vtoroy_yazuk.Add("@prisvaivanie");
                            simvol = "";
                            continue;
                        }
                        if (razdeltel[i][j] == '<' && razdeltel[i][j + 1] == '>')
                        {
                            j++;
                            if (simvol != "")
                                vtoroy_yazuk.Add(simvol);
                            vtoroy_yazuk.Add("@neravno");
                            simvol = "";
                            continue;
                        }
                        if (j + 8 < razdeltel[i].Length)
                        {
                            if (razdeltel[i][j] == 'e' && razdeltel[i][j + 1] == 'l' && razdeltel[i][j + 2] == 's' && razdeltel[i][j + 3] == 'e' && razdeltel[i][j + 4] == ' ' && razdeltel[i][j + 5] == 'i' && razdeltel[i][j + 6] == 'f' && razdeltel[i][j + 7] == ' ')
                            {
                                j += 7;
                                string elseif = "else if ";
                                vtoroy_yazuk.Add(elseif);
                                continue;
                            }
                        }
                        if (j + 4 < razdeltel[i].Length)
                        {
                            if (razdeltel[i][j] == 'e' && razdeltel[i][j + 1] == 'l' && razdeltel[i][j + 2] == 's' && razdeltel[i][j + 3] == 'e' && razdeltel[i][j + 4] == ' ')
                            {
                                string else2 = "else";
                                j += 4;
                                vtoroy_yazuk.Add(else2);
                                continue;
                            }
                        }
                        //if (razdeltel[i][j] == '@')
                        //{
                        //    vtoroy_yazuk.Add(simvol);
                        //    simvol = "@";
                        //}
                        else if (razdeltel[i][j] == ';' && until == 1)
                        {
                            simvol += "));";
                        }
                        else simvol += razdeltel[i][j];
                    }
                    if (simvol != "")
                        vtoroy_yazuk.Add(simvol);
                }
            }
            for (int o = 0; o < vtoroy_yazuk.Count; o++)
            {
                if (vtoroy_yazuk[o] == "" || vtoroy_yazuk[o] == " ")
                    continue;
            }
        }
        static void PascalCodeToNice(List<string> razdeltel2)
        {
            string vvod = "";
            for (int i = 0; i < translator.Count; i++)
            {
                string stroka = translator[i];
                translate(ref stroka);
                for (int j = 0; j < stroka.Length; j++)
                {
                    if (stroka[j] == '{' || stroka[j] == '}')
                    {
                        obrabotkaFigure(ref vvod, ref stroka, ref razdeltel2, j);
                        continue;
                    }
                    vvod += stroka[j];
                    if (stroka[j] == ';')
                    {
                        lishnee(ref vvod, ref razdeltel2);
                    }

                }
                if (vvod != "")
                {
                    lishnee(ref vvod, ref razdeltel2);
                }
            }
        }
        static void CSharpTranslate(List<string> vtoroy_yazuk)
        {
            IncludeLibrary(ref translator2);
            translator2.Add("");
            for (int i = 0; i < vtoroy_yazuk.Count; i += 2)
            {
                if (vtoroy_yazuk[i] == "")
                    continue;
                if (vtoroy_yazuk[i].Length > 13 && vtoroy_yazuk[i].Substring(0, 13) == "@obyavl #arr ")
                {
                    //int[] a = new int[10], b = new int[10];
                    // int[] a, b, c;  a = new int[3]; b = new int[4]; c = new int[5];
                    translator2.Add(massiv(vtoroy_yazuk, i, "C#")[0] + "[]");
                    string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (translator22.Length > 1)
                    {
                        translator2[translator2.Count - 1] += string.Join(",", translator22) + "; ";
                        for (int j = 0; j < translator22.Length; j++)
                        {
                            translator2[translator2.Count - 1] += translator22[j] + " = new " + massiv(vtoroy_yazuk, i, "C#")[0] + "[" + massiv(vtoroy_yazuk, i, "C#")[1] + "]" + ";";
                        }
                    }
                    else
                    {
                        translator2[translator2.Count - 1] += translator22[0] + " = new " + massiv(vtoroy_yazuk, i, "C#")[0] + "[" + massiv(vtoroy_yazuk, i, "C#")[1] + "]";
                    }
                }
                else if (vtoroy_yazuk[i].Length > 16 && vtoroy_yazuk[i].Substring(0, 16) == "@obyavl #arr_2D ")
                {
                    //int[,] a = new int[10, 12];
                    // int[] a, b, c;  a = new int[3]; b = new int[4]; c = new int[5];
                    translator2.Add(massiv(vtoroy_yazuk, i, "C#")[0] + "[,]");
                    string[] translator22 = vtoroy_yazuk[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (translator22.Length > 1)
                    {
                        translator2[translator2.Count - 1] += string.Join(",", translator22) + "; ";
                        for (int j = 0; j < translator22.Length; j++)
                        {
                            translator2[translator2.Count - 1] += translator22[j] + " = new " + massiv(vtoroy_yazuk, i, "C#")[0] + "[" + massiv(vtoroy_yazuk, i, "C#")[1] + "," + massiv(vtoroy_yazuk, i, "C#")[2] + "]" + ";";
                        }
                    }
                    else
                    {
                        translator2[translator2.Count - 1] += translator22[0] + " = new " + massiv(vtoroy_yazuk, i, "C#")[0] + "[" + massiv(vtoroy_yazuk, i, "C#")[1] + "," + massiv(vtoroy_yazuk, i, "C#")[2] + "]";
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
                else if (vtoroy_yazuk[i].Length > 15 && vtoroy_yazuk[i].Substring(0, 15) == "@obyavl @const ")
                {
                    translator2.Add("var " + vtoroy_yazuk[i + 1] + " = "); string[] translator22 = vtoroy_yazuk[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    translator2[translator2.Count - 1] += (translator22[2] + ";");
                }
                else if (vtoroy_yazuk[i] == "if")
                    translator2[translator2.Count - 1] += vtoroy_yazuk[i] + "(";
                //if (simvol != "")
                else if (vtoroy_yazuk[i] == "@iskluchauchiyili")
                    translator2[translator2.Count - 1] += '^';
                else if (vtoroy_yazuk[i] == "@ne")
                    translator2[translator2.Count - 1] += '!';
                else if (vtoroy_yazuk[i] == "@delenie")
                    translator2[translator2.Count - 1] += '/';
                else if (vtoroy_yazuk[i] == "@deleniesostatkom")
                    translator2[translator2.Count - 1] += '%';
                else if (vtoroy_yazuk[i] == "@i")
                    translator2[translator2.Count - 1] += "&&";
                else if (vtoroy_yazuk[i] == "@sravnenie")
                    translator2[translator2.Count - 1] += "==";
                else if (vtoroy_yazuk[i] == "@prisvaivanie")
                    translator2[translator2.Count - 1] += '=';
                else if (vtoroy_yazuk[i] == "@neravno")
                    translator2[translator2.Count - 1] += "!=";
                else if (vtoroy_yazuk[i] == "@konec")
                    translator2[translator2.Count - 1] += ')';
                else if (vtoroy_yazuk[i] == "@ili")
                    translator2[translator2.Count - 1] += "||";
                else if (vtoroy_yazuk[i] == "@openblock")
                    translator2[translator2.Count - 1] += '{';
                else if (vtoroy_yazuk[i] == "@closeblock")
                    translator2[translator2.Count - 1] += '}';
                else if (vtoroy_yazuk[i] == "@do")
                    translator2[translator2.Count - 1] += "do{";
                else if (vtoroy_yazuk[i] == "else if ")
                    translator2[translator2.Count - 1] += "else if(";
                else if (vtoroy_yazuk[i][0] == '@')
                    OutputPow("C#", ref i, ref translator2, ref vtoroy_yazuk);
                else translator2[translator2.Count - 1] += vtoroy_yazuk[i];
                //razdeltel.RemoveAt(0);
            }
            //if (razdeltel.Count != 0)
            //razdeltel.RemoveAt(0);
            for (int i = 0; i < 3; i++)
                translator2.Add("}");
        }
        static void OutputPow(string name2, ref int o, ref List<string> translator2, ref List<string> vtoroy_yazuk)
        {
            switch (vtoroy_yazuk[o])
            {
                case "@vstrfunk_ln":
                    {
                        translator2[translator2.Count - 1] += name2 == "C#" ? "Math.Log" : "log";
                        break;
                    }
                case "@vstrfunk_modul":
                    {
                        translator2[translator2.Count - 1] += name2 == "C#" ? "Math.Abs" : "abs";
                        break;
                    }
                case "@vstrfunk_sin":
                    {
                        translator2[translator2.Count - 1] += name2 == "C#" ? "Math.Sin" : "sin";
                        break;
                    }
                case "@vstrfunk_cos":
                    {
                        translator2[translator2.Count - 1] += name2 == "C#" ? "Math.Cos" : "cos";
                        break;
                    }
                case "@vstrfunk_celchast":
                    {
                        translator2[translator2.Count - 1] += name2 == "C#" ? "Math.Truncate" : "(int)";
                        break;
                    }
                case "@vstrfunk_drobchast":
                    {
                        translator2[translator2.Count - 1] += vtoroy_yazuk[o + 1] + (name2 == "C#" ? "- Math.Truncate" : "(int)");
                        break;
                    }
                case "@vstrfunk_kor":
                    {
                        translator2[translator2.Count - 1] += name2 == "C#" ? "Math.Sqrt" : "sqrt";
                        break;
                    }
                case "@vstrfunk_stepen":
                    {
                        translator2[translator2.Count - 1] += name2 == "C#" ? "Math.Pow" : "pow";
                        break;
                    }
                case "@vstrfunk_plusodin":
                    {
                        translator2[translator2.Count - 1] += "1+" + vtoroy_yazuk[o + 1];
                        break;
                    }
                case "@vstrfunk_kvadrat":
                    {
                        string s1 = vtoroy_yazuk[o + 1];
                        string left = name2 == "C#" ? "Math.Pow(" : "pow(";
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
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

        }
        static void translate(ref string stroka)
        {
            stroka = stroka.Trim();
            StringBuilder str = new StringBuilder();
            if (stroka == "")
                return;
            str.Append(stroka[0]);
            for (int st = 1; st < stroka.Length; st++)
            {
                if (stroka[st] == ' ' && stroka[st - 1] == ' ')
                    continue;
                str.Append(stroka[st]);
            }
            stroka = str.ToString();
        }
        static void lishnee(ref string vvod, ref List<string> razdeltel2)
        {
            while (vvod[vvod.Length - 1] == ' ')
                vvod = vvod.Remove(vvod.Length - 1);
            razdeltel2.Add(vvod);
            vvod = "";
        }
        static void obrabotkaFigure(ref string vvod, ref string stroka, ref List<string> razdeltel2, int j)
        {
            if (vvod != "")
            {
                while (vvod[vvod.Length - 1] == ' ')
                {
                    vvod = vvod.Remove(vvod.Length - 1);
                    if (vvod == "")
                        break;
                }
                razdeltel2.Add(vvod);
                if (stroka[j] == '{')
                    vvod = "//";
                else vvod = "";
            }
            else
            {
                vvod += stroka[j];
                while (vvod[vvod.Length - 1] == ' ')
                    vvod = vvod.Remove(vvod.Length - 1);
                vvod = "//";
            }
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
                else if (Operator(razdeltel[i][j].ToString()))
                {
                    if (pechat[pechat.Length - 1] == '}')
                    {
                        pechat = pechat.TrimEnd('}');
                    }
                    else pechat += "{";
                    pechat += razdeltel[i][j];
                }
                else
                {
                    if (!Operator(pechat[pechat.Length - 1].ToString()))
                        pechat += '{';
                    while (j != razdeltel[i].Length && razdeltel[i][j] != ')' && razdeltel[i][j] != ' ' && razdeltel[i][j] != ',')
                    {
                        pechat += razdeltel[i][j];
                        j++;
                    }
                    if (!Operator(pechat[pechat.Length - 1].ToString()))
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
        static bool Operator(string znak)
        {
            string[] operaciya = { "+", "-", "*", "/" };
            foreach (string sr in operaciya)
            {
                if (sr == znak)
                    return true;
            }
            return false;
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
            int znachenie;
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
                tipdannih[0] = ("long ");
            }
            if (razdel[znachenie] == "obyavl #u_cel_32 ")
            {
                tipdannih[0] = ("uint ");
            }
            if (razdel[znachenie] == "obyavl #u_cel_64 ")
            {
                    tipdannih[0] = ("ulong ");
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
        static void TranslateTypeData(ref string[] vvodsharp, int counter)
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
        static void InputMathematicalFunctions(ref List<string> vtoroy_yazuk, ref int j, int i, List<string> razdeltel, ref string simvol)
        {
            if (j < razdeltel[i].Length - 3 && (j == 0 || !char.IsLetterOrDigit(razdeltel[i][j - 1])) && razdeltel[i].Substring(j, 2) == "ln" && (razdeltel[i][j + 2] == ' ' || razdeltel[i][j + 2] == '('))
            {
                j += 2;
                if (simvol != "")
                    vtoroy_yazuk.Add(simvol);
                simvol = "";
                vtoroy_yazuk.Add("@vstrfunk_ln");
            }
            else if (j < razdeltel[i].Length - 4 && (j == 0 || !char.IsLetterOrDigit(razdeltel[i][j - 1])) && razdeltel[i].Substring(j, 3) == "abs" && (razdeltel[i][j + 3] == ' ' || razdeltel[i][j + 3] == '('))
            {
                j += 3;
                if (simvol != "")
                    vtoroy_yazuk.Add(simvol);
                simvol = "";
                vtoroy_yazuk.Add("@vstrfunk_modul");
            }
            else if (j < razdeltel[i].Length - 4 && (j == 0 || !char.IsLetterOrDigit(razdeltel[i][j - 1])) && razdeltel[i].Substring(j, 3) == "sin" && (razdeltel[i][j + 3] == ' ' || razdeltel[i][j + 3] == '('))
            {
                j += 3;
                if (simvol != "")
                    vtoroy_yazuk.Add(simvol);
                simvol = "";
                vtoroy_yazuk.Add("@vstrfunk_sin");
            }
            else if (j < razdeltel[i].Length - 4 && (j == 0 || !char.IsLetterOrDigit(razdeltel[i][j - 1])) && razdeltel[i].Substring(j, 3) == "cos" && (razdeltel[i][j + 3] == ' ' || razdeltel[i][j + 3] == '('))
            {
                j += 3;
                if (simvol != "")
                    vtoroy_yazuk.Add(simvol);
                simvol = "";
                vtoroy_yazuk.Add("@vstrfunk_cos");
            }
            else if (j < razdeltel[i].Length - 4 && (j == 0 || !char.IsLetterOrDigit(razdeltel[i][j - 1])) && razdeltel[i].Substring(j, 3) == "int" && (razdeltel[i][j + 3] == ' ' || razdeltel[i][j + 3] == '('))
            {
                j += 3;
                if (simvol != "")
                    vtoroy_yazuk.Add(simvol);
                simvol = "";
                vtoroy_yazuk.Add("@vstrfunk_celchast");
            }
            else if (j < razdeltel[i].Length - 4 && (j == 0 || !char.IsLetterOrDigit(razdeltel[i][j - 1])) && razdeltel[i].Substring(j, 3) == "inc" && (razdeltel[i][j + 3] == ' ' || razdeltel[i][j + 3] == '('))
            {
                j += 3;
                if (simvol != "")
                    vtoroy_yazuk.Add(simvol);
                simvol = "";
                vtoroy_yazuk.Add("@vstrfunk_plusodin");
            }
            else if (j < razdeltel[i].Length - 4 && (j == 0 || !char.IsLetterOrDigit(razdeltel[i][j - 1])) && razdeltel[i].Substring(j, 3) == "sqr" && (razdeltel[i][j + 3] == ' ' || razdeltel[i][j + 3] == '('))
            {
                j += 3;
                if (simvol != "")
                    vtoroy_yazuk.Add(simvol);
                simvol = "";
                vtoroy_yazuk.Add("@vstrfunk_kvadrat");
            }
            else if (j < razdeltel[i].Length - 5 && (j == 0 || !char.IsLetterOrDigit(razdeltel[i][j - 1])) && razdeltel[i].Substring(j, 4) == "frac" && (razdeltel[i][j + 4] == ' ' || razdeltel[i][j + 4] == '('))
            {
                j += 4;
                if (simvol != "")
                    vtoroy_yazuk.Add(simvol);
                simvol = "";
                vtoroy_yazuk.Add("@vstrfunk_drobchast");
            }
            else if (j < razdeltel[i].Length - 5 && (j == 0 || !char.IsLetterOrDigit(razdeltel[i][j - 1])) && razdeltel[i].Substring(j, 4) == "sqrt" && (razdeltel[i][j + 4] == ' ' || razdeltel[i][j + 4] == '('))
            {
                j += 4;
                if (simvol != "")
                    vtoroy_yazuk.Add(simvol);
                vtoroy_yazuk.Add("@vstrfunk_kor");
                simvol = "";
            }
            else if (j < razdeltel[i].Length - 6 && (j == 0 || !char.IsLetterOrDigit(razdeltel[i][j - 1])) && razdeltel[i].Substring(j, 5) == "power" && (razdeltel[i][j + 5] == ' ' || razdeltel[i][j + 5] == '('))
            {
                j += 5;
                if (simvol != "")
                    vtoroy_yazuk.Add(simvol);
                simvol = "";
                vtoroy_yazuk.Add("@vstrfunk_stepen");
            }
        }
    }
}