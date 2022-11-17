using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Logisticer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void BTNOpti_Click(object sender, RoutedEventArgs e)
        {
            LBALL.Content = "Gesamtkosten";                                     //Reset Label
            if (RDBMMM.IsChecked == true)                                       //Check wich Mode is chosen
            {                                                                                                                                                              
                Matrix();                                                                                                                                   
            }
            if (RDBNWM.IsChecked == true)
            {                  
                Northwest();
            }
            
            
        }
        
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
        Regex regex = new ("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
        }
    public void Matrix()
        {
            int[,] Fabriken = {
                { Convert.ToInt32(TB1A.Text), Convert.ToInt32(TB1B.Text), Convert.ToInt32(TB1C.Text), Convert.ToInt32(TB1D.Text) },
                { Convert.ToInt32(TB2A.Text), Convert.ToInt32(TB2B.Text), Convert.ToInt32(TB2C.Text), Convert.ToInt32(TB2D.Text) },
                { Convert.ToInt32(TB3A.Text), Convert.ToInt32(TB3B.Text), Convert.ToInt32(TB3C.Text), Convert.ToInt32(TB3D.Text) }              //Load all Input into Variables
            };
            int[] FabrikenSup = { Convert.ToInt32(TB1SUP.Text), Convert.ToInt32(TB2SUP.Text), Convert.ToInt32(TB3SUP.Text) };
            int[][] Shops = {
                new int[]{ Convert.ToInt32(TB1A.Text),Convert.ToInt32(TB2A.Text),Convert.ToInt32(TB3A.Text)},
                new int[]{ Convert.ToInt32(TB1B.Text),Convert.ToInt32(TB2B.Text),Convert.ToInt32(TB3B.Text)},
                new int[]{ Convert.ToInt32(TB1C.Text),Convert.ToInt32(TB2C.Text),Convert.ToInt32(TB3C.Text)},
                new int[]{ Convert.ToInt32(TB1D.Text),Convert.ToInt32(TB2D.Text),Convert.ToInt32(TB3D.Text)}
            };
            int[] ShopDemand = { Convert.ToInt32(TBADEM.Text), Convert.ToInt32(TBBDEM.Text), Convert.ToInt32(TBCDEM.Text), Convert.ToInt32(TBDDEM.Text) };

            int[,] Field = { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };                                                                  //prepare Output-Array

            for (int j = 0; j < Shops.Length; j++)
            {
                int[] ShopSort = new int[3];
                for (int k = 0; k < 3; k++)
                {
                    ShopSort[k] = Shops[j][k];
                }                                                                                                                               //Duplicate Variable, to Sort the Copy
                Array.Sort(ShopSort);                                                                                                           //Sort the Copy
                int index = Array.IndexOf(Shops[j], ShopSort[0]);                                                                               //look for position of lowest value

                while (ShopDemand[j] != 0 && FabrikenSup[index] != 0)                                                                           //checks if Demand and Supply are served, repeats process if not
                {
                    if (FabrikenSup[index] >= ShopDemand[j])
                    {
                        
                        FabrikenSup[index] = FabrikenSup[index] - ShopDemand[j];
                        Field[index, j] = ShopDemand[j];
                        ShopDemand[j] = 0;
                        
                    }
                    else
                    {
                        
                        ShopDemand[j] = ShopDemand[j] - FabrikenSup[index];
                        Field[index, j] = FabrikenSup[index];
                        FabrikenSup[index] = 0;
                        
                    }
                    index = index + 1;                                                                                                          //highers index because index 0 is probably fully served
                }
            }
            LBOUT.Content = "Fabrik\t\tShop A\tShop B\tShop C\tShop D" +
                            "\n\nFabrik A\t\t" + Field[0, 0] + "\t" + Field[0, 1] + "\t" + Field[0, 2] + "\t" + Field[0, 3] +
                            "\nFabrik B\t\t" + Field[1, 0] + "\t" + Field[1, 1] + "\t" + Field[1, 2] + "\t" + Field[1, 3] +
                            "\nFabrik C\t\t" + Field[2, 0] + "\t" + Field[2, 1] + "\t" + Field[2, 2] + "\t" + Field[2, 3] +
                            "\n\nGesamt: \t\t" + (Field[0, 0] + Field[1, 0] + Field[2, 0]) + "\t" + (Field[0, 1] + Field[1, 1] + Field[2, 1]) + "\t" + (Field[0, 2] + Field[1, 2] + Field[2, 2]) + "\t" + (Field[0, 3] + Field[1, 3] + Field[2, 3]);
            
            int Gesamt =    Fabriken[0, 0] * Field[0, 0] + Fabriken[0, 1] * Field[0, 1] + Fabriken[0, 2] * Field[0, 2] + Fabriken[0, 3] * Field[0, 3] +
                            Fabriken[1, 0] * Field[1, 0] + Fabriken[1, 1] * Field[1, 1] + Fabriken[1, 2] * Field[1, 2] + Fabriken[1, 3] * Field[1, 3] +
                            Fabriken[2, 0] * Field[2, 0] + Fabriken[2, 1] * Field[2, 1] + Fabriken[2, 2] * Field[2, 2] + Fabriken[2, 3] * Field[2, 3];
            
            LBALL.Content = LBALL.Content + " " + Gesamt;

        }
        public void Northwest()
        {
            int[,] Fabriken = {
                { Convert.ToInt32(TB1A.Text), Convert.ToInt32(TB1B.Text), Convert.ToInt32(TB1C.Text), Convert.ToInt32(TB1D.Text) },
                { Convert.ToInt32(TB2A.Text), Convert.ToInt32(TB2B.Text), Convert.ToInt32(TB2C.Text), Convert.ToInt32(TB2D.Text) },
                { Convert.ToInt32(TB3A.Text), Convert.ToInt32(TB3B.Text), Convert.ToInt32(TB3C.Text), Convert.ToInt32(TB3D.Text) }
            };
            int[] FabrikenSup = { Convert.ToInt32(TB1SUP.Text), Convert.ToInt32(TB2SUP.Text), Convert.ToInt32(TB3SUP.Text) };
            int[][] Shops = {
                new int[]{ Convert.ToInt32(TB1A.Text),Convert.ToInt32(TB2A.Text),Convert.ToInt32(TB3A.Text)},
                new int[]{ Convert.ToInt32(TB1B.Text),Convert.ToInt32(TB2B.Text),Convert.ToInt32(TB3B.Text)},
                new int[]{ Convert.ToInt32(TB1C.Text),Convert.ToInt32(TB2C.Text),Convert.ToInt32(TB3C.Text)},
                new int[]{ Convert.ToInt32(TB1D.Text),Convert.ToInt32(TB2D.Text),Convert.ToInt32(TB3D.Text)}
            };
            int[] ShopDemand = { Convert.ToInt32(TBADEM.Text), Convert.ToInt32(TBBDEM.Text), Convert.ToInt32(TBCDEM.Text), Convert.ToInt32(TBDDEM.Text) };
            int FabricLenAct = Fabriken.Length / Shops.Length;
            int[,] Field = { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
            for (int i = 0; i < FabricLenAct; i++)                                                                                       //Dividing Lenght by number of items per Array, to just read amount of lines
            {
                for (int j = 0; j < Shops.Length; j++)
                {
                    if (FabrikenSup[i] != 0 && ShopDemand[j] != 0)
                    {
                        if (FabrikenSup[i] >= ShopDemand[j])
                        {
                            FabrikenSup[i] = FabrikenSup[i] - ShopDemand[j];
                            Field[i, j] = ShopDemand[j];
                            ShopDemand[j] = 0;
                        }
                        else
                        {
                            ShopDemand[j] = ShopDemand[j] - FabrikenSup[i];
                            Field[i, j] = ShopDemand[j] - FabrikenSup[i];
                            FabrikenSup[i] = 0;
                        }
                    }
                }
            }
            LBOUT.Content = "Fabrik\t\tShop A\tShop B\tShop C\tShop D" +
                            "\n\nFabrik A\t\t" + Field[0, 0] + "\t" + Field[0, 1] + "\t" + Field[0, 2] + "\t" + Field[0, 3] +
                            "\nFabrik B\t\t" + Field[1, 0] + "\t" + Field[1, 1] + "\t" + Field[1, 2] + "\t" + Field[1, 3] +
                            "\nFabrik C\t\t" + Field[2, 0] + "\t" + Field[2, 1] + "\t" + Field[2, 2] + "\t" + Field[2, 3] +
                            "\n\nGesamt: \t\t" + (Field[0, 0] + Field[1, 0] + Field[2, 0]) + "\t" + (Field[0, 1] + Field[1, 1] + Field[2, 1]) + "\t" + (Field[0, 2] + Field[1, 2] + Field[2, 2]) + "\t" + (Field[0, 3] + Field[1, 3] + Field[2, 3]);
            
            int Gesamt =    Fabriken[0, 0] * Field[0, 0] + Fabriken[0, 1] * Field[0, 1] + Fabriken[0, 2] * Field[0, 2] + Fabriken[0, 3] * Field[0, 3] +
                            Fabriken[1, 0] * Field[1, 0] + Fabriken[1, 1] * Field[1, 1] + Fabriken[1, 2] * Field[1, 2] + Fabriken[1, 3] * Field[1, 3] +
                            Fabriken[2, 0] * Field[2, 0] + Fabriken[2, 1] * Field[2, 1] + Fabriken[2, 2] * Field[2, 2] + Fabriken[2, 3] * Field[2, 3];
            
            LBALL.Content = LBALL.Content + " " + Gesamt;
        }
    }
}
