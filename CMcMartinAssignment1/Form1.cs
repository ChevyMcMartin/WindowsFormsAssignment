/* Program: PROG2370
 * CMcMartinAssignment1
 * 
 *	Revision History
 *		Chevy McMartin, 2017.09.21: Created
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CMcMartinAssignment1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const int NumberOfalphaSeat = 5;
        const int NumberOfNumericSeat = 3;
        const int NumberWaiting = 10;
        string[,] seat = new string[NumberOfalphaSeat, NumberOfNumericSeat];
        string[] waiting = new string[NumberWaiting];


        private void btnShowAll_Click(object sender, EventArgs e)
        {
            rtxtShowAll.Clear();

            for (int i = 0; i < seat.GetLength(0); i++)
            {
                for (int j = 0; j < seat.GetLength(1); j++)
                {
                    rtxtShowAll.Text += "(" + lstSeatAlpha.Items[i] + "," + j + ") - " + seat[i, j] + "\n";
                }
            }
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            bool keepGoing = true;
            int rowSeat = lstSeatAlpha.SelectedIndex;
            int columnSeat = lstSeatNumeric.SelectedIndex;
            string errorMessage = "";

            if (rowSeat == -1)
            {
                keepGoing = false;
                errorMessage += "Please select a Row\n";
            }
            if (columnSeat == -1)
            {
                keepGoing = false;
                errorMessage += "Please select a Column\n";
            }
            if (txtName.Text == "")
            {
                keepGoing = false;
                errorMessage += "Please enter your Name\n";
            }
            if (keepGoing == false)
            {
                MessageBox.Show(errorMessage);
            }
            else
            {
                //seat reserved
                if (seat[rowSeat, columnSeat] == null || seat[rowSeat, columnSeat] == "")
                {
                    seat[rowSeat, columnSeat] = txtName.Text;
                    MessageBox.Show("Your seat has been successfully reserved");
                }
                else
                {
                    //check to see if any seats are available

                    //other seats are available
                    if (checkIfSeatAreAvailable())
                    {
                        MessageBox.Show("Try another seat!");
                    }

                    //all seats are reserved
                    else
                    {
                        // call add to the waiting list event
                        btnAddWaitingList_Click(sender, e);
                    }
                }
            }
        }

        private void btnFillAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < seat.GetLength(0); i++)
            {
                for (int j = 0; j < seat.GetLength(1); j++)
                {
                    seat[i, j] = "Chevy";
                }
            }
        }

        private void btnShowWaitingList_Click(object sender, EventArgs e)
        {
            rtxtShowWaitingList.Clear();

            for (int i = 0; i < waiting.GetLength(0); i++)
            {
                rtxtShowWaitingList.Text += "(" + i + ") - " + waiting[i] + "\n";  
            }
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {            
            int rowSeat = lstSeatAlpha.SelectedIndex;
            int columnSeat = lstSeatNumeric.SelectedIndex;
            string errorMessage = "";
            bool keepGoing = true;

            if (rowSeat == -1)
            {
                keepGoing = false;
                errorMessage += "Please select a Row\n";
            }
            if (columnSeat == -1)
            {
                keepGoing = false;
                errorMessage += "Please select a Column\n";
            }
            if (keepGoing == false)
            {
                MessageBox.Show(errorMessage);
            }
            else
            {
                if (seat[rowSeat, columnSeat] != null && seat[rowSeat, columnSeat] != "")
                {
                    txtStatus.Text = "Not Available";
                }
                else
                {
                    txtStatus.Text = "Available";
                }
            }
        }

        //if there are available seats, then return true
        // otherwise, return false;
        private bool checkIfSeatAreAvailable()
        {
            bool available = false;
            for (int i = 0; i < seat.GetLength(0); i++)
            {
                for (int j = 0; j < seat.GetLength(1); j++)
                {
                    if (seat[i, j] == null || seat[i, j] == "")
                    {
                        available = true;                        
                    }
                }
            }
            return available;
        }

        private void btnAddWaitingList_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            bool keepGoing = true;

            if (txtName.Text == "")
            {
                keepGoing = false;
                errorMessage += "Please enter your Name\n";
            }
            if (keepGoing == false)
            {
                MessageBox.Show(errorMessage);
            }
            else
            {
                bool available = false;

                int waitingListIndex = 0;
                //if there are available seats
                if (checkIfSeatAreAvailable())
                {
                    MessageBox.Show("Seats are available. you can't add to the waiting list.");
                }

                //if there are no available seats
                else
                {
                    for (int i = 0; i < waiting.GetLength(0); i++)
                    {
                        if (waiting[i] == null)
                        {
                            available = true;
                            waitingListIndex = i;
                            break;
                        }
                    }

                    //put the person on the waitinglist
                    if (available)
                    {
                        waiting[waitingListIndex] = txtName.Text;
                        MessageBox.Show("You have been added to the waiting list");
                    }

                    //waiting list is full
                    else
                    {
                        MessageBox.Show("The waiting list is full");
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            int rowSeat = lstSeatAlpha.SelectedIndex;
            int columnSeat = lstSeatNumeric.SelectedIndex;
            string errorMessage = "";
            bool keepGoing = true;

            if (rowSeat == -1)
            {
                keepGoing = false;
                errorMessage += "Please select a Row\n";
            }
            if (columnSeat == -1)
            {
                keepGoing = false;
                errorMessage += "Please select a Column\n";
            }
            if (keepGoing == false)
            {
                MessageBox.Show(errorMessage);
            }
            else
            {
                //The seat is reserved 
                if (seat[rowSeat, columnSeat] != null)
                {
                    //no one is waiting
                    if (waiting[0] == null || waiting[0] == "")
                    {
                        seat[rowSeat, columnSeat] = "";
                        MessageBox.Show("Your seat has been cancelled");
                    }

                    //someone is waiting
                    else
                    {
                        seat[rowSeat, columnSeat] = waiting[0];
                        waiting[0] = "";

                        //shift array
                        waiting = shiftArray(waiting);
                        MessageBox.Show("Your seat has been cancelled, next in waiting list has been given the seat");

                        // newArray is now { 2, 3, 4, 5, 6, null }
                    }
                }
                // The seat is not reserved
                else
                {
                    MessageBox.Show("The seat is not reserved");
                }
            }
        }

        //shift array
        private string[] shiftArray(string[] oldArray)
        {
            string[] newArray = new string[oldArray.Length];
            Array.Copy(waiting, 1, newArray, 0, waiting.Length - 1);

            return newArray;
        }
    }
}
