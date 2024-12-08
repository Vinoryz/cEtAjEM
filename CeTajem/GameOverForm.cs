﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeTajem;

public class GameOverForm : Form
{
    public GameOverForm()
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
            Text = "Game Over\nYour Score : ",
            Font = new Font("Arial", 18, FontStyle.Bold),
            AutoSize = true,
            Location = new Point(150, 20)
        };
        this.Controls.Add(label);
    }
}
