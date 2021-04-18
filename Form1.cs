using System;
using System.Drawing;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        private static readonly string Symbol = "OX";
        private static readonly Color[] Color = new Color[2] { System.Drawing.Color.Blue, System.Drawing.Color.Crimson };
        private static readonly int[] Score = new[] { 0, 0 };

        private int _player = 0, _moves = 0;
        private int[,] _grid;

        public Form1()
        {
            InitializeComponent();
            Init();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void newgame_Click(object sender, EventArgs e)
        {
            Init();
        }
        private void Init()
        {
            _player = 0;
            _moves = 0;
            _grid = new int[3, 3]
            {
                {10, 11, 12},
                {13, 14, 15},
                {16, 17, 18}
            };
            for (var i = 1; i < 10; i++)
            {
                var b = (Button)this.board.Controls["button" + i.ToString()];
                b.Text = "";
                b.Enabled = true;
            }
            ScoreRefresh();
        }

        private void ScoreRefresh()
        {
            this.player1score.Text = Score[0].ToString();
            this.player2score.Text = Score[1].ToString();
        }
        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_Click(object sender, EventArgs e)
        {
            var clicked = (Button)sender;
            if (clicked.Text != "") return;
            clicked.Text = Symbol[_player].ToString();
            clicked.ForeColor = Color[_player];
            var btnIdx = Convert.ToInt32(clicked.Name[6].ToString()) - 1;

            _grid[btnIdx / 3, btnIdx % 3] = _player + 1;
            _moves++;

            Checker();
            _player ^= 1;
            ScoreRefresh();
        }
        private bool Checker()
        {
            var winner = 0;
            for (var i = 0; i < 3 && winner == 0; i++)
            {
                if (_grid[i, 0] == _grid[i, 1] && _grid[i, 1] == _grid[i, 2])
                    winner = _grid[i, 0];
                if (_grid[0, i] == _grid[1, i] && _grid[1, i] == _grid[2, i])
                    winner = _grid[0, i];
            }

            if ((_grid[0, 0] == _grid[1, 1] && _grid[1, 1] == _grid[2, 2]) ||
                (_grid[0, 2] == _grid[1, 1] && _grid[1, 1] == _grid[2, 0]))
                winner = _grid[1, 1];

            if (winner != 0)
            {
                DisableButtons();
                Score[winner - 1]++;
                MessageBox.Show("Winner is Player" + winner);
            }
            else if (_moves == 9)
            {
                DisableButtons();
                MessageBox.Show("Game Drawn");
            }

            return winner != 0 || _moves == 9;
        }

        private void DisableButtons()
        {
            for (var i = 1; i < 10; i++)
            {
                var b = (Button)this.board.Controls["button" + i.ToString()];
                var btnIdx = Convert.ToInt32(b.Name[6].ToString()) - 1;
                if (_grid[btnIdx / 3, btnIdx % 3] > 3) b.Enabled = false;
            }
        }

    }
}
