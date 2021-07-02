using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab1._2
{
    public partial class Form1 : Form/*, IComparer<Fruit>*/
    {
        public Form1()
        {
            InitializeComponent();

            foreach (var item in tableLayoutPanel2.Controls)
            {
                if (item is ListBox)
                {
                    ((ListBox)item).Font = new Font("Montserrat", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, 204);

                }
            }
        }
        private List<Fruit> generatedFruits = new List<Fruit>();
        static Random amountRand = new Random();
        private void button1_Click(object sender, EventArgs e)
        {
            if (short.TryParse(textBox1.Text, out short res))
            {
                if (res < 11)
                {
                    generatedFruits.Clear();
                    listBox1.Items.Clear();
                    List<string> fruitPool = new List<string>
                    {
                        "Яблоко","Груша","Банан","Апельсин","Грейпфрут","Хурма","Мандарин","Манго","Ананас","Персик","Арбуз",
                    };
                    for (int i = 0; i < res; i++)
                    {
                        Random fruitRand = new Random();
                        int fruitIndex = fruitRand.Next(0, fruitPool.Count);
                        string fruit = fruitPool.ElementAt(fruitIndex);
                        fruitPool.RemoveAt(fruitIndex);
                        generatedFruits.Add(new Fruit(fruit, amountRand.Next(1, 10)));
                        //listBox1.Items.Add(new Fruit(fruit, amountRand.);
                    }
                    textBox1.Clear();
                    listBox1.Items.AddRange(generatedFruits.ToArray());
                }
                else
                {
                    MessageBox.Show("Слишком большая коллекция.");
                }
            }
            else
            {
                MessageBox.Show("Введите простое число.");
            }
        }
        public int Compare(Fruit f1, Fruit f2)
        {
            if (f1.fruitAmount > f2.fruitAmount) return 1;
            else if (f1.fruitAmount == f2.fruitAmount) return 0;
            else return -1;
        }
        public int Compare2(Fruit f1, Fruit f2)
        {
            if (f1.fruitAmount > f2.fruitAmount) return -1;
            else if (f1.fruitAmount == f2.fruitAmount) return 0;
            else return 1;
        }
        delegate void Comparator(bool type);
        private void button2_Click(object sender, EventArgs e)
        {
            // создать делегат сравнения
            listBox2.Items.Clear();
            Comparator sort = Comparisson;
            sort(false);
        }
        // метод для сортировки
        public void Comparisson(bool type)
        {
            List<Fruit> sortedFruits = new List<Fruit>(generatedFruits);
            if (!type)
            {   
                sortedFruits.Sort(Compare);
            }
            else
            {
                sortedFruits.Sort(Compare2);
            }
            foreach (var item in sortedFruits)
            {
                listBox2.Items.Add(item);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            Comparator sort = Comparisson;
            sort(true);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            if (listBox1.Items.Count != 0)
            {
                List<Fruit> sortedFruits = new List<Fruit>();
                foreach (var item in listBox1.Items)
                {
                    sortedFruits.Add((Fruit)item);
                }
                var groupByAmount = sortedFruits.OrderByDescending(f => f.fruitAmount).GroupBy(f => f.fruitAmount);
                IGrouping<int, Fruit> g = groupByAmount.First();
                foreach (var f in g)
                {
                    listBox2.Items.Add(f);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            if(listBox1.Items.Count != 0)
            {
                List<Fruit> sortedFruits = new List<Fruit>();
                foreach (var item in listBox1.Items)
                {
                    sortedFruits.Add((Fruit)item);
                }
                var groupByAmount = sortedFruits.OrderBy(f => f.fruitAmount).GroupBy(f => f.fruitAmount);
                IGrouping<int, Fruit> g = groupByAmount.First();
                foreach (var f in g)
                {
                    listBox2.Items.Add(f);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text != "")
            {
                string choice = comboBox1.Text.ToLower();
                List<Fruit> sortedFruits = new List<Fruit>();
                switch (choice)
                {
                    case "желтый":
                        listBox2.Items.Clear();
                        foreach (var fruit in listBox1.Items)
                        {
                            if (((Fruit)fruit).fruitName == "Банан")
                            {
                                listBox2.Items.Add((Fruit)fruit);
                            }
                        }
                        break;
                    case "зеленый":
                        listBox2.Items.Clear();
                        foreach (var fruit in listBox1.Items)
                        {
                            if (((Fruit)fruit).fruitName == "Яблоко" ||
                                ((Fruit)fruit).fruitName == "Арбуз" ||
                                ((Fruit)fruit).fruitName == "Манго" ||
                                ((Fruit)fruit).fruitName == "Груша")
                            {
                                listBox2.Items.Add((Fruit)fruit);
                            }
                        }
                        break;
                    case "оранжевый":
                        listBox2.Items.Clear();
                        foreach (var fruit in listBox1.Items)
                        {
                            if (((Fruit)fruit).fruitName == "Апельсин" ||
                                ((Fruit)fruit).fruitName == "Мандарин" ||
                                ((Fruit)fruit).fruitName == "Хурма" ||
                                ((Fruit)fruit).fruitName == "Грейпфрут")
                            {
                                listBox2.Items.Add((Fruit)fruit);
                            }
                        }
                        break;
                    default:
                        MessageBox.Show("Нет результатов.");
                        break;
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && textBox3.Text == "")
            {
                if (short.TryParse(textBox2.Text, out short from))
                {
                    if (from > 0 && from < 10)
                    {
                        listBox2.Items.Clear();
                        foreach (var fruit in listBox1.Items)
                        {
                            if (((Fruit)fruit).fruitAmount >= from)
                            {
                                listBox2.Items.Add((Fruit)fruit);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Выход за диапазон.");
                    }
                }
                else
                {
                    MessageBox.Show("Неверный формат!");
                }
            }
            else if (textBox2.Text == "" && textBox3.Text != "")
            {
                if (short.TryParse(textBox3.Text, out short to))
                {
                    if (to > 0 && to < 10)
                    {
                        listBox2.Items.Clear();
                        foreach (var fruit in listBox1.Items)
                        {
                            if (((Fruit)fruit).fruitAmount <= to)
                            {
                                listBox2.Items.Add((Fruit)fruit);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Выход за диапазон.");
                    }
                }
                else
                {
                    MessageBox.Show("Неверный формат!");
                }
            }
            else if (textBox2.Text != "" && textBox3.Text != "")
            {
                if (short.TryParse(textBox2.Text, out short from) && short.TryParse(textBox3.Text, out short to))
                {
                    if (from > 0 && to < 10 && from <= to)
                    {
                        listBox2.Items.Clear();
                        foreach (var fruit in listBox1.Items)
                        {
                            if (((Fruit)fruit).fruitAmount >= from && ((Fruit)fruit).fruitAmount <= to)
                            {
                                listBox2.Items.Add((Fruit)fruit);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неверный диапазон.");
                    }
                }
                else
                {
                    MessageBox.Show("Неверный формат!");
                }
            }
        }
    }
    public class Fruit
    {
        
        internal string fruitName { get; set; }
        internal int fruitAmount { get; set; }
        internal Fruit(string fruit, int fruitCount)
        {
            fruitName = fruit;
            fruitAmount = fruitCount;
        }
        public override string ToString()
        {
            return $"{fruitAmount} {fruitName}";
        }
    }
}
