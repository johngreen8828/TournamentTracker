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
    public partial class TournamentViewerForm : Form
    {
        int findNull = 0;
        private TournamentModel tournament;
        BindingList<int> rounds = new BindingList<int>();
        BindingList<MatchupModel> selectedMatchups = new BindingList<MatchupModel>();

        public TournamentViewerForm(TournamentModel tournamentModel)
        {
            InitializeComponent();

            tournament = tournamentModel;

            WireUpLists();

            LoadFormData();

            LoadRounds();
        }

       private void LoadFormData()
       {
            TournamentName.Text = tournament.TournamentName;
       }

        private void WireUpLists()
        {
            roundSelectionDropDown.DataSource = rounds;
            matchupListbox.DataSource = selectedMatchups;
            matchupListbox.DisplayMember = "DisplayName";
        }


        private void LoadRounds()
        {
            //rounds = new BindingList<int>();
            rounds.Clear();

            rounds.Add(1);
            int currRound = 1;

            foreach (List<MatchupModel> matchups in tournament.Rounds)
            {
                if (matchups.First().MatchupRound > currRound)
                {
                    currRound = matchups.First().MatchupRound;
                    rounds.Add(currRound);
                }
                LoadMatchups(1);
            }

            //WireUpRoundsLists();
        }

        private void roundSelectionDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMatchups((int)roundSelectionDropDown.SelectedItem);
        }

        private void LoadMatchups(int round)
        {
            //int round = (int)roundSelectionDropDown.SelectedItem;
            foreach (List<MatchupModel> matchups in tournament.Rounds)
            {
                if (matchups.First().MatchupRound == round)
                {
                    selectedMatchups.Clear();
                    foreach (MatchupModel m in matchups)
                    {
                        if (m.Winner == null || !unplayedOnlyCheckbox.Checked)
                        {
                            selectedMatchups.Add(m); 
                        }
                    }
                }
            }
            //WireUpMatchupsLists();
            if (selectedMatchups.Count > 0)
            {
                LoadMatchup(selectedMatchups.First()); 
            }
            DisplayMatchupInfo();
        }

        private void DisplayMatchupInfo()
        {
            bool isVisible = (selectedMatchups.Count > 0);
            teamOneName.Visible = isVisible;
            teamOneScoreLabel.Visible = isVisible;
            teamOneScoreValue.Visible = isVisible;

            teamTwoName.Visible = isVisible;
            teamTwoScoreLabel.Visible = isVisible;
            teamTwoScoreValue.Visible = isVisible;

            versusLabel.Visible = isVisible;
            scoreButton.Visible = isVisible;

        }

        private void LoadMatchup(MatchupModel m)
        {
            //MatchupModel m = (MatchupModel)matchupListbox.SelectedItem;
            findNull = findNull + 1;

            
            //   MessageBox.Show(findNull.ToString());
                
            

            for (int i = 0; i < m.Entries.Count; i++)
            {
                if (i == 0) //Team 1
                {
                    if (m.Entries[0].TeamCompeting != null)
                    {
                        teamOneName.Text = m.Entries[0].TeamCompeting.TeamName;
                        teamOneScoreValue.Text = m.Entries[0].Score.ToString();

                        teamTwoName.Text = "<Bye>";
                        teamTwoScoreValue.Text = "0";
                    }
                    else
                    {
                        teamOneName.Text = "Not yet set";
                        teamOneScoreValue.Text = "";
                    }
                }
                if (i == 1) //Team 2
                {
                    if (m.Entries[1].TeamCompeting != null)
                    {
                        teamTwoName.Text = m.Entries[1].TeamCompeting.TeamName;
                        teamTwoScoreValue.Text = m.Entries[1].Score.ToString();
                    }
                    else
                    {
                        teamTwoName.Text = "Not yet set";
                        teamTwoScoreValue.Text = "";
                    }
                }
            }
        }
        private void matchupListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMatchup((MatchupModel)matchupListbox.SelectedItem);
        }

        private void unplayedOnlyCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            LoadMatchups((int)roundSelectionDropDown.SelectedItem);
        }

        private void scoreButton_Click(object sender, EventArgs e)
        {
            MatchupModel m = (MatchupModel)matchupListbox.SelectedItem;
            double teamOneScore = 0;
            double teamTwoScore = 0;

            for (int i = 0; i < m.Entries.Count; i++)
            {
                if (i == 0) //Team 1
                {
                    if (m.Entries[0].TeamCompeting.TeamName != null)
                    {
                        bool scoreValid = double.TryParse(teamOneScoreValue.Text, out teamOneScore);

                        if (scoreValid)
                        {
                            m.Entries[0].Score = teamOneScore;
                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid score for Team 1");
                            return;
                        }
                    }

                }
                if (i == 1) //Team 2
                {
                    if (m.Entries[1].TeamCompeting.TeamName != null)
                    {
                        bool scoreValid = double.TryParse(teamTwoScoreValue.Text, out teamTwoScore);

                        if (scoreValid)
                        {
                            m.Entries[1].Score = teamTwoScore;
                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid score for Team 2");
                            return;
                        }
                    }
                }
            }

            TournamentLogic.UpdateTournamentResults(tournament);

            LoadMatchups((int)roundSelectionDropDown.SelectedItem);

            
        }
    }
}
