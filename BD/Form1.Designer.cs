using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Xml.Linq;
using System.Text;
using System.Windows.Forms;


namespace WindowsFormsApplication1
{
    public class FileSaveRead//класс записи
    {
        
        string Lname = string.Empty;
        string Lfam = string.Empty;
        string Lotch = string.Empty;
        string Lser = string.Empty;
        string Lnum = string.Empty;
        string Lkod = string.Empty;
        string Lreys = string.Empty;
        string Lcomp = string.Empty;
        string Lclas = string.Empty;
        string Lmesto = string.Empty;
        string LcityO = string.Empty;
        string LcityP = string.Empty;
        string LtimeO = string.Empty;
        string LtimeP = string.Empty;
        string Lcost = string.Empty;
        ArrayList al = new ArrayList();
        StreamReader sr = null;
        string path = @"C:\DB\DataBase.txt";
        public void slash(object arg)//раделение строки в массив
        {
            string s = arg.ToString();
            string[] Splited = s.Split('~');
            Lname = Splited[0];
            Lfam = Splited[1];
            Lotch = Splited[2];
            Lser = Splited[3];
            Lnum = Splited[4];
            Lkod = Splited[5];
            Lreys = Splited[6];
            Lcomp = Splited[7];
            Lclas = Splited[8];
            Lmesto = Splited[9];
            LcityO = Splited[10];
            LtimeO = Splited[11];
            LcityP = Splited[12];
            LtimeP = Splited[13];
            Lcost = Splited[14];

        }
        public ArrayList slash(ArrayList arg)//раделение строки в массив
        {
            string text = string.Empty;
            ArrayList vaw = new ArrayList();
            for (int i = 0; i < arg.Count; i++)
            {
                text = (String)arg[i];
                string[] Splited = text.Split('~');
                text = Splited[0] + " " + Splited[1] + " " + Splited[2] + " " + Splited[5];
                vaw.Add(text);

            }
            return (vaw);
        }
        public void Bxml()//кнопка xml
        {
            XDocument basa = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Reservation", ""
                )
            );
            for (int i = 0; i < al.Count; i++)
            {
                slash(al[i]);
                basa.Element("Reservation").Add(
                    new XElement("ticket",
                        new XElement("Person",
                            new XElement("name", Lname),
                            new XElement("fam", Lfam),
                            new XElement("otch", Lotch),
                            new XElement("seriya", Lser),
                            new XElement("number", Lnum)
                        ),
                        new XElement("CruiseData",
                            new XElement("kod", Lkod),
                            new XElement("company", Lcomp),
                            new XElement("class", Lclas),
                            new XElement("mesto", Lmesto),
                            new XElement("cityStart", LcityO),
                            new XElement("timeStart", LtimeO),
                            new XElement("cityFinish", LcityP),
                            new XElement("timeFinish", LtimeP),
                            new XElement("cost", Lcost)
                        )
                    )
                );
            }

            basa.Save(@"C:\DB\BxmlD.xml");
        }
        public string BLoad()//состояния файла
        {
            reader();
            string stat = string.Empty;
            if (File.Exists(path) == true)
            {
                stat = "Файл обноружен кол-во записей  " + al.Count;
            }
            else
            {
                File.Create(path);
                stat = "Файл создан";
            }
            return stat;

        }
        public ArrayList alAcc//выгрузка массива
        {
            get
            {
                return al;
            }
            set
            {
               al = value;
            }
        }
        public FileSaveRead(string filepath) => path = filepath;
        public FileSaveRead()
        {}
        public ArrayList Search(ArrayList al, string con)
        {
            ArrayList LocalList = new ArrayList();
            string text = string.Empty;

            for (int i = 0; i < al.Count; i++)
            {
                text = (String)al[i];
                if (text.Contains(con) == true)
                {
                    LocalList.Add(al[i]);
                }
            }
            return LocalList;
        }
        public ArrayList reader()
        {
            string str = string.Empty;

            try
            {
                sr = new StreamReader(path);

                while ((str = sr.ReadLine()) != null)
                {
                    al.Add(str);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (sr != null)
                {

                    sr.Close();
                }
            }
            return al;

        }
        public void writer()
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(path);

                for (int i = 0; i < al.Count; i++)
                {
                    sw.WriteLine(al[i]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }
    }
    public class functional//класс функций
    {
        public string Badd(string text, ArrayList al)//добавить запись
        {
            int count = 0;
            string[] aLocal = text.Split('~');
            for (int i = 0; i < aLocal.Length; i++)
            {
                if (aLocal[i] != "") 
                { 
                    count++;
                }
            }
            if (count == 15)
            {
                al.Add(text);
                return "Запись успешно добавлена!";
            }
            else 
            {
                return "Заполните все поля!";
            }
        }
        public void Bdel(ArrayList al, string text, string fam)//кнопка удаления
        {
            int i = 0;
            int u = al.Count;
            do
            {
                if (u == i)
                {
                    break;
                }
                if (al[i].ToString().Contains(fam) == true)
                {
                    al.RemoveAt(i);
                    i = 0;
                    u = u - 1;
                }

                if (u == i)
                {

                    break;
                }

                if (al[i].ToString().Contains(fam) == false)
                {
                    i++;
                }

            } while (i < u);
            
        }
        public String[] Bpoisk(string nambber, ArrayList al)//кнопка поиск
        {
            int jac = 0;
            string[] err = {"", "", "", "","", "", "", "", "", "", "", "", "", "","" };
            jac = int.Parse(nambber);
            if (jac <= al.Count-1 && jac >= 0)
            {
                string data = (String)al[jac];
                string[] ff = data.Split('~');
                return ff;
            }
            else 
            {
                return err;
            }
        }
        public string[] firstRecord(ArrayList al)
        {
            if (al.Count > 0)
            {
                string data = al[0].ToString();
                string[] ui = data.Split('~');
                return ui;
            }
            else 
            {
                string[] nul = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
                return nul;
            }
        }
        public void Bchange(ArrayList al, string text, string fam)//кнопка изменить
        {
            int i = 0;
            int u = al.Count; 
            do
            {
                if (u == i)
                {

                    break;
                }
                if (al[i].ToString().Contains(fam) == true)
                {
                    al[i] = text;
                    i++;

                }
                if (u == i)
                {

                    break;
                }
                if (al[i].ToString().Contains(fam) == false)
                {
                    i++;
                }
            } while (i < u);
        }
    }
    partial class Form1
    {

        functional Funbuttons = new functional();
        FileSaveRead FunFile = new FileSaveRead();
        #region hlam
        private System.ComponentModel.IContainer components = null;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.name = new System.Windows.Forms.TextBox();
            this.fam = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.otch = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ser = new System.Windows.Forms.TextBox();
            this.button = new System.Windows.Forms.Button();
            this.change = new System.Windows.Forms.Button();
            this.del = new System.Windows.Forms.Button();
            this.nambber = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.poisk = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusLogs = new System.Windows.Forms.ToolStripStatusLabel();
            this.bxml = new System.Windows.Forms.Button();
            this.list = new System.Windows.Forms.ListBox();
            this.SERCH = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.num = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.kod = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.reys = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.comp = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cityO = new System.Windows.Forms.TextBox();
            this.timeO = new System.Windows.Forms.TextBox();
            this.cityP = new System.Windows.Forms.TextBox();
            this.timeP = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.clas = new System.Windows.Forms.TextBox();
            this.mesto = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.cost = new System.Windows.Forms.TextBox();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(15, 25);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(349, 20);
            this.name.TabIndex = 0;
            this.name.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.name_KeyPress);
            // 
            // fam
            // 
            this.fam.Location = new System.Drawing.Point(15, 64);
            this.fam.Name = "fam";
            this.fam.Size = new System.Drawing.Size(349, 20);
            this.fam.TabIndex = 1;
            this.fam.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.fam_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Имя";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Фамилия";
            // 
            // otch
            // 
            this.otch.Location = new System.Drawing.Point(15, 103);
            this.otch.Name = "otch";
            this.otch.Size = new System.Drawing.Size(349, 20);
            this.otch.TabIndex = 4;
            this.otch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.otch_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(12, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Отчество";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(73, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Серия";
            // 
            // ser
            // 
            this.ser.Location = new System.Drawing.Point(76, 147);
            this.ser.Name = "ser";
            this.ser.Size = new System.Drawing.Size(56, 20);
            this.ser.TabIndex = 7;
            this.ser.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.age_KeyPress);
            // 
            // button
            // 
            this.button.Location = new System.Drawing.Point(458, 322);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(92, 33);
            this.button.TabIndex = 8;
            this.button.Text = "Добавить";
            this.button.UseVisualStyleBackColor = true;
            this.button.Click += new System.EventHandler(this.button_Click);
            // 
            // change
            // 
            this.change.Location = new System.Drawing.Point(592, 364);
            this.change.Name = "change";
            this.change.Size = new System.Drawing.Size(92, 32);
            this.change.TabIndex = 11;
            this.change.Text = "Изменить";
            this.change.UseVisualStyleBackColor = true;
            this.change.Click += new System.EventHandler(this.change_Click);
            // 
            // del
            // 
            this.del.Location = new System.Drawing.Point(458, 364);
            this.del.Name = "del";
            this.del.Size = new System.Drawing.Size(92, 32);
            this.del.TabIndex = 12;
            this.del.Text = "Удалить";
            this.del.UseVisualStyleBackColor = true;
            this.del.Click += new System.EventHandler(this.del_Click);
            // 
            // nambber
            // 
            this.nambber.Location = new System.Drawing.Point(517, 229);
            this.nambber.Name = "nambber";
            this.nambber.Size = new System.Drawing.Size(153, 20);
            this.nambber.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(411, 229);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 16);
            this.label5.TabIndex = 14;
            this.label5.Text = "Номер записи";
            // 
            // poisk
            // 
            this.poisk.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.poisk.Location = new System.Drawing.Point(676, 228);
            this.poisk.Name = "poisk";
            this.poisk.Size = new System.Drawing.Size(79, 23);
            this.poisk.TabIndex = 15;
            this.poisk.Text = "Поиск";
            this.poisk.UseVisualStyleBackColor = true;
            this.poisk.Click += new System.EventHandler(this.poisk_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLogs});
            this.statusStrip.Location = new System.Drawing.Point(0, 522);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(767, 22);
            this.statusStrip.TabIndex = 16;
            this.statusStrip.Text = "statusStrip1";
            // 
            // StatusLogs
            // 
            this.StatusLogs.BackColor = System.Drawing.Color.Transparent;
            this.StatusLogs.Name = "StatusLogs";
            this.StatusLogs.Size = new System.Drawing.Size(56, 17);
            this.StatusLogs.Text = "StatusBar";
            // 
            // bxml
            // 
            this.bxml.Location = new System.Drawing.Point(592, 322);
            this.bxml.Name = "bxml";
            this.bxml.Size = new System.Drawing.Size(92, 32);
            this.bxml.TabIndex = 17;
            this.bxml.Text = "XML";
            this.bxml.UseVisualStyleBackColor = true;
            this.bxml.Click += new System.EventHandler(this.bxml_Click);
            // 
            // list
            // 
            this.list.FormattingEnabled = true;
            this.list.Location = new System.Drawing.Point(414, 25);
            this.list.Name = "list";
            this.list.Size = new System.Drawing.Size(341, 173);
            this.list.TabIndex = 18;
            // 
            // SERCH
            // 
            this.SERCH.Location = new System.Drawing.Point(414, 202);
            this.SERCH.Name = "SERCH";
            this.SERCH.Size = new System.Drawing.Size(341, 20);
            this.SERCH.TabIndex = 19;
            this.SERCH.Text = "Поиск";
            this.SERCH.TextChanged += new System.EventHandler(this.SERCH_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(15, 147);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 15);
            this.label6.TabIndex = 20;
            this.label6.Text = "Паспорт";
            // 
            // num
            // 
            this.num.Location = new System.Drawing.Point(138, 147);
            this.num.Name = "num";
            this.num.Size = new System.Drawing.Size(81, 20);
            this.num.TabIndex = 22;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(135, 130);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 15);
            this.label7.TabIndex = 21;
            this.label7.Text = "Номер";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(15, 189);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(210, 15);
            this.label8.TabIndex = 24;
            this.label8.Text = "Код бронирования в авиакомпании";
            // 
            // kod
            // 
            this.kod.Location = new System.Drawing.Point(18, 205);
            this.kod.Name = "kod";
            this.kod.Size = new System.Drawing.Size(349, 20);
            this.kod.TabIndex = 23;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(15, 233);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 15);
            this.label9.TabIndex = 26;
            this.label9.Text = "Рейс";
            // 
            // reys
            // 
            this.reys.Location = new System.Drawing.Point(18, 249);
            this.reys.Name = "reys";
            this.reys.Size = new System.Drawing.Size(349, 20);
            this.reys.TabIndex = 25;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(15, 275);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 15);
            this.label10.TabIndex = 28;
            this.label10.Text = "Перевозчик";
            // 
            // comp
            // 
            this.comp.Location = new System.Drawing.Point(18, 291);
            this.comp.Name = "comp";
            this.comp.Size = new System.Drawing.Size(349, 20);
            this.comp.TabIndex = 27;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(97, 373);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 15);
            this.label11.TabIndex = 29;
            this.label11.Text = "Отбытие";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(243, 373);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 15);
            this.label12.TabIndex = 30;
            this.label12.Text = "Прибытие";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.Location = new System.Drawing.Point(16, 402);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 15);
            this.label13.TabIndex = 31;
            this.label13.Text = "Город";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label14.Location = new System.Drawing.Point(16, 438);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(45, 15);
            this.label14.TabIndex = 32;
            this.label14.Text = "Время";
            // 
            // cityO
            // 
            this.cityO.Location = new System.Drawing.Point(76, 402);
            this.cityO.Name = "cityO";
            this.cityO.Size = new System.Drawing.Size(129, 20);
            this.cityO.TabIndex = 33;
            // 
            // timeO
            // 
            this.timeO.Location = new System.Drawing.Point(76, 438);
            this.timeO.Name = "timeO";
            this.timeO.Size = new System.Drawing.Size(129, 20);
            this.timeO.TabIndex = 34;
            // 
            // cityP
            // 
            this.cityP.Location = new System.Drawing.Point(227, 402);
            this.cityP.Name = "cityP";
            this.cityP.Size = new System.Drawing.Size(124, 20);
            this.cityP.TabIndex = 35;
            // 
            // timeP
            // 
            this.timeP.Location = new System.Drawing.Point(227, 438);
            this.timeP.Name = "timeP";
            this.timeP.Size = new System.Drawing.Size(124, 20);
            this.timeP.TabIndex = 36;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label15.Location = new System.Drawing.Point(16, 326);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(40, 15);
            this.label15.TabIndex = 37;
            this.label15.Text = "Класс";
            // 
            // clas
            // 
            this.clas.Location = new System.Drawing.Point(62, 326);
            this.clas.Name = "clas";
            this.clas.Size = new System.Drawing.Size(70, 20);
            this.clas.TabIndex = 38;
            // 
            // mesto
            // 
            this.mesto.Location = new System.Drawing.Point(192, 326);
            this.mesto.Name = "mesto";
            this.mesto.Size = new System.Drawing.Size(70, 20);
            this.mesto.TabIndex = 40;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label16.Location = new System.Drawing.Point(146, 326);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(41, 15);
            this.label16.TabIndex = 39;
            this.label16.Text = "Место";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label17.Location = new System.Drawing.Point(15, 474);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(69, 15);
            this.label17.TabIndex = 42;
            this.label17.Text = "Стоимость";
            // 
            // cost
            // 
            this.cost.Location = new System.Drawing.Point(100, 474);
            this.cost.Name = "cost";
            this.cost.Size = new System.Drawing.Size(177, 20);
            this.cost.TabIndex = 41;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources._21694119_gray_planes_background_with_stripes;
            this.ClientSize = new System.Drawing.Size(767, 544);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.cost);
            this.Controls.Add(this.mesto);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.clas);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.timeP);
            this.Controls.Add(this.cityP);
            this.Controls.Add(this.timeO);
            this.Controls.Add(this.cityO);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.comp);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.reys);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.kod);
            this.Controls.Add(this.num);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.SERCH);
            this.Controls.Add(this.list);
            this.Controls.Add(this.bxml);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.poisk);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nambber);
            this.Controls.Add(this.del);
            this.Controls.Add(this.change);
            this.Controls.Add(this.button);
            this.Controls.Add(this.ser);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.otch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fam);
            this.Controls.Add(this.name);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Бронирование билетов";
            this.TransparencyKey = System.Drawing.Color.Gray;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.TextBox fam;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox otch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ser;
        private System.Windows.Forms.Button button;
        private System.Windows.Forms.Button change;
        private System.Windows.Forms.Button del;
        private System.Windows.Forms.TextBox nambber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button poisk;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel StatusLogs;
        private Button bxml;
        private ListBox list;
        private TextBox SERCH;
        private Label label6;
        private TextBox num;
        private Label label7;
        private Label label8;
        private TextBox kod;
        private Label label9;
        private TextBox reys;
        private Label label10;
        private TextBox comp;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private TextBox cityO;
        private TextBox timeO;
        private TextBox cityP;
        private TextBox timeP;
        private Label label15;
        private TextBox clas;
        private TextBox mesto;
        private Label label16;
        private Label label17;
        private TextBox cost;
    }
        #endregion
    }

