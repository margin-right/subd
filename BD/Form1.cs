using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;

namespace WindowsFormsApplication1
{

    public partial class Form1 : Form
    {
        public Form1()//Основная форма
        {
            InitializeComponent();
        }

        private void name_KeyPress(object sender, KeyPressEventArgs e)//поле имя
        {
            if ((!Char.IsDigit(e.KeyChar)) && (!Char.IsPunctuation(e.KeyChar)) && (!Char.IsSymbol(e.KeyChar))) return;
            else
            e.Handled = true;
        }

        private void fam_KeyPress(object sender, KeyPressEventArgs e)//поле фамилия
        {
            if ((!Char.IsDigit(e.KeyChar)) && (!Char.IsPunctuation(e.KeyChar)) && (!Char.IsSymbol(e.KeyChar))) return;
            else
            e.Handled = true;
        }

        private void otch_KeyPress(object sender, KeyPressEventArgs e)//поле отчество
        {
            if ((!Char.IsDigit(e.KeyChar)) && (!Char.IsPunctuation(e.KeyChar)) && (!Char.IsSymbol(e.KeyChar))) return;
            else
            e.Handled = true;
        }

        private void age_KeyPress(object sender, KeyPressEventArgs e)//поле возраст
        {
            if ((!Char.IsLetter(e.KeyChar)) && (!Char.IsPunctuation(e.KeyChar)) && (!Char.IsSymbol(e.KeyChar))) return;
            else
            e.Handled = true;
        }

        private void button_Click(object sender, EventArgs e)//кнопка добавить
        {
            string text = name.Text + "~" + fam.Text + "~" + otch.Text + "~" + ser.Text + "~" + num.Text + "~" + kod.Text + "~" + reys.Text + "~" + comp.Text + "~" + clas.Text + "~" + mesto.Text + "~" + cityO.Text + "~" + timeO.Text + "~" + cityP.Text + "~" + timeP.Text + "~" + cost.Text;
            StatusLogs.Text = Funbuttons.Badd(text, FunFile.alAcc);
            FunFile.writer();
            
        }

        private void Form1_Load(object sender, EventArgs e)//загрузка формы
        {
            string status = FunFile.BLoad();
            StatusLogs.Text = status;
            string[] first = Funbuttons.firstRecord(FunFile.alAcc);
            name.Text = first[0];
            fam.Text = first[1];
            otch.Text = first[2];
            ser.Text = first[3];
            num.Text = first[4];
            kod.Text = first[5];
            reys.Text = first[6];
            comp.Text = first[7];
            clas.Text = first[8];
            mesto.Text = first[9];
            cityO.Text = first[10];
            timeO.Text = first[11];
            cityP.Text = first[12];
            timeP.Text = first[13];
            cost.Text = first[14];
            list.DataSource = FunFile.slash(FunFile.alAcc);
        }

        private void poisk_Click(object sender, EventArgs e)//кнопка поиск записей
        {
            String[] aSearch = Funbuttons.Bpoisk(nambber.Text, FunFile.alAcc);
            name.Text = aSearch[0];
            fam.Text = aSearch[1];
            otch.Text = aSearch[2];
            ser.Text = aSearch[3];
            num.Text = aSearch[4];
            kod.Text = aSearch[5];
            reys.Text = aSearch[6];
            comp.Text = aSearch[7];
            clas.Text = aSearch[8];
            mesto.Text = aSearch[9];
            cityO.Text = aSearch[10];
            timeO.Text = aSearch[11];
            cityP.Text = aSearch[12];
            timeP.Text = aSearch[13];
            cost.Text = aSearch[14];
        }

        private void del_Click(object sender, EventArgs e)//кнопка удалить
        {
            string text = name.Text + "~" + fam.Text + "~" + otch.Text + "~" + ser.Text + "~" + num.Text + "~" + kod.Text + "~" + reys.Text + "~" + comp.Text + "~" + clas.Text + "~" + mesto.Text + "~" + cityO.Text + "~" + timeO.Text + "~" + cityP.Text + "~" + timeP.Text + "~" + cost.Text;
            string Pfam = fam.Text;
            Funbuttons.Bdel(FunFile.alAcc, text, Pfam);
            FunFile.writer();
        }

        private void change_Click(object sender, EventArgs e)//кнопка изменить
        {
            string text = name.Text + "~" + fam.Text + "~" + otch.Text + "~" + ser.Text + "~" + num.Text + "~" + kod.Text + "~" + reys.Text + "~" + comp.Text + "~" + clas.Text + "~" + mesto.Text + "~" + cityO.Text + "~" + timeO.Text + "~" + cityP.Text + "~" + timeP.Text + "~" + cost.Text;
            string Pfam = fam.Text;
            Funbuttons.Bchange(FunFile.alAcc, text, Pfam);
            FunFile.writer();
        }

        private void bxml_Click(object sender, EventArgs e)//кнопка создать xml\обновить xml
        {
           FunFile.Bxml();
        }

        private void SERCH_TextChanged(object sender, EventArgs e)
        {
            string con = SERCH.Text;
            ArrayList Io = FunFile.Search(FunFile.alAcc,con);
            Io = FunFile.slash(Io);
            list.DataSource = Io;
        }

       
    }
}
