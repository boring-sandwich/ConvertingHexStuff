using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConvertingStuff
{
    
    public partial class MainWindow : Window
    {
        string strHoldHex01;
        string strHoldHex02;
        int intPercentage;
        decimal decFillUpHDD;
        decimal decimalHDDCapacity;
        decimal decimalHDDUsed;

        int intStep = 1;
        HexToDecimal hex = new HexToDecimal();

        public MainWindow()
        {
            InitializeComponent();
            WhatStep();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            intStep = 1;
            WhatStep();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (intStep == 1)
            {
                //take the input and place it into the hex holder.
                strHoldHex01 = txtInput.Text;
                if (hex.CheckInputHEX(strHoldHex01))//check the hex holder contents meets requirements
                    if (hex.CheckInputHex02(strHoldHex01))
                        intStep = 2;
                    else
                        InputError();
            }
            else if (intStep == 2)
            {
                strHoldHex02 = txtInput.Text;
                if (hex.CheckInputHEX(strHoldHex02))
                    if (hex.CheckInputHex02(strHoldHex02))
                        intStep = 3;
                    else
                        InputError();
            }
            else if (intStep == 3)
            {
                bool result = int.TryParse(txtInput.Text, out intPercentage);
                if (result)
                    if (hex.CheckInputPercentage(intPercentage))
                    {
                        hex.HexMethod(strHoldHex01, out decimalHDDCapacity);
                        hex.HexMethod(strHoldHex02, out decimalHDDUsed);
                        decFillUpHDD = hex.HexSum(decimalHDDCapacity, decimalHDDUsed, intPercentage);
                        if(decFillUpHDD != 0)
                            intStep = 4;
                        else
                        {
                            MessageBox.Show("The number you enetered results in a minus - you cannot fill a hard drive up negatively. Please change your percentage value so that a positive result will occur or \"clear\" to start again.", "Input Error", MessageBoxButton.OK);
                        }
                    }
            }
            else if (intStep == 4)
            {
                intStep = 1;
            }
            WhatStep();
        }
        private void InputError()
        {
            if (intStep == 1 || intStep == 2)
                MessageBox.Show("You can only type in up to nine characters 0-9 and a to f.", "Input Error", MessageBoxButton.OK);
            else
                MessageBox.Show("You can only type in a number from 1 to 100.", "Input Error", MessageBoxButton.OK);
        }

        private void WhatStep()
        {
            switch(intStep)
            {
                case 1:
                    txbSteps.Text = $"step {intStep} of 4";
                    txbText.Text = "size of hard drive:";
                    txtInput.Text = "type HEX here";
                    decFillUpHDD = 0;
                    btnClear.Visibility = Visibility.Hidden;
                    btnSubmit.Content = "submit";
                    break;
                case 2:
                    txbSteps.Text = $"step {intStep} of 4";
                    txbText.Text = "space left on hard drive (HEX):";
                    txtInput.Text = "type HEX here";
                    btnClear.Visibility = Visibility.Visible;
                    break;
                case 3:
                    txbSteps.Text = $"step {intStep} of 4";
                    txbText.Text = "what percentage of the hard drive do you want to fill-up?";
                    txtInput.Text = "e.g.75";
                    break;
                case 4:
                    txbSteps.Text = $"step {intStep} of 4";
                    txbText.Text = "Paste this decimal into the Xbox C Drive:";
                    txtInput.Text = decFillUpHDD.ToString("N0").Replace(",", "");
                    btnClear.Visibility = Visibility.Hidden;
                    btnSubmit.Content = "redo";
                    break;  
            }
            txtInput.Focus();
        }
        private void txtInput_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 3)
            {
                SelectAll(sender);
            }
        }

        private void txtInput_GotFocus(object sender, RoutedEventArgs e)
        {
            SelectAll(sender);
        }

        private static void SelectAll(object sender)
        {
            (sender as TextBox).SelectAll();
        }

        private void MoveDown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (sender == txtInput)
                {
                    btnSubmit.Focus();
                }
                else if (sender == btnSubmit)
                {
                    txtInput.Focus();
                }
                else if (sender == btnClear)
                {
                    txtInput.Focus();
                }
            }
        }
    }
}
