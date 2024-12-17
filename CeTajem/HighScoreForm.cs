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
        private static string _fileName = "Highscore.txt";

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
                Text = "High Scores: " + (_highestScore?.ToString() ?? "Not Set"),
                Font = new Font("Arial", 18, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(10, 10)
            };
            this.Controls.Add(label);
        }

        public static void AddHighscore(int score)
        {
            if (!_highestScore.HasValue || score > _highestScore.Value)
            {
                _highestScore = score;
                WriteHighscore();
            }
        }

        public static void WriteHighscore()
        {
            string filePath = GetHighscoreFilePath();
            File.WriteAllText(filePath, _highestScore.ToString());
        }

        public static void ReadHighscore()
        {
            string filePath = GetHighscoreFilePath();
            if (File.Exists(filePath))
            {
                _highestScore = int.Parse(File.ReadAllText(filePath));
            }
        }

        public static string GetHighscoreFilePath()
        {
            // Option 1: User can specify a custom path
            string customPath = Environment.GetEnvironmentVariable("HIGHSCORE_PATH");
            if (!string.IsNullOrEmpty(customPath))
            {
                return Path.Combine(customPath, _fileName);
            }

            // Option 2: Application's local directory
            string appLocalPath = AppDomain.CurrentDomain.BaseDirectory;
            return Path.Combine(appLocalPath, _fileName);
        }
    }
}