﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PBL
{
    public partial class Mapa_ni_Tiquia : Form
    {
        string[] islandList = { "Luzon", "Visayas", "Mindanao" };
        string[] ncrCity =
        {
            "Caloocan City", "Las Pinas City", "Makati City", "Malabon City", "Mandaluyong City", "Manila City", "Marikina City",
            "Muntinlupa City", "Navotas City", "Paranaque City", "Pasay City", "Pasig City", "Quezon City", "San Juan City", "Taguig City", "Valenzuela City"
        };


        string[,] luzonRegionOneToEight =
        {
            // nag multi dimensional array ako
            {"Region I", "Region II", "Region III", "Region IV-A", "Region IV-B", "Region V", "Cordillera Administrative Region" , "NCR"},
            {"Ilocos Norte", "Ilocos Sur", "La Union", "Pangasinan", "","","",""},                     // ilocos region or region 1
            {"Batanes", "Cagayan", "Isabela", "Nueva Vizcaya", "Quirino Province","","",""},           //  region 2 or Cagayan Valley
            {"Aurora", "Bataan", "Bulacan", "Nueva Ecija", "Pampanga", "Tarlac", "Zambales",""},       //region3 or Central Luzon
            {"Cavite", "Laguna","Batangas","Rizal", "Quezon","","",""},                                // region 4 a calabarzon 
            {"Occidental Mindoro", "Oriental Mindoro", "Marinduque", "Romblon", "Palawan", "","",""},  //region 4b mimaropa
            {"Albay", "Camarines Norte", "Camarines Sur", "Sorsogon","Catanduanes", "Masbate", "",""},    //region 5
            {"Abra", "Benguet", "Ifugao", "Kalinga", "Apayao", "Mountain Province", "",""},    //cordilierra administrative region
        };
        string[,] visayasRegionSixToEight =
        {
            {"Region VI", "Region VII", "Region VIII", "", "", ""},
            {"Aklan", "Antique", "Capiz", "Guimaras", "Iloilo", "Negros Occidental" },  // western visayas Regionn 6
            {"Cebu", "Bohol", "Negros Oriental", "Siquijor", "", "" }, // central visayas region 7
            {"Leyte", "Biliran", "Southern Leyte", "Samar", "Eastern Samar", "Northern Samar"  }, // eastern visayas
        };

        string[,] mindanaoRegions =
        {
            {"Region IX", "Region X", "Region XI", "Region XII","Region XIII", "BARMM" },
            {"Zamboanga del Norte", "Zamboanga del Sur", "Zamboanga Sibugay", "", "", "" }, //Zamboanga Peninsula Region 9
            {"Misamis Oriental", "Misamis Occidental", "Bukidnon", "Camiguin", "Lanao del Norte", "" },         // region 10 northern mindanao
            {"Davao de Oro", "Davao del Norte", "Davao del Sur", "Davao Oriental", "Davao Occidental",""},      // region 11 southern mindanao dabao region
            {"South Cotabato", "Cotabato", "Sultan Kudarat", "Sarangani", "", "" },         //central mindanao sooccsargen 12
            {"Agusan del Norte", "Agusan del Sur", "Surigao del Norte", "Surigao del Sur", "Dinagat Islands", "" }, // region 13 caraga
            {"Lanao del Sur", "Maguindanao", "Sulu", "Tawi-tawi", "", ""},                  // BARMM
        };

        List<string> provinceAndCase = new List<string>();
        List<string> allProvinces = new List<string>();
       

         public void getTotalCase(string province)
         {
            StreamReader read = new StreamReader(@"C:\Users\Denise\Downloads\Total-Cases.txt");
            while (read.Peek ()!=-1)
            {
                string input = read.ReadLine();
                string[] splitInput = input.Split(',');
                string newSplit = splitInput[1].Replace("CITY OF ", "").Replace(" CITY", "").ToLower();
                string caseSplitNew = splitInput[2].Replace(",", "");
                provinceAndCase.Add(newSplit + "*"+caseSplitNew);
            }
            read.Close();
           
            foreach(string provCase in provinceAndCase)
            {
                string[] provCaseSplit = provCase.Split('*');
                Console.Write(appended);
                if (provinceMapComboBox.Text.ToLower() ==provCaseSplit[0])
                {
                    MessageBox.Show("SAME KAMI " + provCaseSplit[0] +"total case is " + provCaseSplit[1]);
                   


                    tCase.Text = "Total Case: " + provCaseSplit[1];
                }
            }




        }
        public Mapa_ni_Tiquia()
        {
            InitializeComponent();
        }
        public void emptyRegionProvince()
        {
            provinceMapComboBox.SelectedIndex = -1;
            regionMapComboBox.SelectedIndex = -1;
        }
        public void showMap()
        {
            // hide
            islandLabel.Visible = false;
            islandComboBox.Visible = false;
            regionLabel.Visible = false;
            regionComboBox.Visible = false;
            provinceLabel.Visible = false;
            provinceComboBox.Visible = false;
            cases.Visible = false;
            caseInputTextBox.Visible = false;
            // clear laman
            islandComboBox.Text = regionComboBox.Text = provinceComboBox.Text = caseInputTextBox.Text = string.Empty;
            // visible na ngyon yung second
            regionMap.Visible = true;
            regionMapComboBox.Visible = true;
            provinceMap.Visible = true;
            provinceMapComboBox.Visible = true;
            currentIsland.Visible = true;
            
        }
        public void hideMap ()
        {
            regionMap.Visible = false;
            regionMapComboBox.Visible = false;
            provinceMapComboBox.Visible = false;
            provinceMap.Visible = false;
            currentIsland.Visible = false;
            panelOfImage.Visible = false;
            // vvisible mo ung orig
            islandLabel.Visible = true;
            islandComboBox.Visible = true;
            regionLabel.Visible = true;
            regionComboBox.Visible = true;
            provinceLabel.Visible = true;
            provinceComboBox.Visible = true;
            cases.Visible = true;
            caseInputTextBox.Visible = true;
        }
        public void populateIsland()
        {
            for (int i = 0; i < islandList.Length;i++)
            {
                islandComboBox.Items.Add(islandList[i]);
            }
        }
        // lagyan mo laman regions
        public void populateRegions(string island)
        {
            int dimension = 0;
            if (island == "Luzon")
            {
                regionComboBox.Items.Clear();
                regionMapComboBox.Items.Clear();
                dimension = luzonRegionOneToEight.GetLength(0);
                for (int i = 0; i <dimension;i++)
                {
                    if (luzonRegionOneToEight[0,i] != string.Empty)
                    {
                        regionComboBox.Items.Add(luzonRegionOneToEight[0, i]);
                        regionMapComboBox.Items.Add(luzonRegionOneToEight[0, i]);
                    }
                }

            }
            if (island == "Visayas")
            {
                regionComboBox.Items.Clear();
                regionMapComboBox.Items.Clear();
                dimension = visayasRegionSixToEight.GetLength(0);
                for (int i = 0; i < dimension; i++)
                {
                    if (visayasRegionSixToEight[0, i] != string.Empty)
                    {
                        regionComboBox.Items.Add(visayasRegionSixToEight[0, i]);
                        regionMapComboBox.Items.Add(visayasRegionSixToEight[0, i]);
                    }
                }

            }
            if (island == "Mindanao")
            {
                regionComboBox.Items.Clear();
                regionMapComboBox.Items.Clear();
                dimension = mindanaoRegions.GetLength(1);
                for (int i = 0; i < dimension; i++)
                {
                    if (mindanaoRegions[0, i] != string.Empty)
                    {
                        regionComboBox.Items.Add(mindanaoRegions[0, i]);
                        regionMapComboBox.Items.Add(mindanaoRegions[0, i]);
                    }
                }
            }
        }
        // lagyan mo laman province
        public void populateProvince(string region)
        {
            int dimension = 0;
            // buonng luzon
            if (region == "Region I")
            {
                dimension = luzonRegionOneToEight.GetLength(0); 
                for (int i = 0; i <dimension;i++ )
                {
                    if (luzonRegionOneToEight[1,i] != string.Empty)
                    {
                        provinceComboBox.Items.Add(luzonRegionOneToEight[1, i]);
                        provinceMapComboBox.Items.Add(luzonRegionOneToEight[1, i]);
                        allProvinces.Add(luzonRegionOneToEight[1, i].ToLower());
                    }
                }
            }
            if (region == "Region II")
            {
                dimension = luzonRegionOneToEight.GetLength(0);
                for (int i = 0; i < dimension; i++)
                {
                    if (luzonRegionOneToEight[2, i] != string.Empty)
                    {
                        provinceComboBox.Items.Add(luzonRegionOneToEight[2, i]);
                        provinceMapComboBox.Items.Add(luzonRegionOneToEight[2, i]);
                        allProvinces.Add(luzonRegionOneToEight[2, i].ToLower());
                    }
                }
            }
            if (region == "Region III")
            {
                dimension = luzonRegionOneToEight.GetLength(0);
                for (int i = 0; i < dimension; i++)
                {
                    if (luzonRegionOneToEight[3, i] != string.Empty)
                    {
                        provinceComboBox.Items.Add(luzonRegionOneToEight[3, i]);
                        provinceMapComboBox.Items.Add(luzonRegionOneToEight[3, i]);
                        allProvinces.Add(luzonRegionOneToEight[3, i].ToLower());
                    }
                }
            }

            if (region == "Region IV-A")
            {
                dimension = luzonRegionOneToEight.GetLength(0);
                for (int i = 0; i < dimension; i++)
                {
                    if (luzonRegionOneToEight[4, i] != string.Empty)
                    {
                        provinceComboBox.Items.Add(luzonRegionOneToEight[4, i]);
                        provinceMapComboBox.Items.Add(luzonRegionOneToEight[4, i]);
                        allProvinces.Add(luzonRegionOneToEight[4, i].ToLower());
                    }
                }
            }
            if (region == "Region IV-B")
            {
                dimension = luzonRegionOneToEight.GetLength(0);
                for (int i = 0; i < dimension; i++)
                {
                    if (luzonRegionOneToEight[5, i] != string.Empty)
                    {
                        provinceComboBox.Items.Add(luzonRegionOneToEight[5, i]);
                        provinceMapComboBox.Items.Add(luzonRegionOneToEight[5, i]);
                        allProvinces.Add(luzonRegionOneToEight[5, i].ToLower());

                    }
                }
            }
            if (region == "Region V")
            {
                dimension = luzonRegionOneToEight.GetLength(0);
                for (int i = 0; i < dimension; i++)
                {
                    if (luzonRegionOneToEight[6, i] != string.Empty)
                    {
                        provinceComboBox.Items.Add(luzonRegionOneToEight[6, i]);
                        provinceMapComboBox.Items.Add(luzonRegionOneToEight[6, i]);
                        allProvinces.Add(luzonRegionOneToEight[6, i].ToLower());
                    }
                }
            }
            if (region == "Cordillera Administrative Region")
            {
                dimension = luzonRegionOneToEight.GetLength(0);
                for (int i = 0; i < dimension; i++)
                {
                    if (luzonRegionOneToEight[7, i] != string.Empty)
                    {
                        provinceComboBox.Items.Add(luzonRegionOneToEight[7, i]);
                        provinceMapComboBox.Items.Add(luzonRegionOneToEight[7, i]);
                        allProvinces.Add(luzonRegionOneToEight[7, i].ToLower());

                    }
                }
            }
            // end ng luzon
            // start ng visayas
            if (region == "Region VI")
            {
                dimension = visayasRegionSixToEight.GetLength(0);
                for (int i = 0; i < dimension; i++)
                {
                    if (visayasRegionSixToEight[1, i] != string.Empty)
                    {
                        provinceComboBox.Items.Add(visayasRegionSixToEight[1, i]);
                        provinceMapComboBox.Items.Add(visayasRegionSixToEight[1, i]);
                        allProvinces.Add(visayasRegionSixToEight[1, i].ToLower());
                    }
                }
            }
            if (region == "Region VII")
            {
                dimension = visayasRegionSixToEight.GetLength(0);
                for (int i = 0; i < dimension; i++)
                {
                    if (visayasRegionSixToEight[2, i] != string.Empty)
                    {
                        provinceComboBox.Items.Add(visayasRegionSixToEight[2, i]);
                        provinceMapComboBox.Items.Add(visayasRegionSixToEight[2, i]);
                        allProvinces.Add(visayasRegionSixToEight[2, i].ToLower());
                    }
                }
            }
            if (region == "Region VIII")
            {
                dimension = visayasRegionSixToEight.GetLength(0);
                for (int i = 0; i < dimension; i++)
                {
                    if (visayasRegionSixToEight[3, i] != string.Empty)
                    {
                        provinceComboBox.Items.Add(visayasRegionSixToEight[3, i]);
                        provinceMapComboBox.Items.Add(visayasRegionSixToEight[3, i]);
                        allProvinces.Add(visayasRegionSixToEight[3, i].ToLower());
                    }
                }
            }
            // end ng visayas
            // start ng sa mindanao
            // length of column kinukuha kasi 6 column 7 rows e column long kailangan
            if (region == "Region IX")
            {
                dimension = mindanaoRegions.GetLength(1);
                for (int i = 0; i < dimension; i++)
                {
                    if (mindanaoRegions[1, i] != string.Empty)
                    {
                        provinceComboBox.Items.Add(mindanaoRegions[1, i]);
                        provinceMapComboBox.Items.Add(mindanaoRegions[1, i]);
                        allProvinces.Add(mindanaoRegions[1, i].ToLower());

                    }
                }
            }
            if (region == "Region X")
            {
                dimension = mindanaoRegions.GetLength(1);
                for (int i = 0; i < dimension; i++)
                {
                    if (mindanaoRegions[2, i] != string.Empty)
                    {
                        provinceComboBox.Items.Add(mindanaoRegions[2, i]);
                        provinceMapComboBox.Items.Add(mindanaoRegions[2, i]);
                        allProvinces.Add(mindanaoRegions[2, i].ToLower());
                    }
                }
            }
            if (region == "Region XI")
            {
                dimension = mindanaoRegions.GetLength(1);
                for (int i = 0; i < dimension; i++)
                {
                    if (mindanaoRegions[3, i] != string.Empty)
                    {
                        provinceComboBox.Items.Add(mindanaoRegions[3, i]);
                        provinceMapComboBox.Items.Add(mindanaoRegions[3, i]);
                        allProvinces.Add(mindanaoRegions[3, i].ToLower());

                    }
                }
            }
            if (region == "Region XII")
            {
                dimension = mindanaoRegions.GetLength(1);
                for (int i = 0; i < dimension; i++)
                {
                    if (mindanaoRegions[4, i] != string.Empty)
                    {
                        provinceComboBox.Items.Add(mindanaoRegions[4, i]);
                        provinceMapComboBox.Items.Add(mindanaoRegions[4, i]);
                        allProvinces.Add(mindanaoRegions[4, i].ToLower());

                    }
                }
            }
            if (region == "Region XIII")
            {
                dimension = visayasRegionSixToEight.GetLength(1);
                for (int i = 0; i < dimension; i++)
                {
                    if (mindanaoRegions[5, i] != string.Empty)
                    {
                        provinceComboBox.Items.Add(mindanaoRegions[5, i]);
                        provinceMapComboBox.Items.Add(mindanaoRegions[5, i]);
                        allProvinces.Add(mindanaoRegions[5, i].ToLower());

                    }
                }
            }
            if (region == "BARMM")
            {
                dimension = mindanaoRegions.GetLength(0);
                for (int i = 0; i < dimension; i++)
                {
                    if (visayasRegionSixToEight[6, i] != string.Empty)
                    {
                        provinceComboBox.Items.Add(mindanaoRegions[6, i]);
                        provinceMapComboBox.Items.Add(mindanaoRegions[6, i]);
                        allProvinces.Add(mindanaoRegions[6, i].ToLower());
                    }
                }
            }





        }
        private void Mapa_ni_Tiquia_Load(object sender, EventArgs e)
        {
            hideMap();
            populateIsland();
            
           
           
            


        }

        private void islandComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedIsland = islandComboBox.GetItemText(islandComboBox.SelectedItem);
           
            populateRegions(selectedIsland);
            provinceComboBox.Text = string.Empty;

        }

        private void provinceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void regionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
         
            string selectedRegion = regionComboBox.GetItemText(regionComboBox.SelectedItem);
            provinceComboBox.Items.Clear();
            populateProvince(selectedRegion);
        }

        private void islandComboBox_DropDown(object sender, EventArgs e)
        {
            provinceComboBox.SelectedIndex = -1;
            regionComboBox.SelectedIndex = -1;
            caseInputTextBox.Text = string.Empty;
            
        }

        private void regionComboBox_DropDown(object sender, EventArgs e)
        {
            

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
         



        }

        private void caseInputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            bool mayLamanIslandRegionAtProvince = islandComboBox.Text != string.Empty && regionComboBox.Text != string.Empty && provinceComboBox.Text != string.Empty;
            if (e.KeyCode == Keys.Enter && mayLamanIslandRegionAtProvince)
            {
                MessageBox.Show("Added user");
                string[] addedCase = { regionComboBox.Text, ",", provinceComboBox.Text, ",", caseInputTextBox.Text };
                StreamWriter addUser = new StreamWriter(@"C:\Users\Denise\Downloads\Total-Cases.txt", true);
                addUser.WriteLine();
                for (int i = 0; i < addedCase.Length; i++)
                {
                    addUser.Write(addedCase[i]);
                }
                addUser.Close();
                islandComboBox.Text = regionComboBox.Text = provinceComboBox.Text = caseInputTextBox.Text = string.Empty;
                islandComboBox.Focus();
                islandComboBox.SelectedIndex = -1;
                regionComboBox.SelectedIndex = -1;
                provinceComboBox.SelectedIndex = -1;



            }
         
        }

        private void luzonButton_Click(object sender, EventArgs e)
        {
            showMap();
            string island = currentIsland.Text = "Luzon";
            emptyRegionProvince();
            populateRegions(island);

        }

        private void backButton_Click(object sender, EventArgs e)
        {
            hideMap();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            showMap();
            string island = currentIsland.Text = "Visayas";
            emptyRegionProvince();
            populateRegions(island);
        }

        private void regionMapComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedRegion = regionMapComboBox.GetItemText(regionMapComboBox.SelectedItem);
            provinceMapComboBox.Items.Clear();
            populateProvince(selectedRegion);
        }

        private void regionMapComboBox_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void regionMapComboBox_DropDown(object sender, EventArgs e)
        {
            
           
        }

        private void provinceMapComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (regionMapComboBox.Text != string.Empty && provinceMapComboBox.Text != string.Empty)
            {
                panelOfImage.Visible = true;
                string province= provinceMapComboBox.GetItemText(provinceMapComboBox.SelectedItem);
                provinceName.Text = province;
                getTotalCase(province);
                foreach (var a in allProvinces)
                {
                    Console.WriteLine(a);
                }


            }
        }

        private void mindanao_Click(object sender, EventArgs e)
        {
            showMap();
            string selectedIsland = currentIsland.Text = "Mindanao";
            emptyRegionProvince();
            populateRegions(selectedIsland);
        }

        private void tCase_Click(object sender, EventArgs e)
        {

        }
    }
}
