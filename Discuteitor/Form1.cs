using System;
using System.Media;
using System.Windows.Forms;

// 1 - Quando clicar em pausa passar quem fala para "Parar"
// 1.1 - Se clicar em "Parar", passar para "Falar" e o outro também, e o 'continuar' retornar para "Pausa"

// 2 - Quando para de falar, atinge o zero, mas não faz nada, para o timer

    // 3 - Quando clicar em se inscrever, deve mostrar a opção de Sair da inscrição
    // mas ainda deve ter também o botão de parar
    // então tem que aparecer os dois botões
    // portanto tem que ter uma alteraçãod e interface

/* Bug1 
    Cliquei pra Dayse falar
    e me inscrevi

    Falei
    ela se inscreveu

    Falou

    eu cliquei (não lembro como tava)
    E não mudou pra me inscrever
    (ficou aparecendo falar) */

namespace Discuteitor
{
public partial class Form1 : Form
{
    private int vez = -1;
    private int tempofinal = 0;
    private int tempo = 0;
    private int Inscrito = -1;
    private bool EmFlash = false;

    // private int TmpFinalMult = 10;
    private int TmpFinalMult = 61;

    private bool Ativou = false;

    public Form1()
    {
        InitializeComponent();
        //this.TopMost = true;
    }

    private void Inicializar()
    {
        tempo = 0;
        tempofinal = TmpFinalMult;
        timer.Enabled = true;
        radioButton3.Text = "Pausa";
        TmpFinalMult--;
    }

    private void Terminou()
    {
        timer.Enabled = false;
        if (Inscrito > -1)
        {
            string sSom = "";
            if (Inscrito==0)
            {
                radioButton1.Text = "Falar";
                radioButton2.Text = "Se inscrever";
                sSom = "Vez do Arnaldo.wav";
            } else
            {
                radioButton1.Text = "Se inscrever";
                radioButton2.Text = "Falar";
                sSom = "Vez da Dayse.wav";
            }
            SoundPlayer simpleSound = new SoundPlayer(sSom);
            simpleSound.Play();
            FlashWindow.Start(this);
            EmFlash = true;
        }
    }

    private void timer_Tick(object sender, EventArgs e)
    {
        this.tempo++;
        int mostrar = tempofinal - tempo;
        string m = "";
        if (mostrar>60)
        {
            int min = (mostrar / 60);
            m = min.ToString()+":";
            mostrar -= min * 60;
        }
        if (mostrar > -0.9)
        {
            m += mostrar.ToString();
            lbTempo.Text = m;
        } else
            Terminou();
    }

    private void radioButton3_Click(object sender, EventArgs e)
    {
        if (timer.Enabled)
            radioButton3.Text = "Continuar";
        else
            radioButton3.Text = "Pausa";
        timer.Enabled = !timer.Enabled;
        TiraFlash();
    }

    private void radioButton1_Click(object sender, EventArgs e)
    {
        if (Ativou)
        {
            vez = 0;
            OperBotoes(radioButton1);
        } else
            Ativou = true;
    }

    private void radioButton2_Click(object sender, EventArgs e)
    {
        vez = 1;
        OperBotoes(radioButton2);
    }

    private void OperBotoes(RadioButton RD)
    {
        if (RD.Text == "Se Inscrever")
        {
            RD.Text = "Falar";
            Inscrito = vez;
            Inicializar();
        }
        else
        {
            string c1 = "";
            string c0 = "";
            if (vez == 0)
            {
                c0 = "Falando";
                c1 = "Se Inscrever";
            }
            else
            {
                c0 = "Se Inscrever";
                c1 = "Falando";                
                
            }
            radioButton1.Text = c0;
            radioButton2.Text = c1;
            tempo = 0;
            lbTempo.Text = "";
            TmpFinalMult--;
            tempofinal = TmpFinalMult;
            timer.Enabled = false;
        }
        TiraFlash();
    }

    private void TiraFlash()
    {
        if (EmFlash)
        {
            FlashWindow.Stop(this);
            EmFlash = false;
        }
    }
}
}
