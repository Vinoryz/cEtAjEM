using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace CeTajem
{
    public class HighScoreForm : Form
    {
        private static int? _highestScore;
        private static string _filePath = @"D:\Informatics Semester 3\@Pemrograman Berorientasi Obyek B by Pak Rizky Januar\cEtAjEM\CeTajem\Highscore.txt";

        public HighScoreForm()
        {
            InitializeForm();
        }

        private void InitializeForm()
        {
            this.Text = "High Scores";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label label = new Label
            {
                Text = "High Scores : " + _highestScore.ToString(),
                Font = new Font("Arial", 18, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(10, 10)
            };
            this.Controls.Add(label);
        }
        public static void AddHighscore(int score)
        {
            if (_highestScore == null) _highestScore = score;
            else
            {
                if (score > _highestScore) _highestScore = score;
            }
            HighScoreForm.WriteHighscore();
        }
        public static void WriteHighscore()
        {
            File.WriteAllText(_filePath, _highestScore.ToString());
        }
        public static void ReadHighscore()
        {
            _highestScore = int.Parse(File.ReadAllText(_filePath));
        }
    }
}
