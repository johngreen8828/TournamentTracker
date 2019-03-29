using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class CreateTeamForm : Form
    {
        //This form is connected to the Create Tournament form thru the ITeamRequester interface
        //It uses the
        public CreateTeamForm(ITeamRequester caller)
        {
            InitializeComponent();
            callingForm = caller; //ITeamRequester
           // CreateSampleData();

            WireUpLists();
        }

        private List<PersonModel> availableTeamMembers = GlobalConfig.Connection.GetPerson_All();
        private List<PersonModel> selectedTeamMembers = new List<PersonModel>();
        private ITeamRequester callingForm;

       
        private void CreateSampleData()
        {
            availableTeamMembers.Add(new PersonModel { FirstName = "Bill", LastName = "Smith" });
            availableTeamMembers.Add(new PersonModel { FirstName = "Suzie", LastName = "Kurtis" });

            selectedTeamMembers.Add(new PersonModel { FirstName = "Sam", LastName = "Johnson" });
            selectedTeamMembers.Add(new PersonModel { FirstName = "Jennifer", LastName = "Rodriguez" });
        }

        private void WireUpLists()
        {
            //set select team member dropdown data source to null so it will refresh
            selectTeamMemberDropDown.DataSource = null;
            selectTeamMemberDropDown.DataSource = availableTeamMembers;
            selectTeamMemberDropDown.DisplayMember = "FullName";

            //set team members list box data source to null so it will refresh
            teamMembersListBox.DataSource = null;
            teamMembersListBox.DataSource = selectedTeamMembers;
            teamMembersListBox.DisplayMember = "FullName";
        }

        private void createMemberButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                PersonModel p = new PersonModel();
                p.FirstName = firstNameValue.Text;
                p.LastName = lastNameValue.Text;
                p.EmailAddress = emailValue.Text;
                p.CellPhoneNumber = cellPhoneValue.Text;

                //Added p to capture the person data and added the code to wire up the list box during the save
                p = GlobalConfig.Connection.CreatePerson(p);
                selectedTeamMembers.Add(p);
                WireUpLists();

                firstNameValue.Text = "";
                lastNameValue.Text = "";
                emailValue.Text = "";
                cellPhoneValue.Text = "";
            }
            else
            {
                MessageBox.Show("Please fill in all the fields for the member.");
            }
        }

        private bool ValidateForm()
        {
            if (firstNameValue.Text.Length == 0)
            {
                return false;
            }
            if (lastNameValue.Text.Length == 0)
            {
                return false;
            }
            if (emailValue.Text.Length == 0)
            {
                return false;
            }
            if (cellPhoneValue.Text.Length == 0)
            {
                return false;
            }
            return true;
        }

        private void addMemberButton_Click(object sender, EventArgs e)
        {
            //TODO - Look into a better way to refresh these fields

            //Take person from selected drop down and cast it into the person model
            PersonModel p = (PersonModel)selectTeamMemberDropDown.SelectedItem;

            //After the person is selected remove them from the list and add them to selected team members 
            //buggy if there is a null value.  so must do a check
            if (p != null)
            {
                availableTeamMembers.Remove(p);
                selectedTeamMembers.Add(p);

                WireUpLists(); 
            }
            //selectTeamMemberDropDown.Refresh();
            //teamMembersListBox.Refresh();

        }

        private void removeSelectedMember_Click(object sender, EventArgs e)
        {
            PersonModel p = (PersonModel)teamMembersListBox.SelectedItem;

            if (p != null)
            {
                selectedTeamMembers.Remove(p);
                availableTeamMembers.Add(p);
                WireUpLists(); 
            }
        }

        private void createTeam_Click(object sender, EventArgs e)
        {
            TeamModel t = new TeamModel();
            t.TeamName = teamNameValue.Text;
            t.TeamMembers = selectedTeamMembers;

            GlobalConfig.Connection.CreateTeam(t);
            callingForm.TeamComplete(t);
            this.Close();

        }
    }
}
