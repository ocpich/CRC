using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

// Programme: Calculate the CRC (Cyclic Redundancy Code) on a credit card number
// Author:    X. Carrel
// Date:      Mars 2013
// Modification : F. Andolfatto February 2021

namespace Debugger__CRC_
{
    public partial class frmCRC : Form
    {
        const int initialNbDigits = 18;    // number of digits in the number without the CRC
        long crcVal = 0; // CRC (cumulative)

        public frmCRC()
        {
            InitializeComponent();
        }

        private long CRC(string data)
        // Methode to calculate a data CRC
        {
            int     nbDigits = 0; // To count the number of digits in the given number

            // to browse the data
            for (int i = 0; i < data.Length; i++)
            {
                char c = data[i];
                if ((c > '0') && (c < '9')) // it's a digit
                {
                    nbDigits++;
                    crcVal = crcVal + ((int)c - (int)'0');
                    if (crcVal > 100) // We cannot go over 100 because we only have 2 digits in the CRC
                        crcVal = crcVal - 100;
                }
            }
            if (nbDigits != initialNbDigits)
            {
                MessageBox.Show(string.Format("Erreur: un numéro de carte doit contenir {0} chiffres (sans le CRC)",initialNbDigits));
                return -1;
            }
            return crcVal;
        }

        private void cmdCheckCRC_Click(object sender, EventArgs e)
        {
            string num = txtData.Text;

            if (num == "")
            {
                MessageBox.Show("Introduisez un numéro de carte SVP");
                return;
            }

            long crcVal;

            if (rbtCalcul.Checked) // We want to calculate the CRC
            {
                crcVal = CRC(num); // CRC calculation
                if (crcVal >= 0)
                    MessageBox.Show("Le CRC vaut: " + crcVal.ToString());
            }
            else // Check of the whole number
            {
                int CRCIntro = 10 * ((int)num[num.Length - 2] - (int)'0') + ((int)num[num.Length - 1] - (int)'0'); // Le CRC inclus dans le numéro (deux derniers chiffres)
                num = num.Substring(0, num.Length - 2); // We don't take into account the last two digits
                crcVal = CRC(num); // and we calculate the CRC

                if (CRCIntro == crcVal)
                    MessageBox.Show("Le numéro est valide");
                else
                    MessageBox.Show("Le numéro n'est pas valide");
            }
        }

    }
}
