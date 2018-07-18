using System;
using System.Collections.Generic;
using app_meu_dia.Modelos;

using Xamarin.Forms;

namespace app_meu_dia.Telas
{
    public partial class Cadastro : ContentPage
    {
        private byte Prioridade { get; set; }

        public Cadastro()
        {
            InitializeComponent();
        }

        public void PrioridadeSelectAction(object sender, EventArgs args)
        {
            var stacks = slPrioridades.Children;

            foreach (var linha in stacks)
            {
                Label lblPriopridade = ((StackLayout)linha).Children[1] as Label;
                lblPriopridade.TextColor = Color.Gray;
            }

            ((Label)((StackLayout)sender).Children[1]).TextColor = Color.Black;
            FileImageSource source = ((Image)((StackLayout)sender).Children[0]).Source as FileImageSource;

            String prioridade = source.File.ToString().Replace("Resources/", "").Replace(".png", "").Replace("p", "");

            Prioridade = byte.Parse(prioridade);
        }

        public void SalvarAction(object sender, EventArgs args)
        {
            bool existeErro = false;

            if ( !(edtNome.Text.Trim().Length > 0) )
            {
                existeErro = true;
                DisplayAlert("Erro", "Nome não preenchido!", "OK");
            }

            if (!(Prioridade > 0))
            {
                existeErro = true;
                DisplayAlert("Erro", "Prioridade nào informada!", "OK");    
            }


            if (!existeErro)
            {
                Tarefa tarefa = new Tarefa();
                tarefa.Nome = edtNome.Text.Trim();
                tarefa.Prioridade = Prioridade;

                new GerenciadorTarefa().Salvar(tarefa);

                App.Current.MainPage = new NavigationPage(new Inicio());
            }
        }
    }
}
