// Eric Amrhein, POS 409 Dr. Spees
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace EmployeeStuff
{
    public partial class Form1 : Form
    {
        public Regex NameText = new Regex("^[A-Za-z ]+$"); // Regex to test if Names are just letters
        public Regex numCheck = new Regex("^[0-9 ]+$");    // Regex to test number codes are just numbers
        public Form1()
        {
           
            
            InitializeComponent();
            textBox6.Enabled = false;  // Manager Boxes are false unless the checkbox is checked
            textBox7.Enabled = false;
            textBox8.Enabled = false;
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool valid = formvalidator();  // initiates form validation on sumbit

            if (checkBox1.Checked && valid == true)  // records manager information
            { 
                Manager sumbitManager = new Manager();
                sumbitManager.firstName = textBox1.Text;
                sumbitManager.lastName = textBox2.Text;
                sumbitManager.phoneNum = textBox3.Text;
                sumbitManager.zipCode = textBox4.Text;
                sumbitManager.title = comboBox1.Items[comboBox1.SelectedIndex].ToString();
                sumbitManager.salary = textBox6.Text;
                sumbitManager.storeCode = textBox7.Text;
                sumbitManager.passWord = textBox8.Text;
                sumbitManager.write();
                clear();
                PopulateList(@"output.txt");

            }
            else if (checkBox1.ThreeState == false && valid == true) // records employee information
            { 
                Employee submitEmployee = new Employee();
                submitEmployee.firstName = textBox1.Text;
                submitEmployee.lastName = textBox2.Text;
                submitEmployee.phoneNum = textBox3.Text;
                submitEmployee.zipCode = textBox4.Text;
                submitEmployee.title = comboBox1.Items[comboBox1.SelectedIndex].ToString();
                submitEmployee.write();
                clear();
                PopulateList(@"output.txt");
            } 

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) // Enables Manager Form Boxes
        {
            textBox6.Enabled = checkBox1.Checked;
            textBox7.Enabled = checkBox1.Checked;
            textBox8.Enabled = checkBox1.Checked;
        }
        public bool formvalidator()  // Checks for basic form validity (not extreme)
        {
            if (checkBox1.ThreeState == false && NameText.IsMatch(textBox1.Text + textBox2.Text) && numCheck.IsMatch(textBox3.Text + textBox4.Text))
            {
                vLabel.Visible = false;
                return true;
                
            }
            if (checkBox1.ThreeState == true && NameText.IsMatch(textBox1.Text + textBox2.Text) && numCheck.IsMatch(textBox3.Text + textBox4.Text + textBox6.Text + textBox7.Text))
            {
                vLabel.Visible = false;
                return true;

            }

            else
            {
                vLabel.Text = " Please Check That all forms are filled and use just letters(A-z) OR numbers(0-9)";
                vLabel.Visible = true;
                return false;
            }
        }
        public void clear() // this is a change
        {
            textBox1.Clear();
            comboBox1.SelectedIndex = 0;
            checkBox1.Checked = false;
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();

        }
        private void PopulateList(string filepath)
        {
            List<string> dog = new List<string>();
            using (Stream stream = File.OpenRead(filepath))
            {
            using (TextReader reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    dog.Add(line);
                }
                reader.Close();
            } stream.Close();
            }
            if (listBox1 != null)
            {
                listBox1.Items.AddRange(dog.ToArray());
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PopulateList(@"output.txt");
        }
    }

    public class Employee  // Employee Mainclass
    {
       
        public string firstName { get; set; }
        public string lastName  { get; set; }
        public string phoneNum  { get; set; }
        public string zipCode   { get; set; }
        public string title     { get; set; }
   

        public void write()
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"output.txt", true))
            {
                file.WriteLine(firstName + " " + lastName + " " + phoneNum + " " +zipCode + " " + title );
            }
        }
  
    }
    public class Manager : Employee  // Manager Subclass
    {
        public string salary { get; set; }
        public string storeCode { get; set; }
        public string passWord { get; set; }

        new public void write()
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"output.txt", true))
            {
                file.WriteLine(this.firstName + " " + this.lastName + " " + this.phoneNum + " " +this.zipCode + " " + this.title +" " + this.salary + " " + this.storeCode + " " + this.passWord);
            }
        }
    }
    
}
