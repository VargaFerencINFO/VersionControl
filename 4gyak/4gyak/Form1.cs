﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace _4gyak
{
    public partial class Form1 : Form
    {
        RealEstateEntities context = new RealEstateEntities();
        List<Flat> Flat;

        Excel.Application xlApp; // A Microsoft Excel alkalmazás
        Excel.Workbook xlWB; // A létrehozott munkafüzet
        Excel.Worksheet xlSheet; // Munkalap a munkafüzeten belül
        private int _million = (int)Math.Pow(10, 6);

        public Form1()
        {
            InitializeComponent();
            LoadData();
            CreateExcel();
            CreateTable();

        }
        private void LoadData()
        {
            Flat = context.Flat.ToList();
        }

        private void CreateTable()
        {
            string[] headers = new string[]
            {
             "Kód",
             "Eladó",
             "Oldal",
             "Kerület",
             "Lift",
             "Szobák száma",
             "Alapterület (m2)",
             "Ár (mFt)",
             "Négyzetméter ár (Ft/m2)"};

            for (int i = 0; i < headers.Length; i++)
            {
                xlSheet.Cells[1, i + 1] = headers[i];
            }
            object[,] values = new object[Flat.Count, headers.Length];
            int counter = 0;
            foreach (var f in Flat)
            {
                values[counter, 0] = f.Code;
                values[counter, 1] = f.Vendor;
                values[counter, 2] = f.Side;
                values[counter, 3] = f.District;

                if (f.Elevator == true)
                {
                    values[counter, 4] = "Van";
                }
                else
                {
                    values[counter, 4] = "Nincs";
                }
                values[counter, 5] = f.NumberOfRooms;
                values[counter, 6] = f.FloorArea;
                values[counter, 7] = f.Price;
                values[counter, 8] = String.Format("={0} / {1} * {2}",
                    "H" + (counter + 2).ToString(),
                    "G" + (counter + 2).ToString(),
                    _million.ToString());
                counter++;
            }
            var range = xlSheet.get_Range(GetCell(2, 1), GetCell(1 + values.GetLength(0), values.GetLength(1)));
            range.Value2 = values;
        }

        private void CreateExcel()
        {
            try
            {
                // Excel elindítása és az applikáció objektum betöltése
                xlApp = new Excel.Application();

                // Új munkafüzet
                xlWB = xlApp.Workbooks.Add(Missing.Value);

                // Új munkalap
                xlSheet = xlWB.ActiveSheet;

                // Tábla létrehozása
                CreateTable(); // Ennek megírása a következő feladatrészben következik

                // Control átadása a felhasználónak
                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex) // Hibakezelés a beépített hibaüzenettel
            {
                string errMsg = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(errMsg, "Error");

                // Hiba esetén az Excel applikáció bezárása automatikusan
                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }
           
        }
     

        private string GetCell(int x, int y)
        {
            string ExcelCoordinate = "";
            int dividend = y;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                ExcelCoordinate = Convert.ToChar(65 + modulo).ToString() + ExcelCoordinate;
                dividend = (int)((dividend - modulo) / 26);
            }
            ExcelCoordinate += x.ToString();

            return ExcelCoordinate;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
